using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoDB.Bson;
using POCForDistanceCalculation.Model;
namespace POCForDistanceCalculation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class LocationController : ControllerBase
    {
        [HttpGet("getDistance/{from}/{to}")]
        public async Task<ActionResult<List<Locations>>> GetDistance(string from, string to)
        {

            var connectionString = "mongodb+srv://anshulpareektechy:Hosting123@cluster0.owslrhw.mongodb.net/?retryWrites=true&w=majority";
            var client = new MongoClient(connectionString);
            var databaseName = "Locations";
            var database = client.GetDatabase(databaseName);
            var collectionName = "locations";
            var collection = database.GetCollection<Locations>(collectionName);

            var _from = await collection.Find(_ => _.ZIP.ToString() == from).ToListAsync();
            var _to = await collection.Find(_ => _.ZIP.ToString() == to).ToListAsync();

            Response httpResponse = new Response();
            httpResponse.Message ="The Distance between "+ _from.FirstOrDefault().City + " ( "+ from+" ) "+ " to "+ _to.FirstOrDefault().City+" ("+to+" )"+" is " +Math.Round(distance(_from.FirstOrDefault().LAT, _to.FirstOrDefault().LAT,
                          _from.FirstOrDefault().LNG, _to.FirstOrDefault().LNG) * 0.621371, 3)  + " Miles";

            return Ok(httpResponse);

        }
        private double toRadians(
           double angleIn10thofaDegree)
        {
            // Angle in 10th
            // of a degree
            return (angleIn10thofaDegree *
                           Math.PI) / 180;
        }
        private double distance(double lat1,
                           double lat2,
                           double lon1,
                           double lon2)
        {

            // The math module contains
            // a function named toRadians
            // which converts from degrees
            // to radians.
            lon1 = toRadians(lon1);
            lon2 = toRadians(lon2);
            lat1 = toRadians(lat1);
            lat2 = toRadians(lat2);

            // Haversine formula
            double dlon = lon2 - lon1;
            double dlat = lat2 - lat1;
            double a = Math.Pow(Math.Sin(dlat / 2), 2) +
                       Math.Cos(lat1) * Math.Cos(lat2) *
                       Math.Pow(Math.Sin(dlon / 2), 2);

            double c = 2 * Math.Asin(Math.Sqrt(a));

            // Radius of earth in
            // kilometers. Use 3956
            // for miles
            double r = 6371;

            // calculate the result
            return (c * r);
        }
    }
}
