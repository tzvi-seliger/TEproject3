using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Transactions;
using Capstone.Web.Models;
using Capstone.Web.DAL;

namespace CapstoneTests.DAL_Tests
{
    
    [TestClass]
    public class WeatherDALTests
    {
        private readonly string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=NPGeek;Integrated Security=True";

        private TransactionScope transactionScope;
        Weather weatherobj = new Weather();
        Park parkobj = new Park();

        [TestInitialize]
        public void Initialize()
        {
            transactionScope = new TransactionScope();

            weatherobj.parkCode = "XYZ";
            weatherobj.fiveDayForecastValue = 1;
            weatherobj.low = 20;
            weatherobj.high = 80;
            weatherobj.forecast = "sunny";
            weatherobj.tempUnit = "f";
            //weatherobj.AddRecommendations();

            //weather.Recommendations.Add("Wear Breathable Layers");
            //weather.Recommendations.Add("Bring Extra Gallon Of Water");
            //weather.Recommendations.Add("pack sunblock");

            parkobj.ParkCode = "XYZ";
            parkobj.ParkName = "Test";
            parkobj.State = "PA";
            parkobj.Acreage = 1000;
            parkobj.ElevationInFeet = 5000;
            parkobj.MilesOfTrail = 200;
            parkobj.NumberOfCampsites = 10;
            parkobj.Climate = "Beachy";
            parkobj.YearFounded = 1950;
            parkobj.AnnualVisitorCount = 200000;
            parkobj.InspirationalQuote = "none";
            parkobj.InspirationalQuoteSource = "unknown";
            parkobj.ParkDescription = "testing testing";
            parkobj.EntryFee = 0;
            parkobj.NumberOfAnimalSpecies = 200;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd;
                cmd = new SqlCommand("  insert into park values('XYZ', 'Test', 'PA', 1000, 5000, 200, 10, 'Beachy', "
                    + "1950, 200000, 'none', 'unknown', 'testing testing', 0, 200);", conn);
                cmd.ExecuteNonQuery();
               
                cmd = new SqlCommand("insert into weather (parkCode, fiveDayForecastValue, low, high, forecast) "
                    + " values ('XYZ', 1, 20, 80, 'sunny');", conn);
                cmd.ExecuteNonQuery();
                conn.Close();

            }

        }

        [TestCleanup]
        public void Cleanup()
        {
            transactionScope.Dispose();
        }

        //TEST:  Test the GetForecasts method in the WeatherDAL.  It should return a list containing
        //the info inserted in the TestInitialize section.  This method invokes another method called
        //GetWeatherByCode which inserts the SQL command.  Passing of this test means that both of those
        //methods are working properly.
        [TestMethod]
        public void GetForecastsTest()
        {
            //Arrange
            WeatherDAL weatherDAL = new WeatherDAL(connectionString);
            //string code = "XYZ";
            //string tempUnit = "f";
            List<Weather> forecasts = new List<Weather>();
            List<Weather> expectedForecasts = new List<Weather>();
            expectedForecasts.Add(weatherobj);   //weather instantiated in TestInitialize section           


            //Act
            forecasts = weatherDAL.GetForecasts("XYZ", "f");

            //Assert

            Assert.AreEqual(expectedForecasts[0].parkCode, forecasts[0].parkCode, $"{expectedForecasts[0].parkCode} should equal {expectedForecasts[0].parkCode}");
            //CollectionAssert.AreEquivalent(expectedForecasts, forecasts);

        }
        
    }
}
