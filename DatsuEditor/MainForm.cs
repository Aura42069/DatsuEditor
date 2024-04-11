using DatsuEditor;
using RisingFormats.Dat;
using RMTK.Nodes;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using ZanLibrary;

namespace RMTK
{
    public partial class MainForm : Form
    {
        TreeNode RootNode;
        bool IsDarkMode = false;

        // Loaded stuff
        public static string LoadedFilePath = "";
        public static byte[] LoadedData = { };

        public MainForm()
        {
            InitializeComponent();

            Console.WriteLine("I opened the console!!");

            var IconStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("DatsuEditor.DatsuIcons.dat");
            byte[] IconBytes = new byte[IconStream.Length];
            IconStream.ReadExactly(IconBytes, 0, (int)IconStream.Length);

            var IconPackage = DatFile.Load(IconBytes);

            Dictionary<string, MemoryStream> IconMap = new();

            foreach (var IconFile in IconPackage)
            {
                IconMap.Add(IconFile.Name, new MemoryStream(IconFile.Data));
            }

            ImageList list = new();

            list.Images.Add(Image.FromStream(IconMap["unk.png"]));
            list.Images.Add(Image.FromStream(IconMap["dat.png"]));
            list.Images.Add(Image.FromStream(IconMap["bxm.png"]));
            list.Images.Add(Image.FromStream(IconMap["wtb.png"]));
            list.Images.Add(Image.FromStream(IconMap["wmb.png"]));
            list.Images.Add(Image.FromStream(IconMap["unk.png"])); // mot
            list.Images.Add(Image.FromStream(IconMap["est.png"]));
            list.Images.Add(Image.FromStream(IconMap["bnk.png"]));
            list.Images.Add(Image.FromStream(IconMap["unk.png"])); // scr
            list.Images.Add(Image.FromStream(IconMap["unk.png"])); // syn
            list.Images.Add(Image.FromStream(IconMap["unk.png"])); // ly2
            list.Images.Add(Image.FromStream(IconMap["unk.png"])); // uid
            list.Images.Add(Image.FromStream(IconMap["unk.png"])); // sop
            list.Images.Add(Image.FromStream(IconMap["unk.png"])); // exp
            list.Images.Add(Image.FromStream(IconMap["unk.png"])); // ctx
            list.Images.Add(Image.FromStream(IconMap["unk.png"])); // uvd
            list.Images.Add(Image.FromStream(IconMap["unk.png"])); // sae
            list.Images.Add(Image.FromStream(IconMap["unk.png"])); // sas
            list.Images.Add(Image.FromStream(IconMap["unk.png"])); // hkx
            list.Images.Add(Image.FromStream(IconMap["unk.png"])); // cpk
            list.Images.Add(Image.FromStream(IconMap["wem.png"]));
            list.Images.Add(Image.FromStream(IconMap["unk.png"])); // vcd
            list.Images.Add(Image.FromStream(IconMap["unk.png"])); // brd
            FileTree.ImageList = list;

            if (File.Exists("DatsuConfig.cfg"))
                LoadConfig();
            
            UpdateTheme();
        }

        void SaveConfig()
        {
            StringBuilder configFile = new();
            configFile.AppendLine("Dark=" + (IsDarkMode == true ? "1" : "0"));
            File.WriteAllText("DatsuConfig.cfg", configFile.ToString());
        }

        void LoadConfig()
        {
            var lines = File.ReadAllLines("DatsuConfig.cfg");
            foreach (var line in lines)
            {
                string[] split = line.Split('=');
                switch (split[0])
                {
                    case "Dark":
                        IsDarkMode = split[1] == "1" ? true : false;
                        return;
                }
            }
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy | DragDropEffects.Move;
            TryLoadFile(e.Data.GetData(DataFormats.FileDrop));
        }

        private void TryLoadFile(object Data)
        {
            string path = ((string[])Data)[0];
            string filename = Path.GetFileName(path);
            byte[] data = File.ReadAllBytes(path);

            switch (FormatUtils.DetectFileFormat(data))
            {
                case MGRFileFormat.DAT:
                    try
                    {
                        Stopwatch timer = new();
                        timer.Start();
                        FileTree.Nodes.Clear();
                        var Files = DatFile.Load(data);
                        LoadedFilePath = path;
                        DatNode datNode = new(path, data, false);
                        datNode.Text = filename;
                        // datNode.InitNode(Files);
                        FileTree.Nodes.Add(datNode);
                        timer.Stop();
                        StatusMessage.Text = $"Loaded \"{filename}\" ({Files.Length} files) in {timer.ElapsedMilliseconds}ms.";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error reading DAT file: " + ex.Message);
                    }
                    return;
            }
        }

        private void DarkThemeButton_Click(object sender, EventArgs e)
        {
            IsDarkMode = true;
            UpdateTheme();
        }

        private void LightThemeButton_Click(object sender, EventArgs e)
        {
            IsDarkMode = false;
            UpdateTheme();
        }

        void UpdateTheme()
        {
            if (IsDarkMode)
            {
                LightThemeButton.Checked = false;
                DarkThemeButton.Checked = true;

                BackColor = ThemeDefines.DarkMode_Lowestground;
                ForeColor = ThemeDefines.DarkMode_Foreground;

                FileTree.BackColor = ThemeDefines.DarkMode_Background;
                FileTree.ForeColor = ThemeDefines.DarkMode_Foreground;

                PropertyGrid.ViewBackColor = ThemeDefines.DarkMode_Background;

                menuStrip1.Renderer = new DatsuDarkRenderer(new DatsuDarkColorTable());
                menuStrip1.BackColor = ThemeDefines.DarkMode_Lowestground;
                menuStrip1.ForeColor = ThemeDefines.DarkMode_Foreground;

                statusStrip1.BackColor = ThemeDefines.DarkMode_Background;
                statusStrip1.ForeColor = ThemeDefines.DarkMode_Foreground;

                ImmersiveWindow.ToggleWindowDark(Handle, true);
            }
            else
            {
                LightThemeButton.Checked = true;
                DarkThemeButton.Checked = false;

                BackColor = SystemColors.Control;
                ForeColor = SystemColors.ControlText;

                FileTree.BackColor = SystemColors.Window;
                FileTree.ForeColor = SystemColors.ControlText;

                PropertyGrid.ViewBackColor = SystemColors.Window;

                menuStrip1.Renderer = new ToolStripProfessionalRenderer();
                menuStrip1.BackColor = SystemColors.Control;
                menuStrip1.ForeColor = SystemColors.ControlText;

                statusStrip1.BackColor = SystemColors.Control;
                statusStrip1.ForeColor = SystemColors.ControlText;

                ImmersiveWindow.ToggleWindowDark(Handle, false);
            }
            SaveConfig();
        }

        private void AboutButton_Click(object sender, EventArgs e)
        {
            new AboutDialog().ShowDialog();
        }

        private void SaveFileButton_Click(object sender, EventArgs e)
        {
            List<DatFileEntry> files = new();
            foreach (FileNode node in FileTree.Nodes[0].Nodes)
                files.Add(new(node.Text, node.Data));
            File.WriteAllBytes(LoadedFilePath, DatFile.Save(files.ToArray()));
        }

        private void FileTree_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileTree.Nodes.Clear();
            LoadedData = new byte[] { };
            LoadedFilePath = string.Empty;
        }

        private void OpenFileButton_Click(object sender, EventArgs e)
        {

        }

        private void ExitToolButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
