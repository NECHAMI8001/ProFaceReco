//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL
{
    using System;
    using System.Collections.Generic;

    public partial class Presence_Tbl
    {
        public int Presence_Code { get; set; }
        public string Presence_Lesson_Name { get; set; }
        public string Presence_Student_ID { get; set; }
        public Nullable<int> Presence_Grade_Code { get; set; }
        public Nullable<System.DateTime> Presence_Date { get; set; }
        public Nullable<bool> Presence_TimeBLesson { get; set; }
        public Nullable<bool> Presence_TimeMLesson { get; set; }
        public Nullable<bool> Presence_TimeELesson { get; set; }
        public Nullable<System.TimeSpan> Presence_Hour { get; set; }

        public virtual Grade_Tbl Grade_Tbl { get; set; }
        public virtual Student_Tbl Student_Tbl { get; set; }
    }
}
