using BusinessLogic.Interfaces;
using DALEF.Models;
using DTO;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private readonly string _connectionString;

        public UserService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public string Role { get; set; }

        public UserService()
        {
            _connectionString = "Data Source=DESKTOP-7L66KNT\\SQLEXPRESS;Initial Catalog=AuctionDB;Integrated Security=True;TrustServerCertificate=True;"; // You can fetch this from config
        }

        public User GetUserById(int userId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT * FROM tblUser WHERE User_Id = @User_Id";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@User_Id", userId);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new User
                        {
                            User_Id = reader.GetInt32(reader.GetOrdinal("User_Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            Email = reader.GetString(reader.GetOrdinal("Email")),
                            Role = reader.GetString(reader.GetOrdinal("Role"))
                        };
                    }
                }
            }

            return null;
        }

        public User AuthenticateUser(string username, string email)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT * FROM tblUser WHERE Name = @Name AND Email = @Email";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Name", username);
                command.Parameters.AddWithValue("@Email", email);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new User
                        {
                            User_Id = reader.GetInt32(reader.GetOrdinal("User_Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            Email = reader.GetString(reader.GetOrdinal("Email")),
                            Role = reader.GetString(reader.GetOrdinal("Role"))
                        };
                    }
                }
            }

            return null;
        }

        public void RegisterUser(User user)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "INSERT INTO tblUser (Name, Email, Role) VALUES (@Name, @Email, @Role)";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Name", user.Name);
                command.Parameters.AddWithValue("@Email", user.Email);
                command.Parameters.AddWithValue("@Role", user.Role);

                // Execute the insert query
                command.ExecuteNonQuery();
            }
        }

        public List<User> GetAllUsers()
        {
            var users = new List<User>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT * FROM tblUser";
                var command = new SqlCommand(query, connection);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        users.Add(new User
                        {
                            User_Id = reader.GetInt32(reader.GetOrdinal("User_Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            Email = reader.GetString(reader.GetOrdinal("Email")),
                            Role = reader.GetString(reader.GetOrdinal("Role"))
                        });
                    }
                }
            }

            return users;
        }

        public void SaveUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
