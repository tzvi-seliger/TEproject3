using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.DAL
{
    public class ParkDAL
    {

        //redo using Dapper
        private readonly string connectionString;

        public ParkDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Park> GetParks(string query = "SELECT * FROM park")
        {
            List<Park> parks = new List<Park>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(query, connection);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        WeatherDAL wd = new WeatherDAL(connectionString);
                        parks.Add(
                            new Park()
                            {
                                ParkCode = Convert.ToString(reader["parkCode"]),
                                ParkName = Convert.ToString(reader["parkName"]),
                                State = Convert.ToString(reader["state"]),
                                Acreage = Convert.ToInt32(reader["acreage"]),
                                ElevationInFeet = Convert.ToInt32(reader["elevationInFeet"]),
                                MilesOfTrail = Convert.ToInt32(reader["milesOfTrail"]),
                                NumberOfCampsites = Convert.ToInt32(reader["numberOfCampsites"]),
                                Climate = Convert.ToString(reader["climate"]),
                                YearFounded = Convert.ToInt32(reader["yearFounded"]),
                                AnnualVisitorCount = Convert.ToInt32(reader["annualVisitorCount"]),
                                InspirationalQuote = Convert.ToString(reader["inspirationalQuote"]),
                                InspirationalQuoteSource = Convert.ToString(reader["inspirationalQuoteSource"]),
                                ParkDescription = Convert.ToString(reader["parkDescription"]),
                                EntryFee = Convert.ToInt32(reader["entryFee"]),
                                NumberOfAnimalSpecies = Convert.ToInt32(reader["numberOfAnimalSpecies"]),
                                Forecast = wd.GetForecasts(Convert.ToString(reader["parkCode"]))
                            }
                        );
                    }
                }
            }
            catch
            {

            }
            return parks;
        }
        public Park GetPark(string parkCode, string tempUnit = "f")
        {
            Park park = new Park();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand($"SELECT * FROM park where parkCode = '{parkCode}'", connection);
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Read();
                    WeatherDAL wd = new WeatherDAL(connectionString);

                    park.ParkCode = Convert.ToString(reader["parkCode"]);
                    park.ParkName = Convert.ToString(reader["parkName"]);
                    park.State = Convert.ToString(reader["state"]);
                    park.Acreage = Convert.ToInt32(reader["acreage"]);
                    park.ElevationInFeet = Convert.ToInt32(reader["elevationInFeet"]);
                    park.MilesOfTrail = Convert.ToInt32(reader["milesOfTrail"]);
                    park.NumberOfCampsites = Convert.ToInt32(reader["numberOfCampsites"]);
                    park.Climate = Convert.ToString(reader["climate"]);
                    park.YearFounded = Convert.ToInt32(reader["yearFounded"]);
                    park.AnnualVisitorCount = Convert.ToInt32(reader["annualVisitorCount"]);
                    park.InspirationalQuote = Convert.ToString(reader["inspirationalQuote"]);
                    park.InspirationalQuoteSource = Convert.ToString(reader["inspirationalQuoteSource"]);
                    park.ParkDescription = Convert.ToString(reader["parkDescription"]);
                    park.EntryFee = Convert.ToInt32(reader["entryFee"]);
                    park.NumberOfAnimalSpecies = Convert.ToInt32(reader["numberOfAnimalSpecies"]);
                    park.Forecast = wd.GetForecasts(Convert.ToString(reader["parkCode"]), tempUnit);
                }

            }
            catch
            {

            }
            return park;
        }
    }
}
