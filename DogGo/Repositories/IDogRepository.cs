﻿using DogGo.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
namespace DogGo.Repositories
{
    public interface IDogRepository
    {
        List<Dog> GetAllDogs();
        List<Dog> GetDogsByOwner(int ownerId);
        Dog GetDogDetail(int id);
    }
}