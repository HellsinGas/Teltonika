using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teltonika.Models;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.IO;
using teltonika.Models;
using System.Globalization;

namespace Teltonika.Repository
{
    public class FileSerializer
    {
        /*File paths for testing purposes:
            E:\Downloads\C# uzduotis\C# uzduotis\2019-08.csv
            E:\Downloads\C# uzduotis\C# uzduotis\2019-07.json
            E:\Downloads\C# uzduotis\C# uzduotis\2019-09.bin
            */
        public DataModelList SelectFile(DataModelList dataModelList)
        {
            

            Console.WriteLine("Please enter a file path:");
            Console.WriteLine("Path Example: E:\\Downloads\\C# užduotis\\C# užduotis\\2019-09.bin ");            
            string filePath = Console.ReadLine();

            try
            {
                File.Exists(filePath);            
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading file: {ex.Message}");
            }
            if (filePath.Contains(".bin") && File.Exists(filePath))
            {
                return dataModelList = BinaryDeserializer(dataModelList, filePath);
            }
            else if (filePath.Contains(".csv") && File.Exists(filePath))
            {
                return dataModelList = CsvDeserializer(dataModelList, filePath);
            }
            else if (filePath.Contains(".json") && File.Exists(filePath))
            {
                return dataModelList = JsonDeSerializer(dataModelList, filePath);
            }
            else { Console.WriteLine("Unsupported file format");
                return null; }
            
        }
        public DataModelList JsonDeSerializer(DataModelList dataModelList, string filePath)
        {
            List<DataModel> dataModels = new List<DataModel>();
            using (StreamReader r = new StreamReader(filePath))
            {
                string json = r.ReadToEnd();
                dataModels = JsonSerializer.Deserialize<List<DataModel>>(json);
            }
            if (dataModelList.DataModels != null)
            {
                dataModelList.DataModels = dataModelList.DataModels.Concat(dataModels).ToList();
            }
            else
                dataModelList.DataModels = dataModels;
            return dataModelList;
        }

        public DataModelList CsvDeserializer(DataModelList dataModelList, string filePath)
        {
            List<DataModel> data = new List<DataModel>();            
            using (StreamReader r = new StreamReader(filePath))
            {
                while (!r.EndOfStream)
                {
                    string csvLine = r.ReadLine();
                    string[] splitData = csvLine.Split(',');
                    DataModel dataModel = new DataModel();
                    dataModel.Latitude = float.Parse(splitData[0], CultureInfo.InvariantCulture.NumberFormat);
                    dataModel.Longitude = (float)Convert.ToDouble(splitData[1], CultureInfo.InvariantCulture.NumberFormat);
                    dataModel.GpsTime = DateTime.Parse(splitData[2]);
                    dataModel.Speed = int.Parse(splitData[3]);
                    dataModel.Angle = int.Parse(splitData[4]);
                    dataModel.Altitude = int.Parse(splitData[5]);
                    dataModel.Satellites = int.Parse(splitData[6]);
                    data.Add(dataModel);
                }

            }
            if (dataModelList.DataModels != null)
            {
                dataModelList.DataModels = dataModelList.DataModels.Concat(data).ToList();
            }
            else
                dataModelList.DataModels = data;

            return dataModelList;
        }

        public DataModelList BinaryDeserializer(DataModelList dataModelList,string filePath)
        {
            List<DataModel> data = new List<DataModel>();
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
            {
                
                using (BinaryReader reader = new BinaryReader(fileStream, Encoding.BigEndianUnicode))
                {
                    byte[] buffer = new byte[23];
                    while (reader.Read(buffer, 0, buffer.Length) > 0)
                    {
                        DataModel dataModel = new DataModel();
                        byte[] latitude = reader.ReadBytes(4);
                        if (BitConverter.IsLittleEndian)
                        {
                            Array.Reverse(latitude);
                        }
                        int la = BitConverter.ToInt32(latitude, 0);
                        float latit = (float)la / 10000000;
                        byte[] longitude = reader.ReadBytes(4);
                        if (BitConverter.IsLittleEndian)
                        {
                            Array.Reverse(longitude);
                        }
                        int lo = BitConverter.ToInt32(longitude, 0);
                        float longi = (float)lo / 10000000;
                        byte[] gpsTime = reader.ReadBytes(8);
                        if (BitConverter.IsLittleEndian)
                        {
                            Array.Reverse(gpsTime);
                        }
                        long ticks = BitConverter.ToInt64(gpsTime, 0);
                        DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(ticks);
                        byte[] speed = reader.ReadBytes(2);
                        if (BitConverter.IsLittleEndian)
                        {
                            Array.Reverse(speed);
                        }
                        int sp = BitConverter.ToInt16(speed, 0);
                        byte[] angle = reader.ReadBytes(2);
                        if (BitConverter.IsLittleEndian)
                        {
                            Array.Reverse(angle);
                        }
                        int angl = BitConverter.ToInt16(angle, 0);
                        byte[] altitude = reader.ReadBytes(2);
                        if (BitConverter.IsLittleEndian)
                        {
                            Array.Reverse(altitude);
                        }
                        int alt = BitConverter.ToInt16(altitude, 0);
                        byte[] satellites = reader.ReadBytes(1);
                        if (BitConverter.IsLittleEndian)
                        {
                            Array.Reverse(satellites);
                        }
                        int sate = (int)satellites[0];
                        dataModel.Latitude = latit;
                        dataModel.Longitude = longi;
                        dataModel.GpsTime = dateTime;
                        dataModel.Altitude = alt;
                        dataModel.Speed = sp;
                        dataModel.Angle = angl;
                        dataModel.Altitude = alt;
                        dataModel.Satellites = sate;

                        data.Add(dataModel);
                    }                   
                }
                
            }
            if (dataModelList.DataModels != null)
            {
                dataModelList.DataModels = dataModelList.DataModels.Concat(data).ToList();
            }
            else
                dataModelList.DataModels = data;
            return dataModelList;
        }
    }
}
