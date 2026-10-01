using IDP.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using IDP.Domain.IRepository.Command.Base;
using System.Linq;
using System.Threading.Tasks;
using IDP.Domain.DTO;



namespace IDP.Domain.IRepository.Command
{
    public interface IOtpRedisRepository : ICommandRepository<Otp>
    {
      

    }
}
