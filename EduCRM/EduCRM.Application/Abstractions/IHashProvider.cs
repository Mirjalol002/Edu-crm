using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduCRM.Application.Abstractions
{
    public interface IHashProvider
    {
        string GetHash(string value);
    }
}
