using IWshRuntimeLibrary;
using System.Reflection.Emit;
using System.Windows.Forms;

namespace RandomGameSelector
{
    public partial class RandomGamePicker : Form
    {
        private string folderPath = string.Empty;
        private string lastSelectedFile = string.Empty;

        public RandomGamePicker()
        {
            InitializeComponent();
        }

        private void selectFolderBtn_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                folderPath = folderBrowserDialog.SelectedPath;
                selectedFolderLblTextBox.Text = folderPath;
            }
        }

        private void selectRandGameBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(folderPath))
            {
                MessageBox.Show("Please select a folder first.");
                return;
            }

            if (Directory.Exists(folderPath))
            {
                var files = Directory.GetFiles(folderPath).Where(file => file != lastSelectedFile).ToArray();
                if (files.Length > 0)
                {
                    Random rnd = new Random();

                    // If there is only one game (other than the previously selected one), select it directly.
                    if (files.Length == 1)
                    {
                        lastSelectedFile = files[0];
                    }
                    else
                    {
                        string selectedFile;
                        do
                        {
                            selectedFile = files[rnd.Next(files.Length)];
                        } while (selectedFile == lastSelectedFile);

                        lastSelectedFile = selectedFile;
                    }

                    // selected file name without its extension and file icon
                    DisplaySelectedGame(lastSelectedFile);

                    // Adjust the TextBox size to fit the game name
                    ResizeButtonToFitText(openSelectedGameBtn, openSelectedGameBtn.Text, 250);
                }
                else
                {
                    MessageBox.Show("No files found in the folder.");
                }
            }
            else
            {
                MessageBox.Show($"The folder '{folderPath}' does not exist.");
            }
        }

        private async void openSelectedGameBtn_Click(object sender, EventArgs e)
        {
            // Check if the lastSelectedFile variable is not empty or null
            if (!string.IsNullOrEmpty(lastSelectedFile))
            {
                try
                {
                    // Use Process.Start to open the game or associated application
                    var gameProcess = OpenGame(lastSelectedFile);
                    if (gameProcess != null)
                    {
                        // Wait for the game to become idle before exiting
                        await Task.Delay(5000);

                        // Exit the application
                        Application.Exit();
                    }
                }
                catch (Exception ex)
                {
                    // In case of an error, show a message box to the user.
                    MessageBox.Show($"Unable to open the file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void DisplaySelectedGame(string filePath)
        {
            string gamePath = filePath; // Assume gamePath is determined as shown previously

            openSelectedGameBtn.Text = Path.GetFileNameWithoutExtension(gamePath);

            try
            {
                Icon extractedIcon = Icon.ExtractAssociatedIcon(gamePath);
                if (extractedIcon != null)
                {
                    // Define your desired icon size
                    int iconWidth = 64; // or any other width
                    int iconHeight = 64; // or any other height

                    // Create a thumbnail of the icon at the desired size
                    Image iconResized = extractedIcon.ToBitmap().GetThumbnailImage(iconWidth, iconHeight, null, IntPtr.Zero);

                    selectedGameIcon.Image = iconResized;
                }
            }
            catch (Exception ex)
            {
                selectedGameIcon.Image = null; // Clear the icon if there's an error
                Console.WriteLine($"Error extracting icon: {ex.Message}");
            }
        }

        private void ResizeButtonToFitText(Button button, string text, int maxWidth)
        {
            button.Text = text;

            Size maxSize = new Size(maxWidth, int.MaxValue);
            TextFormatFlags flags = TextFormatFlags.WordBreak | TextFormatFlags.LeftAndRightPadding;

            Size textSize = TextRenderer.MeasureText(text, button.Font, maxSize, flags);

            // Manually adjust the height calculation if necessary. For instance, you might
            // add a multiplier based on empirical observations or ensure a minimum height.
            int adjustedHeight = textSize.Height + button.Padding.Top + button.Padding.Bottom;

            // Optionally, add a fixed amount or a proportion based on the original calculation
            adjustedHeight = (int)(adjustedHeight * 2); // Example adjustment

            button.Height = adjustedHeight;
            button.Width = maxWidth;
        }

        private System.Diagnostics.Process? OpenGame(string filePath)
        {
            try
            {
                System.Diagnostics.Process? gameProcess = null;
                // Determine the type of file or its requirements for opening
                if (filePath.EndsWith(".url") || filePath.StartsWith("http"))
                {
                    // Open URL in default browser
                    gameProcess = System.Diagnostics.Process.Start("explorer.exe", filePath);
                    return gameProcess;
                }
                else if (filePath.EndsWith(".bat") || filePath.EndsWith(".ps1"))
                {
                    // Directly run scripts or batch files
                    gameProcess = System.Diagnostics.Process.Start(filePath);
                    return gameProcess;
                }
                else if (filePath.EndsWith(".lnk"))
                {
                    // Create a new WScript.Shell object to handle the shortcut
                    var shell = new IWshRuntimeLibrary.WshShell();
                    // Use the shell to open the shortcut
                    IWshRuntimeLibrary.IWshShortcut shortcut = (IWshRuntimeLibrary.IWshShortcut)shell.CreateShortcut(filePath);

                    // Execute the target path of the shortcut
                    gameProcess = System.Diagnostics.Process.Start(shortcut.TargetPath);
                    return gameProcess;
                }
                else
                {
                    // For other cases, you might specify a specific launcher or handle differently
                    gameProcess = System.Diagnostics.Process.Start("pathToLauncher", filePath);
                    return gameProcess;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unable to open the file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        //private string GetLocalPathFromUrlFile(string urlFilePath)
        //{
        //    var lines = System.IO.File.ReadAllLines(urlFilePath);
        //    string targetPath = lines.FirstOrDefault(line => line.StartsWith("URL=file:///"));
        //    if (!string.IsNullOrEmpty(targetPath))
        //    {
        //        targetPath = targetPath.Replace("URL=file:///", "").Replace('/', '\\');
        //        targetPath = Uri.UnescapeDataString(targetPath);
        //        return targetPath;
        //    }
        //    return null;
        //}

        //private string ResolveShortcutTarget(string shortcutPath)
        //{
        //    if (Path.GetExtension(shortcutPath).ToLower() != ".lnk")
        //        return shortcutPath; // Not a shortcut, return original path

        //    try
        //    {
        //        WshShell shell = new WshShell();
        //        IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutPath);
        //        return shortcut.TargetPath;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error resolving shortcut target: {ex.Message}");
        //        return null; // Or handle the error as appropriate for your application
        //    }
        //}

        //private void DisplaySelectedGame(string filePath)
        //{
        //    string gamePath = filePath;

        //    // Check if the file is a .url shortcut and resolve the local path
        //    if (Path.GetExtension(filePath).Equals(".url", StringComparison.OrdinalIgnoreCase))
        //    {
        //        string localPath = GetLocalPathFromUrlFile(filePath);
        //        if (!string.IsNullOrEmpty(localPath))
        //        {
        //            gamePath = localPath;
        //        }
        //    }
        //    // Resolve the target path if the file is a shortcut (.lnk)
        //    else if (Path.GetExtension(filePath).Equals(".lnk", StringComparison.OrdinalIgnoreCase))
        //    {
        //        gamePath = ResolveShortcutTarget(filePath);
        //    }

        //    // Use gamePath to set the text box and extract the icon
        //    openSelectedGameBtn.Text = Path.GetFileNameWithoutExtension(gamePath);

        //    try
        //    {
        //        Icon extractedIcon = Icon.ExtractAssociatedIcon(gamePath);
        //        if (extractedIcon != null)
        //        {
        //            selectedGameIcon.Image = extractedIcon.ToBitmap();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        selectedGameIcon.Image = null; // Clear the icon if there's an error
        //        Console.WriteLine($"Error extracting icon: {ex.Message}");
        //    }
        //}
    }
}
