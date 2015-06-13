namespace CameraCapture
{
    partial class CameraCaptureForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CameraCaptureForm));
            this.videoSourcePlayer = new AForge.Controls.VideoSourcePlayer();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.lblEtalonNotifPanel = new System.Windows.Forms.Label();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblP = new System.Windows.Forms.Label();
            this.lblT = new System.Windows.Forms.Label();
            this.lblL = new System.Windows.Forms.Label();
            this.txbP = new System.Windows.Forms.TextBox();
            this.txbT = new System.Windows.Forms.TextBox();
            this.txbL = new System.Windows.Forms.TextBox();
            this.lblY = new System.Windows.Forms.Label();
            this.lblX = new System.Windows.Forms.Label();
            this.txbY = new System.Windows.Forms.TextBox();
            this.txbX = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblScaleStep = new System.Windows.Forms.Label();
            this.lblLeftBorder = new System.Windows.Forms.Label();
            this.txbScaleStep = new System.Windows.Forms.TextBox();
            this.txbLeftBorder = new System.Windows.Forms.TextBox();
            this.btnSetScale = new System.Windows.Forms.Button();
            this.btnEtalon2 = new System.Windows.Forms.Button();
            this.btnEtalon1 = new System.Windows.Forms.Button();
            this.lblCoordinate2 = new System.Windows.Forms.Label();
            this.lblCoordinate1 = new System.Windows.Forms.Label();
            this.txbCoordinate2 = new System.Windows.Forms.TextBox();
            this.txbCoordinate1 = new System.Windows.Forms.TextBox();
            this.lblSecondCalibration = new System.Windows.Forms.Label();
            this.lblFirstCalibration = new System.Windows.Forms.Label();
            this.txbSecondCalibration = new System.Windows.Forms.TextBox();
            this.txbFirstCalibration = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSaveToFile = new System.Windows.Forms.Button();
            this.btnOpenFile = new System.Windows.Forms.Button();
            this.btnStartCamera = new System.Windows.Forms.Button();
            this.comboBoxCameras = new System.Windows.Forms.ComboBox();
            this.menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // videoSourcePlayer
            // 
            resources.ApplyResources(this.videoSourcePlayer, "videoSourcePlayer");
            this.videoSourcePlayer.BackColor = System.Drawing.SystemColors.WindowText;
            this.videoSourcePlayer.Cursor = System.Windows.Forms.Cursors.Cross;
            this.videoSourcePlayer.Name = "videoSourcePlayer";
            this.videoSourcePlayer.VideoSource = null;
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFileToolStripMenuItem,
            this.saveFileToolStripMenuItem,
            this.toolStripMenuItem2,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            resources.ApplyResources(this.fileToolStripMenuItem, "fileToolStripMenuItem");
            // 
            // openFileToolStripMenuItem
            // 
            this.openFileToolStripMenuItem.Name = "openFileToolStripMenuItem";
            resources.ApplyResources(this.openFileToolStripMenuItem, "openFileToolStripMenuItem");
            this.openFileToolStripMenuItem.Click += new System.EventHandler(this.openFileToolStripMenuItem_Click);
            // 
            // saveFileToolStripMenuItem
            // 
            this.saveFileToolStripMenuItem.Name = "saveFileToolStripMenuItem";
            resources.ApplyResources(this.saveFileToolStripMenuItem, "saveFileToolStripMenuItem");
            this.saveFileToolStripMenuItem.Click += new System.EventHandler(this.saveFileToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            resources.ApplyResources(this.toolStripMenuItem2, "toolStripMenuItem2");
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            resources.ApplyResources(this.exitToolStripMenuItem, "exitToolStripMenuItem");
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            resources.ApplyResources(this.menuStrip, "menuStrip");
            this.menuStrip.Name = "menuStrip";
            // 
            // splitContainer1
            // 
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.trackBar1);
            this.splitContainer1.Panel1.Controls.Add(this.lblEtalonNotifPanel);
            this.splitContainer1.Panel1.Controls.Add(this.pictureBox);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox3);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox2);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel1.Controls.Add(this.videoSourcePlayer);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.splitContainer1.Panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel2_Paint);
            // 
            // trackBar1
            // 
            resources.ApplyResources(this.trackBar1, "trackBar1");
            this.trackBar1.Maximum = 48;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Value = 24;
            this.trackBar1.ValueChanged += new System.EventHandler(this.trackBar1_ValueChanged);
            // 
            // lblEtalonNotifPanel
            // 
            resources.ApplyResources(this.lblEtalonNotifPanel, "lblEtalonNotifPanel");
            this.lblEtalonNotifPanel.Name = "lblEtalonNotifPanel";
            // 
            // pictureBox
            // 
            resources.ApplyResources(this.pictureBox, "pictureBox");
            this.pictureBox.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.pictureBox.Cursor = System.Windows.Forms.Cursors.Cross;
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.TabStop = false;
            // 
            // groupBox3
            // 
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Controls.Add(this.lblP);
            this.groupBox3.Controls.Add(this.lblT);
            this.groupBox3.Controls.Add(this.lblL);
            this.groupBox3.Controls.Add(this.txbP);
            this.groupBox3.Controls.Add(this.txbT);
            this.groupBox3.Controls.Add(this.txbL);
            this.groupBox3.Controls.Add(this.lblY);
            this.groupBox3.Controls.Add(this.lblX);
            this.groupBox3.Controls.Add(this.txbY);
            this.groupBox3.Controls.Add(this.txbX);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // lblP
            // 
            resources.ApplyResources(this.lblP, "lblP");
            this.lblP.Name = "lblP";
            // 
            // lblT
            // 
            resources.ApplyResources(this.lblT, "lblT");
            this.lblT.Name = "lblT";
            // 
            // lblL
            // 
            resources.ApplyResources(this.lblL, "lblL");
            this.lblL.Name = "lblL";
            // 
            // txbP
            // 
            resources.ApplyResources(this.txbP, "txbP");
            this.txbP.Name = "txbP";
            this.txbP.ReadOnly = true;
            // 
            // txbT
            // 
            resources.ApplyResources(this.txbT, "txbT");
            this.txbT.Name = "txbT";
            this.txbT.ReadOnly = true;
            // 
            // txbL
            // 
            resources.ApplyResources(this.txbL, "txbL");
            this.txbL.Name = "txbL";
            this.txbL.ReadOnly = true;
            // 
            // lblY
            // 
            resources.ApplyResources(this.lblY, "lblY");
            this.lblY.Name = "lblY";
            // 
            // lblX
            // 
            resources.ApplyResources(this.lblX, "lblX");
            this.lblX.Name = "lblX";
            // 
            // txbY
            // 
            this.txbY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            resources.ApplyResources(this.txbY, "txbY");
            this.txbY.Name = "txbY";
            // 
            // txbX
            // 
            this.txbX.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            resources.ApplyResources(this.txbX, "txbX");
            this.txbX.Name = "txbX";
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Controls.Add(this.lblScaleStep);
            this.groupBox2.Controls.Add(this.lblLeftBorder);
            this.groupBox2.Controls.Add(this.txbScaleStep);
            this.groupBox2.Controls.Add(this.txbLeftBorder);
            this.groupBox2.Controls.Add(this.btnSetScale);
            this.groupBox2.Controls.Add(this.btnEtalon2);
            this.groupBox2.Controls.Add(this.btnEtalon1);
            this.groupBox2.Controls.Add(this.lblCoordinate2);
            this.groupBox2.Controls.Add(this.lblCoordinate1);
            this.groupBox2.Controls.Add(this.txbCoordinate2);
            this.groupBox2.Controls.Add(this.txbCoordinate1);
            this.groupBox2.Controls.Add(this.lblSecondCalibration);
            this.groupBox2.Controls.Add(this.lblFirstCalibration);
            this.groupBox2.Controls.Add(this.txbSecondCalibration);
            this.groupBox2.Controls.Add(this.txbFirstCalibration);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // lblScaleStep
            // 
            resources.ApplyResources(this.lblScaleStep, "lblScaleStep");
            this.lblScaleStep.Name = "lblScaleStep";
            // 
            // lblLeftBorder
            // 
            resources.ApplyResources(this.lblLeftBorder, "lblLeftBorder");
            this.lblLeftBorder.Name = "lblLeftBorder";
            // 
            // txbScaleStep
            // 
            resources.ApplyResources(this.txbScaleStep, "txbScaleStep");
            this.txbScaleStep.Name = "txbScaleStep";
            this.txbScaleStep.ReadOnly = true;
            // 
            // txbLeftBorder
            // 
            resources.ApplyResources(this.txbLeftBorder, "txbLeftBorder");
            this.txbLeftBorder.Name = "txbLeftBorder";
            this.txbLeftBorder.ReadOnly = true;
            // 
            // btnSetScale
            // 
            resources.ApplyResources(this.btnSetScale, "btnSetScale");
            this.btnSetScale.Name = "btnSetScale";
            this.btnSetScale.UseVisualStyleBackColor = true;
            // 
            // btnEtalon2
            // 
            resources.ApplyResources(this.btnEtalon2, "btnEtalon2");
            this.btnEtalon2.Name = "btnEtalon2";
            this.btnEtalon2.UseVisualStyleBackColor = true;
            this.btnEtalon2.Click += new System.EventHandler(this.btnEtalon2_Click);
            // 
            // btnEtalon1
            // 
            resources.ApplyResources(this.btnEtalon1, "btnEtalon1");
            this.btnEtalon1.Name = "btnEtalon1";
            this.btnEtalon1.UseVisualStyleBackColor = true;
            this.btnEtalon1.Click += new System.EventHandler(this.btnEtalon1_Click);
            // 
            // lblCoordinate2
            // 
            resources.ApplyResources(this.lblCoordinate2, "lblCoordinate2");
            this.lblCoordinate2.Name = "lblCoordinate2";
            // 
            // lblCoordinate1
            // 
            resources.ApplyResources(this.lblCoordinate1, "lblCoordinate1");
            this.lblCoordinate1.Name = "lblCoordinate1";
            // 
            // txbCoordinate2
            // 
            resources.ApplyResources(this.txbCoordinate2, "txbCoordinate2");
            this.txbCoordinate2.Name = "txbCoordinate2";
            this.txbCoordinate2.ReadOnly = true;
            // 
            // txbCoordinate1
            // 
            resources.ApplyResources(this.txbCoordinate1, "txbCoordinate1");
            this.txbCoordinate1.Name = "txbCoordinate1";
            this.txbCoordinate1.ReadOnly = true;
            // 
            // lblSecondCalibration
            // 
            resources.ApplyResources(this.lblSecondCalibration, "lblSecondCalibration");
            this.lblSecondCalibration.Name = "lblSecondCalibration";
            // 
            // lblFirstCalibration
            // 
            resources.ApplyResources(this.lblFirstCalibration, "lblFirstCalibration");
            this.lblFirstCalibration.Name = "lblFirstCalibration";
            // 
            // txbSecondCalibration
            // 
            resources.ApplyResources(this.txbSecondCalibration, "txbSecondCalibration");
            this.txbSecondCalibration.Name = "txbSecondCalibration";
            this.txbSecondCalibration.ReadOnly = true;
            // 
            // txbFirstCalibration
            // 
            resources.ApplyResources(this.txbFirstCalibration, "txbFirstCalibration");
            this.txbFirstCalibration.Name = "txbFirstCalibration";
            this.txbFirstCalibration.ReadOnly = true;
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.btnSaveToFile);
            this.groupBox1.Controls.Add(this.btnOpenFile);
            this.groupBox1.Controls.Add(this.btnStartCamera);
            this.groupBox1.Controls.Add(this.comboBoxCameras);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // btnSaveToFile
            // 
            resources.ApplyResources(this.btnSaveToFile, "btnSaveToFile");
            this.btnSaveToFile.Name = "btnSaveToFile";
            this.btnSaveToFile.UseVisualStyleBackColor = true;
            this.btnSaveToFile.Click += new System.EventHandler(this.btnSaveToFile_Click);
            // 
            // btnOpenFile
            // 
            resources.ApplyResources(this.btnOpenFile, "btnOpenFile");
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.UseVisualStyleBackColor = true;
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // btnStartCamera
            // 
            resources.ApplyResources(this.btnStartCamera, "btnStartCamera");
            this.btnStartCamera.Name = "btnStartCamera";
            this.btnStartCamera.UseVisualStyleBackColor = true;
            this.btnStartCamera.Click += new System.EventHandler(this.btnStartCamera_Click);
            // 
            // comboBoxCameras
            // 
            this.comboBoxCameras.FormattingEnabled = true;
            resources.ApplyResources(this.comboBoxCameras, "comboBoxCameras");
            this.comboBoxCameras.Name = "comboBoxCameras";
            // 
            // CameraCaptureForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.MaximizeBox = false;
            this.Name = "CameraCaptureForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CameraCaptureForm_FormClosing);
            this.Load += new System.EventHandler(this.CameraCaptureForm_Load);
            this.SizeChanged += new System.EventHandler(this.CameraCaptureForm_SizeChanged);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private AForge.Controls.VideoSourcePlayer videoSourcePlayer;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem openFileToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripMenuItem saveFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox comboBoxCameras;
        private System.Windows.Forms.Button btnStartCamera;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblSecondCalibration;
        private System.Windows.Forms.Label lblFirstCalibration;
        private System.Windows.Forms.TextBox txbSecondCalibration;
        private System.Windows.Forms.TextBox txbFirstCalibration;
        private System.Windows.Forms.Label lblCoordinate2;
        private System.Windows.Forms.Label lblCoordinate1;
        private System.Windows.Forms.TextBox txbCoordinate2;
        private System.Windows.Forms.TextBox txbCoordinate1;
        private System.Windows.Forms.Button btnEtalon2;
        private System.Windows.Forms.Button btnEtalon1;
        private System.Windows.Forms.Button btnSetScale;
        private System.Windows.Forms.Label lblScaleStep;
        private System.Windows.Forms.Label lblLeftBorder;
        private System.Windows.Forms.TextBox txbScaleStep;
        private System.Windows.Forms.TextBox txbLeftBorder;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txbY;
        private System.Windows.Forms.TextBox txbX;
        private System.Windows.Forms.Label lblY;
        private System.Windows.Forms.Label lblX;
        private System.Windows.Forms.Label lblP;
        private System.Windows.Forms.Label lblT;
        private System.Windows.Forms.Label lblL;
        private System.Windows.Forms.TextBox txbP;
        private System.Windows.Forms.TextBox txbT;
        private System.Windows.Forms.TextBox txbL;
        private System.Windows.Forms.Button btnSaveToFile;
        private System.Windows.Forms.Button btnOpenFile;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Label lblEtalonNotifPanel;
        private System.Windows.Forms.TrackBar trackBar1;
    }
}

