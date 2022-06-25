using System.Collections.Generic;
using System.Threading.Tasks;

namespace AquariumPeek.Infrastructure.DataAccess
{
    public interface ISqlDataAccess
    {
        Task<IEnumerable<T>> LoadData<T, U>(string storedProcedure, U paramters, string connectionId = "Default");
        Task SaveData<T>(string storedProcedure, T paramters, string connectionId = "Default");
    }
}