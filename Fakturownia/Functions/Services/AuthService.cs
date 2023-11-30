using Fakturownia.Functions.Models;
using Microsoft.Extensions.Configuration;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fakturownia.Functions.Services
{
    public interface IAuthService
    {
        RestClient SetEMAGRestClient(string url);
    }
    public class AuthService : IAuthService
    {
        private readonly AccountSettings _accountSettings;
        public AuthService(AccountSettings accountSettings)
        {
            _accountSettings = accountSettings;
        }

        public RestClient SetEMAGRestClient(string url)
        {
            var client = new RestClient(url);
            client.Authenticator = new HttpBasicAuthenticator(_accountSettings.Username, _accountSettings.Password);

            return client;
        }
    }
}
