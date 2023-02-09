﻿using Course_appForms.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Course_appForms.Http
{
    internal class SheduleApi
    {
        public static async Task<List<Models.Shedule>> GetShedule(string Group, int subGroup, Tokens tokens)
        {
            return JsonConvert.DeserializeObject<List<Models.Shedule>>(await Api.TokenyzeGet($"shedule/get?group={Group}&subgroup={subGroup}", tokens));
        }
        public static async Task<List<Models.Shedule2>> GetSheduleTeacher(string FIO, Tokens tokens)
        {
            return JsonConvert.DeserializeObject<List<Models.Shedule2>>(await Api.TokenyzeGet($"shedule/get/teacher?fio={FIO}", tokens));
        }
        public static async Task<List<Models.SheduleClassRoom>> GetSheduleClassRoom(string ClassRoom, Tokens tokens)
        {
            return JsonConvert.DeserializeObject<List<Models.SheduleClassRoom>>(await Api.TokenyzeGet($"shedule/get/classroom?classroom={ClassRoom}", tokens));
        }
        public static async Task<List<Models.ClassRoom>> GetSheduleClassRooms(string corpus, Tokens tokens)
        {
            return JsonConvert.DeserializeObject<List<Models.ClassRoom>>(await Api.TokenyzeGet($"shedule/get/classrooms?corpus={corpus}", tokens));
        }
    }
}