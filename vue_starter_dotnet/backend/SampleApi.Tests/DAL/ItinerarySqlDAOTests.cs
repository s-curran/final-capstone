using Microsoft.VisualStudio.TestTools.UnitTesting;
using SampleApi.DAL;
using SampleApi.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Transactions;

namespace SampleApi.Tests.DAL
{
    [TestClass]
    public class ItinerarySqlDAOTests
    {
        // Create transaction and access DAL
        private TransactionScope transaction = null;
        private IItineraryDAO itineraryDAO;
        private IUserDAO userDAO;
        private string connection = "Data Source=.\\SQLEXPRESS;Initial Catalog=DemoTestDB;Integrated Security=True";



        [TestMethod]
        public void GetLandmarksFromItinerary()
        {
            // Arrange
            itineraryDAO = new ItinerarySqlDAO(connection);

            // Act 
            IList<Landmark> result = itineraryDAO.getItinerary(3);

            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("Anotherplace", result[1].LandmarkName);
        }
    }
}
