using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace teltonika.Models
{
    public class DataModel
    {
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public DateTime GpsTime { get; set; }
        public int Speed { get; set; }
        public int Angle { get; set; }
        public int Altitude { get; set; }
        public int Satellites { get; set; }

        public DataModel()
        {
        }

        public DataModel(float latitude, float longitude, DateTime gpsTime, int speed, int angle, int altitude, int satellites)
        {
            Latitude = latitude;
            Longitude = longitude;
            GpsTime = gpsTime;
            Speed = speed;
            Angle = angle;
            Altitude = altitude;
            Satellites = satellites;
        }
    }
}
