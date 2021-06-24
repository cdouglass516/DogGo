using DogGo.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace DogGo.Repositories
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly IConfiguration _config;
        public OwnerRepository(IConfiguration config)
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

        public List<Owner> GetAllOwners()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT Id, [Name], Email, NeighborhoodId,Address,Phone
                        FROM Owner
                    ";

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Owner> owners = new List<Owner>();
                    while (reader.Read())
                    {
                        Owner owner = new Owner
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            Email = reader.GetString(reader.GetOrdinal("Email")),
                            Address = reader.GetString(reader.GetOrdinal("Address")),
                            NeighborhoodId = reader.GetInt32(reader.GetOrdinal("NeighborhoodId")),
                            Phone = reader.GetString(reader.GetOrdinal("Phone")),
                        };

                        owners.Add(owner);
                    }

                    reader.Close();

                    return owners;
                }
            }
        }

        public Owner GetOwnerById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT o.Id, o.[Name], o.Email, o.NeighborhoodId,o.Address,o.Phone,d.Name as DogName, d.Breed,d.Id as DogId, n.Name as nh
                        FROM Owner o left join Dog d
                        on o.id = d.OwnerId
                        left join Neighborhood n
                        on n.id = o.NeighborhoodId 
                        Where o.Id = @id
                    ";
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    List<Dog> dogs = new(); 
                    Owner owner = null;
                    Neighborhood nh = null;
                    while (reader.Read())
                    {
                        if (owner == null)
                        {
                            owner = new Owner
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                Email = reader.GetString(reader.GetOrdinal("Email")),
                                Address = reader.GetString(reader.GetOrdinal("Address")),
                                NeighborhoodId = reader.GetInt32(reader.GetOrdinal("NeighborhoodId")),
                                Phone = reader.GetString(reader.GetOrdinal("Phone")),
                            };
                            if (!reader.IsDBNull(reader.GetOrdinal("nh"))){
                                nh = new Neighborhood
                                {
                                    Id = owner.NeighborhoodId,
                                    Name = reader.GetString(reader.GetOrdinal("nh")),
                                };
                            }
                            owner.Neighborhood = nh;
                        }

                        if (!reader.IsDBNull(reader.GetOrdinal("DogId")))
                        {
                            dogs.Add(new Dog
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("DogId")),
                                Name = reader.GetString(reader.GetOrdinal("DogName")),
                                Breed = reader.GetString(reader.GetOrdinal("Breed")),
                            });
                        }
                    }
                    owner.Dogs = dogs;
                    reader.Close();
                    return owner;
                }

            }
        }
    
        public Owner GetOwnerByEmail(string email)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        select Id, [Name], Email, Address, Phone, NeighborhoodId
                        From Owner
                        Where Email = @email
                        ";
                    cmd.Parameters.AddWithValue("@email", email);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        Owner owner = new()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            Email = reader.GetString(reader.GetOrdinal("Email")),
                            Address = reader.GetString(reader.GetOrdinal("Address")),
                            Phone = reader.GetString(reader.GetOrdinal("Phone")),
                            NeighborhoodId = reader.GetInt32(reader.GetOrdinal("NeighborhoodId"))
                        };
                        reader.Close();
                        return owner;
                    }
                    reader.Close();
                    return null;
                }
            }
        }

        public string GetOwnerNameByDogId(int OwnerId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        select [Name]
                        From Owner
                        Where Id = @id
                        ";
                    cmd.Parameters.AddWithValue("@id", OwnerId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        reader.Close();
                        return reader.GetString(reader.GetOrdinal("Name"));
                    }
                    reader.Close();
                    return null;
                }
            }
        }
        public  void AddOwner(Owner owner)
        {
            using(SqlConnection conn = Connection)
            {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        insert into Owner (Name,Email, Address, Phone, NeighborhoodId)
                        values(@Name,@Email, @Address, @Phone, @NeighborhoodId)
                    ";
                    cmd.Parameters.AddWithValue("@Name", owner.Name);
                    cmd.Parameters.AddWithValue("@Email", owner.Email);
                    cmd.Parameters.AddWithValue("@Address", owner.Address);
                    cmd.Parameters.AddWithValue("@Phone", owner.Phone);
                    cmd.Parameters.AddWithValue("@NeighborhoodId", owner.NeighborhoodId);
                    cmd.ExecuteNonQuery();
                }
            }

        }

        public void Edit(Owner owner)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        Update Owner 
                            set Name = @name,
                             Email = @email, 
                             Address = @address, 
                             Phone = @phone, 
                             NeighborhoodId = @neighborhoodId
                            where id = @id
                    ";
                    cmd.Parameters.AddWithValue("@id", owner.Id);
                    cmd.Parameters.AddWithValue("@name", owner.Name);
                    cmd.Parameters.AddWithValue("@email", owner.Email);
                    cmd.Parameters.AddWithValue("@address", owner.Address);
                    cmd.Parameters.AddWithValue("@phone", owner.Phone);
                    cmd.Parameters.AddWithValue("@neighborhoodId", owner.NeighborhoodId);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void Delete(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    //Cull any dogs they have
                    cmd.CommandText = "Delete From Dog where OwnerId = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "Delete From Owner where Id = @id";
                    cmd.ExecuteNonQuery();
                }

            }
        }
        public List<Neighborhood> GetNeighborhoods()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        select Id, [Name]
                        From Neighborhood
                        ";
                    SqlDataReader reader = cmd.ExecuteReader();
                    List<Neighborhood> nhs = new();
                    while (reader.Read())
                    {
                        Neighborhood nh = new()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name"))
                        };
                        nhs.Add(nh);
                    }
                    reader.Close();
                    return nhs;
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
                        Select Id, Name, OwnerId, Breed, Notes, ImageUrl
                        From Dog
                        Where OwnerId = @id
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
                            ImageUrl = reader.IsDBNull(reader.GetOrdinal("ImageUrl")) ? null : reader.GetString(reader.GetOrdinal("Notes"))
                        };
                        dogs.Add(dog);
                    }
                    reader.Close();
                    return dogs;
                }
            }
        }
        //End of Class
    }
    // End of Namespace
}
