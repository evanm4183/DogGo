using System.Collections.Generic;
using DogGo.Models;

namespace DogGo.Repositories
{
    public interface IWalkRepository
    {
        public List<Walk> GetWalksByWalkerId(int id);
    }
}
