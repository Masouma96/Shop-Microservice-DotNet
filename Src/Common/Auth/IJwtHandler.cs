using System;
using System.Collections.Generic;
using System.Text;

namespace Auth
{
    public interface IJwtHandler
    {
        JsonWebToken Create(Int64 userId);
    }
}
