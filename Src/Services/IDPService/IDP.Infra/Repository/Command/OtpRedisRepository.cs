using IDP.Domain.DTO;
using IDP.Domain.IRepository.Command.Base;
using IDP.Domain.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;
using IDP.Domain.IRepository.Command;

namespace IDP.Infra.Repository.Command
{
    public class OtpRedisRepository : IOtpRedisRepository
    {
        private readonly IDistributedCache _distributedCache;
        private readonly IConfiguration _configuration;

        public OtpRedisRepository(IDistributedCache distributedCache, IConfiguration configuration)
        {
            _distributedCache = distributedCache;
            _configuration = configuration;
        }

        public async Task<bool> Insert(Otp entity)
        {
        
            int time = Convert.ToInt32(_configuration.GetSection("Otp:OtpTime").Value);

            _distributedCache.SetString(entity.UserId.ToString(), JsonConvert.SerializeObject(entity), new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(time)).SetAbsoluteExpiration(TimeSpan.FromMinutes(time)));

            return true; 
        }  

        public async Task<bool> Delete(Otp entity)
        {
            await _distributedCache.RemoveAsync(entity.UserId.ToString());
            return true;
        }

        public Task<bool> Update(Otp entity)
        {
            throw new NotImplementedException();
        }
    }
}
