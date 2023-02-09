using Course_appForms.Http;
using Course_appForms.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Course_appForms.Services
{
    internal class AuthService
    {
        public static async Task<bool> Login(string login, string password)
        {
            Tokens tokens = await AuthApi.Login(login, password);
            if (tokens == null)
                return false;
            try
            {
                await SecureStorage.SetAsync(StorageKey.AccessToken, tokens.AccessToken);
                await SecureStorage.SetAsync(StorageKey.RefreshToken, tokens.RefreshToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return true;
        }
    }
}
