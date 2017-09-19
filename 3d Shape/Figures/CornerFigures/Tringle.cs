using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace _3d_Shape.ListFigures
{
    public class Tringle : CornerFigure
    {
        protected Tringle()
        { 
            
        }

        public Tringle(int X, int Y, Point point1, Point point2, Point point3)
        {
            center.X = X;
            center.Y = Y;
            points.Add(point1);
            points.Add(point2);
            points.Add(point3);
        }
    }
}
