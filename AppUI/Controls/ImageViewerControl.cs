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

        public event EventHandler NextImage;
        public event EventHandler PrevImage;

        public event EventHandler NextTwoImages;
        public event EventHandler PrevTwoImages;

        Point? mouseLastPosition;

        SKPoint imageLocation;
        SKSize imageSize;
        float baseImageZoom = 1.0f;
        float zoomStep = 1.1f;
        int zoomLevel = 0;

        public ImageViewerControl()
        {
            InitializeComponent();
            imageLocation = new SKPoint(0, 0);
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

            InitCenterImageSizeToFit();
        }

        public void SetImage(SKBitmap image)
        {
            image1 = image;
            imageSize = new SKSize(image.Width, image.Height);
            InitCenterImageSizeToFit();
        }

        void InitCenterImageSizeToFit()
        {
            if (showDouble)
            {

            }
            else
            {
                if (image1 == null) return;
                var w = image1.Width;
                var h = image1.Height;
                var heightRatio = h / (float)skglControl1.Height;
                var widthRatio = w / (float)skglControl1.Width;
                int destHeight;
                int destWidth;
                if (heightRatio >= widthRatio)
                {
                    destHeight = this.Height;
                    destWidth = (int)(w / heightRatio);
                }
                else
                {
                    destWidth = skglControl1.Width;
                    destHeight = (int)(h / widthRatio);
                }
                var targetY = (this.Height - destHeight) / 2;
                var targetX = (skglControl1.Width - destWidth) / 2;
                this.imageLocation = new SKPoint(targetX, targetY);
                this.imageSize = new SKSize(destWidth, destHeight);
                this.baseImageZoom = imageSize.Height / image1.Height;
            }
        }

        void KeepImageWithinBounds()
        {
            if (showDouble)
            {

            }
            else
            {
                if (image1 == null) return;
                var w = imageSize.Width;
                var h = imageSize.Height;
                //var heightRatio = h / this.Height;
                //var widthRatio = w / skglControl1.Width;

                var targetX = imageLocation.X;
                var targetY = imageLocation.Y;

                if ( w <= skglControl1.Width)
                {
                    targetX = (skglControl1.Width - w) / 2;
                }
                else
                {
                    if( targetX > skglControl1.Width / 2)
                    {
                        targetX = skglControl1.Width / 2;
                    }
                    else if (targetX < -skglControl1.Width / 2)
                    {
                        targetX = -skglControl1.Width / 2;
                    }
                }
                if ( h <= skglControl1.Height)
                {
                    targetY = (this.Height - h) / 2;
                }
                else
                {
                    if (targetY > skglControl1.Height / 2)
                    {
                        targetY = skglControl1.Height / 2;
                    }
                    else if (targetY < -skglControl1.Height / 2)
                    {
                        targetY = -skglControl1.Height / 2;
                    }
                }

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
                //var rect = new SKRect(imageLocation.X, imageLocation.Y + imageSize.Height, imageLocation.X + imageSize.Width, imageLocation.Y);
                SKRect rect = SKRect.Create(this.imageLocation, imageSize);
                skcanvas.DrawBitmap(image1, rect);
            }
        }



        private void skglControl1_MouseUp(object sender, MouseEventArgs e)
        {
            skglControl1.Capture = false;
            this.mouseLastPosition = null;
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
            if (image1 == null) return;

            var oldSize = new SKSize(imageSize.Width, imageSize.Height);
            var oldLocation = new SKPoint(imageLocation.X, imageLocation.Y);

            InitCenterImageSizeToFit();
            imageSize = oldSize;
            imageSize = getZoomedSize();
            imageLocation.X += (oldSize.Width - imageSize.Width) / 2;
            imageLocation.Y += (oldSize.Height - imageSize.Height) / 2;
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

        int numberOfZooms = 0;
        private void ImageViewerControl_MouseWheel(object? sender, MouseEventArgs e)
        {
            numberOfZooms++;
            var delta = e.Delta;
            var oldSize = new SKSize(imageSize.Width, imageSize.Height);

            var zoomPoint = new SKPoint(e.Location.X - imageLocation.X, e.Location.Y - imageLocation.Y);

            if (delta > 0)
            {
                zoomLevel++;
            }
            else
            {
                zoomLevel--;
            }
            imageSize = getZoomedSize();
            imageLocation.X -= zoomPoint.X * (imageSize.Width - oldSize.Width) / oldSize.Width;
            imageLocation.Y -= zoomPoint.Y * (imageSize.Width - oldSize.Width) / oldSize.Width;
            //CenterImage();


            skglControl1.Invalidate();
        }

        private float applyZoom(float value)
        {
            if (zoomLevel < 0)
            {
                for (int i = 0; i < -zoomLevel; i++) { value /= zoomStep; }
            }
            else
            {
                for (int i = 0; i < zoomLevel; i++) { value *= zoomStep; }
            }
            return value;
        }

        private SKSize getZoomedSize()
        {
            float zoomedWidth = applyZoom(image1!.Width * baseImageZoom);
            float zoomedHeight = applyZoom(image1!.Height * baseImageZoom);
            return new SKSize(zoomedWidth, zoomedHeight);
        }

        private void ImageViewerControl_MouseMove(object sender, MouseEventArgs e)
        {
            
        }

        private void ImageViewerControl_MouseDown(object sender, MouseEventArgs e)
        {
            
        }

        private void ImageViewerControl_MouseUp(object sender, MouseEventArgs e)
        {
            skglControl1_MouseUp(sender, e);
        }

        private void ImageViewerControl_KeyDown(object sender, KeyEventArgs e)
        {
            //skglControl1_KeyDown(sender, e);
        }

        private void ImageViewerControl_DoubleClick(object sender, EventArgs e)
        {

        }

        private void ImageViewerControl_Resize(object sender, EventArgs e)
        {

        }

        private void skglControl1_MouseDown(object sender, MouseEventArgs e)
        {
            skglControl1.Capture = true;
            var success = skglControl1.Focus();
            if (!success)
            {
                bool canF = CanFocus;
            }
        }

        private void skglControl1_MouseMove(object sender, MouseEventArgs e)
        {
            if (skglControl1.Capture)
            {
                if (mouseLastPosition == null)
                {
                    mouseLastPosition = e.Location;
                    return;
                }
                var mouseDelta = new SKPoint(e.X - mouseLastPosition!.Value.X, e.Y - mouseLastPosition!.Value.Y);
                var newLocation = new SKPoint(this.imageLocation.X + mouseDelta.X, this.imageLocation.Y + mouseDelta.Y);
                
                this.imageLocation = newLocation;
                KeepImageWithinBounds();

                this.mouseLastPosition = e.Location;
                skglControl1.Invalidate();
            }
        }

        
    }
}
