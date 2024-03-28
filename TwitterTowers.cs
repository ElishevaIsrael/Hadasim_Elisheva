using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleApp1
{
    public class TwitterTowers
    {
        public static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("\n Hello Wellcome to Twitter Towers:\n Press 1 to choose Rectangle\n Press 2  to choose Triangular \n Press 3 for exit the program \n Press now:");
                int choosenInput = int.Parse(Console.ReadLine());
                switch (choosenInput)
                {
                    case 1: Rectangle(); break;
                    case 2: Triangle(); break;
                    case 3: Console.WriteLine("You Are out"); break;
                    default: Console.WriteLine("Please try again, your input is is incorrect"); break;
                }
            }
        }

        private static void Rectangle()
        {
            Console.Write("Enter the height of the rectangle: ");
            int height = int.Parse(Console.ReadLine());
            Console.Write("Enter the width of the rectangle: ");
            int width = int.Parse(Console.ReadLine());
            int squareArea = height * width;
            int squarePerimeter = 2 * (height + width);
            if (height == width || Math.Abs(height - width) > 5)
            {
                Console.WriteLine("The rectangle's area is: " + squareArea);
            }
            else
            {
                Console.WriteLine("The rectangle's perimeter is: " + squarePerimeter);
            }
        }

        private static void Triangle()
        {
            Console.Write("Enter the triangle's base: ");
            double baseTriangle = double.Parse(Console.ReadLine());
            Console.Write("Enter the triangle's height: ");
            double height = double.Parse(Console.ReadLine());
            double hypotenuse = Math.Sqrt(Math.Pow((baseTriangle / 2), 2) + Math.Pow(height, 2));//Pitagoras sentence
            double TrianglePerimeter = (hypotenuse * 2) + baseTriangle;
            Console.WriteLine("Choose between these option:");
            Console.WriteLine("1. Calculation of the triangle's perimeter");
            Console.WriteLine("2. Triangle Print \n Press now");
            int choosenInput = int.Parse(Console.ReadLine());
            if (choosenInput == 1)
            {
                Console.WriteLine("The triangle's perimeter is: " + TrianglePerimeter);
            }
            else if (choosenInput == 2)
            {
                if (baseTriangle % 2 == 0 || baseTriangle > 2 * height)
                {
                    Console.WriteLine("The triangle can't be printed.");
                }
                else
                {
                    int numBlank = (int)baseTriangle / 2;
                    int numStars = 1;
                    //first row, blank until the middle+*
                    for (int j = 0; j < numBlank; j++) { Console.Write(" "); }
                    Console.Write("*\n");
                    //middle
                    int levels = ((int)baseTriangle / 2) - 1;//levels in the triangle
                    int restRows = (int)height - 2;//rows besides the first and the last
                    int rows = (restRows / levels) + (restRows % levels);//for the top we need to add the % also
                    numStars += 2;
                    numBlank -= 1;
                    if (restRows >= 2)//print top
                    {
                        for (int i = 0; i < rows; i++)
                        {
                            for (int j = 0; j < numBlank; j++) { Console.Write(" "); }
                            for (int j = 0; j < numStars; j++) { Console.Write("*"); }
                            Console.Write("\n");
                        }
                    }
                    while (numStars < baseTriangle - 2)//print the rest rows
                    {
                        numStars += 2;
                        numBlank -= 1;
                        rows = restRows / levels;//for the rest rows we need this calculation without the % above
                        for (int i = 0; i < rows; i++)
                        {
                            for (int j = 0; j < numBlank; j++) { Console.Write(" "); }
                            for (int j = 0; j < numStars; j++) { Console.Write("*"); }
                            Console.Write("\n");
                        }
                    }
                    ////print last row
                    for (int j = 0; j < baseTriangle; j++) { Console.Write("*"); }
                }
            }
            else
            {
                Console.WriteLine("Please try again, your input is is incorrect");
            }
        }
    }
}
