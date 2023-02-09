using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Course_appForms.Http;
using Course_appForms.Models;
using Xamarin.Essentials;

namespace Course_appForms.Services
{
    internal class UserService
    {
        public static async Task<User> GetMe()
        {
            try
            {
                string at = await SecureStorage.GetAsync(StorageKey.AccessToken);
                string rt = await SecureStorage.GetAsync(StorageKey.RefreshToken);

                Tokens tok = new Tokens() { AccessToken = at, RefreshToken = rt };
                return await UserApi.GetMe(tok);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
    }
}
