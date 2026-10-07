
using IDP.Domain.Entities;
using IDP.Domain.IRepository.Query;
using IDP.Infra.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDP.Infra.Repository.Query
{
    public class UserQueryRepository : IUserQueryRepository
    {
        private readonly ShopQueryDbContext _db;
        public UserQueryRepository(ShopQueryDbContext shopQueryDbContext)
        {
            _db = shopQueryDbContext;
        }
        public async Task<User> GetUserAsync(string mobilenumber)
        {
            var userfound = await _db.Tbl_Users.FirstOrDefaultAsync(p => p.MobileNumber == mobilenumber);
            return userfound;
        }
    }
}
