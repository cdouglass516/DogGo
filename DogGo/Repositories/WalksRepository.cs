using DogGo.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System;

namespace DogGo.Repositories
{
    public class WalksRepository : IWalksRepository
    {
        private readonly IConfiguration _config;

        public WalksRepository(IConfiguration config)
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

        public List<DogGo.Models.ViewModels.WalksViewModel> GetWalks()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        Select w.[Id] ,w.[Date],w.[Duration],w.[WalkerId], wk.Name as WalkerName ,w.[DogId], d.Name as DogName, o.Name as OwnerName
                        From Walks w 
                        join Dog d on w.DogId = d.ID 
                        Join Owner o on d.OwnerId = o.id
                        join Walker wk on wk.Id = w.WalkerId
                    ";
                    SqlDataReader reader = cmd.ExecuteReader();
                    List<DogGo.Models.ViewModels.WalksViewModel> walks = new();
                    while (reader.Read())
                    {
                        DogGo.Models.ViewModels.WalksViewModel walk = new()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                            Duration = reader.GetInt32(reader.GetOrdinal("Duration")),
                            WalkerId = reader.GetInt32(reader.GetOrdinal("WalkerId")),
                            WalkerName = reader.GetString(reader.GetOrdinal("WalkerName")),
                            DogId = reader.GetInt32(reader.GetOrdinal("DogId")),
                            OwnerName = reader.GetString(reader.GetOrdinal("OwnerName")),
                            DogName = reader.GetString(reader.GetOrdinal("DogName")),
                        };
                        int mins = walk.Duration / 60;
                        if (mins > 60)
                        {
                            int hours = 0;
                            while (mins > 60)
                            {
                                hours++;
                                mins = mins - 60;
                            }
                            if (hours > 0)
                            {
                                walk.DisplayDuration += $"{hours.ToString()} hrs and ";
                            }
                        }
                        walk.DisplayDuration += $"{mins.ToString()} minutes";

                        walk.DisplayDate = walk.Date.ToShortDateString();
                        walks.Add(walk);
                    }
                    reader.Close();
                    return walks;
                }
            }
        }
        public List<DogGo.Models.ViewModels.WalksViewModel> GetWalksByOwner(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        Select w.[Id] ,w.[Date],w.[Duration],w.[WalkerId],w.[DogId], d.Name as DogName, o.Name as OwnerName
                        From Walks w 
                        join Dog d on w.DogId = d.ID 
                        Join Owner o on d.OwnerId = o.id
                        Where w.WalkerId = @id
                    ";
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    List<DogGo.Models.ViewModels.WalksViewModel> walks = new();
                    while (reader.Read())
                    {
                        DogGo.Models.ViewModels.WalksViewModel walk = new()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                            Duration = reader.GetInt32(reader.GetOrdinal("Duration")),
                            WalkerId = reader.GetInt32(reader.GetOrdinal("WalkerId")),
                            DogId = reader.GetInt32(reader.GetOrdinal("DogId")),
                            OwnerName = reader.GetString(reader.GetOrdinal("OwnerName")),
                            DogName = reader.GetString(reader.GetOrdinal("DogName")),
                        };
                        int mins = walk.Duration / 60;
                        if (mins > 60)
                        {
                            int hours = 0;                            
                                while (mins > 60)
                            {
                                hours++;
                                mins = mins - 60;
                            }
                            if (hours > 0)
                            {
                                walk.DisplayDuration += $"{hours.ToString()} hrs and ";
                            }
                        }
                        walk.DisplayDuration += $"{mins.ToString()} minutes";

                        walk.DisplayDate = walk.Date.ToShortDateString();
                        walks.Add(walk);
                    }
                    reader.Close();
                    return walks;
                }
            }
        }
        public DogGo.Models.ViewModels.WalksViewModel GetWalkById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        Select w.[Id] ,w.[Date],w.[Duration],w.[WalkerId], wk.Name as WalkerName, w.[DogId], d.Name as DogName,o.Id as OwnerId, o.Name as OwnerName
                        From Walks w 
                        join Dog d on w.DogId = d.ID 
                        Join Owner o on d.OwnerId = o.id
                        join Walker wk on wk.Id = w.WalkerId
                        Where w.[Id] = @id
                    ";
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        DogGo.Models.ViewModels.WalksViewModel walk = new()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                            Duration = reader.GetInt32(reader.GetOrdinal("Duration")),
                            WalkerId = reader.GetInt32(reader.GetOrdinal("WalkerId")),
                            WalkerName = reader.GetString(reader.GetOrdinal("WalkerName")),
                            DogId = reader.GetInt32(reader.GetOrdinal("DogId")),
                            OwnerId = reader.GetInt32(reader.GetOrdinal("OwnerId")),
                            OwnerName = reader.GetString(reader.GetOrdinal("OwnerName")),
                            DogName = reader.GetString(reader.GetOrdinal("DogName")),
                        };
                        int mins = walk.Duration / 60;
                        if (mins > 60)
                        {
                            int hours = 0;
                            while (mins > 60)
                            {
                                hours++;
                                mins = mins - 60;
                            }
                            if (hours > 0)
                            {
                                walk.DisplayDuration += $"{hours.ToString()} hrs and ";
                            }
                        }
                        walk.DisplayDuration += $"{mins.ToString()} minutes";

                        walk.DisplayDate = walk.Date.ToShortDateString();
                        return walk;
                        reader.Close();
                    }
                    reader.Close();
                    return null;
                    
                }
            }
        }
    }
}
