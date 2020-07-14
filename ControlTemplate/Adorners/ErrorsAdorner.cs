using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Media;

namespace ControlTemplate.Adroners
{
    public sealed class ErrorsAdorner : Adorner
    {
        private static Pen BorderPen { get; }

        private static Brush ErrorBrush { get; }

        private static Brush TextBrush { get; }

        static ErrorsAdorner()
        {
            ErrorBrush = Brushes.Red;
            TextBrush = Brushes.White;
            BorderPen = new Pen(ErrorBrush, 1d);
            BorderPen.Freeze();
        }

        public ErrorsAdorner(UIElement adornedElement) : base(adornedElement)
        {
            Popup_Error = new Popup() { AllowsTransparency = true, IsOpen = true, Placement = PlacementMode.Right };
            TextBlock_Error = new TextBlock() { Padding = new Thickness(3), Background = ErrorBrush, Foreground = TextBrush };
            Popup_Error.Child = TextBlock_Error;
            //定义两个视觉对象之间的父子关系(Adorner层与Popup之间父子关系)
            AddVisualChild(Popup_Error);
            Loaded += Local_Loaded;
        }

        public void SetErrors(IEnumerable errors)
        {
            foreach (var error in errors)
            {
                TextBlock_Error.Text = error.ToString();
                break;
            }
        }

        private Popup Popup_Error { get; }

        private TextBlock TextBlock_Error { get; }


        protected override Visual GetVisualChild(int index)
        {
            return Popup_Error;
        }

        protected override int VisualChildrenCount => 1;

        private void Local_Loaded(object sender, EventArgs e)
        {
            TextBlock_Error.MouseEnter -= TextBlock_Error_MouseEnter;
            TextBlock_Error.MouseEnter += TextBlock_Error_MouseEnter;
            TextBlock_Error.MouseLeave -= TextBlock_Error_MouseLeave;
            TextBlock_Error.MouseLeave += TextBlock_Error_MouseLeave;
            Unloaded -= Local_Unloaded;
            Unloaded += Local_Unloaded;
        }

        private void Local_Unloaded(object sender, EventArgs e)
        {
            TextBlock_Error.MouseEnter -= TextBlock_Error_MouseEnter;
            TextBlock_Error.MouseLeave -= TextBlock_Error_MouseLeave;
            Unloaded -= Local_Unloaded;
        }

        private void TextBlock_Error_MouseEnter(object sender, EventArgs e)
        {
            TextBlock_Error.Opacity = 0.2;
        }

        private void TextBlock_Error_MouseLeave(object sender, EventArgs e)
        {
            TextBlock_Error.Opacity = 1;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            Popup_Error.PlacementRectangle = new Rect(new Point(-3, 0), new Size(finalSize.Width + 6, finalSize.Height));
            return base.ArrangeOverride(finalSize);
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            var rect = new Rect(AdornedElement.RenderSize);
            //装饰TextBox外框对象
            drawingContext.DrawRectangle(null, BorderPen, rect);
            var errorMark = new StreamGeometry();
            using (var context = errorMark.Open())
            {
                //装饰Popup弹窗矩形框
                context.BeginFigure(new Point(rect.Right - 5, 0), true, true);
                context.LineTo(rect.TopRight, false, false);
                context.LineTo(new Point(rect.Right, 5), false, false);
            }
            errorMark.Freeze();
            drawingContext.DrawGeometry(ErrorBrush, null, errorMark);
        }
    }
}

