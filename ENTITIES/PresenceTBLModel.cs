using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class PresenceTBLModel
    {
        public int Presence_Code { get; set; }
        public string Presence_Lesson_Name { get; set; }
        public string Presence_Student_ID { get; set; }
        public Nullable<int> Presence_Grade_Code { get; set; }
        public Nullable<System.DateTime> Presence_Date { get; set; }
        public Nullable<bool> Presence_TimeBLesson { get; set; }
        public Nullable<System.TimeSpan> Presence_Hour { get; set; }

        //   public Nullable<bool> Presence_TimeMLesson { get; set; }
        //  public Nullable<bool> Presence_TimeELesson { get; set; }
    }
}
