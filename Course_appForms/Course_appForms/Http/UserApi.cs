using Course_appForms.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Course_appForms.Http
{
    internal class UserApi
    {

        public static async Task<User> GetMe(Tokens tokens)
        {
            return JsonConvert.DeserializeObject<User>(await Api.TokenyzeGet("users/me", tokens));
        }

    }
}
