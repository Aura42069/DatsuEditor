using RisingFormats.Dat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZanLibrary;
using ZanLibrary.Bxm;

namespace RMTK.Nodes
{
    class DatNode : FileNode
    {
        public event EventHandler OnAdd;
        public event EventHandler OnBinaryRefresh;
        public DatNode(string filename, byte[] filedata, bool nested) : base(filename, filedata)
        {
            ContextMenuStrip = new ContextMenuStrip();

            var exportBtn = new ToolStripMenuItem("Export");
            exportBtn.Click += (sender, args) =>
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.FileName = Text;
                saveFileDialog.Filter = $"All Files (*.*)|*.*";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllBytes(saveFileDialog.FileName, Data);
                }
            };

            var replaceBtn = new ToolStripMenuItem("Replace");
            replaceBtn.Click += (sender, args) =>
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.FileName = Text;
                ofd.Filter = $"All Files (*.*)|*.*";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    Data = File.ReadAllBytes(ofd.FileName);
                    InitNode(DatFile.Load(Data));
                }
            };

            var renameBtn = new ToolStripMenuItem("Rename");
            renameBtn.Click += (sender, args) =>
            {
                BeginEdit();
            };

            var removeBtn = new ToolStripMenuItem("Remove");
            removeBtn.Click += (sender, args) =>
            {
                Remove();
            };

            var addButton = new ToolStripMenuItem("Add File");
            addButton.Click += (s, e) =>
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "All Files (*.*)|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    byte[] data = File.ReadAllBytes(openFileDialog.FileName);
                    Nodes.Add(new FileNode(Path.GetFileName(openFileDialog.FileName), data));
                    UpdateBinary();
                    OnAdd?.Invoke(this, new EventArgs());
                }
            };

            if (nested)
            {
                ContextMenuStrip.Items.Add(exportBtn);
                ContextMenuStrip.Items.Add(replaceBtn);
                ContextMenuStrip.Items.Add(renameBtn);
                ContextMenuStrip.Items.Add(removeBtn);
                ContextMenuStrip.Items.Add(new ToolStripSeparator());
            }
            ContextMenuStrip.Items.Add(addButton);
            ImageIndex = 1;
            SelectedImageIndex = 1;

            InitNode(DatFile.Load(filedata));
        }
        public void UpdateBinary()
        {
            List<DatFileEntry> files = new();
            foreach (FileNode node in Nodes)
                files.Add(new(node.Text, node.Data));
            Data = DatFile.Save(files.ToArray());
            OnBinaryRefresh?.Invoke(this, new EventArgs());
        }
        public void InitNode(DatFileEntry[] Files)
        {
            Nodes.Clear();
            foreach (var file in Files)
            {
                FileNode node;
                switch (FormatUtils.DetectFileFormat(file.Data))
                {
                    case MGRFileFormat.DAT:
                        node = new DatNode(file.Name, file.Data, true);
                        ((DatNode)node).OnAdd += (s, e) => { UpdateBinary(); };
                        ((DatNode)node).OnBinaryRefresh += (s, e) => { UpdateBinary(); };
                        break;
                    case MGRFileFormat.BXM:
                        node = new BxmNode(file.Name, file.Data);
                        break;
                    case MGRFileFormat.WTB:
                        node = new WtbNode(file.Name, file.Data);
                        break;
                    case MGRFileFormat.WMB:
                        node = new WmbNode(file.Name, file.Data);
                        break;
                    default:
                        node = new FileNode(file.Name, file.Data);
                        break;
                }
                node.OnReplace += (s, e) => { UpdateBinary(); };
                Nodes.Add(node);
            }
        }
        public void DeconstructFiles()
        {

        }
    }
}
