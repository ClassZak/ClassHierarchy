using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassHierarchy
{
    abstract class ShapeI
    {
        new public abstract string ToString();
        new public abstract int GetHashCode();
        new public abstract bool Equals(object o);
    }


    class Point : ShapeI
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Point() { this.X = 0; this.Y = 0; }
        public Point(double x, double y) { this.X = x; this.Y = y; }
        public Point(Point point) { this.X = point.X; this.Y=point.Y; }
        public override bool  Equals(object point)
        {
            Point compareObj= point as Point;
            if (compareObj == null)
                throw new ArgumentException("Wrong Type!");
            else
                return this.X == (compareObj).X && this.Y == (compareObj).Y;
        }

        #region Operators
        public static bool operator==(Point a, Point b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(Point a, Point b)
        {
            return a.Equals(b);
        }





        public static Point operator+(Point a, Point b)
        {
            return new Point(a.X + b.X, a.Y + b.Y);
        }
        public static Point operator-(Point a, Point b)
        {
            return new Point(a.X - b.X, a.Y - b.Y);
        }
        public static Point operator*(Point a, double k)
        {
            return new Point(a.X * k, a.Y * k);
        }
        public static Point operator /(Point a, double k)
        {
            return new Point(a.X / k, a.Y / k);
        }


        public static Point operator-(Point a)
        {
            return new Point(-a.X, -a.Y);
        }
        #endregion

        public override string ToString()
        {
            return $"Point\nX:{X}\tY:{Y}";
        }
        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
    class ColorPoint : Point
    {
        public Color color { get; set; }
        public ColorPoint()
        {
            this.X = this.Y=0;
            InitilizeColor();
        }
        public ColorPoint(double x, double y)
        {
            this.X = X;
            this.Y = Y;
            InitilizeColor();
        }
        public ColorPoint(double x,double y,Color color)
        {
            this.X = x;
            this.Y = y;
            this.color = color;
        }
        public ColorPoint(Point point)
        {
            this.X= point.X;
            this.Y= point.Y;
            InitilizeColor();
        }
        public ColorPoint(ColorPoint colorPoint)
        {
            this.X= colorPoint.X;
            this.Y= colorPoint.Y;
            this.color = colorPoint.color;
        }

        #region Operators
        public static bool operator ==(ColorPoint a, ColorPoint b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(ColorPoint a, ColorPoint b)
        {
            return a.Equals(b);
        }
        public static bool operator ==(ColorPoint a, Point b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(ColorPoint a, Point b)
        {
            return a.Equals(b);
        }



        public static ColorPoint operator -(ColorPoint a)
        {
            return new ColorPoint(-a.X, -a.Y,a.color);
        }




        public static ColorPoint operator +(ColorPoint a, Point b)
        {
            return new ColorPoint(a.X + b.X, a.Y + b.Y,a.color);
        }
        public static ColorPoint operator -(ColorPoint a, Point b)
        {
            return new ColorPoint(a.X - b.X, a.Y - b.Y, a.color);
        }
        public static ColorPoint operator *(ColorPoint a, double k)
        {
            return new ColorPoint(a.X * k, a.Y * k, a.color);
        }
        public static ColorPoint operator /(ColorPoint a, double k)
        {
            return new ColorPoint(a.X / k, a.Y / k, a.color);
        }




        public static ColorPoint operator +(ColorPoint a, ColorPoint b)
        {
            return new ColorPoint(a.X + b.X, a.Y + b.Y, a.color);
        }
        public static ColorPoint operator -(ColorPoint a, ColorPoint b)
        {
            return new ColorPoint(a.X - b.X, a.Y - b.Y, a.color);
        }
        #endregion

        public override string ToString()
        {
            return $"Point\nX:{X}\tY:{Y}\tColor:{color}";
        }
        public override bool Equals(object point)
        {
            ColorPoint compareObj = point as ColorPoint;
            if(compareObj==null)
            {
                if((compareObj = (ColorPoint)(point as Point))==null)
                    throw new ArgumentException("Wrong Type!");
                else
                    return this.X==compareObj.X && this.Y==compareObj.Y;
            }
            else
                return this.X==compareObj.X && this.Y==compareObj.Y && this.color==compareObj.color;
        }



        protected void InitilizeColor()
        {
            this.color = new Color();
        }
    }


    #region Class Color
    class Color
    {
        UInt32 ColorCode;
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
                ColorCode = (UInt32)(value << 8) + ((ColorCode >> 16) <<16) + ((ColorCode % 0x100));
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
        public override string  ToString()
        {
            return $"Color\nA:{A}\tR:{R}\tG:{G}\tB:{B}";
        }


        public Color() { ColorCode = 0; }
        public Color(UInt32 colorCode) { ColorCode = colorCode; }
        public Color(byte R, byte G, byte B)
        {
            ColorCode = 0xFF000000 +  (UInt32)(R << 16) + (UInt32)(G << 8) + B;
        }
        public Color(byte A, byte R, byte G, byte B) { ColorCode = (UInt32)A << 24 + (R << 16) + (G << 8) + B; }
    }
    #endregion


    internal class Program
    {
        static void Main(string[] args)
        {
            
            Color color = new Color(90,150,200);
            Console.WriteLine(color.ToString());
            color.A = 15;
            Console.WriteLine(color.ToString());
            color.R = 43;
            Console.WriteLine(color.ToString());
            color.G = 112;
            Console.WriteLine(color.ToString());
            color.B = 225;
            Console.WriteLine(color.ToString());



            Console.ReadKey(false);
        }
    }
}
