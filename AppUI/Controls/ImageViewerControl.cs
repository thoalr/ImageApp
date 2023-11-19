using OpenTK.Core;
using SixLabors.ImageSharp.PixelFormats;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppUI
{
    public partial class ImageViewerControl : UserControl
    {

        SKBitmap? image1;
        SKBitmap? image2;

        bool showDouble;
        bool leftToRight;

        public EventHandler NextImage;
        public EventHandler PrevImage;

        public EventHandler NextTwoImages;
        public EventHandler PrevTwoImages;

        Point? mouseLastPosition;

        SKPoint imageLocation;
        SKSize imageSize;
        double imageZoom;
        double zoomStep = 1.1;

        public ImageViewerControl()
        {
            InitializeComponent();
            imageLocation = new SKPoint(0, 0);
            imageZoom = 1.0;
            showDouble = false;
            leftToRight = true;
            this.MouseWheel += ImageViewerControl_MouseWheel;
            this.PreviewKeyDown += ImageViewerControl_PreviewKeyDown;
            skglControl1.PreviewKeyDown += SkglControl1_PreviewKeyDown;
        }

        private void SkglControl1_PreviewKeyDown(object? sender, PreviewKeyDownEventArgs e)
        {
            skglControl1_KeyDown(this, new KeyEventArgs(e.KeyData));
        }

        private void ImageViewerControl_PreviewKeyDown(object? sender, PreviewKeyDownEventArgs e)
        {
            skglControl1_KeyDown(this, new KeyEventArgs(e.KeyData));
        }

        public void SetImage(string filePath)
        {
            image1 = SKBitmap.Decode(filePath);
            imageSize = new SKSize(image1.Width, image1.Height);

            InitCenterSizeToFitImage();
        }

        public void SetImage(SKBitmap image)
        {
            image1 = image;
            imageSize = new SKSize(image.Width, image.Height);
            InitCenterSizeToFitImage();
        }

        void InitCenterSizeToFitImage()
        {
            if (showDouble)
            {

            }
            else
            {
                if (image1 == null) return;
                var w = image1.Width;
                var h = image1.Height;
                var heightRatio = h / (double)this.Height;
                var widthRatio = w / (double)this.Width;
                int destHeight;
                int destWidth;
                if (heightRatio >= widthRatio)
                {
                    destHeight = this.Height;
                    destWidth = (int)(w / heightRatio);
                }
                else
                {
                    destWidth = this.Width;
                    destHeight = (int)(h / widthRatio);
                }
                var targetY = (this.Height - destHeight) / 2;
                var targetX = (this.Width - destWidth) / 2;
                this.imageLocation = new SKPoint(targetX, targetY);
                this.imageSize = new SKSize(destWidth, destHeight);
                this.imageZoom = imageSize.Height / image1.Height;
            }
        }

        void CenterImage()
        {
            if (showDouble)
            {

            }
            else
            {
                if (image1 == null) return;
                var w = image1.Width * imageZoom;
                var h = image1.Height * imageZoom;
                var heightRatio = h / this.Height;
                var widthRatio = w / this.Width;

                var targetX = (this.Height - h) / 2;
                var targetY = (this.Width - w) / 2;
                this.imageLocation = new SKPoint((float)targetX, (float)targetY);
                //this.imageZoom = imageSize.Height / image1.Height;
            }
        }

        public void SetTwoImages(string firstFile, string secondFile)
        {
            image1 = SKBitmap.Decode(firstFile);
            image2 = SKBitmap.Decode(secondFile);
        }

        private void skglControl1_PaintSurface(object sender, SkiaSharp.Views.Desktop.SKPaintGLSurfaceEventArgs e)
        {
            var skinfo = e.Info;
            var sksurface = e.Surface;
            var skcanvas = sksurface.Canvas;
            if (showDouble)
            {
                if (leftToRight)
                {

                }
            }
            if (image1 != null)
            {
                skcanvas.Clear();
                var rect = new SKRect(imageLocation.X, imageLocation.Y + imageSize.Height, imageLocation.X + imageSize.Width, imageLocation.Y);
                rect = SKRect.Create(this.imageLocation, imageSize);
                skcanvas.DrawBitmap(image1, rect);
            }
        }


        private void skglControl1_MouseDown(object sender, MouseEventArgs e)
        {
            this.Capture = true;
            var success = skglControl1.Focus();
            if (!success)
            {
                bool canF = CanFocus;
            }
        }

        private void skglControl1_MouseMove(object sender, MouseEventArgs e)
        {

            if (this.Capture)
            {
                if (mouseLastPosition == null)
                {
                    mouseLastPosition = e.Location;
                    return;
                }
                var mouseDelta = new Point(e.X - mouseLastPosition!.Value.X, e.Y - mouseLastPosition!.Value.Y);
                var newLocation = new SKPoint(this.imageLocation.X + mouseDelta.X, this.imageLocation.Y + mouseDelta.Y);
                if (newLocation.X < 0) { newLocation.X = 0; }
                if (newLocation.Y < 0) { newLocation.Y = 0; }
                if (newLocation.X + imageSize.Width > this.Width) { newLocation.X = this.Width - imageSize.Width; }
                if (newLocation.Y + imageSize.Height > this.Height) { newLocation.Y = this.Height - imageSize.Height; }
                this.imageLocation = newLocation;

                skglControl1.Invalidate();
                this.mouseLastPosition = e.Location;
            }
        }

        private void skglControl1_MouseUp(object sender, MouseEventArgs e)
        {
            this.Capture = false;
        }

        private void skglControl1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Right:
                    NextImage?.Invoke(this, new EventArgs());
                    skglControl1.Invalidate();
                    break;
                case Keys.Left:
                    PrevImage?.Invoke(this, new EventArgs());
                    skglControl1.Invalidate();
                    break;


            }
        }

        private void skglControl1_Resize(object sender, EventArgs e)
        {

        }

        const int WM_MOUSEWHEEL = 0x020a;
        const int WM_MOUSEHWHEEL = 0x020E;
        protected override void WndProc(ref Message m)
        {
            // Listen for operating system messages.
            switch (m.Msg)
            {
                case WM_MOUSEHWHEEL:
                    // var mw = (m.WParam, m.LParam);
                    m.Result = 1;

                    var eventToMouseWheel = (IntPtr p) =>
                    {
                        int val32 = p.ToInt32();
                        int v = ((val32 >> 16) & 0xFFFF);
                        return (Int16)v;
                    };

                    //OnMouseWheel(eventToMouseWheel(m.WParam));

                    break;
            }
            base.WndProc(ref m);
        }

        private void ImageViewerControl_MouseWheel(object? sender, MouseEventArgs e)
        {
            OnMouseWheel(e.Delta);
        }

        private void OnMouseWheel(int delta)
        {
            if (delta > 0)
            {
                imageZoom = imageZoom * zoomStep;
            }
            else
            {
                imageZoom = imageZoom / zoomStep;
            }
            CenterImage();
            skglControl1.Invalidate();
        }

        private void skglControl1_DoubleClick(object sender, EventArgs e)
        {
            // TODO: set this control as fullscreen
        }

        private void ImageViewerControl_MouseMove(object sender, MouseEventArgs e)
        {
            skglControl1_MouseMove(sender, e);
        }

        private void ImageViewerControl_MouseDown(object sender, MouseEventArgs e)
        {
            skglControl1_MouseDown(sender, e);
        }

        private void ImageViewerControl_MouseUp(object sender, MouseEventArgs e)
        {
            skglControl1_MouseUp(sender, e);
        }

        private void ImageViewerControl_KeyDown(object sender, KeyEventArgs e)
        {
            skglControl1_KeyDown(sender, e);
        }

        private void ImageViewerControl_DoubleClick(object sender, EventArgs e)
        {

        }

        private void ImageViewerControl_Resize(object sender, EventArgs e)
        {

        }
    }
}
