using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace _3d_Shape
{
    public abstract class CornerFigure : Figure
    {
        public List<Point> points = new List<Point>();

        public override void DrownFigure(ref Graphics picture, int angle)
        {
            Pen pen = new Pen(Color.Black);
            RotatingFigure(angle);

            picture.DrawPolygon(pen, points.ToArray());
        }

        public override void RotatingFigure(int angle)
        {
            if (IsRotating)
            {
                for (int i = 0; i < points.Count; i++)
                {
                    points[i] = RotatingPoint(points[i], angle);
                }
            }
            
        }

        public override bool IsPointBelongFigure(Point p)
        {
            int i1, i2, n, N;
            float S, S1, S2, S3;
            bool flag = false;
            N = points.Count;

            for (n = 0; n < N; n++)
            {
                flag = false;
                i1 = n < N - 1 ? n + 1 : 0;
                while (flag == false)
                {
                    i2 = i1 + 1;
                    if (i2 >= N)
                        i2 = 0;
                    if (i2 == (n < N - 1 ? n + 1 : 0))
                        break;
                    S = Math.Abs(points[i1].X * (points[i2].Y - points[n].Y) +
                                 points[i2].X * (points[n].Y - points[i1].Y) +
                                 points[n].X * (points[i1].Y - points[i2].Y));
                    S1 = Math.Abs(points[i1].X * (points[i2].Y - p.Y) +
                                  points[i2].X * (p.Y - points[i1].Y) +
                                  p.X * (points[i1].Y - points[i2].Y));
                    S2 = Math.Abs(points[n].X * (points[i2].Y - p.Y) +
                                  points[i2].X * (p.Y - points[n].Y) +
                                  p.X * (points[n].Y - points[i2].Y));
                    S3 = Math.Abs(points[i1].X * (points[n].Y - p.Y) +
                                  points[n].X * (p.Y - points[i1].Y) +
                                  p.X * (points[i1].Y - points[n].Y));
                    if (S == S1 + S2 + S3)
                    {
                        flag = true;
                        break;
                    }

                    i1 = i1 + 1;
                    if (i1 >= N)
                        i1 = 0;
                }
                if (!flag)
                    break;
            }
            return flag;
        }
    }
}
