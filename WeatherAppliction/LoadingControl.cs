using System.Drawing.Drawing2D;

namespace WeatherAppliction
{
    internal static partial class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        /// 
        public class LoadingControl : UserControl
        {
            private System.Windows.Forms.Timer animationTimer;
            private float rotationAngle = 0f;
            private string loadingText = "Loading...";

            public LoadingControl()
            {
                InitializeControl();
                StartAnimation();
            }

            private void InitializeControl()
            {
                // Set control properties
                this.SetStyle(ControlStyles.AllPaintingInWmPaint |
                             ControlStyles.UserPaint |
                             ControlStyles.DoubleBuffer |
                             ControlStyles.SupportsTransparentBackColor, true);

                this.BackColor = Color.FromArgb(150, 0, 0, 0); // Semi-transparent black
                this.Dock = DockStyle.Fill; // Fill the entire parent form
                this.Visible = false; // Hidden by default
            }

            private void StartAnimation()
            {
                animationTimer = new System.Windows.Forms.Timer();
                animationTimer.Interval = 50; // 20 FPS
                animationTimer.Tick += (s, e) =>
                {
                    rotationAngle += 10f;
                    if (rotationAngle >= 360f) rotationAngle = 0f;
                    this.Invalidate();
                };
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                Graphics g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;

                // Draw spinner in center
                DrawSpinner(g);

                // Draw loading text
                DrawLoadingText(g);
            }

            private void DrawSpinner(Graphics g)
            {
                int centerX = this.Width / 2;
                int centerY = this.Height / 2;
                int radius = 20;

                g.TranslateTransform(centerX, centerY);
                g.RotateTransform(rotationAngle);

                // Draw 8 lines with decreasing opacity
                for (int i = 0; i < 8; i++)
                {
                    float angle = i * 45f;
                    int alpha = 255 - (i * 30);

                    using (Pen pen = new Pen(Color.FromArgb(alpha, Color.White), 3))
                    {
                        pen.StartCap = LineCap.Round;
                        pen.EndCap = LineCap.Round;

                        float x1 = (float)(Math.Cos(angle * Math.PI / 180) * (radius - 8));
                        float y1 = (float)(Math.Sin(angle * Math.PI / 180) * (radius - 8));
                        float x2 = (float)(Math.Cos(angle * Math.PI / 180) * radius);
                        float y2 = (float)(Math.Sin(angle * Math.PI / 180) * radius);

                        g.DrawLine(pen, x1, y1, x2, y2);
                    }
                }

                g.ResetTransform();
            }

            private void DrawLoadingText(Graphics g)
            {
                using (Font font = new Font("Segoe UI", 10, FontStyle.Regular))
                {
                    SizeF textSize = g.MeasureString(loadingText, font);
                    float x = (this.Width - textSize.Width) / 2;
                    float y = (this.Height / 2) + 40;

                    using (SolidBrush brush = new SolidBrush(Color.White))
                    {
                        g.DrawString(loadingText, font, brush, x, y);
                    }
                }
            }

            // Public methods to control the loading
            public void ShowLoading(string text = "Loading...")
            {
                loadingText = text;
                this.Visible = true;
                this.BringToFront();
                animationTimer.Start();
                Application.DoEvents(); // Force UI update
            }

            public void HideLoading()
            {
                animationTimer.Stop();
                this.Visible = false;
            }

            protected override void Dispose(bool disposing)
            {
                if (disposing)
                {
                    animationTimer?.Stop();
                    animationTimer?.Dispose();
                }
                base.Dispose(disposing);
            }
        }
    }
}