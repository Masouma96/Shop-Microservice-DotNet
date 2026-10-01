
using Newtonsoft.Json;
using IDP.Application.DTO;
using IDP.Domain.IRepository.Command;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Caching.Distributed;

namespace IDP.Infra.Repository.Command;

public class OtpRedisRepository(IDistributedCache DistributedCache, IConfiguration Configuration) : IOtpRedisRepository<Otp>
{
    public async Task<bool> Insert(Otp entity)
    {
        int time = Convert.ToInt32(Configuration["Otp:OtpTime"]!);

        DistributedCache.SetString(entity.UserId.ToString(), 
            JsonConvert.SerializeObject(entity), new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(time)).SetAbsoluteExpiration(TimeSpan.FromMinutes(time)));

        return true; 
    }  

    public async Task<bool> Delete(Otp entity)
    {
        await DistributedCache.RemoveAsync(entity.UserId.ToString());
        return true;
    }

    public Task<bool> Update(Otp entity)
    {
        throw new NotImplementedException();
    }
}
