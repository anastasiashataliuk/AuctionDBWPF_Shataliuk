using BusinessLogic.Interfaces;
using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace BusinessLogic.Services
{
    public class AuctionService : IAuctionService
    {
        private readonly string _connectionString; // The connection string to the database

        // Constructor to initialize the connection string
        public AuctionService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public string Role => throw new NotImplementedException(); // Assuming you have role logic

        // Fetch all auctions from the database
        public List<Auction> GetAllAuctions()
        {
            var auctions = new List<Auction>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT * FROM Auctions";

                using (var command = new SqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        auctions.Add(new Auction
                        {
                            Auction_Id = (int)reader["Auction_Id"],
                            Start_Date = (DateTime)reader["Start_Date"],
                            End_Date = (DateTime)reader["End_Date"],
                            Starting_Price = (decimal)reader["Starting_Price"],
                            Buyout_Price = (decimal)reader["Buyout_Price"],
                            Status = reader["Status"].ToString()
                        });
                    }
                }
            }
            return auctions;
        }

        // Fetch a single auction by ID
        public Auction GetAuctionById(int auctionId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT * FROM Auctions WHERE Auction_Id = @AuctionId";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@AuctionId", auctionId);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Auction
                            {
                                Auction_Id = (int)reader["Auction_Id"],
                                Start_Date = (DateTime)reader["Start_Date"],
                                End_Date = (DateTime)reader["End_Date"],
                                Starting_Price = (decimal)reader["Starting_Price"],
                                Buyout_Price = (decimal)reader["Buyout_Price"],
                                Status = reader["Status"].ToString()
                            };
                        }
                    }
                }
            }
            return null; // Return null if auction is not found
        }

        // Create a new auction in the database
        public void CreateAuction(Auction auction)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "INSERT INTO Auctions (Start_Date, End_Date, Starting_Price, Buyout_Price, Status) " +
                            "VALUES ( @Start_Date, @End_Date, @Starting_Price, @Buyout_Price, @Status)";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Start_Date", auction.Start_Date);
                    command.Parameters.AddWithValue("@End_Date", auction.End_Date);
                    command.Parameters.AddWithValue("@Starting_Price", auction.Starting_Price);
                    command.Parameters.AddWithValue("@Buyout_Price", auction.Buyout_Price);
                    command.Parameters.AddWithValue("@Status", auction.Status);

                    command.ExecuteNonQuery(); // Execute the command to insert the new auction
                }
            }
        }

        // Delete an auction by ID
        public void DeleteAuction(int auctionId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "DELETE FROM Auctions WHERE Auction_Id = @Auction_Id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@AuctionId", auctionId);
                    command.ExecuteNonQuery(); // Execute the command to delete the auction
                }
            }
        }

        public void SaveAuction(Auction auction)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var query = "UPDATE Auctions SET Start_Date = @Start_Date, End_Date = @End_Date, " +
                            "Starting_Price = @Starting_Price, Buyout_Price = @Buyout_Price, Status = @Status " +
                            "WHERE Auction_Id = @Auction_Id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", auction.Auction_Id);
                    command.Parameters.AddWithValue("@StartDate", auction.Start_Date);
                    command.Parameters.AddWithValue("@EndDate", auction.End_Date);
                    command.Parameters.AddWithValue("@StartingPrice", auction.Starting_Price);
                    command.Parameters.AddWithValue("@BuyoutPrice", auction.Buyout_Price);
                    command.Parameters.AddWithValue("@Status", auction.Status);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateAuction(Auction auction)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            var command = new SqlCommand(
                "UPDATE Auctions SET Start_Date = @StartDate, End_Date = @EndDate, Starting_Price = @StartingPrice, Buyout_Price = @BuyoutPrice, Status = @Status WHERE Auction_Id = @Id",
                connection);
            command.Parameters.AddWithValue("@StartDate", auction.Start_Date);
            command.Parameters.AddWithValue("@EndDate", auction.End_Date);
            command.Parameters.AddWithValue("@StartingPrice", auction.Starting_Price);
            command.Parameters.AddWithValue("@BuyoutPrice", auction.Buyout_Price);
            command.Parameters.AddWithValue("@Status", auction.Status);
            command.Parameters.AddWithValue("@Id", auction.Auction_Id);

            command.ExecuteNonQuery();
        }

    }
}
