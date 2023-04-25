using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using teltonika.Models;
using Teltonika.Models;

namespace Teltonika.Repository
{
    internal class HistogramDataProcessing
    {
        public void HistogramSatellites(DataModelList dataM)
        {
            int[] data = { 5, 2, 8, 6, 1, 9, 3 };
            int max = data[0];
            int maxSatellites = 0;
            foreach (DataModel model in dataM.DataModels)
            {
                if (model.Satellites > maxSatellites)
                {
                    maxSatellites = model.Satellites;
                }
            }

            List<float> hitList = new List<float>();
            hitList = Populate(maxSatellites);
            foreach (DataModel model in dataM.DataModels)
            {
                hitList[model.Satellites] = hitList[model.Satellites] + 1;
            }
            int maxHits = MostHits(hitList);
            hitList = Percentage(hitList);
            /* // Find the maximum value in the data
             for (int i = 1; i < data.Length; i++)
             {
                 if (data[i] > max)
                 {
                     max = data[i];
                 }
             }*/

            // Draw the histogram
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
                    else if (i == 0 && !lastLine && hitList[j] /10 > 0)
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

            // Draw the x-axis labels
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
        public void HistogramSpeeds(DataModelList data)
        {
            int maxSatellites = 0;
            foreach (DataModel model in data.DataModels)
            {
                if (model.Satellites > maxSatellites)
                {
                    maxSatellites = model.Satellites;
                }
            }
            List<float> hitList = new List<float>();
            hitList = Populate(maxSatellites);
            foreach (DataModel model in data.DataModels)
            {
                hitList[model.Satellites] = hitList[model.Satellites] + 1;
            }
            hitList = Percentage(hitList);
            /*int sum = 0;
            foreach (int test in hitList)
            {
                sum += test;
            }*/
            /*Console.WriteLine(sum);
            Console.WriteLine(data.DataModels.Count);*/
            Console.WriteLine(maxSatellites.ToString());
        }

        private List<float> Percentage(List<float> hitList)
        {
            float max = 0;
            foreach (float hit in hitList)
            {
                if (hit > max)
                    max = hit;
            }
            float percentage = 0;
            for (int i = 0; i < hitList.Count; i++)
            {

                hitList[i] = hitList[i] / max * (float)100;
                // Console.WriteLine(percentage.ToString());
            }
            Console.WriteLine("test");
            return hitList;
        }

        private List<float> Populate(int maxSatellites)
        {
            List<float> hitList = new List<float>();
            for (int i = 0; i <= maxSatellites; i++)
            {
                hitList.Add(0);
            }
            return hitList;
        }
        private int MostHits(List<float> hitList)
        {
            int max = 0;
            foreach (float hit in hitList)
            {
                if (hit > max)
                {
                    max = (int)hit;
                }
            }
            return max;
        }
    }
}

        

    
