using DogGo.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
namespace DogGo.Repositories
{
    public class DogRepository : IDogRepository
    {
        private readonly IConfiguration _config;

        // The constructor accepts an IConfiguration object as a parameter. This class comes from the ASP.NET framework and is useful for retrieving things out of the appsettings.json file like connection strings.
        public DogRepository(IConfiguration config)
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
        public List<Dog> GetAllDogs()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        Select d.Id, d.Name, d.OwnerId, d.Breed, d.Notes, d.ImageUrl, o.Name as OwnerName
                        From Dog d Left Join Owner o on d.OwnerId = o.id
                    ";
                    SqlDataReader reader = cmd.ExecuteReader();
                    List<Dog> dogs = new();
                    while (reader.Read())
                    {
                        Dog dog = new()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            OwnerId = reader.GetInt32(reader.GetOrdinal("OwnerId")),
                            Breed = reader.GetString(reader.GetOrdinal("Breed")),
                            Notes = reader.IsDBNull(reader.GetOrdinal("Notes")) ? null : reader.GetString(reader.GetOrdinal("Notes")),
                            ImageUrl = reader.IsDBNull(reader.GetOrdinal("ImageUrl")) ? null : reader.GetString(reader.GetOrdinal("Notes")),
                            OwnerName = reader.IsDBNull(reader.GetOrdinal("OwnerName")) ? null : reader.GetString(reader.GetOrdinal("OwnerName")),
                        };
                        dogs.Add(dog);
                    }
                    reader.Close();
                    return dogs;
                }
            }
        }

        public Dog GetDogDetail(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        Select d.Id, d.Name, d.OwnerId, d.Breed, d.Notes, d.ImageUrl, o.Name as OwnerName
                        From Dog d Left Join Owner o on d.OwnerId = o.id
                        Where d.Id = @id
                    ";
                    cmd.Parameters.AddWithValue("@id",id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        Dog dog = new()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            OwnerId = reader.GetInt32(reader.GetOrdinal("OwnerId")),
                            Breed = reader.GetString(reader.GetOrdinal("Breed")),
                            Notes = reader.IsDBNull(reader.GetOrdinal("Notes")) ? null : reader.GetString(reader.GetOrdinal("Notes")),
                            ImageUrl = reader.IsDBNull(reader.GetOrdinal("ImageUrl")) ? null : reader.GetString(reader.GetOrdinal("Notes")),
                            OwnerName = reader.IsDBNull(reader.GetOrdinal("OwnerName")) ? null : reader.GetString(reader.GetOrdinal("OwnerName")),
                        };

                        reader.Close();
                        return dog;
                    }
                    reader.Close();
                    return null;
                }
            }
        }

        public List<Dog> GetDogsByOwner(int ownerId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        Select d.Id, d.Name, d.OwnerId, d.Breed, d.Notes, d.ImageUrl, o.Name as OwnerName
                        From Dog d Left Join Owner o on d.OwnerId = o.id
                        Where d.Id = @id
                    ";
                    cmd.Parameters.AddWithValue("@id", ownerId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    List<Dog> dogs = new();
                    while (reader.Read())
                    {
                        Dog dog = new()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            OwnerId = reader.GetInt32(reader.GetOrdinal("OwnerId")),
                            Breed = reader.GetString(reader.GetOrdinal("Breed")),
                            Notes = reader.IsDBNull(reader.GetOrdinal("Notes")) ? null : reader.GetString(reader.GetOrdinal("Notes")),
                            ImageUrl = reader.IsDBNull(reader.GetOrdinal("ImageUrl")) ? null : reader.GetString(reader.GetOrdinal("Notes")),
                            OwnerName = reader.IsDBNull(reader.GetOrdinal("OwnerName")) ? null : reader.GetString(reader.GetOrdinal("OwnerName")),
                        };
                        dogs.Add(dog);
                    }
                    reader.Close();
                    return dogs;
                }
            }
        }
    }
}
