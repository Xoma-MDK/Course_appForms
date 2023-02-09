using System;
using System.Collections.Generic;
using System.Text;

namespace Course_appForms.Models
{
    public class User
    {
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string Studygroup { get; set; }
        public int Substudygroup { get; set; }
        public int Role { get; set; }
        public string Login { get; set; }

    }
}
