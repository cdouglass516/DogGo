using DogGo.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace DogGo.Repositories
{
    public interface IOwnerRepository
    {
        List<Owner> GetAllOwners();
        Owner GetOwnerById(int id);
        void AddOwner(Owner owner);
        List<Neighborhood> GetNeighborhoods();
        void Delete(int Id);
        void Edit(Owner owner);
        List<Dog> GetDogsByOwner(int ownerId);
        string GetOwnerNameByDogId(int OwnerId);
    }
}
