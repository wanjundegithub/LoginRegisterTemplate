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
            BrushConverter bc = new BrushConverter();
            Brush adornerBrush = (Brush)bc.ConvertFrom("#FF00A0FF");
            Pen pen = new Pen(adornerBrush, 2);
            drawingContext.DrawRectangle(null,pen,rect);
        }
    }
}
