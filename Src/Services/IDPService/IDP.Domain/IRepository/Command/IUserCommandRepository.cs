using IDP.Domain.IRepository.Command.Base;
using System;
using System.Collections.Generic;
using System.Text;
using IDP.Domain.Entities;


namespace IDP.Domain.IRepository.Command
{
    public interface IUserCommandRepository : ICommandRepository<User>
    {

    }
}
