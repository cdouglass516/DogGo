using DogGo.Models.ViewModels;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace DogGo.Repositories
{
    public interface IWalksRepository
    {
       public List<WalksViewModel> GetWalksByOwner(int id);
        List<DogGo.Models.ViewModels.WalksViewModel> GetWalks();
        DogGo.Models.ViewModels.WalksViewModel GetWalkById(int id);
    }
}
