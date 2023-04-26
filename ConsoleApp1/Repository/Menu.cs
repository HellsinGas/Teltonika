using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teltonika.Repository
{
    internal class Menu
    {
        public int MainMenu()
        {
            int input =0;
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Input 1 to upload data from a file");
                Console.WriteLine("Input 2 to calculate Fastest 100km road from uploaded data");
                Console.WriteLine("Input 3 To draw a Histogram of satellites from uploaded data");
                Console.WriteLine("Input 4 To draw a Histogram of speeds from uploaded data");
                Console.WriteLine("Input 5 to Close the program.");
                Console.WriteLine();
                Console.WriteLine();
                string inputString= Console.ReadLine();
                if (int.TryParse(inputString, out input))
                {
                    if (input == 1 || input == 2 || input == 3 || input == 4 || input == 5)
                    {
                        break;
                    }
                    else
                        Console.WriteLine("Wrong input. Try again.");
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid integer.");
                }
                
            }
            return input;
        }
    }
}
