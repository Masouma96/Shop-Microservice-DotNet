using IDP.Domain.IRepository.Command.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDP.Domain.DTO;

namespace IDP.Domain.IRepository.Command
{
    public interface IOtpRedisRepository : ICommandRepository<Otp>
    {
        Task<Otp> Getdata(string mobile);

    }
}
