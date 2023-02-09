using System.Collections.Generic;

namespace Course_appForms.Models
{
    public class Shedule
    {
        public string Date { get; set; }
        public List<Lesson> listOfLessons { get; set; }
    }
}