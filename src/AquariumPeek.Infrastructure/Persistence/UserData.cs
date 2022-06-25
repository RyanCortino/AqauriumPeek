using AquariumPeek.Infrastructure.DataAccess;
using AquariumPeek.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquariumPeek.Infrastructure.Persistence
{
    public class UserData
    {
        private readonly ISqlDataAccess _db;

        public UserData(ISqlDataAccess db)
        {
            _db = db;
        }

        public Task<IEnumerable<UserModel>> GetUsers()
            => _db.LoadData<UserModel, dynamic>("dbo.spUser_GetAll", new { });

        public async Task<UserModel> GetUser(Guid id)
            => (await LoadUser(id)).FirstOrDefault();

        public Task InserUser(UserModel user)
            => _db.SaveData("dbo.spUser_Insert", new { user.Id, user.Name });

        public Task UpdateUser(UserModel user)
            => _db.SaveData("dbo.spUser_Update", user);

        public Task DeleteUser(Guid guid)
            => _db.SaveData("dbo.spUser_Delete", new { Id = guid });

        private async Task<IEnumerable<UserModel>> LoadUser(Guid id)
        {
            return await _db.LoadData<UserModel, dynamic>("dbo.spUser_Get", new UserModel { Id = id });
        }
    }
}
