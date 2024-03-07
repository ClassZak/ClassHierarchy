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
        public abstract bool Equals(ShapeI other);
    }


    class Point : ShapeI
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Point() { this.X = 0; this.Y = 0; }
        public Point(double x, double y) { this.X = x; this.Y = y; }
        public override bool  Equals(ShapeI point)
        {
            Point compareObj= point as Point;
            if (compareObj != null)
                return this.X == (compareObj).X && this.Y == (compareObj).Y;
            else
                throw new ArgumentException("Wrong Type!");
        }


        public override string ToString()
        {
            return $"Point\nX:{X}\tY:{Y}\n";
        }
        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }


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
                ColorCode |= (byte)((ColorCode << 24));
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
                ColorCode |= (byte)((ColorCode << 16));
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
                ColorCode |= (byte)((ColorCode << 8));
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
                ColorCode |= (byte)((ColorCode));
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
    class ColorPoint : Point
    {

        public ColorPoint()
        {

        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Color color = new Color(90,150,200);
            Console.WriteLine(color.ToString());



            Console.ReadKey(false);
        }
    }
}
