using AForge.Video.DirectShow;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace CameraCapture
{
    public partial class CameraCaptureForm : Form
    {
        #region Private fields

        // I'm using double buffering for rendering graphic to avoid frequent blinkings.
        private BufferedGraphicsContext context;
        private BufferedGraphics grafx;
        private byte bufferingMode = 1;
        private System.Windows.Forms.Timer timer1;

        // Video source.
        private FilterInfoCollection _videoDevices;
        private VideoCaptureDevice _videoSource;
        private int _currentVideoDeviceIndex = -1;

        // Graphic properties.
        private GridSettings _gridSettings;
        private float[] _bitmapBrightness = null;
        private readonly object _lock = new object();

        // Calibration.
        private int _trackBarValue;
        private int _etalon1;
        private int _etalon2;
        private int _etalonCoord1;
        private int _etalonCoord2;

        #endregion

        #region Constructor

        public CameraCaptureForm()
        {
            InitializeComponent();

            // Configure a timer to draw graphics updates.
            timer1 = new System.Windows.Forms.Timer();
            timer1.Interval = 200;
            timer1.Tick += new EventHandler(this.OnTimer);

            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, bufferingMode == 1);
            context = BufferedGraphicsManager.Current;
            context.MaximumBuffer = new System.Drawing.Size(this.splitContainer1.Panel2.Height + 1, this.splitContainer1.Panel2.Width + 1);
            grafx = context.Allocate(this.CreateGraphics(), new Rectangle(
                0, 0, this.splitContainer1.Panel2.Width, this.splitContainer1.Panel2.Height));

            _gridSettings = new GridSettings();
            _gridSettings.Height = splitContainer1.Panel2.Height;
            _gridSettings.Width = splitContainer1.Panel2.Width;
            _gridSettings.Step = 20;
            _gridSettings.XName = "Длина волны, нм";
            _gridSettings.YName = "Плотность энергии, отн.ед.";

            pictureBox.Height = 0;
            pictureBox.Width = 0;
            pictureBox.Visible = false;

            // Draw the first frame to the buffer.
            _DrawMathGrid(grafx.Graphics);

            timer1.Start();

            _ReinitializeCalibration(0, 640);
        }

        #endregion

        #region Private event handlers

        private void OnTimer(object sender, EventArgs e)
        {
            // Draw randomly positioned ellipses to the buffer.
            _DrawMathGrid(grafx.Graphics);
            this.Refresh();
        }

        private void CameraCaptureForm_Load(object sender, EventArgs e)
        {
            this.SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint, true);
            this.videoSourcePlayer.SendToBack();
            _startCameraCapture(_currentVideoDeviceIndex);
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {
            //_DrawMathGrid(e.Graphics);
            grafx.Render(e.Graphics);
        }

        private void CameraCaptureForm_SizeChanged(object sender, EventArgs e)
        {
            _gridSettings.Height = splitContainer1.Panel2.Height;
            _gridSettings.Width = splitContainer1.Panel2.Width;

            this.Update();
        }

        // new frame event handler
        private void playerControl_NewFrame(object sender, ref Bitmap image)
        {
            // process new frame somehow ...

            // Note: it may be even changed, so the control will display the result
            // of image processing done here

            lock (_lock)
            {
                image.RotateFlip(RotateFlipType.RotateNoneFlipX);
                _FillBrightnessBuffer(image);
            }

            splitContainer1.Panel2.Invalidate();
        }

        private void CameraSwitched_Click(object sender, EventArgs e)
        {
            int camera_nums = _videoDevices.Count;
            _currentVideoDeviceIndex++;
            if (_currentVideoDeviceIndex >= camera_nums)
            {
                _currentVideoDeviceIndex = 0;
            }
            if (videoSourcePlayer != null && videoSourcePlayer.IsRunning)
            {
                videoSourcePlayer.SignalToStop();
                videoSourcePlayer.WaitForStop();
            }
            _startCameraCapture(_currentVideoDeviceIndex);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer1.Stop();

            _stopCameraCapture();

            videoSourcePlayer.Dispose();
            videoSourcePlayer = null;

            this.Dispose();
            this.Close();
        }

        private void comboBoxCameras_SelectedIndexChanged(object sender, EventArgs e)
        {
            _currentVideoDeviceIndex = comboBoxCameras.SelectedIndex;
            _startCameraCapture(_currentVideoDeviceIndex);
        }

        private void CameraCaptureForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer1.Stop();

            _stopCameraCapture();

            videoSourcePlayer.Dispose();
            videoSourcePlayer = null;

            this.Dispose();
            this.Close();
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            _HandleFile();
        }

        private void btnSaveToFile_Click(object sender, EventArgs e)
        {
            _SaveToFile();
        }

        private void btnStartCamera_Click(object sender, EventArgs e)
        {
            _startCameraCapture(_currentVideoDeviceIndex);
        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _HandleFile();
        }

        private void saveFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _SaveToFile();
        }

        private void btnEtalon1_Click(object sender, EventArgs e)
        {
            txbFirstCalibration.ReadOnly = false;
            lblEtalonNotifPanel.Visible = true;
            if (pictureBox.Visible)
            {
                pictureBox.MouseClick += pictureBoxEtalon1_Click;
            }
            else
            {
                videoSourcePlayer.MouseClick += videoSourcePlayerEtalon1_Click;
            }
        }

        private void btnEtalon2_Click(object sender, EventArgs e)
        {
            txbSecondCalibration.ReadOnly = false;
            lblEtalonNotifPanel.Visible = true;
            if (pictureBox.Visible)
            {
                pictureBox.MouseClick += pictureBoxEtalon2_Click;
            }
            else
            {
                videoSourcePlayer.MouseClick += videoSourcePlayerEtalon2_Click;
            }
        }

        private void pictureBoxEtalon1_Click(object sender, MouseEventArgs e)
        {
            lblEtalonNotifPanel.Visible = false;
            txbFirstCalibration.ReadOnly = true;
            // Calculate position of etalon by scaling clicked X to real size of image related to size of picture control.
            if (pictureBox.Image != null)
            {
                _etalonCoord1 = (int)((float)e.X * (float)pictureBox.Image.Width / (float)pictureBox.Width);
                txbCoordinate1.Text = _etalonCoord1.ToString();
                Int32.TryParse(txbFirstCalibration.Text, out _etalon1);
            }
            pictureBox.MouseClick -= pictureBoxEtalon1_Click;
        }

        private void pictureBoxEtalon2_Click(object sender, MouseEventArgs e)
        {
            lblEtalonNotifPanel.Visible = false;
            txbSecondCalibration.ReadOnly = true;

            // Calculate position of etalon by scaling clicked X to real size of image related to size of picture control.
            if (pictureBox.Image != null)
            {
                _etalonCoord2 = (int)((float)e.X * (float)pictureBox.Image.Width / (float)pictureBox.Width);
                txbCoordinate2.Text = _etalonCoord2.ToString();
                Int32.TryParse(txbSecondCalibration.Text, out _etalon2);
            }

            pictureBox.MouseClick -= pictureBoxEtalon2_Click;
        }

        private void videoSourcePlayerEtalon1_Click(object sender, MouseEventArgs e)
        {
            txbFirstCalibration.ReadOnly = true;
            lblEtalonNotifPanel.Visible = false;
            // Calculate position of etalon by scaling clicked X to real size of frame related to size of video control.
            if (_videoSource != null)
            {
                _etalonCoord1 = (int)((float)e.X * (float)_videoSource.VideoResolution.FrameSize.Width / (float)videoSourcePlayer.Width);
                txbCoordinate1.Text = _etalonCoord1.ToString();
                Int32.TryParse(txbFirstCalibration.Text, out _etalon1);
            }
            videoSourcePlayer.MouseClick -= videoSourcePlayerEtalon1_Click;
        }

        private void videoSourcePlayerEtalon2_Click(object sender, MouseEventArgs e)
        {
            txbSecondCalibration.ReadOnly = true;
            lblEtalonNotifPanel.Visible = false;
            // Calculate position of etalon by scaling clicked X to real size of frame related to size of video control.
            if (_videoSource != null)
            {
                _etalonCoord2 = (int)((float)e.X * (float)_videoSource.VideoResolution.FrameSize.Width / (float)videoSourcePlayer.Width);
                txbCoordinate2.Text = _etalonCoord2.ToString();
                Int32.TryParse(txbSecondCalibration.Text, out _etalon2);
            }
            videoSourcePlayer.MouseClick -= videoSourcePlayerEtalon2_Click;
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            _trackBarValue = trackBar1.Value;
        }

        #endregion

        #region Private methods

        private void _ReinitializeCalibration(int start, int end)
        {
            if (_videoSource != null && this.videoSourcePlayer.IsRunning)
            {
                trackBar1.Maximum = _videoSource.VideoResolution.FrameSize.Height / 10;
                trackBar1.Value = _videoSource.VideoResolution.FrameSize.Height / 20;
                _trackBarValue = trackBar1.Value;
            }
            else if (pictureBox.Image != null)
            {
                trackBar1.Maximum = pictureBox.Image.Height / 10;
                trackBar1.Value = pictureBox.Image.Height / 20;
                _trackBarValue = trackBar1.Value;
            }
            else
            {
                // Use default.
                _trackBarValue = trackBar1.Value;
            }

            _etalon1 = 400;
            _etalon2 = 700;
            _etalonCoord1 = start;
            _etalonCoord2 = end;
            txbCoordinate1.Text = _etalonCoord1.ToString();
            txbCoordinate2.Text = _etalonCoord2.ToString();
            txbFirstCalibration.Text = _etalon1.ToString();
            txbSecondCalibration.Text = _etalon2.ToString();
        }

        private void _HandleFile()
        {
            _stopCameraCapture();

            String textboxString = String.Empty;
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "(*.bmp, *.png)|*.bmp;*.png";

                if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (ofd.CheckFileExists == true)
                    {
                        textboxString = ofd.FileName;

                        pictureBox.Image = Image.FromFile(textboxString);

                        pictureBox.Height = 480;
                        pictureBox.Width = 640;
                        pictureBox.Visible = true;

                        _ReinitializeCalibration(0, pictureBox.Image.Width);

                        lock (_lock)
                        {
                            if (_bitmapBrightness != null)
                            {
                                Array.Clear(_bitmapBrightness, 0, _bitmapBrightness.Length);
                            }
                        }

                        splitContainer1.Panel2.Invalidate();
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void _SaveToFile()
        {
            try
            {
                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.AddExtension = true;
                    sfd.CheckFileExists = false;
                    sfd.CheckPathExists = false;
                    sfd.DefaultExt = ".png";
                    sfd.Filter = "(*.png)|*.png";
                    DialogResult result = sfd.ShowDialog();

                    if (result == System.Windows.Forms.DialogResult.OK && !String.IsNullOrEmpty(sfd.FileName))
                    {
                        Bitmap bitmap = null;
                        if (pictureBox.Visible)
                        {
                            bitmap = new Bitmap(pictureBox.Image);
                        }
                        else if (videoSourcePlayer != null && videoSourcePlayer.IsRunning)
                        {
                            bitmap = videoSourcePlayer.GetCurrentVideoFrame();
                        }

                        using (var sw = new StreamWriter(sfd.FileName, false))
                        {
                            bitmap.Save(sw.BaseStream, ImageFormat.Png);
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        private void _startCameraCapture(int currentVideoDeviceIndex)
        {
            this.pictureBox.Height = 0;
            this.pictureBox.Width = 0;
            this.pictureBox.Visible = false;

            this.videoSourcePlayer.Visible = true;
            try
            {
                _stopCameraCapture();

                _videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                comboBoxCameras.DisplayMember = "Name";
                comboBoxCameras.ValueMember = "MonikerString";
                comboBoxCameras.DataSource = _videoDevices;

                // Preselect USB cam.
                for (int i = 0; i < _videoDevices.Count; i++)
                {
                    if (_videoDevices[i].MonikerString.Contains("65e8773d-8f56-11d0-a3b9-00a0c9223196") || _videoDevices[i].Name == "USB Camera")
                    {
                        currentVideoDeviceIndex = i;
                        break;
                    }
                }

                if (currentVideoDeviceIndex > -1)
                {
                    _videoSource = new VideoCaptureDevice(_videoDevices[currentVideoDeviceIndex].MonikerString);
                }
                else if (_videoDevices.Count > 0)
                {
                    _videoSource = new VideoCaptureDevice(_videoDevices[0].MonikerString);
                }
                else
                {
                    MessageBox.Show("No source video stream!");
                }

                if (_videoSource != null)
                {
                    _videoSource.VideoResolution = _videoSource.VideoCapabilities[_videoSource.VideoCapabilities.Length - 1];
                    this.videoSourcePlayer.VideoSource = _videoSource;
                    this.videoSourcePlayer.NewFrame += this.playerControl_NewFrame;
                    this.videoSourcePlayer.Start();

                    this.comboBoxCameras.SelectedIndexChanged += comboBoxCameras_SelectedIndexChanged;

                    _ReinitializeCalibration(0, _videoSource.VideoResolution.FrameSize.Width);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("startCameraCapture Exception");
                this.Dispose();
                this.Close();
            }
        }

        private void _stopCameraCapture()
        {
            if (this.videoSourcePlayer != null && this.videoSourcePlayer.IsRunning)
            {
                this.videoSourcePlayer.SignalToStop();
                this.videoSourcePlayer.WaitForStop();
                this.videoSourcePlayer.NewFrame -= this.playerControl_NewFrame;

                this.comboBoxCameras.SelectedIndexChanged -= comboBoxCameras_SelectedIndexChanged;
            }
        }

        private void _DrawMathGrid(Graphics formGraphics)
        {
            if (pictureBox.Visible && pictureBox.Image != null)
            {
                _DrawGraphicFromImage(new Bitmap(pictureBox.Image), formGraphics);
            }
            else
            {
                _DrawGraphic(formGraphics);
            }
        }

        private void _DrawGraphicFromImage(Bitmap image, Graphics formGraphics)
        {
            // Build bufer
            if (image != null)
            {
                _FillBrightnessBuffer(image);
                _DrawGraphic(formGraphics);
            }
        }

        private void _FillBrightnessBuffer(Bitmap image)
        {
            double percent = (double)_trackBarValue / (double)trackBar1.Maximum;
            percent = 1 - percent; // Invert percents because we draw from Top (and left) corner, but Track control starts from Bottom.
            int horizont = (int)((double)image.Height * percent);

            if (horizont == 0)
            {
                horizont = 10;
            }
            else if (horizont == image.Height)
            {
                horizont = image.Height - 10;
            }

            int deltaMin = horizont - 5;
            int deltaMax = horizont + 5;

            _bitmapBrightness = new float[image.Width];
            for (int i = 0; i < image.Width; i++)
            {
                float maxBrightness = 0;
                for (int j = deltaMin; j < deltaMax; j++)
                {
                    Color pixel = image.GetPixel(i, j);
                    float br = pixel.GetBrightness();

                    if (br > maxBrightness)
                    {
                        maxBrightness = br;
                    }
                }

                _bitmapBrightness[i] = maxBrightness;
            }
        }

        private void _DrawGraphic(Graphics formGraphics)
        {
            // We use INT number of values in Pixel.
            int valueDiff = Math.Abs(_etalon1 - _etalon2);
            int fullXDrawGraphArea = splitContainer1.Panel2.Width - 4 * _gridSettings.Step;
            int pixelsForSingleValue = (int)Math.Floor((double)fullXDrawGraphArea / (double)valueDiff);
            pixelsForSingleValue = pixelsForSingleValue == 0 ? 1 : pixelsForSingleValue; // For invalid (should never happen) cases.

            int offsetX = 4 * _gridSettings.Step;
            int offsetY = offsetX;

            double workingXImageArea = (double)Math.Abs(_etalonCoord2 - _etalonCoord1);
            double tension = (valueDiff * pixelsForSingleValue) / workingXImageArea;

            lock (_lock)
            {
                // Build coordinates for graphic.
                if (_bitmapBrightness != null &&
                    // Check that brightness collection already rebuilt (for cases if switch images).
                    _bitmapBrightness.Length >= Math.Max(_etalonCoord2, _etalonCoord1))
                {
                    int fullYDrawGraphArea = _gridSettings.Height - offsetY;
                    float minBrightness = _bitmapBrightness.Min();
                    float brightnessInPixel = (_bitmapBrightness.Max() - minBrightness) / (float)fullYDrawGraphArea;
                    //float pixelsForSingleBrightness = (int)Math.Floor((double)(_gridSettings.Height - 4 * _gridSettings.Step) / ((double)(brightnessMax - brightnessMin) / 10));

                    var points = new List<System.Drawing.Point>();

                    int counter = 0;
                    // Take only portion of image in selected area by user.
                    for (int i = _etalonCoord1; i < _etalonCoord2; i++)
                    {
                        points.Add(new System.Drawing.Point(offsetX + (int)((float)counter * tension), fullYDrawGraphArea - (int)((_bitmapBrightness[i] - minBrightness) / brightnessInPixel)));
                        ++counter;
                    }

                    _DrawAll(formGraphics, brightnessInPixel, points, pixelsForSingleValue);
                }
            }
        }

        private void _DrawAll(Graphics formGraphics, float brightnessInPixel, List<System.Drawing.Point> points, int pixelsForSingleValue)
        {
            // Clear screen.
            formGraphics.FillRectangle(Brushes.Black, 0, 0, splitContainer1.Panel2.Width, splitContainer1.Panel2.Height);

            int smallRulerStep = 5 * pixelsForSingleValue;
            int bigRulerStep = 25 * pixelsForSingleValue;
            int offset = 4 * _gridSettings.Step;

            using (Pen pen = new Pen(System.Drawing.Color.Green))
            {

                // Horizontal green lines of grid
                for (int y = _gridSettings.Height - offset; y > 0; y -= smallRulerStep)
                {
                    formGraphics.DrawLine(pen, offset, y, _gridSettings.Width, y);
                }

                // Vertical green lines of grid
                for (int x = offset; x < _gridSettings.Width; x += smallRulerStep)
                {
                    formGraphics.DrawLine(pen, x, _gridSettings.Height - offset, x, 0);
                }
            }

            using (Pen pen = new Pen(System.Drawing.Color.LightGreen))
            {
                int step = 25 * pixelsForSingleValue;

                // Horizontal green lines of grid
                for (int y = _gridSettings.Height - offset; y > 0; y -= bigRulerStep)
                {
                    formGraphics.DrawLine(pen, offset, y, _gridSettings.Width, y);
                }

                // Vertical green lines of grid
                for (int x = offset; x < _gridSettings.Width; x += bigRulerStep)
                {
                    formGraphics.DrawLine(pen, x, _gridSettings.Height - offset, x, 0);
                }
            }

            using (Pen pen = new Pen(System.Drawing.Color.White))
            {
                float fontSizeInPixels = _gridSettings.Font.SizeInPoints * formGraphics.DpiX / 72;

                // Draw Graphic
                formGraphics.DrawCurve(pen, points.ToArray());

                // Horizontal ruler main line
                formGraphics.DrawLine(pen,
                    offset, _gridSettings.Height - offset,
                    _gridSettings.Width, _gridSettings.Height - offset);
                // Vertical ruler main line
                formGraphics.DrawLine(pen,
                    offset, 0,
                    offset, _gridSettings.Height - offset);

                int wid = offset - _gridSettings.Step / 4;
                int lwid = offset - _gridSettings.Step / 2;
                bool flag = true;

                ////////////////////////////////////////////////////////////////////////////// Ruler
                float valueY = 0;
                float valueYStep = _gridSettings.Step * brightnessInPixel;
                for (int y = _gridSettings.Height - offset; y > 0; y -= _gridSettings.Step)
                {
                    ////////////////////////////////////////////////////////////////////////// Horizontal ruler lines
                    formGraphics.DrawLine(pen, flag ? lwid : wid, y, offset, y);

                    formGraphics.DrawString(valueY.ToString("0.00"), _gridSettings.Font, Brushes.White,
                        _gridSettings.Step, y - fontSizeInPixels / 2);

                    valueY += valueYStep;
                    flag = !flag;
                }

                // We cannot say anything about Wave Length until calibration will be done.
                float nextMarkerValue = (float)Math.Min(_etalon1, _etalon2);
                int stepNumberBetweenMarkers = (int)Math.Ceiling(fontSizeInPixels * 6.0 / (double)_gridSettings.Step);
                int markerStepIterator = bigRulerStep;

                wid = _gridSettings.Step - _gridSettings.Step / 4;
                lwid = _gridSettings.Step - _gridSettings.Step / 2;
                flag = true;
                for (int x = offset; x < _gridSettings.Width; x += smallRulerStep)
                {
                    if (markerStepIterator == bigRulerStep)
                    {
                        flag = false;
                        markerStepIterator = 0;

                        formGraphics.DrawString(nextMarkerValue.ToString("0.0"), _gridSettings.Font, Brushes.White,
                            x - (int)(fontSizeInPixels * 3.0 / 2.0), _gridSettings.Height - 3 * _gridSettings.Step + _gridSettings.Step / 4);

                        nextMarkerValue += 25;
                    }

                    // Vertical ruler lines
                    formGraphics.DrawLine(pen,
                        x, _gridSettings.Height - offset,
                        x, _gridSettings.Height - offset + (flag ? lwid : wid));

                    flag = true;
                    markerStepIterator += smallRulerStep;
                }

                // Horizontal ruler text
                formGraphics.DrawString(_gridSettings.XName, _gridSettings.Font, Brushes.White,
                   2 * _gridSettings.Step + (_gridSettings.Width - offset) / 2,
                   _gridSettings.Height - 3 * _gridSettings.Step + _gridSettings.Step);

                // Vertical ruler text
                formGraphics.RotateTransform(90);
                formGraphics.DrawString(_gridSettings.YName, _gridSettings.Font, Brushes.White,
                    (_gridSettings.Height - offset) / 2 - _gridSettings.XName.Length * fontSizeInPixels / 4, // Divded by 4 because /2 to find cetner and also Font Height ~ 2 * Font Width
                    -_gridSettings.Step);
            }
        }

        #endregion
    }
}
