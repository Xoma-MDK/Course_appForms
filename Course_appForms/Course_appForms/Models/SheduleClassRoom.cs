using System.Collections.Generic;

namespace Course_appForms.Models
{
    public class SheduleClassRoom
    {
        public string Date { get; set; }
        public List<LessonClassRoom> listOfLessons { get; set; }
    }
}