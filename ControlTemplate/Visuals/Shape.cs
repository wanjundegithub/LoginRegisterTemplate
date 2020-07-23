using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace ControlTemplate.Visuals
{
    public class Shape
    {
        public DrawingVisual DrawRectangleVisual()
        {
            DrawingVisual visual = new DrawingVisual();
            var drawingContent = visual.RenderOpen();
            Rect rect = new Rect(new Point(50,50), new Size(100,100));
            drawingContent.DrawRectangle(Brushes.Red, new Pen(Brushes.Blue, 1), rect);
            drawingContent.Close();
            return visual;
        }
    }
}
