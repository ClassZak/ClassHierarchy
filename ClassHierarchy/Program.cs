using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Shapes;

namespace ClassHierarchy
{

    internal class Program
    {
        static void CheckColor()
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
        }
        static void Main(string[] args)
        {
            CheckColor();
            Console.WriteLine();



            List<IShape> shapes = new List<IShape>();
            shapes.Add(new Point());
            shapes.Add(new Point(1,2));
            shapes.Add(new Point((Point)shapes.ElementAt(shapes.Count-1)));

            shapes.Add(new ColorPoint());
            shapes.Add(new ColorPoint(5,10));
            shapes.Add(new ColorPoint((Point)shapes.ElementAt(1)));
            shapes.Add(new ColorPoint(5, 10,new Color(123,45,11)));
            shapes.Add(new ColorPoint((Point)shapes.ElementAt(1),new Color(55,90,2)));
            shapes.Add(new ColorPoint((ColorPoint)shapes.ElementAt(shapes.Count - 1)));

            shapes.Add(new Line());
            shapes.Add(new Line(0, 1, 15, 47));
            shapes.Add(new Line((Line)shapes.ElementAt(shapes.Count - 1)));
            shapes.Add(new Line((Point)shapes.ElementAt(1), (Point)shapes.ElementAt(1)*2));

            shapes.Add(new ColorLine());
            shapes.Add(new ColorLine(new Color(255,255,128)));
            shapes.Add(new ColorLine(19,120,99,77));
            shapes.Add(new ColorLine((Line)shapes.ElementAt(shapes.Count - 6)));
            shapes.Add(new ColorLine((Line)shapes.ElementAt(shapes.Count - 6), new Color(70, 80, 90)));
            shapes.Add(new ColorLine((Point)shapes.ElementAt(1), (Point)shapes.ElementAt(1) * 2));
            shapes.Add(new ColorLine((Point)shapes.ElementAt(1) * 4, (Point)shapes.ElementAt(1) / 5, new Color(0, 0, 0)));
            shapes.Add(new ColorLine(19, 120, 99, 77, new Color(7, 8, 9)));
            shapes.Add(new ColorLine((ColorLine)shapes.ElementAt(shapes.Count - 1)));

            shapes.Add(new Polygon());
            shapes.Add(new Polygon((Point)shapes.ElementAt(0), (Point)shapes.ElementAt(1), (ColorLine)shapes.ElementAt(shapes.Count-3), (Line)shapes.ElementAt(10)));
            shapes.Add(new Polygon((Polygon)shapes.ElementAt(shapes.Count-1)));


            foreach (var shape in shapes)
                Console.WriteLine(shape);

            #region Points
            Console.WriteLine();
            Console.WriteLine("Point Methods");
            Console.WriteLine("Method Equals");
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    Console.WriteLine($"{i} point\tequals\t{j} point\t:{((shapes.ElementAt(i)).Equals(shapes.ElementAt(j)))}");
            Console.WriteLine();
            Console.WriteLine("Operator ==");
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    Console.WriteLine($"{i} point\tequals\t{j} point\t:{(shapes.ElementAt(i))==(shapes.ElementAt(j))}");
            Console.WriteLine();
            Console.WriteLine("Operator !=");
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    Console.WriteLine($"{i} point\tequals\t{j} point\t:{(shapes.ElementAt(i)) != (shapes.ElementAt(j))}");
            Console.WriteLine();
            Console.WriteLine("Operator - (unary)");
            for (int i = 0; i < 3; i++)
                Console.WriteLine($"{i} -point\t{-((Point)shapes.ElementAt(i))}");
            Console.WriteLine();
            Console.WriteLine("Operator - (binary)");
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    Console.WriteLine($"{i} point\t-\t{j} point\t:{((Point)shapes.ElementAt(i)) - (Point)(shapes.ElementAt(j))}");
            Console.WriteLine();
            Console.WriteLine("Operator + ");
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    Console.WriteLine($"{i} point\t+\t{j} point\t:{((Point)shapes.ElementAt(i)) + (Point)(shapes.ElementAt(j))}");
            
            {
                Random random = new Random();
                double curr;
                Console.WriteLine();
                Console.WriteLine("Operator * ");
                for (int i = 0; i < 3; i++)
                {
                    curr = Math.Round(random.NextDouble()*100)/100 + random.Next(10);
                    Console.WriteLine($"{i} point\t*\t{curr} = {((Point)shapes.ElementAt(i))* curr}");
                }
                Console.WriteLine();
                Console.WriteLine("Operator / ");
                for (int i = 0; i < 3; i++)
                {
                    curr = Math.Round(random.NextDouble() * 100) / 100 + random.Next(10);
                    try 
                    {
                        Console.WriteLine($"{i} point\t/\t{curr} = {((Point)shapes.ElementAt(i)) / curr}");
                    }
                    catch(Exception e )
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
            #endregion
            #region Color points
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Color point methods");
            Console.WriteLine("Method Equals");
            for (int i = 3; i < 9; i++)
                for (int j = 0; j < 9; j++)
                {
                    try 
                    {Console.WriteLine($"{i} point\tequals\t{j} point\t:{(shapes.ElementAt(i)).Equals(shapes.ElementAt(j))}"); }
                    catch(Exception e) { Console.WriteLine(e.Message); }
                }
                    
            Console.WriteLine();
            Console.WriteLine("Operator ==");
            for (int i = 3; i < 9; i++)
                for (int j = 0; j < 9; j++)
                {
                    try {Console.WriteLine($"{i} point\tequals\t{j} point\t:{(shapes.ElementAt(i)) == (shapes.ElementAt(j))}"); }
                    catch(Exception e) { Console.WriteLine(e.Message); }
                }
                    
            Console.WriteLine();
            Console.WriteLine("Operator !=");
            for (int i = 3; i < 9; i++)
                for (int j = 0; j < 9; j++)
                {
                    try { Console.WriteLine($"{i} point\tequals\t{j} point\t:{(shapes.ElementAt(i)) != (shapes.ElementAt(j))}"); }
                    catch(Exception e) { Console.WriteLine(e.Message); }
                }
                    
            Console.WriteLine();
            Console.WriteLine("Operator - (unary)");
            for (int i = 3; i < 9; i++)
                Console.WriteLine($"{i} -point\t{-((ColorPoint)shapes.ElementAt(i))}");
            Console.WriteLine();
            Console.WriteLine("Operator - (binary)");
            for (int i = 3; i < 9; i++)
                for (int j = 3; j < 9; j++)
                    Console.WriteLine($"{i} point\t-\t{j} point\t:{((ColorPoint)shapes.ElementAt(i)) - (ColorPoint)(shapes.ElementAt(j))}");
            Console.WriteLine();
            Console.WriteLine("Operator + ");
            for (int i = 3; i < 9; i++)
                for (int j = 3; j < 9; j++)
                    Console.WriteLine($"{i} point\t+\t{j} point\t:{((ColorPoint)shapes.ElementAt(i)) + (ColorPoint)(shapes.ElementAt(j))}");
            {
                Random random = new Random();
                double curr;
                Console.WriteLine();
                Console.WriteLine("Operator * ");
                for (int i = 3; i < 9; i++)
                {
                    curr = Math.Round(random.NextDouble() * 100) / 100 + random.Next(10);
                    Console.WriteLine($"{i} point\t*\t{curr} = {((ColorPoint)shapes.ElementAt(i)) * curr}");
                }
                Console.WriteLine();
                Console.WriteLine("Operator / ");
                for (int i = 3; i < 9; i++)
                {
                    curr = Math.Round(random.NextDouble() * 100) / 100 + random.Next(10);
                    try
                    {
                        Console.WriteLine($"{i} point\t/\t{curr} = {((ColorPoint)shapes.ElementAt(i)) / curr}");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
            #endregion
            #region Lines
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Line methods");
            Console.WriteLine("Vectors:");
            for (int i = 9; i < 13; ++i)
                Console.WriteLine(((Line)(shapes.ElementAt(i))).Vector);
            Console.WriteLine("Method Equals");
            for (int i = 9; i < 13; i++)
                for (int j = 9; j < 13; j++)
                    Console.WriteLine($"{i} line\tequals\t{j} line\t:{((shapes.ElementAt(i)).Equals(shapes.ElementAt(j)))}");
            Console.WriteLine();
            Console.WriteLine("Operator ==");
            for (int i = 9; i < 13; i++)
                for (int j = 9; j < 13; j++)
                    Console.WriteLine($"{i} line\tequals\t{j} line\t:{(shapes.ElementAt(i)) == (shapes.ElementAt(j))}");
            Console.WriteLine();
            Console.WriteLine("Operator !=");
            for (int i = 9; i < 13; i++)
                for (int j = 9; j < 13; j++)
                    Console.WriteLine($"{i} line\tequals\t{j} line\t:{(shapes.ElementAt(i)) != (shapes.ElementAt(j))}");
            Console.WriteLine();
            Console.WriteLine("Operator - (unary)");
            for (int i = 9; i < 13; i++)
                Console.WriteLine($"{i} -line\t{-((Line)shapes.ElementAt(i))}");
            Console.WriteLine();
            Console.WriteLine("Operator - (binary)");
            for (int i = 9; i < 13; i++)
                for (int j = 9; j < 13; j++)
                    Console.WriteLine($"{i} line\t-\t{j} line\t:{((Line)shapes.ElementAt(i)) - (Line)(shapes.ElementAt(j))}");
            Console.WriteLine();
            Console.WriteLine("Operator + ");
            for (int i = 9; i < 13; i++)
                for (int j = 9; j < 13; j++)
                    Console.WriteLine($"{i} line\t+\t{j} line\t:{((Line)shapes.ElementAt(i)) + (Line)(shapes.ElementAt(j))}");

            {
                Random random = new Random();
                double curr;
                Console.WriteLine();
                Console.WriteLine("Operator * ");
                for (int i = 9; i < 13; i++)
                {
                    curr = Math.Round(random.NextDouble() * 100) / 100 + random.Next(10);
                    Console.WriteLine($"{i} line\t*\t{curr} = {((Line)shapes.ElementAt(i)) * curr}");
                }
                Console.WriteLine();
                Console.WriteLine("Operator / ");
                for (int i = 9; i < 13; i++)
                {
                    curr = Math.Round(random.NextDouble() * 100) / 100 + random.Next(10);
                    try
                    {
                        Console.WriteLine($"{i} line\t/\t{curr} = {((Line)shapes.ElementAt(i)) / curr}");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }

            #endregion
            #region Color line
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Color line methods");
            Console.WriteLine("Vectors:");
            for (int i = 13; i < 22; ++i)
                Console.WriteLine(((Line)(shapes.ElementAt(i))).Vector);
            Console.WriteLine("Method Equals");
            for (int i = 13; i < 22; i++)
                for (int j = 9; j < 22; j++)
                    Console.WriteLine($"{i} line\tequals\t{j} line\t:{((shapes.ElementAt(i)).Equals(shapes.ElementAt(j)))}");
            Console.WriteLine();
            Console.WriteLine("Operator ==");
            for (int i = 13; i < 22; i++)
                for (int j = 13; j < 22; j++)
                    Console.WriteLine($"{i} line\tequals\t{j} line\t:{(shapes.ElementAt(i)) == (shapes.ElementAt(j))}");
            Console.WriteLine();
            Console.WriteLine("Operator !=");
            for (int i = 13; i < 22; i++)
                for (int j = 9; j < 22; j++)
                    Console.WriteLine($"{i} line\tequals\t{j} line\t:{(shapes.ElementAt(i)) != (shapes.ElementAt(j))}");
            Console.WriteLine();
            Console.WriteLine("Operator - (unary)");
            for (int i = 13; i < 22; i++)
                Console.WriteLine($"{i} -line\t{-((ColorLine)shapes.ElementAt(i))}");
            Console.WriteLine();
            Console.WriteLine("Operator - (binary)");
            for (int i = 13; i < 22; i++)
                for (int j = 9; j < 22; j++)
                    Console.WriteLine($"{i} line\t-\t{j} line\t:{((ColorLine)shapes.ElementAt(i)) - (Line)(shapes.ElementAt(j))}");
            Console.WriteLine();
            Console.WriteLine("Operator + ");
            for (int i = 13; i < 22; i++)
                for (int j = 9; j < 22; j++)
                    Console.WriteLine($"{i} line\t+\t{j} line\t:{((ColorLine)shapes.ElementAt(i)) + (Line)(shapes.ElementAt(j))}");

            {
                Random random = new Random();
                double curr;
                Console.WriteLine();
                Console.WriteLine("Operator * ");
                for (int i = 13; i < 22; i++)
                {
                    curr = Math.Round(random.NextDouble() * 100) / 100 + random.Next(10);
                    Console.WriteLine($"{i} line\t*\t{curr} = {((ColorLine)shapes.ElementAt(i)) * curr}");
                }
                Console.WriteLine();
                Console.WriteLine("Operator / ");
                for (int i = 13; i < 22; i++)
                {
                    curr = Math.Round(random.NextDouble() * 100) / 100 + random.Next(10);
                    try
                    {
                        Console.WriteLine($"{i} line\t/\t{curr} = {((ColorLine)shapes.ElementAt(i)) / curr}");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
            #endregion
            #region Polygon
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Polygon methods");
            Console.WriteLine("Method Equals");
            for (int i = shapes.Count - 3; i != shapes.Count; i++)
                for (int j = shapes.Count - 3; j != shapes.Count; j++)
                    Console.WriteLine($"{i} point\tequals\t{j} point\t:{((shapes.ElementAt(i)).Equals(shapes.ElementAt(j)))}");
            Console.WriteLine();
            Console.WriteLine("Operator ==");
            for (int i = shapes.Count - 3; i != shapes.Count; i++)
                for (int j = shapes.Count - 3; j != shapes.Count; j++)
                    Console.WriteLine($"{i} point\tequals\t{j} point\t:{(shapes.ElementAt(i)) == (shapes.ElementAt(j))}");
            Console.WriteLine();
            Console.WriteLine("Operator !=");
            for (int i = shapes.Count - 3; i != shapes.Count; i++)
                for (int j = shapes.Count - 3; j != shapes.Count; j++)
                    Console.WriteLine($"{i} point\tequals\t{j} point\t:{(shapes.ElementAt(i)) != (shapes.ElementAt(j))}");

            {
                Random random = new Random();
                double curr;
                Console.WriteLine();
                Console.WriteLine("Method MoveOffX");
                for (int i = shapes.Count - 3; i != shapes.Count - 1; i++)
                {
                    curr = Math.Round(random.NextDouble() * 100) / 100 + random.Next(10);
                    Console.WriteLine($"{i} Polygon\tmove off X\t{curr} = {(((Polygon)shapes.ElementAt(i))).MoveOffX(curr)}");
                }
                Console.WriteLine();
                Console.WriteLine("Method MoveOffY");
                for (int i = shapes.Count - 3; i != shapes.Count - 1; i++)
                {
                    curr = Math.Round(random.NextDouble() * 100) / 100 + random.Next(10);
                    Console.WriteLine($"{i} Polygon\tmove off Y\t{curr} = {(((Polygon)shapes.ElementAt(i))).MoveOffY(curr)}");
                }
                Console.WriteLine();
                Console.WriteLine("Method MoveOffXY");
                double curr2;
                for (int i = shapes.Count - 3; i != shapes.Count - 1; i++)
                {
                    curr = Math.Round(random.NextDouble() * 100) / 100 + random.Next(10);
                    curr2= Math.Round(random.NextDouble() * 100) / 100 + random.Next(10);
                    Console.WriteLine($"{i} Polygon\tmove off X\t{curr} and off Y\t{curr2} = {(((Polygon)shapes.ElementAt(i))).MoveOffXY(curr,curr2)}");
                }
            }
            #endregion

            Console.WriteLine("Для завершения работы нажмите клавишу...");
            Console.CursorVisible = false;
            Console.ReadKey(true);
        }
    }
}
