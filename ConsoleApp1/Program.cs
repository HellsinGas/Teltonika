using System;
using teltonika.Models;
using Teltonika.Models;
using Teltonika.Repository;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {

            FileSerializer serializer = new FileSerializer();            
            DataModelList dataModel = new DataModelList();
            GpsDataProcessing gpsDataProcessing = new GpsDataProcessing();
            HistogramDataProcessing histogramDataProcessing = new HistogramDataProcessing();
            Menu menu = new Menu();


            while (true)
            {
                int input = menu.MainMenu();
                if (input == 1)
                {                    
                    serializer.SelectFile(dataModel);
                }
                if (input == 2)
                {
                    if (dataModel.DataModels == null)
                    {
                        Console.WriteLine("Data set is empty, please upload data from a file first");
                        input = 0;
                        continue;
                    }
                    gpsDataProcessing.Fastest100km(dataModel);
                }
                if (input == 3)
                {
                    if (dataModel.DataModels == null)
                    {
                        Console.WriteLine("Data set is empty, please upload data from a file first");
                        input = 0;
                        continue;
                    }                  
                    
                    histogramDataProcessing.HistogramSatellites(dataModel);
                    
                }
                if (input == 4)
                {
                    if (dataModel.DataModels == null)
                    {
                        Console.WriteLine("Data set is empty, please upload data from a file first");
                        input = 0;
                        continue;
                    }
                    histogramDataProcessing.HistogramSpeeds(dataModel);
                }
                if(input == 5)
                {
                    break;
                }
            }         
                    

        }
    }
}
