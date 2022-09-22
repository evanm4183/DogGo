using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using DogGo.Models;

namespace DogGo.Repositories
{
    public class WalkRepository : IWalkRepository
    {
        private IConfiguration _config;

        public WalkRepository(IConfiguration config)
        {
            _config = config;
        }

        public SqlConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }

        public List<Walk> GetWalksByWalkerId(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT w.Id, Date, Duration, o.Name 
                                        FROM Walks w
                                            JOIN Dog d ON d.Id = w.DogId
                                            JOIN Owner o ON o.Id = d.OwnerId
                                        WHERE WalkerId = @id
                                        ORDER BY Date DESC";
                    cmd.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<Walk> walks = new List<Walk>();

                        while (reader.Read())
                        {
                            Walk walk = new Walk()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                                Duration = reader.GetInt32(reader.GetOrdinal("Duration")),
                                Client = new Owner()
                                {
                                    Name = reader.GetString(reader.GetOrdinal("Name"))
                                }
                            };

                            walks.Add(walk);
                        }

                        return walks;
                    }
                }
            }
        }
    }
}
