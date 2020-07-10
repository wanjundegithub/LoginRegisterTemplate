using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;


namespace ControlTemplate.Adorners
{
    public class BorderAdorner:Adorner
    {
        public BorderAdorner(UIElement adornedElement):base(adornedElement)
        {

        }

      

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            Rect rect = new Rect(AdornedElement.RenderSize);
            SolidColorBrush brush = new SolidColorBrush(Colors.Transparent);
            Pen pen = new Pen(Brushes.Red, 2);
            drawingContext.DrawRectangle(brush,pen,rect);
        }
    }
}
