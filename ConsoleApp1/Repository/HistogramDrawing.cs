using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teltonika.Repository
{
    internal class HistogramDrawing
    {
        internal void SatelliteHistogram(List<float> hitList, int maxHits)
        {
            bool firstLine = true;
            bool lastLine = false;
            for (int i = 10; i >= 0; i--)
            {
                Console.Write("|");
                for (int j = 0; j < hitList.Count; j++)
                {
                    if (hitList[j] / 10 >= i && i != 0)
                    {
                        Console.Write("** ");
                    }
                    else if (i == 0 && !lastLine && hitList[j] / 10 > 0)
                    {
                        Console.Write("** ");
                    }
                    else
                    {
                        Console.Write("   ");
                    }
                }
                if (firstLine)
                {
                    Console.Write($" Hits: {maxHits}");
                    firstLine = false;
                    Console.WriteLine();
                }
                else if (i == 0 && !lastLine)
                {
                    Console.Write($" Hits : 0");
                    Console.WriteLine();
                    lastLine = true;
                }
                else
                    Console.WriteLine();
            }


            Console.Write(" ");
            for (int i = 0; i < hitList.Count + 4; i++)
            {
                Console.Write("-  ");
            }
            Console.WriteLine();
            Console.Write(" ");
            for (int i = 0; i < hitList.Count; i++)
            {
                if (i < 10)
                {
                    Console.Write($"0{i} ");
                }
                else
                    Console.Write(i + " ");
            }
            Console.Write(" Satellites");
            Console.WriteLine();
        }

        internal void SpeedHistogram(List<float> hitList, List<float> hitListPercentages)
        {
            Console.WriteLine("Speed histogram ------------------------");
            int j = 9;
            for (int i = 0; hitList.Count > i; i++)
            {
                if (i == 0)
                {
                    Console.Write($"[  {i} -   {j} ] |");
                }
                else if (i > 0 && i < 10)
                {
                    Console.Write($"[ {i * 10} -  {j} ] |");
                }
                else if (i >= 10)
                {
                    Console.Write($"[{i * 10} - {j} ] |");
                }
                for (int k = 0; k <= 100; k = k + 10)
                {
                    if (hitListPercentages[i] >= k && hitListPercentages[i] != 0)
                    {
                        Console.Write("**");
                    }
                    else
                        Console.Write("  ");
                }
                Console.WriteLine($": Hits {hitList[i].ToString()} ");
                j = j + 10;
            }
        }
    }
}
