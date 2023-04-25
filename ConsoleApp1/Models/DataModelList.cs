using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using teltonika.Models;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace Teltonika.Models
{
    public class DataModelList
    {
        [JsonPropertyName("DataModels")]
        public List<DataModel> DataModels { get; set; }

        public DataModelList()
        {
        }

        public DataModelList(List<DataModel> dataModels)
        {
            DataModels = dataModels;
        }
    }
}
