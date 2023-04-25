using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using teltonika.Models;
using Teltonika.Models;

namespace Teltonika.Repository
{
    internal class HistogramDataProcessing
    {
        public void HistogramSatellites(DataModelList dataModelList)
        {
            int[] data = { 5, 2, 8, 6, 1, 9, 3 };
            int max = data[0];

            // Find the maximum value in the data
            for (int i = 1; i < data.Length; i++)
            {
                if (data[i] > max)
                {
                    max = data[i];
                }
            }

            // Draw the histogram
            for (int i = max; i > 0; i--)
            {
                Console.Write(i + "|");
                for (int j = 0; j < data.Length; j++)
                {
                    if (data[j] >= i)
                    {
                        Console.Write("* ");
                    }
                    else
                    {
                        Console.Write("  ");
                    }
                }
                Console.WriteLine();
            }

            // Draw the x-axis labels
            Console.Write("  ");
            for (int i = 0; i < data.Length; i++)
            {
                Console.Write("- ");
            }
            Console.WriteLine();
            Console.Write("  ");
            for (int i = 0; i < data.Length; i++)
            {
                Console.Write(i + " ");
            }
            Console.WriteLine();
        }
        public void HistogramSpeeds(DataModelList data)
        {
            int maxSatellites = 0;
            foreach(DataModel model in data.DataModels) {
                if (model.Satellites > maxSatellites)
                {
                    maxSatellites=model.Satellites;
                }
            }
            Console.WriteLine(maxSatellites.ToString());
        }
    }
}
