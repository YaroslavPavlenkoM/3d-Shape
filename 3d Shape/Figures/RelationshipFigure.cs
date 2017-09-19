using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using _3d_Shape.ListFigures;

namespace _3d_Shape.Figures
{
    public static class RelationshipFigure
    {
        public static bool IntersectCornerFigureWithCircle(CornerFigure cornerFigure, Circle circleFigure)
        {
            double numberOne, numberTwo;
            int h;
            Point p1, p2;

            // Чи знаходиться вершина в колі
            foreach (var point in cornerFigure.points)
            {
                if (circleFigure.IsPointBelongFigure(point))
                    return true;
            }

            // Чи перетинає пряма коло по двом точкам
            for (int i = 0; i < cornerFigure.points.Count - 1; i++)
            {
                p1 = cornerFigure.points[i];  
                p2 = cornerFigure.points[i + 1]; 

                if (CommonSectionCircle(p1, p2, circleFigure))
                    return true;
            }

            p1 = cornerFigure.points[cornerFigure.points.Count - 1];  // B
            p2 = cornerFigure.points[0];  // C

            if (CommonSectionCircle(p1, p2, circleFigure))
                return true;

            return false;
        }

        private static bool CommonSectionCircle(Point p1, Point p2, Circle circle)
        {
            p1.X -= circle.CenterX;
            p1.Y -= circle.CenterY;
            p2.X -= circle.CenterX;
            p2.Y -= circle.CenterY;

            double dx = p2.X - p1.X;
            double dy = p2.Y - p1.Y;

            double a = dx * dx + dy * dy;
            double b = 2 * (p1.X * dx + p1.Y * dy);
            double c = p1.X * p1.X + p1.Y * p1.Y - circle.Radius * circle.Radius;

            if (-b < 0)
                return (c < 0);
            if (-b < (2 * a))
                return ((4 * a * c - b * b) < 0);

            return (a + b + c < 0);
        }

        public static bool IntersectCornerFigureWithCornerFigure(CornerFigure cornerFigureOne, CornerFigure cornerFigureTwo)
        {
            for (int i = 0; i < cornerFigureOne.points.Count - 1; i++)
            {
                for (int j = 0; j < cornerFigureTwo.points.Count - 1; j++)
                {
                    if (IntersectLine(cornerFigureOne.points[i], cornerFigureOne.points[i + 1],
                                      cornerFigureTwo.points[j], cornerFigureTwo.points[j + 1]))
                        return true;
                }
            }

            for (int i = 0; i < cornerFigureOne.points.Count - 1; i++)
            {
                if (IntersectLine(cornerFigureOne.points[i], cornerFigureOne.points[i + 1],
                                  cornerFigureTwo.points[cornerFigureTwo.points.Count - 1], cornerFigureTwo.points[0]))
                    return true;
            }

            for (int j = 0; j < cornerFigureTwo.points.Count - 1; j++)
            {
                if (IntersectLine(cornerFigureOne.points[cornerFigureOne.points.Count - 1],
                                  cornerFigureOne.points[0], cornerFigureTwo.points[j], cornerFigureTwo.points[j + 1]))
                    return true;
            }

            if (IntersectLine(cornerFigureOne.points[cornerFigureOne.points.Count - 1], cornerFigureOne.points[0],
                              cornerFigureTwo.points[cornerFigureTwo.points.Count - 1], cornerFigureTwo.points[0]))
                return true;

            return false;
        }

        public static bool IntersectCircleWithCircle(Circle circleFigureOne, Circle circleFigureTwo)
        {
            if (circleFigureTwo.Radius + circleFigureOne.Radius >= Math.Sqrt(
                Math.Pow(circleFigureTwo.CenterX - circleFigureOne.CenterX, 2) +
                Math.Pow(circleFigureTwo.CenterY - circleFigureOne.CenterY, 2)))
                return true;

            return false;
        }

        private static bool IntersectCornerFigure(List<Figure> figures, CornerFigure investigateFigure)
        {
            foreach (var figure in figures)
            {
                if (figure.Equals(investigateFigure))
                    continue;

                if (figure is CornerFigure)
                {
                    CornerFigure currentFigure = (CornerFigure)figure;

                    if (IntersectCornerFigureWithCornerFigure(investigateFigure, currentFigure))
                        return true;
                }

                if (figure is Circle)
                {
                    Circle currentFigure = (Circle)figure;
                    if (IntersectCornerFigureWithCircle(investigateFigure, currentFigure))
                        return true;
                }
            }

            return false;
        }

        private static bool IntersectCircleFigure(List<Figure> figures, Circle investigateFigure)
        {
            foreach (var figure in figures)
            {
                if (figure.Equals(investigateFigure))
                    continue;

                if (figure is CornerFigure)
                {
                    CornerFigure currentFigure = (CornerFigure)figure;

                    if (IntersectCornerFigureWithCircle(currentFigure, investigateFigure))
                        return true;
                }

                if (figure is Circle)
                {
                    Circle currentFigure = (Circle)figure;

                    if (IntersectCircleWithCircle(currentFigure, investigateFigure))
                        return true;
                }
            }

            return false;
        }

        public static bool IntersectFigures(List<Figure> figures, Figure investigateFigure)
        {
            if (figures.Count == 0)
                return false;


            if (investigateFigure is CornerFigure)
            {
                return IntersectCornerFigure(figures, (CornerFigure)investigateFigure);
            }

            if (investigateFigure is Circle)
            {
                return IntersectCircleFigure(figures, (Circle)investigateFigure);
            }

            else
            {
                throw new Exception("Даного типа нет в библиотеке");
            }
        }

        public static bool IntersectLine(PointF start1, PointF end1, PointF start2, PointF end2)
        {
            PointF dir1 = new PointF { X = 0, Y = 0 }; 
            PointF dir2 = new PointF { X = 0, Y = 0 };

            dir1.X = end1.X - start1.X;
            dir1.Y = end1.Y - start1.Y;
            dir2.X = end2.X - start2.X;
            dir2.Y = end2.Y - start2.Y;

            //считаем уравнения прямых проходящих через отрезки
            float a1 = -dir1.Y;
            float b1 = +dir1.X;
            float d1 = -(a1 * start1.X + b1 * start1.Y);

            float a2 = -dir2.Y;
            float b2 = +dir2.X;
            float d2 = -(a2 * start2.X + b2 * start2.Y);

            //подставляем концы отрезков, для выяснения в каких полуплоскотях они
            float seg1_line2_start = a2 * start1.X + b2 * start1.Y + d2;
            float seg1_line2_end = a2 * end1.X + b2 * end1.Y + d2;

            float seg2_line1_start = a1 * start2.X + b1 * start2.Y + d1;
            float seg2_line1_end = a1 * end2.X + b1 * end2.Y + d1;

            //если концы одного отрезка имеют один знак, значит он в одной полуплоскости и пересечения нет.
            if (seg1_line2_start * seg1_line2_end >= 0 || seg2_line1_start * seg2_line1_end >= 0)
                return false;

            return true;
        }
    }
}
