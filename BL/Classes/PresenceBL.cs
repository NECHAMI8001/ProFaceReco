using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Models;

namespace BL.Classes
{
    public class PresenceBL : FaceRecognitionBL
    {
        DBConnection dbCon;
        List<Models.PresenceTBLModel> listOfPresence_Tbl;


        public PresenceBL()
        {
            dbCon = new DBConnection();
            listOfPresence_Tbl = ConvertListToModel(dbCon.GetDbSet<Presence_Tbl>().ToList());
        }
        public List<Models.PresenceTBLModel> GetAllPresence()
        {
            return listOfPresence_Tbl;
        }
        //פונקציה שמחזירה את מספרי המשתמשים
        public int GetCount()
        {
            List<Models.PresenceTBLModel> p = new List<Models.PresenceTBLModel>();
            int max = 0;
            // return GetAll().Count;
            p = GetAllPresence();

            for (int a = 0; a < p.Count; a++)
            {
                if (p[a].Presence_Code > max)
                {
                    max = p[a].Presence_Code;
                }
            }
            return max;
        }
        public int InsertPresence(Models.PresenceTBLModel presence)
        {
            if (listOfPresence_Tbl.Find(p => p.Presence_Code == presence.Presence_Code) == null)
                try
                {
                    dbCon.Execute<Presence_Tbl>(ConvertPresenceToEF(presence),
                    DBConnection.ExecuteActions.Insert);
                    //dbCon.Execute<Presence_Tbl>(ConvertPresenceToEF(presence),
                    //    DBConnection.ExecuteActions.Insert);
                    listOfPresence_Tbl = ConvertListToModel(dbCon.GetDbSet<Presence_Tbl>().ToList());
                    return listOfPresence_Tbl.First(p => p.Presence_Code == presence.Presence_Code).Presence_Code;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            //return GetCount() + 1;
            return listOfPresence_Tbl.First(p => p.Presence_Code == presence.Presence_Code).Presence_Code;
        }

        public int UpdatePresence(Models.PresenceTBLModel presence)
        {
            if (listOfPresence_Tbl.Find(p => p.Presence_Code == presence.Presence_Code) != null)
                try
                {
                    dbCon.Execute<Presence_Tbl>(ConvertPresenceToEF(presence),
                    DBConnection.ExecuteActions.Update);
                    listOfPresence_Tbl = ConvertListToModel(dbCon.GetDbSet<Presence_Tbl>().ToList());
                    return listOfPresence_Tbl.First(p => p.Presence_Code == presence.Presence_Code).Presence_Code;
                }
                catch (Exception ex)
                {
                    return 0;
                }
            return listOfPresence_Tbl.First(p => p.Presence_Code == presence.Presence_Code).Presence_Code;


        }
        public bool DeletePresence(int presenceCode)
        {
            PresenceTBLModel presence = listOfPresence_Tbl.Find(p => p.Presence_Code == presenceCode);

            if (listOfPresence_Tbl.Find(p => p.Presence_Code == presence.Presence_Code) != null)
                try
                {
                    dbCon.Execute<Presence_Tbl>(ConvertPresenceToEF(presence),
                    DBConnection.ExecuteActions.Delete);
                    listOfPresence_Tbl = ConvertListToModel(dbCon.GetDbSet<Presence_Tbl>().ToList());
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }

            return false;

        }

        #region convert functions

        public Presence_Tbl ConvertPresenceToEF(Models.PresenceTBLModel a)
        {
            return new Presence_Tbl
            {
                Presence_Code = a.Presence_Code,
                Presence_Lesson_Name = a.Presence_Lesson_Name,
                Presence_Student_ID = a.Presence_Student_ID,
                Presence_Grade_Code = a.Presence_Grade_Code,
                Presence_Date = a.Presence_Date,
                Presence_TimeBLesson = a.Presence_TimeBLesson,
                //  Presence_TimeMLesson = a.Presence_TimeMLesson,
                //  Presence_TimeELesson = a.Presence_TimeELesson,
                Presence_Hour = a.Presence_Hour
            };
        }

        public static Models.PresenceTBLModel ConvertPresenceToModel(Presence_Tbl a)
        {
            return new Models.PresenceTBLModel
            {
                Presence_Code = a.Presence_Code,
                Presence_Lesson_Name = a.Presence_Lesson_Name,
                Presence_Student_ID = a.Presence_Student_ID,
                Presence_Grade_Code = a.Presence_Grade_Code,
                Presence_Date = a.Presence_Date,
                Presence_TimeBLesson = a.Presence_TimeBLesson,
                //Presence_TimeMLesson = a.Presence_TimeMLesson,
                //  Presence_TimeELesson = a.Presence_TimeELesson,
                Presence_Hour = a.Presence_Hour
            };
        }
        public static List<Models.PresenceTBLModel> ConvertListToModel(List<Presence_Tbl> li)
        {
            return li.Select(l => ConvertPresenceToModel(l)).ToList();
        }
        public static List<Presence_Tbl> ConvertListToEF(List<Models.PresenceTBLModel> li)
        {
            List<Presence_Tbl> l = new List<Presence_Tbl>();
            Presence_Tbl a = new Presence_Tbl();
            foreach (var item in li)
            {
                a = new Presence_Tbl();
                a.Presence_Code = item.Presence_Code;
                a.Presence_Lesson_Name = item.Presence_Lesson_Name;
                a.Presence_Student_ID = item.Presence_Student_ID;
                a.Presence_Grade_Code = item.Presence_Grade_Code;
                a.Presence_Date = item.Presence_Date;
                a.Presence_TimeBLesson = item.Presence_TimeBLesson;
                // a.Presence_TimeMLesson = item.Presence_TimeMLesson;
                //  a.Presence_TimeELesson = item.Presence_TimeELesson;
                a.Presence_Hour = item.Presence_Hour;
                l.Add((a));
            }
            return l;
        }
        #endregion




    }
}
