﻿using Course_appForms.Models;
using Course_appForms.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Course_appForms.Http
{
    internal class AuthApi
    {
        public static async Task<Tokens> Login(string Login, string Password)
        {
            StringContent data = new StringContent(
                JsonConvert.SerializeObject(new
                {
                    Login,
                    Password,
                }),
                Encoding.UTF8,
                "application/json"
            );

            HttpResponseMessage res = await Api.api.PostAsync("auth/login", data);

            if (res.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonRes = await res.Content.ReadAsStringAsync();
                Tokens tokens = JsonConvert.DeserializeObject<Tokens>(jsonRes);
                return tokens;
            }
            return null;
        }

        public static async Task Logout(string token)
        {
            HttpClient headerApi = new HttpClient()
            {
                BaseAddress = Api.api.BaseAddress
            };

            headerApi.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            await headerApi.PostAsync("auth/logout", null);
        }


        public static async Task<Tokens> Refresh(string refreshToken)
        {
            HttpClient headerApi = new HttpClient()
            {
                BaseAddress = Api.api.BaseAddress
            };

            headerApi.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", refreshToken);
            HttpResponseMessage res = await headerApi.PostAsync("auth/refresh", null);

            if (res.StatusCode == System.Net.HttpStatusCode.OK)
            {
                try
                {
                    var jsonRes = await res.Content.ReadAsStringAsync();
                    Tokens tokens = JsonConvert.DeserializeObject<Tokens>(jsonRes);
                    await SecureStorage.SetAsync(StorageKey.AccessToken, tokens.AccessToken);
                    await SecureStorage.SetAsync(StorageKey.RefreshToken, tokens.RefreshToken);
                    return tokens;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return null;
                }
            }
            return null;
        }
    }
}
