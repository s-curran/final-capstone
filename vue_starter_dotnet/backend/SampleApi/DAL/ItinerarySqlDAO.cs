using SampleApi.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApi.DAL
{
    public class ItinerarySqlDAO
    {
        private readonly string connectionString;
        public ItinerarySqlDAO(string connection)
        {
            this.connectionString = connection;
        }
        public int? createItinerary(Itinerary itinerary)
        {
            //Itinerary itinerary = new Itinerary();
            int? id;
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

                    id = Convert.ToInt32(cmd.ExecuteScalar());


                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return id;
        }

        public bool addToItinerary(int itineraryId, int orderNum, Landmark landmark)
        {

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sql =
                       @"INSERT INTO Landmark (LandmarkID, LandmarkName, LandmarkAddress)
                        VALUES (@landmarkid, @name, @landmarkaddress)

                        INSERT INTO LandmarkItinerary (LandmarkID, ItineraryID, OrderNumber)
                        VALUES (@landmarkid, @itineraryid, @ordernumber)";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@landmarkid", landmark.LandmarkId);
                    cmd.Parameters.AddWithValue("@name", landmark.LandmarkName);
                    cmd.Parameters.AddWithValue("@landmarkaddress", landmark.LandmarkAddress);
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

        public Dictionary<int, Landmark> getItinerary(int itineraryId)
        {
            Dictionary<int, Landmark> trip = new Dictionary<int, Landmark>();

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

                    while(rdr.Read())
                    {
                        Landmark landmark = new Landmark();
                        landmark.LandmarkId = Convert.ToString(rdr["LandmarkID"]);
                        landmark.LandmarkName = Convert.ToString(rdr["LandmarkName"]);
                        landmark.LandmarkAddress = Convert.ToString(rdr["LandmarkAddress"]);
                        int orderNum = Convert.ToInt32(rdr["OrderNumber"]);
                        trip[orderNum] = landmark;
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return trip;
        }
    }
}
