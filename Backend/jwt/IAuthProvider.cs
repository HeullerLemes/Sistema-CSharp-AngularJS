using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aulaWebApi.Authorization
{
    public interface IAuthProvider
    {

        string CreateToken(string[] claims, Dictionary<string, string> fields);

        bool HasClaim(string token, string claim);

        string GetField(string token, string field);

    }
}
