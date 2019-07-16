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
    public class SurveyResultDALTests
    {
        private string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=NPGeek;Integrated Security=True";

        private TransactionScope transactionScope;
        Park park3 = new Park();
        SurveyResult survey = new SurveyResult();
        int surveyIdCode;

        [TestInitialize]
        public void Initialize()
        {
            transactionScope = new TransactionScope();

            //park.ParkCode = "ABC";
            //park.ParkName = "Test";
            //park.State = "PA";
            //park.Acreage = 1000;
            //park.ElevationInFeet = 5000;
            //park.MilesOfTrail = 200;
            //park.NumberOfCampsites = 10;
            //park.Climate = "Beachy";
            //park.YearFounded = 1950;
            //park.AnnualVisitorCount = 200000;
            //park.InspirationalQuote = "none";
            //park.InspirationalQuoteSource = "unknown";
            //park.ParkDescription = "testing testing";
            //park.EntryFee = 0;
            //park.NumberOfAnimalSpecies = 200;

            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                connect.Open();
                SqlCommand cmd = new SqlCommand("insert into park values ('ABC', 'Test', 'PA', 1000, 5000, 200, 10,"
                    + "'Beachy', 1950, 200000, 'none', 'unknown', 'testing testing', 0, 200);", connect);
                cmd.ExecuteNonQuery();
            }

            survey.parkCode = "ABC";
            survey.emailAddress = "test@test.com";
            survey.state = "PA";
            survey.activityLevel = "High";

        }

        [TestCleanup]
        public void Cleanup()
        {
            transactionScope.Dispose();
        }


        //TEST:  Test InsertSurvey Method.
        [TestMethod]
        public void InsertSurveyTest()
        {
            //Arrange
            SurveyResultDAL surveyDAL = new SurveyResultDAL(connectionString);
            bool isAdded;

            //Act
            isAdded = surveyDAL.InsertSurvey(survey);

            //Assert
            Assert.IsTrue(isAdded);


            //TEST:  Expected to return False when null values are passed in
            //Arrange
            SurveyResult surveyNull = new SurveyResult();
            surveyNull.parkCode = "ABC";
            surveyNull.emailAddress = "test2@test2.com";
            surveyNull.state = "VA";
            surveyNull.activityLevel = null;
            bool isAdded2;

            //Act
            isAdded2 = surveyDAL.InsertSurvey(surveyNull);

            //Assert
            Assert.IsFalse(isAdded2);

        }

        //TEST: Test the GetSurveyResults Method.  Expected to return an accurate list of survey object(s).
        [TestMethod]
        public void GetSurveyResultsTest ()
        {
            //Arrange
            SurveyResultDAL surveyDAL = new SurveyResultDAL(connectionString);
            List<SurveyResult> surveys = new List<SurveyResult>();

            //Act
            surveyDAL.InsertSurvey(survey); 

            //Obtain surveyId and store it
            //try
            //{
            //    using (SqlConnection connect = new SqlConnection(connectionString))
            //    {
            //        connect.Open();
            //        SqlCommand cmd;
            //        cmd = new SqlCommand("Select @@identity;", connect);
            //        surveyIdCode = Convert.ToInt32(cmd.ExecuteScalar());           

            //    }
            //}
            //catch (Exception)
            //{
            //    throw;
            //}

            //survey.surveyId = surveyIdCode;

            surveys = surveyDAL.GetSurveyResults();

            //Assert
            //Ensures correct email is returned.
            /*
            bool containsCorrectEmail = false;
            foreach (SurveyResult survey in surveys)
            {
                if (survey.emailAddress == "test@test.com")
                {
                    containsCorrectEmail = true;
                }
            }
            Assert.IsTrue(containsCorrectEmail);

            //Assert
            //Ensures correct parkCode is returned.
            bool containsCorrectParkCode = false;
            foreach (SurveyResult survey in surveys)
            {
                if (survey.parkCode == "ABC")
                {
                    containsCorrectParkCode = true;
                }
            }
            Assert.IsTrue(containsCorrectParkCode);

            //Assert
            //Ensures correct state is returned.
            bool containsCorrectState = false;
            foreach (SurveyResult survey in surveys)
            {
                if (survey.state == "PA")
                {
                    containsCorrectState = true;

                }
            }
            Assert.IsTrue(containsCorrectState);
            */
            //Assert
            //Ensures correct activity level is returned.
            bool containsCorrectActivityLevel = false;
            foreach (SurveyResult survey in surveys)
            {
                if (survey.activityLevel == "High")
                {
                    containsCorrectActivityLevel = true;

                }
            }
            Assert.IsTrue(containsCorrectActivityLevel);

        }

        //TEST:  Tests the TopParks method.
    }
}
