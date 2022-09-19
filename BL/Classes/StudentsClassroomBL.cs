using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Models;

namespace BL.Classes
{
    public class StudentsClassroomBL : FaceRecognitionBL
    {
        DBConnection dbCon;
        List<Models.StudentsClassroomTBLModel> listOfStudentsClassroom_Tbl;

        public StudentsClassroomBL()
        {
            dbCon = new DBConnection();
            listOfStudentsClassroom_Tbl = ConvertListToModel(dbCon.GetDbSet<StudentsClassroom_Tbl>().ToList());
        }
        public List<Models.StudentsClassroomTBLModel> GetAllStudentsClassroom()
        {
            return listOfStudentsClassroom_Tbl;
        }
        public string InsertStudentsClassroom(Models.StudentsClassroomTBLModel studentsClassroom)
        {
            if (listOfStudentsClassroom_Tbl.Find(sc => sc.Student_ID == studentsClassroom.Student_ID) == null)
                try
                {
                    dbCon.Execute<StudentsClassroom_Tbl>(ConvertStudentsClassroomToEF(studentsClassroom),
                    DBConnection.ExecuteActions.Insert);
                    listOfStudentsClassroom_Tbl = ConvertListToModel(dbCon.GetDbSet<StudentsClassroom_Tbl>().ToList());
                    return listOfStudentsClassroom_Tbl.First(sc => sc.Student_ID == studentsClassroom.Student_ID).Student_ID;
                }
                catch (Exception ex)
                {
                    return "";
                }
            return listOfStudentsClassroom_Tbl.First(sc => sc.Student_ID == studentsClassroom.Student_ID).Student_ID;
        }
        public string UpdateStudentsClassroom(Models.StudentsClassroomTBLModel studentsClassroom)
        {
            if (listOfStudentsClassroom_Tbl.Find(sc => sc.Student_ID == studentsClassroom.Student_ID) != null)
                try
                {
                    dbCon.Execute<StudentsClassroom_Tbl>(ConvertStudentsClassroomToEF(studentsClassroom),
                    DBConnection.ExecuteActions.Update);
                    listOfStudentsClassroom_Tbl = ConvertListToModel(dbCon.GetDbSet<StudentsClassroom_Tbl>().ToList());
                    return listOfStudentsClassroom_Tbl.First(sc => sc.Student_ID == studentsClassroom.Student_ID).Student_ID;
                }
                catch (Exception ex)
                {
                    return "";
                }
            return listOfStudentsClassroom_Tbl.First(sc => sc.Student_ID == studentsClassroom.Student_ID).Student_ID;

        }
        public bool DeleteStudentsClassroom(string studentID)
        {
            StudentsClassroomTBLModel StudentsClassroom = listOfStudentsClassroom_Tbl.Find(s => s.Student_ID == studentID);

            if (listOfStudentsClassroom_Tbl.Find(sc => sc.Grade_Code == StudentsClassroom.Grade_Code) != null)
                try
                {
                    dbCon.Execute<StudentsClassroom_Tbl>(ConvertStudentsClassroomToEF(StudentsClassroom),
                    DBConnection.ExecuteActions.Delete);
                    listOfStudentsClassroom_Tbl = ConvertListToModel(dbCon.GetDbSet<StudentsClassroom_Tbl>().ToList());
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }

            return false;

        }



        #region convert functions

        public StudentsClassroom_Tbl ConvertStudentsClassroomToEF(Models.StudentsClassroomTBLModel a)
        {
            return new StudentsClassroom_Tbl
            {
                Student_ID = a.Student_ID,
                Grade_Code = a.Grade_Code
            };
        }

        public static Models.StudentsClassroomTBLModel ConvertStudentsClassroomToModel(StudentsClassroom_Tbl a)
        {
            return new Models.StudentsClassroomTBLModel
            {
                Student_ID = a.Student_ID,
                Grade_Code = a.Grade_Code
            };
        }
        public static List<Models.StudentsClassroomTBLModel> ConvertListToModel(List<StudentsClassroom_Tbl> li)
        {
            return li.Select(l => ConvertStudentsClassroomToModel(l)).ToList();
        }
        public static List<StudentsClassroom_Tbl> ConvertListToEF(List<Models.StudentsClassroomTBLModel> li)
        {
            List<StudentsClassroom_Tbl> l = new List<StudentsClassroom_Tbl>();
            StudentsClassroom_Tbl a = new StudentsClassroom_Tbl();
            foreach (var item in li)
            {
                a = new StudentsClassroom_Tbl();
                a.Student_ID = item.Student_ID;
                a.Grade_Code = item.Grade_Code;
                l.Add((a));
            }
            return l;
        }

        #endregion





    }
}
