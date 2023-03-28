using EduCRM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduCRM.Application.Abstractions
{
    public interface ITokenService
    {
        string GenerateAccessToken(User user);
    }
}
