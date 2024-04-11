namespace RMTK
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            SplitPreviewTool = new SplitContainer();
            SplitTreeProperty = new SplitContainer();
            FileTree = new TreeView();
            PropertyGrid = new PropertyGrid();
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            OpenFileButton = new ToolStripMenuItem();
            SaveFileButton = new ToolStripMenuItem();
            saveAsToolStripMenuItem = new ToolStripMenuItem();
            closeToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            ExitToolButton = new ToolStripMenuItem();
            editToolStripMenuItem = new ToolStripMenuItem();
            undoToolStripMenuItem = new ToolStripMenuItem();
            redoToolStripMenuItem = new ToolStripMenuItem();
            optionsToolStripMenuItem = new ToolStripMenuItem();
            themeToolStripMenuItem = new ToolStripMenuItem();
            LightThemeButton = new ToolStripMenuItem();
            DarkThemeButton = new ToolStripMenuItem();
            helpToolStripMenuItem = new ToolStripMenuItem();
            AboutButton = new ToolStripMenuItem();
            statusStrip1 = new StatusStrip();
            StatusMessage = new ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)SplitPreviewTool).BeginInit();
            SplitPreviewTool.Panel2.SuspendLayout();
            SplitPreviewTool.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)SplitTreeProperty).BeginInit();
            SplitTreeProperty.Panel1.SuspendLayout();
            SplitTreeProperty.Panel2.SuspendLayout();
            SplitTreeProperty.SuspendLayout();
            menuStrip1.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // SplitPreviewTool
            // 
            SplitPreviewTool.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            SplitPreviewTool.Location = new Point(10, 30);
            SplitPreviewTool.Name = "SplitPreviewTool";
            // 
            // SplitPreviewTool.Panel2
            // 
            SplitPreviewTool.Panel2.Controls.Add(SplitTreeProperty);
            SplitPreviewTool.Size = new Size(883, 515);
            SplitPreviewTool.SplitterDistance = 514;
            SplitPreviewTool.TabIndex = 0;
            // 
            // SplitTreeProperty
            // 
            SplitTreeProperty.Dock = DockStyle.Fill;
            SplitTreeProperty.Location = new Point(0, 0);
            SplitTreeProperty.Name = "SplitTreeProperty";
            SplitTreeProperty.Orientation = Orientation.Horizontal;
            // 
            // SplitTreeProperty.Panel1
            // 
            SplitTreeProperty.Panel1.Controls.Add(FileTree);
            // 
            // SplitTreeProperty.Panel2
            // 
            SplitTreeProperty.Panel2.Controls.Add(PropertyGrid);
            SplitTreeProperty.Size = new Size(365, 515);
            SplitTreeProperty.SplitterDistance = 250;
            SplitTreeProperty.TabIndex = 0;
            // 
            // FileTree
            // 
            FileTree.Dock = DockStyle.Fill;
            FileTree.LabelEdit = true;
            FileTree.Location = new Point(0, 0);
            FileTree.Name = "FileTree";
            FileTree.Size = new Size(365, 250);
            FileTree.TabIndex = 0;
            FileTree.AfterSelect += FileTree_AfterSelect;
            // 
            // PropertyGrid
            // 
            PropertyGrid.Dock = DockStyle.Fill;
            PropertyGrid.HelpVisible = false;
            PropertyGrid.Location = new Point(0, 0);
            PropertyGrid.Name = "PropertyGrid";
            PropertyGrid.Size = new Size(365, 261);
            PropertyGrid.TabIndex = 0;
            PropertyGrid.ToolbarVisible = false;
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = SystemColors.Control;
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, editToolStripMenuItem, optionsToolStripMenuItem, helpToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(903, 24);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { OpenFileButton, SaveFileButton, saveAsToolStripMenuItem, closeToolStripMenuItem, toolStripSeparator1, ExitToolButton });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // OpenFileButton
            // 
            OpenFileButton.Name = "OpenFileButton";
            OpenFileButton.ShortcutKeyDisplayString = "Ctrl+O";
            OpenFileButton.Size = new Size(195, 22);
            OpenFileButton.Text = "Open";
            OpenFileButton.Click += OpenFileButton_Click;
            // 
            // SaveFileButton
            // 
            SaveFileButton.Name = "SaveFileButton";
            SaveFileButton.ShortcutKeyDisplayString = "Ctrl+S";
            SaveFileButton.Size = new Size(195, 22);
            SaveFileButton.Text = "Save";
            SaveFileButton.Click += SaveFileButton_Click;
            // 
            // saveAsToolStripMenuItem
            // 
            saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            saveAsToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+Shift+S";
            saveAsToolStripMenuItem.Size = new Size(195, 22);
            saveAsToolStripMenuItem.Text = "Save As...";
            // 
            // closeToolStripMenuItem
            // 
            closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            closeToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+Shift+C";
            closeToolStripMenuItem.Size = new Size(195, 22);
            closeToolStripMenuItem.Text = "Close";
            closeToolStripMenuItem.Click += closeToolStripMenuItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(192, 6);
            // 
            // ExitToolButton
            // 
            ExitToolButton.Name = "ExitToolButton";
            ExitToolButton.ShortcutKeyDisplayString = "Alt+F4";
            ExitToolButton.Size = new Size(195, 22);
            ExitToolButton.Text = "Exit";
            ExitToolButton.Click += ExitToolButton_Click;
            // 
            // editToolStripMenuItem
            // 
            editToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { undoToolStripMenuItem, redoToolStripMenuItem });
            editToolStripMenuItem.Name = "editToolStripMenuItem";
            editToolStripMenuItem.Size = new Size(39, 20);
            editToolStripMenuItem.Text = "Edit";
            // 
            // undoToolStripMenuItem
            // 
            undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            undoToolStripMenuItem.Size = new Size(103, 22);
            undoToolStripMenuItem.Text = "Undo";
            // 
            // redoToolStripMenuItem
            // 
            redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            redoToolStripMenuItem.Size = new Size(103, 22);
            redoToolStripMenuItem.Text = "Redo";
            // 
            // optionsToolStripMenuItem
            // 
            optionsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { themeToolStripMenuItem });
            optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            optionsToolStripMenuItem.Size = new Size(61, 20);
            optionsToolStripMenuItem.Text = "Options";
            // 
            // themeToolStripMenuItem
            // 
            themeToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { LightThemeButton, DarkThemeButton });
            themeToolStripMenuItem.Name = "themeToolStripMenuItem";
            themeToolStripMenuItem.Size = new Size(110, 22);
            themeToolStripMenuItem.Text = "Theme";
            // 
            // LightThemeButton
            // 
            LightThemeButton.Name = "LightThemeButton";
            LightThemeButton.Size = new Size(101, 22);
            LightThemeButton.Text = "Light";
            LightThemeButton.Click += LightThemeButton_Click;
            // 
            // DarkThemeButton
            // 
            DarkThemeButton.Name = "DarkThemeButton";
            DarkThemeButton.Size = new Size(101, 22);
            DarkThemeButton.Text = "Dark";
            DarkThemeButton.Click += DarkThemeButton_Click;
            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { AboutButton });
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            helpToolStripMenuItem.Size = new Size(44, 20);
            helpToolStripMenuItem.Text = "Help";
            // 
            // AboutButton
            // 
            AboutButton.Name = "AboutButton";
            AboutButton.Size = new Size(171, 22);
            AboutButton.Text = "About DatsuEditor";
            AboutButton.Click += AboutButton_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.GripMargin = new Padding(0);
            statusStrip1.Items.AddRange(new ToolStripItem[] { StatusMessage });
            statusStrip1.Location = new Point(0, 548);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.RenderMode = ToolStripRenderMode.Professional;
            statusStrip1.Size = new Size(903, 22);
            statusStrip1.SizingGrip = false;
            statusStrip1.TabIndex = 2;
            statusStrip1.Text = "statusStrip1";
            // 
            // StatusMessage
            // 
            StatusMessage.Name = "StatusMessage";
            StatusMessage.Overflow = ToolStripItemOverflow.Always;
            StatusMessage.Size = new Size(888, 17);
            StatusMessage.Spring = true;
            StatusMessage.Text = "Drag and drop a file to start.";
            // 
            // MainForm
            // 
            AllowDrop = true;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(903, 570);
            Controls.Add(statusStrip1);
            Controls.Add(SplitPreviewTool);
            Controls.Add(menuStrip1);
            DoubleBuffered = true;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MainForm";
            Text = "DatsuEditor";
            DragDrop += Form1_DragDrop;
            DragEnter += Form1_DragEnter;
            SplitPreviewTool.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)SplitPreviewTool).EndInit();
            SplitPreviewTool.ResumeLayout(false);
            SplitTreeProperty.Panel1.ResumeLayout(false);
            SplitTreeProperty.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)SplitTreeProperty).EndInit();
            SplitTreeProperty.ResumeLayout(false);
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private SplitContainer SplitPreviewTool;
        private SplitContainer SplitTreeProperty;
        private TreeView FileTree;
        private PropertyGrid PropertyGrid;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem editToolStripMenuItem;
        private ToolStripMenuItem optionsToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem OpenFileButton;
        private ToolStripMenuItem SaveFileButton;
        private ToolStripMenuItem saveAsToolStripMenuItem;
        private ToolStripMenuItem closeToolStripMenuItem;
        private ToolStripMenuItem ExitToolButton;
        private ToolStripMenuItem themeToolStripMenuItem;
        private ToolStripMenuItem LightThemeButton;
        private ToolStripMenuItem DarkThemeButton;
        private ToolStripMenuItem AboutButton;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem undoToolStripMenuItem;
        private ToolStripMenuItem redoToolStripMenuItem;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel StatusMessage;
    }
}
