using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
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
        public void HistogramSpeeds(DataModelList dataM)
        {
            int maxSpeed = 0;
            foreach (DataModel model in dataM.DataModels)
            {
                if (model.Speed > maxSpeed)
                {
                    maxSpeed = model.Speed;
                }
            }
            List<float> hitList = new List<float>();
            hitList = PopulateSpeed(maxSpeed);
            Console.WriteLine(hitList.Count);


            hitList= SpeedHitList(dataM,hitList);
            List<float> hitListPercentages = new List<float>();            
            hitListPercentages = Percentage(hitList);

            Console.WriteLine("Speed histogram ------------------------");
            int j = 9;
            for(int i = 0;hitList.Count > i;i++)
            {
                if(i == 0)
                {
                    Console.Write($"[  {i} -   {j} ] |");
                }else if (i > 0 && i <10) {
                    Console.Write($"[ {i*10} -  {j} ] |");
                }else if(i>=10)
                {
                    Console.Write($"[{i * 10} - {j} ] |");
                }
                for(int k =0;k <= 100; k=k+10)
                {
                    if (hitListPercentages[i] >= k && hitListPercentages[i] != 0)
                    {
                        Console.Write("**");
                    }
                    else
                        Console.Write("  ");
                }
                Console.WriteLine($": Hits {hitList[i].ToString()} ") ;
                j = j + 10;
            }







            /*int sum = 0;
            foreach (int test in hitList)
            {
                sum += test;
            }*/
            /*Console.WriteLine(sum);
            Console.WriteLine(data.DataModels.Count);*/
            Console.WriteLine(maxSpeed.ToString());
        }

        private List<float> SpeedHitList(DataModelList dataM, List<float> hitList)
        {
            int j = 9;
            for(int i = 0; i < hitList.Count; i++)
            {
                
                foreach (DataModel model in dataM.DataModels)
                {
                    if(model.Speed >= (i * 10) && model.Speed <= j)
                    {
                        hitList[i] = hitList[i] + 1;
                    }
                }
                j = j + 10;
            }
            /*int sum = 0;
            foreach (int test in hitList)
            {
                sum += test;
            }
            Console.WriteLine(sum);
            Console.WriteLine(dataM.DataModels.Count);*/
            return hitList;
        }

        private List<float> PopulateSpeed(int maxSpeed)
        {
            maxSpeed = maxSpeed / 10 + 1;
            List<float> hitList = new List<float>();
            for (int i = 0; i <= maxSpeed; i++)
            {
                hitList.Add(0);
            }
            return hitList;
        }

        private List<float> Percentage(List<float> hitList)
        {
            float max = 0;
            foreach (float hit in hitList)
            {
                if (hit > max)
                    max = hit;
            }
            List<float> returnList = new List<float>();
            // float percentage = 0;
            for (int i = 0; i < hitList.Count; i++)
            {

                returnList.Add(hitList[i] / max * (float)100);
                // Console.WriteLine(percentage.ToString());
            }
            Console.WriteLine("test");
            return returnList;
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

        

    
