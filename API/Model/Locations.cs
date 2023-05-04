using System.Text.Json.Serialization;

namespace POCForDistanceCalculation.Model
{
    public class Locations
    {
        [JsonIgnore]
        public object _id { get; set; }
        public Int32 ZIP { get; set; }
        public double LAT { get; set; }
        public double LNG { get; set; }
        public string City { get; set; }
    }
}
