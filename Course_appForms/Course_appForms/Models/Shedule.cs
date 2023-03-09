using System.Collections.Generic;
using SQLite;
namespace Course_appForms.Models
{
    public class Shedule
    {
        public string Date { get; set; }
        public List<Lesson> listOfLessons { get; set; }
    }

    [Table("Shedule")]
    public class SheduleDB
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }

        public string SheduleDamp { get; set; }
    }
}