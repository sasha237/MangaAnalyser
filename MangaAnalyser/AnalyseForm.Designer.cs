namespace MangaAnalyser
{
    partial class AnalyseForm
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
            this.Imagepanel = new System.Windows.Forms.Panel();
            this.ImagepictureBox = new System.Windows.Forms.PictureBox();
            this.ImagepropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.AddButton = new System.Windows.Forms.Button();
            this.Analysebutton = new System.Windows.Forms.Button();
            this.SelectFilebutton = new System.Windows.Forms.Button();
            this.Filelabel = new System.Windows.Forms.Label();
            this.FiletextBox = new System.Windows.Forms.TextBox();
            this.JapanpictureBox = new System.Windows.Forms.PictureBox();
            this.JapantextBox = new System.Windows.Forms.TextBox();
            this.Translatebutton = new System.Windows.Forms.Button();
            this.EnglishtextBox = new System.Windows.Forms.TextBox();
            this.Imagepanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ImagepictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.JapanpictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // Imagepanel
            // 
            this.Imagepanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Imagepanel.AutoScroll = true;
            this.Imagepanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Imagepanel.Controls.Add(this.ImagepictureBox);
            this.Imagepanel.Location = new System.Drawing.Point(309, 157);
            this.Imagepanel.Name = "Imagepanel";
            this.Imagepanel.Size = new System.Drawing.Size(480, 448);
            this.Imagepanel.TabIndex = 23;
            // 
            // ImagepictureBox
            // 
            this.ImagepictureBox.Cursor = System.Windows.Forms.Cursors.Cross;
            this.ImagepictureBox.Location = new System.Drawing.Point(3, 0);
            this.ImagepictureBox.Name = "ImagepictureBox";
            this.ImagepictureBox.Size = new System.Drawing.Size(500, 500);
            this.ImagepictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.ImagepictureBox.TabIndex = 5;
            this.ImagepictureBox.TabStop = false;
            this.ImagepictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ImagepictureBox_MouseMove);
            this.ImagepictureBox.Click += new System.EventHandler(this.ImagepictureBox_Click);
            this.ImagepictureBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ImagepictureBox_MouseClick);
            this.ImagepictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ImagepictureBox_MouseDown);
            this.ImagepictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ImagepictureBox_MouseUp);
            // 
            // ImagepropertyGrid
            // 
            this.ImagepropertyGrid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.ImagepropertyGrid.Location = new System.Drawing.Point(15, 81);
            this.ImagepropertyGrid.Name = "ImagepropertyGrid";
            this.ImagepropertyGrid.Size = new System.Drawing.Size(288, 509);
            this.ImagepropertyGrid.TabIndex = 21;
            this.ImagepropertyGrid.ToolbarVisible = false;
            this.ImagepropertyGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.ImagepropertyGrid_PropertyValueChanged);
            // 
            // DeleteButton
            // 
            this.DeleteButton.Location = new System.Drawing.Point(164, 35);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(139, 40);
            this.DeleteButton.TabIndex = 19;
            this.DeleteButton.Text = "Remove";
            this.DeleteButton.UseVisualStyleBackColor = true;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // AddButton
            // 
            this.AddButton.Location = new System.Drawing.Point(15, 35);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(139, 40);
            this.AddButton.TabIndex = 20;
            this.AddButton.Text = "Add";
            this.AddButton.UseVisualStyleBackColor = true;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // Analysebutton
            // 
            this.Analysebutton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Analysebutton.Location = new System.Drawing.Point(662, 100);
            this.Analysebutton.Name = "Analysebutton";
            this.Analysebutton.Size = new System.Drawing.Size(127, 23);
            this.Analysebutton.TabIndex = 17;
            this.Analysebutton.Text = "Japan";
            this.Analysebutton.UseVisualStyleBackColor = true;
            this.Analysebutton.Click += new System.EventHandler(this.Analysebutton_Click);
            // 
            // SelectFilebutton
            // 
            this.SelectFilebutton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SelectFilebutton.Location = new System.Drawing.Point(662, 6);
            this.SelectFilebutton.Name = "SelectFilebutton";
            this.SelectFilebutton.Size = new System.Drawing.Size(127, 23);
            this.SelectFilebutton.TabIndex = 16;
            this.SelectFilebutton.Text = "Select";
            this.SelectFilebutton.UseVisualStyleBackColor = true;
            this.SelectFilebutton.Click += new System.EventHandler(this.SelectFilebutton_Click);
            // 
            // Filelabel
            // 
            this.Filelabel.AutoSize = true;
            this.Filelabel.Location = new System.Drawing.Point(12, 9);
            this.Filelabel.Name = "Filelabel";
            this.Filelabel.Size = new System.Drawing.Size(34, 17);
            this.Filelabel.TabIndex = 14;
            this.Filelabel.Text = "File:";
            // 
            // FiletextBox
            // 
            this.FiletextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.FiletextBox.Location = new System.Drawing.Point(67, 6);
            this.FiletextBox.Name = "FiletextBox";
            this.FiletextBox.ReadOnly = true;
            this.FiletextBox.Size = new System.Drawing.Size(585, 22);
            this.FiletextBox.TabIndex = 11;
            // 
            // JapanpictureBox
            // 
            this.JapanpictureBox.Location = new System.Drawing.Point(309, 56);
            this.JapanpictureBox.Name = "JapanpictureBox";
            this.JapanpictureBox.Size = new System.Drawing.Size(480, 38);
            this.JapanpictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.JapanpictureBox.TabIndex = 24;
            this.JapanpictureBox.TabStop = false;
            // 
            // JapantextBox
            // 
            this.JapantextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.JapantextBox.Location = new System.Drawing.Point(313, 101);
            this.JapantextBox.Name = "JapantextBox";
            this.JapantextBox.Size = new System.Drawing.Size(339, 22);
            this.JapantextBox.TabIndex = 0;
            this.JapantextBox.TextChanged += new System.EventHandler(this.JapantextBox_TextChanged);
            // 
            // Translatebutton
            // 
            this.Translatebutton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Translatebutton.Location = new System.Drawing.Point(662, 129);
            this.Translatebutton.Name = "Translatebutton";
            this.Translatebutton.Size = new System.Drawing.Size(127, 23);
            this.Translatebutton.TabIndex = 17;
            this.Translatebutton.Text = "English";
            this.Translatebutton.UseVisualStyleBackColor = true;
            this.Translatebutton.Click += new System.EventHandler(this.Translatebutton_Click);
            // 
            // EnglishtextBox
            // 
            this.EnglishtextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.EnglishtextBox.Location = new System.Drawing.Point(313, 129);
            this.EnglishtextBox.Name = "EnglishtextBox";
            this.EnglishtextBox.Size = new System.Drawing.Size(339, 22);
            this.EnglishtextBox.TabIndex = 0;
            this.EnglishtextBox.TextChanged += new System.EventHandler(this.EnglishtextBox_TextChanged);
            // 
            // AnalyseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(801, 617);
            this.Controls.Add(this.EnglishtextBox);
            this.Controls.Add(this.JapantextBox);
            this.Controls.Add(this.JapanpictureBox);
            this.Controls.Add(this.Imagepanel);
            this.Controls.Add(this.ImagepropertyGrid);
            this.Controls.Add(this.DeleteButton);
            this.Controls.Add(this.AddButton);
            this.Controls.Add(this.Translatebutton);
            this.Controls.Add(this.Analysebutton);
            this.Controls.Add(this.SelectFilebutton);
            this.Controls.Add(this.Filelabel);
            this.Controls.Add(this.FiletextBox);
            this.Name = "AnalyseForm";
            this.Text = "AnalyseForm";
            this.Imagepanel.ResumeLayout(false);
            this.Imagepanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ImagepictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.JapanpictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel Imagepanel;
        private System.Windows.Forms.PictureBox ImagepictureBox;
        private System.Windows.Forms.PropertyGrid ImagepropertyGrid;
        private System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.Button AddButton;
        private System.Windows.Forms.Button Analysebutton;
        private System.Windows.Forms.Button SelectFilebutton;
        private System.Windows.Forms.Label Filelabel;
        private System.Windows.Forms.TextBox FiletextBox;
        private System.Windows.Forms.PictureBox JapanpictureBox;
        private System.Windows.Forms.TextBox JapantextBox;
        private System.Windows.Forms.Button Translatebutton;
        private System.Windows.Forms.TextBox EnglishtextBox;
    }
}

