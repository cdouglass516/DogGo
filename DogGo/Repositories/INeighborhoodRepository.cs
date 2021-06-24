using System;
using System.Collections.Generic;
using DogGo.Models;

namespace DogGo.Repositories
{
    interface INeighborhoodRepository
    {
        List<Neighborhood> GetAllNeighborhoods();
    }
}
