using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.DAL
{
    public class WeatherDAL
    {
        private readonly string connectionString;
        public WeatherDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public string GetWeatherByCode(string code) => $"select * from weather where weather.parkCode = '{code}'";
        public List<Weather> GetForecasts(string parkCode, string tempUnit = "f")
        {
            List<Weather> forecasts = new List<Weather>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(GetWeatherByCode(parkCode), connection);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Weather weather = new Weather();
                        weather.parkCode = Convert.ToString(reader["parkCode"]);
                        weather.fiveDayForecastValue = Convert.ToInt32(reader["fiveDayForecastValue"]);
                        weather.low = Convert.ToInt32(reader["low"]);
                        weather.high = Convert.ToInt32(reader["high"]);
                        weather.forecast = Convert.ToString(reader["forecast"]);
                        weather.tempUnit = tempUnit;
                        weather.Recommendations = weather.AddRecommendations();
                        forecasts.Add(weather);
                    }
                }
            }
            catch
            {
            }
            return forecasts;
        }
    }
}
