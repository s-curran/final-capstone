using SampleApi.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApi.DAL
{
    public class ItinerarySqlDAO : IItineraryDAO
    {
        private readonly string connectionString;
        public ItinerarySqlDAO(string connection)
        {
            this.connectionString = connection;
        }
        public int createItinerary(Itinerary itinerary)
        {
            //Itinerary itinerary = new Itinerary();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sql = "INSERT INTO Itinerary (UserID, DateOfTour, TourName, StartPoint) VALUES (@user, @date, @name, @start)";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@user", itinerary.UserId);
                    cmd.Parameters.AddWithValue("@date", itinerary.DateOfTour);
                    cmd.Parameters.AddWithValue("@name", itinerary.TourName);
                    cmd.Parameters.AddWithValue("@start", itinerary.StartPoint);

                    return Convert.ToInt32(cmd.ExecuteScalar());


                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public bool addLandmark(Landmark landmark)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sql = @"INSERT INTO Landmark (LandmarkID, LandmarkName, LandmarkAddress)
                        VALUES(@landmarkid, @name, @landmarkaddress)";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@landmarkid", landmark.LandmarkId);
                    cmd.Parameters.AddWithValue("@name", landmark.LandmarkName);
                    cmd.Parameters.AddWithValue("@landmarkaddress", landmark.LandmarkAddress);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return true;
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return false;
        }
        public IList<Landmark> getLandmarks()
        {
            IList<Landmark> landmarks = new List<Landmark>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sql = "select * from Landmark";

                    SqlCommand cmd = new SqlCommand(sql, conn);

                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        Landmark landmark = new Landmark()
                        {
                            LandmarkId = Convert.ToString(rdr["LandmarkID"]),
                            LandmarkAddress = Convert.ToString(rdr["LandmarkAddress"]),
                            LandmarkName = Convert.ToString(rdr["LandmarkName"])
                        };
                        landmarks.Add(landmark);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return landmarks;
        }
        public Landmark getLandmarkById(string landmarkId)
        {
            Landmark landmark = new Landmark();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sql = "select * from Landmark where LandmarkID = @landmarkId";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@landmarkId", landmarkId);

                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {

                        landmark.LandmarkId = Convert.ToString(rdr["LandmarkID"]);
                        landmark.LandmarkAddress = Convert.ToString(rdr["LandmarkAddress"]);
                        landmark.LandmarkName = Convert.ToString(rdr["LandmarkName"]);
                      
                        
                    }
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return landmark;
        }

        public bool addToItinerary(int itineraryId, int orderNum, Landmark landmark)
        {

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sql =
                       @"INSERT INTO LandmarkItinerary (LandmarkID, ItineraryID, OrderNumber)
                        VALUES (@landmarkid, @itineraryid, @ordernumber)";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@landmarkid", landmark.LandmarkId);
                    cmd.Parameters.AddWithValue("@itineraryid", itineraryId);
                    cmd.Parameters.AddWithValue("@ordernumber", orderNum);


                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return true;
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return false;
        }
        public IList<Itinerary> getUserItineraries(int userId)
        {
            IList<Itinerary> userItinerary = new List<Itinerary>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sql = "select * from Itinerary where UserID = @id";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@id", userId);

                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        Itinerary itinerary = new Itinerary()
                        {
                            ItineraryId = Convert.ToInt32(rdr["ItineraryID"]),
                            UserId = Convert.ToInt32(rdr["UserID"]),
                            DateOfTour = Convert.ToDateTime(rdr["DateOfTour"]),
                            TourName = Convert.ToString(rdr["TourName"]),
                            StartPoint = Convert.ToString(rdr["StartPoint"])
                        };

                        userItinerary.Add(itinerary);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return userItinerary;
        }

        public IList<Landmark> getItinerary(int itineraryId)
        {
            IList<Landmark> trip = new List<Landmark>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sql =
                        @"SELECT *
FROM LandmarkItinerary as LI
JOIN Landmark ON LI.LandmarkID = Landmark.LandmarkID
WHERE ItineraryID = @id
ORDER BY OrderNumber";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@id", itineraryId);

                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        Landmark landmark = new Landmark();
                        landmark.LandmarkId = Convert.ToString(rdr["LandmarkID"]);
                        landmark.LandmarkName = Convert.ToString(rdr["LandmarkName"]);
                        landmark.LandmarkAddress = Convert.ToString(rdr["LandmarkAddress"]);

                        trip.Add(landmark);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return trip;
        }

        public Itinerary getItineraryById(int Itineraryid)
        {
            Itinerary itinerary = new Itinerary();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sql = "select * from Itinerary where ItineraryID = @id";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@id", Itineraryid);

                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        itinerary.ItineraryId = Convert.ToInt32(rdr["ItineraryID"]);
                        itinerary.UserId = Convert.ToInt32(rdr["UserID"]);
                        itinerary.DateOfTour = Convert.ToDateTime(rdr["DateOfTour"]);
                        itinerary.TourName = Convert.ToString(rdr["TourName"]);
                        itinerary.StartPoint = Convert.ToString(rdr["StartPoint"]);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return itinerary;

        }

        public bool deleteItinerary(int itineraryId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sql =
                        @"DELETE FROM LandmarkItinerary WHERE ItineraryID = @id
                        DELETE FROM Itinerary WHERE ItineraryID = @id";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@id", itineraryId);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return true;
                    }
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return false;
        }
        public bool removeLandmark(int itineraryId, string landmarkId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sql =
                        @"DELETE FROM LandmarkItinerary WHERE ItineraryID = @id AND LandmarkID = @landmark";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@id", itineraryId);
                    cmd.Parameters.AddWithValue("@landmark", landmarkId);


                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return true;
                    }
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return false;
        }

    }
}
