using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DogStation.Utils;

namespace DogStation.IServices
{
    public interface IAccountService
    {
        MyStatusCode ValidateAccount(string username, string password, ref long userId);

        MyStatusCode RegisterAccount(string username, string password, string gender);

        string GenerateToken(string username, string password, long userId);

        bool GenerateCookie(string token);

        MyStatusCode CleanSessionAndCookie(string username);
    }
}
