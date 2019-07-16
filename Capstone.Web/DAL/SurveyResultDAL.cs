using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.DAL
{
    public class SurveyResultDAL
    {
        private readonly string connectionString;

        public SurveyResultDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public string InsertSQL() => $"INSERT INTO survey_result values(@parkCode, @emailAddress, @state, @activityLevel)";
        public bool InsertSurvey(SurveyResult SR)
        {
            int isExecuted = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(InsertSQL(), connection);
                    cmd.Parameters.AddWithValue("@parkCode", SR.parkCode);
                    cmd.Parameters.AddWithValue("@emailAddress", SR.emailAddress);
                    cmd.Parameters.AddWithValue("@state", SR.state);
                    cmd.Parameters.AddWithValue("@activityLevel", SR.activityLevel);
                    isExecuted = cmd.ExecuteNonQuery();
                }

            }
            catch 
            {

            }
            return isExecuted > 0;
        }

        public List<SurveyResult> GetSurveyResults()
        {
            List<SurveyResult> surveyResults = new List<SurveyResult>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM survey_result", connection);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        surveyResults.Add(
                            new SurveyResult()
                            {
                                parkCode = Convert.ToString(reader["parkCode"]),
                                surveyId = Convert.ToInt32(reader["surveyId"]),
                                activityLevel = Convert.ToString(reader["activityLevel"]),
                                emailAddress = Convert.ToString(reader["emailAddress"]),
                                state = Convert.ToString(reader["state"])

                            });
                    }
                }
            }
            catch
            {
            }
            return surveyResults;
        }

        public Park TopPark()
        {
            Park topPark = new Park();
            ParkDAL PD = new ParkDAL(connectionString);
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("select top 1 Count(parkCode) as counts, parkcode from survey_result Group by parkCode order by counts desc", connection);
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Read();
                    WeatherDAL wd = new WeatherDAL(connectionString);
                    topPark = PD.GetPark(Convert.ToString(reader["parkCode"]));
                }
            }
            catch
            {

            }
            return topPark;
        }

        //TODO - remember why we need Survey Model.

        public List<Park> TopParks(string query = "select Count(survey_result.parkCode) as counts, survey_Result.parkcode, park.parkname, park.state, park.acreage, park.elevationInFeet, " +
            " park.milesOfTrail, park.numberOfCampsites, park.climate, park.yearFounded, park.annualVisitorCount, park.inspirationalQuote," +
            " park.inspirationalQuoteSource, park.parkDescription, park.entryFee, park.numberOfAnimalSpecies" +
            " from survey_result JOIN park on survey_result.parkCode = park.parkCode" +
            " Group by survey_result.parkCode, park.parkname, park.state, park.acreage, park.elevationInFeet," +
            " park.milesOfTrail, park.numberOfCampsites, park.climate, park.yearFounded, park.annualVisitorCount, park.inspirationalQuote," +
            " park.inspirationalQuoteSource, park.parkDescription, park.entryFee, park.numberOfAnimalSpecies" +
            " order by counts desc")
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
                                Count = Convert.ToInt32(reader["counts"]),
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
    }
}
