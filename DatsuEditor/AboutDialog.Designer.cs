namespace RMTK
{
    partial class AboutDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutDialog));
            label1 = new Label();
            button1 = new Button();
            richTextBox1 = new RichTextBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Dock = DockStyle.Top;
            label1.Location = new Point(0, 0);
            label1.Name = "label1";
            label1.Size = new Size(401, 29);
            label1.TabIndex = 0;
            label1.Text = "DatsuEditor - MGRR Modding Toolkit";
            label1.TextAlign = ContentAlignment.BottomCenter;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            button1.Location = new Point(314, 239);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 1;
            button1.Text = "OK";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // richTextBox1
            // 
            richTextBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            richTextBox1.BackColor = SystemColors.ControlLightLight;
            richTextBox1.BorderStyle = BorderStyle.FixedSingle;
            richTextBox1.Location = new Point(12, 32);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.ReadOnly = true;
            richTextBox1.Size = new Size(377, 201);
            richTextBox1.TabIndex = 2;
            richTextBox1.Text = "Aura - ZanLibrary + DatsuEditor (format parsing)\nKwasior - Format Structs (kwastools/kwaslib https://github.com/ThisKwasior/KwasTools)\nSomeGuyWhoPlaysMGR - LY2 Icon Idea\nbonefulDelight - MOT Icon Idea";
            // 
            // AboutDialog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(401, 273);
            Controls.Add(richTextBox1);
            Controls.Add(button1);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "AboutDialog";
            Text = "About";
            ResumeLayout(false);
        }

        #endregion

        private Label label1;
        private Button button1;
        private RichTextBox richTextBox1;
    }
}