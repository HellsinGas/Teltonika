using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geolocation;
using teltonika.Models;
using Teltonika.Models;

namespace Teltonika.Repository
{
    public class GpsDataProcessing
    {
        public void Fastest100km(DataModelList dataModelList)
        {
            List<double> points = new List<double>();
            int i = 1;
            double max = 0;          
            DataModel origin = new DataModel();
            DataModel destination = new DataModel();
            var tupleList = new List<Tuple<DataModel, DataModel>>();
            foreach (DataModel model in dataModelList.DataModels)
            {
                if (model.Speed > 0)
                {
                    for (int j = i; j < dataModelList.DataModels.Count; j++)
                    {
                        double distance = CalculateDistance(model, dataModelList.DataModels[j]);                        
                        if (distance >= 100)
                        {
                            var tuple = new Tuple<DataModel, DataModel>(model, dataModelList.DataModels[j]);
                            tupleList.Add(tuple);
                        }
                        else if (distance >= max)
                        {
                            origin = model;
                            destination = dataModelList.DataModels[j];
                            max = distance;                                                       
                        }
                    }
                }
                i++;
                
            }
            if (tupleList.Count > 0)
            {
                DistanceOver100Found(tupleList);
            }
            else
            {
                DistanceOver100NotFound(origin, destination);
            }           
        }

        private void DistanceOver100NotFound(DataModel origin, DataModel destination)
        {
            Console.WriteLine("Distance of over 100 km was not found");
            Console.WriteLine("Longest section of the road inside provided dataset:");
            Console.WriteLine($"Origin point: Latitude :{origin.Latitude} Longitude : {origin.Longitude}");
            Console.WriteLine($"Destination point: Latitude:{destination.Latitude} Longitude:{destination.Longitude}");
            Console.WriteLine($"Distance Traveled {GeoCalculator.GetDistance(origin.Latitude,origin.Longitude,destination.Latitude,destination.Longitude) * 1.609344}");
        }

        private void DistanceOver100Found(List<Tuple<DataModel, DataModel>> tupleList)
        {
            double tempShortest = double.MaxValue;
            DataModel tempOrigin = null;
            DataModel tempDestination = null;
            double seconds = 0;
            double distance = 0;
            for (int i = 0; i < tupleList.Count; i++)
            {
                DataModel origin = tupleList[i].Item1;
                DataModel destination = tupleList[i].Item2;
                distance = CalculateDistance(origin, destination);
                seconds = CalculateDriveTime(distance, origin);
                if(seconds < tempShortest)
                {
                    tempShortest = seconds;
                    tempOrigin = origin;
                    tempDestination = destination;
                }                
            }
            Console.WriteLine($"Fastest road section of at least 100 km was driven over {seconds.ToString()} seconds" +
                $" and was {distance} km long.");
            Console.WriteLine($"Start position {tempOrigin.Latitude} ; {tempOrigin.Longitude}");
            Console.WriteLine($"Start gps time {tempOrigin.GpsTime}");
            Console.WriteLine($"End   position {tempDestination.Latitude};{tempDestination.Longitude}");
            Console.WriteLine($"End   gps time {tempOrigin.GpsTime.AddSeconds(tempShortest)}");
            Console.WriteLine($"Averge speed {tempOrigin.Speed} km/h");

        }

        private double CalculateDriveTime(double distance, DataModel origin)
        {
            double meters = distance * 1000;
            double metersPerSecond = origin.Speed * 1000 / 3600;
            double secondsTaken = meters / metersPerSecond;
            return secondsTaken;

        }

        private double CalculateDistance(DataModel origin, DataModel destination)
        {
            double distance;
            distance = GeoCalculator.GetDistance(origin.Latitude, origin.Longitude, destination.Latitude, destination.Longitude, 1);
            return distance * 1.609344;           
        }
    }
}
