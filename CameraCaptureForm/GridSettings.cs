using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace CameraCapture
{
    public class GridSettings
    {
        public GridSettings()
        {
            Font = new Font("Calibri", 10);
        }

        public int Width { get; set; }
        public int Height { get; set; }
        public int Step { get; set; }

        public double XMinValue { get; set; }
        public double XMaxValue { get; set; }

        public double YMinValue { get; set; }
        public double YMaxValue { get; set; }

        public string XName { get; set; }
        public string YName { get; set; }

        public Font Font { get; set; }
    }
}
