
using IDP.Domain.IRepository.Command.Base;

namespace IDP.Domain.IRepository.Command;

public interface IOtpRedisRepository<T> : ICommandRepository<T> where T : class
{
}
