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
            Console.WriteLine("Hello World!");
            DataModelList dataModel = new DataModelList();
            GpsDataProcessing gpsDataProcessing = new GpsDataProcessing();
            HistogramDataProcessing histogramDataProcessing = new HistogramDataProcessing();
            //dataModel = serializer.JsonDeSerializer(dataModel);
            //dataModel = serializer.CsvDeserializer(dataModel);
             dataModel = serializer.BinaryDeserializer(dataModel);
            // gpsDataProcessing.Fastest100km(dataModel);
            histogramDataProcessing.HistogramSatellites(dataModel);
            histogramDataProcessing.HistogramSpeeds(dataModel);
          

           /* foreach (DataModel dataModelItem in dataModel.DataModels)
            {
                Console.WriteLine($"Satelites :{dataModelItem.Satellites}, Speed :{dataModelItem.Speed}");
            }*/
            

            Console.WriteLine("check yourself before you wreck yourself");

        }
    }
}
