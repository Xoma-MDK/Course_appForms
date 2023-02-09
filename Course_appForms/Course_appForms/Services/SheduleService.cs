using Course_appForms.Http;
using Course_appForms.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Course_appForms.Services
{
    internal class SheduleService
    {
        public static async Task<List<Models.Shedule>> GetShedule( string group, int subgroup)
        {
            try
            {
                string at = await SecureStorage.GetAsync(StorageKey.AccessToken);
                string rt = await SecureStorage.GetAsync(StorageKey.RefreshToken);

                Tokens tok = new Tokens() { AccessToken = at, RefreshToken = rt };
                return await SheduleApi.GetShedule(group, subgroup, tok);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
        public static async Task<List<Models.Shedule2>> GetSheduleTeacher(string FIO)
        {
            try
            {
                string at = await SecureStorage.GetAsync(StorageKey.AccessToken);
                string rt = await SecureStorage.GetAsync(StorageKey.RefreshToken);

                Tokens tok = new Tokens() { AccessToken = at, RefreshToken = rt };
                return await SheduleApi.GetSheduleTeacher(FIO, tok);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
        public static async Task<List<Models.SheduleClassRoom>> GetSheduleClassRoom(string ClassRoom)
        {
            try
            {
                string at = await SecureStorage.GetAsync(StorageKey.AccessToken);
                string rt = await SecureStorage.GetAsync(StorageKey.RefreshToken);

                Tokens tok = new Tokens() { AccessToken = at, RefreshToken = rt };
                return await SheduleApi.GetSheduleClassRoom(ClassRoom, tok);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
        public static async Task<List<Models.ClassRoom>> GetSheduleClassRooms(string corpus)
        {
            try
            {
                string at = await SecureStorage.GetAsync(StorageKey.AccessToken);
                string rt = await SecureStorage.GetAsync(StorageKey.RefreshToken);

                Tokens tok = new Tokens() { AccessToken = at, RefreshToken = rt };
                return await SheduleApi.GetSheduleClassRooms(corpus, tok);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
    }
}
