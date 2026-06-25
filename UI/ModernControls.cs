using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace GISUniversalConverterPro.UI
{
    internal sealed class StaticGradientTableLayoutPanel : TableLayoutPanel
    {
        public StaticGradientTableLayoutPanel()
        {
            DoubleBuffered = true;
            ResizeRedraw = true;
        }

        public Color TopColor { get; set; } = ColorTranslator.FromHtml("#E0F7FA");
        public Color BottomColor { get; set; } = ColorTranslator.FromHtml("#B2EBF2");

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            using var brush = new LinearGradientBrush(ClientRectangle, TopColor, BottomColor, LinearGradientMode.Vertical);
            e.Graphics.FillRectangle(brush, ClientRectangle);
        }
    }

    internal class RoundedPanel : Panel
    {
        public RoundedPanel()
        {
            DoubleBuffered = true;
            ResizeRedraw = true;
            BackColor = Color.White;
        }

        public int CornerRadius { get; set; } = 24;
        public Color BorderColor { get; set; } = Color.FromArgb(220, 238, 246);
        public int BorderThickness { get; set; } = 1;

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (Width < 2 || Height < 2)
            {
                return;
            }

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            using var path = CreateRoundRectanglePath(new Rectangle(0, 0, Width - 1, Height - 1), CornerRadius);
            using var brush = new SolidBrush(BackColor);
            e.Graphics.FillPath(brush, path);
            using var pen = new Pen(BorderColor, BorderThickness);
            e.Graphics.DrawPath(pen, path);
        }

        protected override void OnResize(EventArgs eventargs)
        {
            base.OnResize(eventargs);
            if (Width < 2 || Height < 2)
            {
                return;
            }

            using var path = CreateRoundRectanglePath(new Rectangle(0, 0, Width, Height), CornerRadius);
            Region = new Region(path);
        }

        protected static GraphicsPath CreateRoundRectanglePath(Rectangle bounds, int radius)
        {
            radius = Math.Max(1, Math.Min(radius, Math.Min(bounds.Width, bounds.Height) / 2));
            var diameter = radius * 2;
            var path = new GraphicsPath();
            path.AddArc(bounds.Right - diameter, bounds.Top, diameter, diameter, 270, 90);
            path.AddArc(bounds.Left, bounds.Top, diameter, diameter, 180, 90);
            path.AddArc(bounds.Left, bounds.Bottom - diameter, diameter, diameter, 90, 90);
            path.AddArc(bounds.Right - diameter, bounds.Bottom - diameter, diameter, diameter, 0, 90);
            path.CloseFigure();
            return path;
        }
    }

    internal sealed class GradientRoundedPanel : RoundedPanel
    {
        public Color StartColor { get; set; } = ColorTranslator.FromHtml("#0891B2");
        public Color EndColor { get; set; } = ColorTranslator.FromHtml("#06B6D4");

        protected override void OnPaint(PaintEventArgs e)
        {
            if (Width < 2 || Height < 2)
            {
                return;
            }

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            using var path = CreateRoundRectanglePath(new Rectangle(0, 0, Width - 1, Height - 1), CornerRadius);
            using var brush = new LinearGradientBrush(ClientRectangle, StartColor, EndColor, LinearGradientMode.Horizontal);
            e.Graphics.FillPath(brush, path);
            using var pen = new Pen(BorderColor, BorderThickness);
            e.Graphics.DrawPath(pen, path);
        }
    }

    internal sealed class RoundedButton : Button
    {
        public RoundedButton()
        {
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
            Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            Cursor = Cursors.Hand;
        }

        public int CornerRadius { get; set; } = 18;
        public Color FillColor { get; set; } = Color.FromArgb(241, 250, 255);
        public Color HoverColor { get; set; } = Color.FromArgb(225, 246, 255);
        public Color PressedColor { get; set; } = Color.FromArgb(207, 239, 252);
        public Color TextColor { get; set; } = Color.FromArgb(13, 64, 83);
        private bool _hovered;
        private bool _pressed;

        protected override void OnMouseEnter(EventArgs e) { _hovered = true; Invalidate(); base.OnMouseEnter(e); }
        protected override void OnMouseLeave(EventArgs e) { _hovered = false; _pressed = false; Invalidate(); base.OnMouseLeave(e); }
        protected override void OnMouseDown(MouseEventArgs mevent) { _pressed = true; Invalidate(); base.OnMouseDown(mevent); }
        protected override void OnMouseUp(MouseEventArgs mevent) { _pressed = false; Invalidate(); base.OnMouseUp(mevent); }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            if (Width < 2 || Height < 2)
            {
                return;
            }

            pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            var fill = _pressed ? PressedColor : _hovered ? HoverColor : FillColor;
            using var path = CreateRoundRectanglePath(new Rectangle(0, 0, Width - 1, Height - 1), CornerRadius);
            using var brush = new SolidBrush(Enabled ? fill : Color.FromArgb(229, 235, 240));
            pevent.Graphics.FillPath(brush, path);
            TextRenderer.DrawText(pevent.Graphics, Text, Font, ClientRectangle, Enabled ? TextColor : Color.FromArgb(130, 146, 160), TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis | TextFormatFlags.RightToLeft);
        }
    }
}
