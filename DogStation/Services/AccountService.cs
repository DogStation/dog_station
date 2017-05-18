using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using DogStation.DAO;

namespace DogStation.Services
{
    public class AccountService
    {
        private static readonly DogLoverRepository repository = new DogLoverRepository(new dog_stationEntities());
        public string GenerateToken(string username, string password)
        {
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(0, username, DateTime.Now,
                DateTime.Now.AddHours(6), true, string.Format("{0}&{1}", username, password),
                FormsAuthentication.FormsCookiePath);
            var token = FormsAuthentication.Encrypt(ticket);
            HttpContext.Current.Session[username] = token;
            return token;
        }

        public bool ValidateAccount(string username, string password)
        {
            if (username == null || password == null
                || username.Equals("") || password.Equals(""))
                return false;
            return password.Equals(repository.GetPw(username));
        }
    }
}