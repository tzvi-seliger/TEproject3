using Capstone.Web.DAL;
using Capstone.Web.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Transactions;

namespace CapstoneTests.DAL_Tests
{
    [TestClass]
    public class ParkDALTests
    {
        private string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=NPGeek;Integrated Security=True";

        private TransactionScope transactionScope;
        Park park = new Park();
        Park park2 = new Park();


        [TestInitialize]
        public void Initialize()
        {
            transactionScope = new TransactionScope();

            

            park.ParkCode = "GHI";
            park.ParkName = "Test";
            park.State = "PA";
            park.Acreage = 1000;
            park.ElevationInFeet = 5000;
            park.MilesOfTrail = 200;
            park.NumberOfCampsites = 10;
            park.Climate = "Beachy";
            park.YearFounded = 1950;
            park.AnnualVisitorCount = 200000;
            park.InspirationalQuote = "none";
            park.InspirationalQuoteSource = "unknown";
            park.ParkDescription = "testing testing";
            park.EntryFee = 0;
            park.NumberOfAnimalSpecies = 200;

            park2.ParkCode = "DEF";
            park2.ParkName = "Test2";
            park2.State = "VA";
            park2.Acreage = 1200;
            park2.ElevationInFeet = 10000;
            park2.MilesOfTrail = 100;
            park2.NumberOfCampsites = 8;
            park2.Climate = "Tropical";
            park2.YearFounded = 1985;
            park2.AnnualVisitorCount = 80000;
            park2.InspirationalQuote = "none2";
            park2.InspirationalQuoteSource = "unknown2";
            park2.ParkDescription = "testing testing2";
            park2.EntryFee = 10;
            park2.NumberOfAnimalSpecies = 150;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd;
                cmd = new SqlCommand("insert into park values('GHI', 'Test', 'PA', 1000, 5000, 200, 10, 'Beachy', "
                   + "1950, 200000, 'none', 'unknown', 'testing testing', 0, 200);", connection);
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("insert into park values('DEF', 'Test2', 'VA', 1200, 10000, 100, 8, 'Tropical', "
                    + "1985, 80000, 'none2', 'unknown2', 'testing testing2', 10, 150);", connection);
                cmd.ExecuteNonQuery();
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            transactionScope.Dispose();
        }

        //TEST:  Testing GetParks method.  Expected to return a list that contains park objects identical to the ones created in the
        //TestInitialize section.
        //[TestMethod]
        //public void GetParksTest()
        //{
        //    //Arrange
        //    ParkDAL parkDAL = new ParkDAL(connectionString);
        //    List<Park> output = new List<Park>();
        //    string query = "SELECT * FROM park";

        //    //Arrange
        //    output = parkDAL.GetParks(query);

        //    //Assert
        //    CollectionAssert.Contains(output, park);
        //    //CollectionAssert.Contains(output, park2);
        //}

        //TEST:  Testing GetPark method.  Expected to return a park object that is equivalent to the one set up in the Test Initialize section.
        [TestMethod]
        public void GetParkTest()
        {
            //Arrange
            ParkDAL parkDAL = new ParkDAL(connectionString);
            Park output = new Park();
            string parkCode;
            string code = "DEF";
            string tempUnit = "f";

            //Act
            output = parkDAL.GetPark(code, tempUnit);
            parkCode = output.ParkCode;

            //Assert
            //I dont think it is possible to test a whole object for equality 
            //Rather, test the individual properties for equality
            //Assert.AreEqual(park2, output);
            Assert.AreEqual(park2.ParkCode, output.ParkCode);
            Assert.AreEqual(park2.ParkDescription, output.ParkDescription);

        }

    }
}
