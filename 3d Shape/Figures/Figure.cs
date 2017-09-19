using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Xml.Serialization;
using _3d_Shape.ListFigures;

namespace _3d_Shape
{
    [XmlInclude(typeof(Tringle)), XmlInclude(typeof(Quadrilateral)), 
    XmlInclude(typeof(CornerFigure)), XmlInclude(typeof(Circle))]
    public abstract class Figure
    {
        private bool isRotating = false;
        protected Point center;

        protected Figure() { }

        public virtual int CenterX
        {
            get
            {
                return center.X;
            }
            set
            {
                center.X = value;
            }
        }

        public virtual int CenterY
        {
            get
            {
                return center.Y;
            }
            set
            {
                center.Y = value;
            }
        }

        public virtual bool IsRotating
        {
            set
            {
                isRotating = value;
            }
            get
            {
                return isRotating;
            }
        }

        protected virtual Point RotatingPoint(Point point, int angle)
        {
            Point changedPoint = new Point { X = 0, Y = 0 };
            double rad = angle * (Math.PI / 180);

            changedPoint.X = Convert.ToInt32(CenterX - Convert.ToSingle((CenterX - point.X) * Math.Cos(rad) - (CenterY - point.Y) * Math.Sin(rad)));
            changedPoint.Y = Convert.ToInt32(CenterY - Convert.ToSingle((CenterX - point.X) * Math.Sin(rad) + (CenterY - point.Y) * Math.Cos(rad)));

            return changedPoint;
        }

        public abstract void DrownFigure(ref Graphics picture, int angle);

        public abstract void RotatingFigure(int angle);

        public abstract bool IsPointBelongFigure(Point point);
    }
}
