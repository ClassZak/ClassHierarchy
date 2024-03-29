﻿using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
namespace Shapes
{
    public interface IShape : IEquatable<object>, IEqualityComparer<object>
    {
        string ToString();
    }

    #region Class Color
    public class Color
    {
        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
        public UInt32 ColorCode { get; set; }
        public byte A
        {
            get
            {
                return (byte)((ColorCode >> 24) & 0xFF);
            }
            set
            {
                ColorCode = (UInt32)(value << 24) + ((ColorCode << 8) >> 8);
            }
        }
        public byte R
        {
            get
            {
                return (byte)((ColorCode >> 16) & 0xFF);
            }
            set
            {
                ColorCode = (UInt32)(value << 16) + ((ColorCode >> 24) << 24) + ((ColorCode % 0x10000));
            }
        }
        public byte G
        {
            get
            {
                return (byte)((ColorCode >> 8) & 0xFF);
            }
            set
            {
                ColorCode = (UInt32)(value << 8) + ((ColorCode >> 16) << 16) + ((ColorCode % 0x100));
            }
        }
        public byte B
        {
            get
            {
                return (byte)(ColorCode & 0xFF);
            }
            set
            {
                ColorCode = (UInt32)(value) + ((ColorCode >> 8) << 8);
            }
        }
        public override string ToString()
        {
            return $"Color\tA:{A}\tR:{R}\tG:{G}\tB:{B}";
        }


        public Color() { ColorCode = 0; }
        public Color(UInt32 colorCode) { ColorCode = colorCode; }
        public Color(byte R, byte G, byte B)
        {
            ColorCode = 0xFF000000 + (UInt32)(R << 16) + (UInt32)(G << 8) + B;
        }
        public Color(byte A, byte R, byte G, byte B) { ColorCode = (UInt32)A << 24 + (R << 16) + (G << 8) + B; }



        public override bool Equals(object obj)
        {
            object compareObj = obj as Color;
            if (compareObj == null)
                throw new ArgumentException("Wrong type!");
            else
                return this.ColorCode == ((Color)(compareObj)).ColorCode;
        }
    }
    #endregion
    #region Point Classes
    public class Point : IShape
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Point() { this.X = 0; this.Y = 0; }
        public Point(double x, double y) { this.X = x; this.Y = y; }
        public Point(Point point) { this.X = point.X; this.Y = point.Y; }
        public override bool Equals(object point)
        {
            Point compareObj = point as Point;
            if (compareObj == null)
                throw new ArgumentException("Wrong type!");
            else
                return this.X == (compareObj).X && this.Y == (compareObj).Y;
        }

        #region Operators
        public static bool operator ==(Point a, Point b)
        {
            if (ReferenceEquals(a, b)) return true;
            if (b is null) return false;
            return a.Equals(b);
        }
        public static bool operator !=(Point a, Point b)
        {
            if (b is null) throw new ArgumentException("Wrong type!");
            if (ReferenceEquals(a, b)) return false;
            return !a.Equals(b);
        }





        public static Point operator +(Point a, Point b)
        {
            return new Point(a.X + b.X, a.Y + b.Y);
        }
        public static Point operator -(Point a, Point b)
        {
            return new Point(a.X - b.X, a.Y - b.Y);
        }
        public static Point operator *(Point a, double k)
        {
            return new Point(a.X * k, a.Y * k);
        }
        public static Point operator /(Point a, double k)
        {
            return new Point(a.X / k, a.Y / k);
        }


        public static Point operator -(Point a)
        {
            return new Point(-a.X, -a.Y);
        }
        #endregion

        public override string ToString()
        {
            return $"Point\tX:{X}\tY:{Y}";
        }
        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }

        bool IEqualityComparer<object>.Equals(object x, object y)
        {
            if (x is null) throw new ArgumentException("Wrong type!");
            if (ReferenceEquals(x, y)) return false;
            return !x.Equals(y);
        }
        int IEqualityComparer<object>.GetHashCode(object obj)
        {
            throw new NotImplementedException();
        }
    }
    public class ColorPoint : Point
    {
        public Color Color { get; set; }
        public ColorPoint()
        {
            this.X = this.Y = 0;
            InitilizeColor();
        }
        public ColorPoint(double x, double y)
        {
            this.X = x;
            this.Y = y;
            InitilizeColor();
        }
        public ColorPoint(Point point, Color color)
        {
            this.X = point.X;
            this.Y = point.Y;
            this.Color = color;
        }
        public ColorPoint(double x, double y, Color color)
        {
            this.X = x;
            this.Y = y;
            this.Color = color;
        }
        public ColorPoint(Point point)
        {
            this.X = point.X;
            this.Y = point.Y;
            InitilizeColor();
        }
        public ColorPoint(ColorPoint colorPoint)
        {
            this.X = colorPoint.X;
            this.Y = colorPoint.Y;
            this.Color = colorPoint.Color;
        }

        #region Operators
        public static bool operator ==(ColorPoint a, ColorPoint b)
        {
            if (ReferenceEquals(a, b)) return true;
            if (b is null) return false;
            return a.Equals(b);
        }
        public static bool operator !=(ColorPoint a, ColorPoint b)
        {
            if (b is null) throw new ArgumentException("Wrong type!");
            if (ReferenceEquals(a, b)) return false;
            return a.Equals(b);
        }
        public static bool operator ==(ColorPoint a, Point b)
        {
            if (ReferenceEquals(a, b)) return true;
            if (b is null) return false;
            return a.Equals(b);
        }
        public static bool operator !=(ColorPoint a, Point b)
        {
            if (b is null) throw new ArgumentException("Wrong type!");
            if (ReferenceEquals(a, b)) return false;
            return a.Equals(b);
        }



        public static ColorPoint operator -(ColorPoint a)
        {
            return new ColorPoint(-a.X, -a.Y, a.Color);
        }




        public static ColorPoint operator +(ColorPoint a, Point b)
        {
            return new ColorPoint(a.X + b.X, a.Y + b.Y, a.Color);
        }
        public static ColorPoint operator -(ColorPoint a, Point b)
        {
            return new ColorPoint(a.X - b.X, a.Y - b.Y, a.Color);
        }
        public static ColorPoint operator *(ColorPoint a, double k)
        {
            return new ColorPoint(a.X * k, a.Y * k, a.Color);
        }
        public static ColorPoint operator /(ColorPoint a, double k)
        {
            return new ColorPoint(a.X / k, a.Y / k, a.Color);
        }




        public static ColorPoint operator +(ColorPoint a, ColorPoint b)
        {
            return new ColorPoint(a.X + b.X, a.Y + b.Y, a.Color);
        }
        public static ColorPoint operator -(ColorPoint a, ColorPoint b)
        {
            return new ColorPoint(a.X - b.X, a.Y - b.Y, a.Color);
        }
        #endregion

        public override string ToString()
        {
            return $"ColorPoint\tX:{X}\tY:{Y}\tColor:{Color}";
        }
        public override bool Equals(object point)
        {
            ColorPoint compareObj = point as ColorPoint;
            if (compareObj == null)
            {
                if ((compareObj = new ColorPoint(point as Point)) == null)
                    throw new ArgumentException("Wrong Type!");
                else
                    return this.X == compareObj.X && this.Y == compareObj.Y;
            }
            else
                return this.X == compareObj.X && this.Y == compareObj.Y && this.Color == compareObj.Color;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }



        protected void InitilizeColor()
        {
            this.Color = new Color();
        }
    }
    #endregion
    #region Line Classes
    public class Line : IShape
    {
        public Point Point1 { get; set; }
        public Point Point2 { get; set; }

        public Line Vector
        {
            get
            {
                return new Line(0, 0, Point2.X - Point1.X, Point2.Y - Point1.Y);
            }
        }

        #region Constructors
        public Line()
        {
            InitPoints();
        }
        public Line(double X1, double Y1, double X2, double Y2)
        {
            InitPoints();
            this.Point1.X = X1; this.Point1.Y = Y1;
            this.Point2.X = X2; this.Point2.Y = Y2;
        }
        public Line(Line line)
        {
            InitPoints();
            this.Point1 = new Point(line.Point1);
            this.Point2 = new Point(line.Point2);
        }
        public Line(Point point1, Point point2)
        {
            InitPoints();
            this.Point1 = point1;
            this.Point2 = point2;
        }
        protected void InitPoints()
        {
            this.Point1 = new Point();
            this.Point2 = new Point();
        }
        #endregion
        #region Base Methods
        public override string ToString()
        {
            return "Line\n" + Point1.ToString() + "\n" + Point2.ToString();
        }
        public override bool Equals(object o)
        {
            Line compareObj = o as Line;
            if (compareObj is null)
                throw new ArgumentException("Wrong type!");
            else
                return this.Point1 == compareObj.Point1 && this.Point1 == compareObj.Point1;
        }

        bool IEqualityComparer<object>.Equals(object x, object y)
        {
            if (x is null) throw new ArgumentException("Wrong type!");
            if (ReferenceEquals(x, y)) return false;
            return !x.Equals(y);
        }
        int IEqualityComparer<object>.GetHashCode(object obj)
        {
            throw new NotImplementedException();
        }
        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
        #endregion
        #region Operators
        public static bool operator ==(Line a, Line b)
        {
            if (ReferenceEquals(a, b)) return true;
            if (b is null) return false;
            return a.Equals(b);
        }
        public static bool operator !=(Line a, Line b)
        {
            if (b is null) throw new ArgumentException("Wrong type!");
            if (ReferenceEquals(a, b)) return false;
            return !a.Equals(b);
        }


        static public Line operator +(Line line1, Line line2)
        {
            return
                new Line
                (
                    new Point(line1.Point1.X + line2.Point1.X, line1.Point1.Y + line2.Point1.Y),
                    new Point(line1.Point2.X + line2.Point2.X, line1.Point2.Y + line2.Point2.Y)
                );
        }
        static public Line operator -(Line line1, Line line2)
        {
            return
                new Line
                (
                    new Point(line1.Point1.X - line2.Point1.X, line1.Point1.Y - line2.Point1.Y),
                    new Point(line1.Point2.X - line2.Point2.X, line1.Point2.Y - line2.Point2.Y)
                );
        }
        public static Line operator -(Line a)
        {
            return new Line(-a.Point1, -a.Point2);
        }
        public static Line operator *(Line a, double k)
        {
            return new Line(a.Point1 * k, a.Point2 * k);
        }
        public static Line operator /(Line a, double k)
        {
            return new Line(a.Point1 / k, a.Point2 / k);
        }
        #endregion
    }

    public class ColorLine : Line
    {
        public Color Color { get; set; }
        #region Constructors
        public ColorLine()
        {
            base.InitPoints();
            Color = new Color();
        }
        public ColorLine(Color color)
        {
            base.InitPoints();
            Color = color;
        }
        public ColorLine(Line line)
        {
            this.Point1 = line.Point1;
            this.Point2 = line.Point2;
            Color = new Color();
        }
        public ColorLine(Line line, Color color)
        {
            this.Point1 = line.Point1;
            this.Point2 = line.Point2;
            Color = color;
        }
        public ColorLine(Point point1, Point point2)
        {
            this.Point1 = point1;
            this.Point2 = point2;
            Color = new Color();
        }
        public ColorLine(Point point1, Point point2, Color color)
        {
            this.Point1 = point1;
            this.Point2 = point2;
            Color = color;
        }
        public ColorLine(double X1, double Y1, double X2, double Y2)
        {
            Color = new Color();
            this.Point1 = new Point(X1, Y1);
            this.Point2 = new Point(X2, Y2);
        }
        public ColorLine(double X1, double Y1, double X2, double Y2, Color color)
        {
            this.Point1 = new Point(X1, Y1);
            this.Point2 = new Point(X2, Y2);
            Color = color;
        }
        public ColorLine(ColorLine colorLine)
        {
            this.Point1 = colorLine.Point1;
            this.Point2 = colorLine.Point2;
            this.Color = colorLine.Color;
        }
        #endregion
        #region Base Methods
        public override string ToString()
        {
            return "ColorLine\n" + Point1.ToString() + "\n" + Point2.ToString() + "\n" + Color.ToString();
        }
        public override bool Equals(object o)
        {
            object compareObj = o as ColorLine;
            if (compareObj is null)
            {
                compareObj = o as Line;
                if (compareObj == null)
                    throw new ArgumentException("Wrong type!");
                else
                    return this.Point1 == ((Line)(compareObj)).Point1 && this.Point1 == ((Line)(compareObj)).Point1;
            }
            else
                return this.Point1 == ((ColorLine)(compareObj)).Point1 && this.Point1 == ((ColorLine)(compareObj)).Point1 && this.Color == ((ColorLine)(compareObj)).Color;
        }
        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
        #endregion
        #region Operators
        static public bool operator ==(ColorLine a, ColorLine b)
        {
            if (ReferenceEquals(a, b)) return true;
            if (b is null) return false;
            return a.Equals(b);
        }
        static public bool operator !=(ColorLine a, ColorLine b)
        {
            if (b is null) throw new ArgumentException("Wrong type!");
            if (ReferenceEquals(a, b)) return false;
            return !a.Equals(b);
        }
        static public bool operator ==(ColorLine a, Line b)
        {
            if (ReferenceEquals(a, b)) return true;
            if (b is null) return false;
            return a.Equals(b);
        }
        static public bool operator !=(ColorLine a, Line b)
        {
            if (b is null) throw new ArgumentException("Wrong type!");
            if (ReferenceEquals(a, b)) return false;
            return !a.Equals(b);
        }





        static public ColorLine operator -(ColorLine a, ColorLine b)
        {
            return
                new ColorLine
                (
                    a.Point1 - b.Point1,
                    a.Point2 - b.Point2, a.Color
                );
        }
        static public ColorLine operator +(ColorLine a, ColorLine b)
        {
            return
                new ColorLine
                (
                    a.Point1 + b.Point1,
                    a.Point2 + b.Point2, a.Color
                );
        }


        static public ColorLine operator -(ColorLine a, Line b)
        {
            return
                new ColorLine
                (
                    a.Point1 - b.Point1,
                    a.Point2 - b.Point2, a.Color
                );
        }
        static public ColorLine operator +(ColorLine a, Line b)
        {
            return
                new ColorLine
                (
                    a.Point1 + b.Point1,
                    a.Point2 + b.Point2, a.Color
                );
        }
        static public ColorLine operator -(ColorLine colorLine)
        {
            return new ColorLine(-colorLine.Point1, -colorLine.Point2, colorLine.Color);
        }
        static public ColorLine operator *(ColorLine colorLine, double k)
        {
            return new ColorLine(colorLine.Point1 * k, colorLine.Point2 * k, colorLine.Color);
        }
        static public ColorLine operator /(ColorLine colorLine, double k)
        {
            return new ColorLine(colorLine.Point1 / k, colorLine.Point2 / k, colorLine.Color);
        }
        #endregion
    }
    #endregion
    public class Polygon : IShape
    {
        public List<Point> Points;
        #region Constructors
        public Polygon()
        {
            Points = new List<Point>();
        }
        public Polygon(params object[] point)
        {
            Points = new List<Point>();
            foreach (object e in point)
            {
                if (e.GetType() == typeof(Point))
                    Points.Add(new Point((Point)e));
                else if (e is ColorPoint point1)
                    Points.Add(new Point(point1.X, point1.Y));
                else if (e is Line line1)
                {
                    Points.Add(new Point(line1.Point1));
                    Points.Add(new Point(line1.Point2));
                }
                else if (e is ColorLine line)
                {
                    Points.Add(new Point(line.Point1));
                    Points.Add(new Point(line.Point2));
                }
            }

        }
        public Polygon(Polygon polygon)
        {
            Points = new List<Point>();
            foreach (Point p in polygon.Points)
                this.Points.Add(p);
        }
        #endregion
        #region Base methods
        public override string ToString()
        {
            string result = "Polygon:\n";
            for (int i = 0; i != this.Points.Count; ++i)
                result += Points.ElementAt<Point>(i).ToString() + "\n";
            return result;
        }
        bool IEqualityComparer<object>.Equals(object x, object y)
        {
            if (x is null) throw new ArgumentException("Wrong type!");
            if (ReferenceEquals(x, y)) return false;
            return !x.Equals(y);
        }
        int IEqualityComparer<object>.GetHashCode(object obj)
        {
            throw new NotImplementedException();
        }
        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
        public override bool Equals(object obj)
        {
            if (!(obj is Polygon compareObj))
                throw new ArgumentException("Wrong type!");
            else
            {
                if (this.Points.Count != compareObj.Points.Count)
                    return false;

                bool equal = true;
                for (int i = 0, j = 0; i != this.Points.Count; ++i, ++j)
                {
                    if (this.Points.ElementAt<Point>(i) != (compareObj.Points.ElementAt<Point>(j)))
                    {
                        equal = false;
                        break;
                    }
                }
                return equal;
            }
        }

        #endregion
        #region Operators
        public static bool operator ==(Polygon a, Polygon b)
        {
            if (ReferenceEquals(a, b)) return true;
            if (b is null) return false;
            return a.Equals(b);
        }
        public static bool operator !=(Polygon a, Polygon b)
        {
            if (b is null) throw new ArgumentException("Wrong type!");
            if (ReferenceEquals(a, b)) return false;
            return !a.Equals(b);
        }
        #endregion
        public Polygon MoveOffX(double x)
        {
            foreach (var point in this.Points)
                point.X += x;
            return this;
        }
        public Polygon MoveOffY(double y)
        {
            foreach (var point in this.Points)
                point.Y += y;
            return this;
        }
        public Polygon MoveOffXY(double x, double y)
        {
            foreach (var point in this.Points)
            {
                point.X += x;
                point.Y += y;
            }
            return this;
        }
    }
}
