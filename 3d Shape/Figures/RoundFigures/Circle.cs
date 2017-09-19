using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace _3d_Shape.ListFigures
{
    public class Circle : Figure
    {

        protected Circle()
        { }

        public int Radius { get; set; }

        public Circle(int X, int Y, int radius)
        {
            center.X = X;
            center.Y = Y;
            Radius = radius;
        }
        public override void DrownFigure(ref Graphics picture, int angle = 0)
        {
            Pen pen = new Pen(Color.Black);
            Rectangle rect = new Rectangle(CenterX - Radius, CenterY - Radius, Radius * 2, Radius * 2);

            picture.DrawEllipse(pen, rect);
        }

        public override void RotatingFigure(int angle)
        {
        }

        public override bool IsPointBelongFigure(Point point)
        {
            if (Math.Pow(CenterX - point.X, 2) + Math.Pow(CenterY - point.Y, 2) <= Radius * Radius)
            {
                return true;
            }
            else
            {
                return false;
            }         
        }
    }
}
