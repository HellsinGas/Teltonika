using System.Collections.Generic;
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

            HistogramDrawing histogramDrawing = new HistogramDrawing();
            histogramDrawing.SatelliteHistogram(hitList, maxHits);

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


            hitList = SpeedHitList(dataM, hitList);
            List<float> hitListPercentages = new List<float>();
            hitListPercentages = Percentage(hitList);

            HistogramDrawing histogramDrawing = new HistogramDrawing();
            histogramDrawing.SpeedHistogram(hitList, hitListPercentages);

        }

        private List<float> SpeedHitList(DataModelList dataM, List<float> hitList)
        {
            int j = 9;
            for (int i = 0; i < hitList.Count; i++)
            {

                foreach (DataModel model in dataM.DataModels)
                {
                    if (model.Speed >= (i * 10) && model.Speed <= j)
                    {
                        hitList[i] = hitList[i] + 1;
                    }
                }
                j = j + 10;
            }
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

            for (int i = 0; i < hitList.Count; i++)
            {

                returnList.Add(hitList[i] / max * (float)100);

            }

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




