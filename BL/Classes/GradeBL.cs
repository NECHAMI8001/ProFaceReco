using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Models;





namespace BL.Classes
{
    public class GradeBL : FaceRecognitionBL
    {

        DBConnection dbCon;
        List<Models.GradeTBLModel> listOfGrade_Tbl;


        public GradeBL()
        {
            dbCon = new DBConnection();
            listOfGrade_Tbl = ConvertListToModel(dbCon.GetDbSet<Grade_Tbl>().ToList());
        }

        public List<Models.GradeTBLModel> GetAllGrades()
        {
            return listOfGrade_Tbl;
        }
        public string GetGradeNameByCode(int GradeCode)
        {

            return listOfGrade_Tbl.Where(c => c.Grade_Code == GradeCode).Select(n => n.Grade_Name).ToList()[0];
        }
        public int InsertGrades(Models.GradeTBLModel grade)
        {
            if (listOfGrade_Tbl.Find(g => g.Grade_Code == grade.Grade_Code) == null)
                try
                {
                    dbCon.Execute<Grade_Tbl>(ConvertGradeToEF(grade),
                    DBConnection.ExecuteActions.Insert);
                    listOfGrade_Tbl = ConvertListToModel(dbCon.GetDbSet<Grade_Tbl>().ToList());
                    return listOfGrade_Tbl.First(c => c.Grade_Code == grade.Grade_Code).Grade_Code;
                }
                catch (Exception ex)
                {
                    return 0;
                }
            return listOfGrade_Tbl.First(c => c.Grade_Code == grade.Grade_Code).Grade_Code;
        }
        //public int Login(int gradeCode, int password)
        //{

        //}

        public int UpdateGrades(Models.GradeTBLModel grade)
        {
            if (listOfGrade_Tbl.Find(g => g.Grade_Code == grade.Grade_Code) != null)
                try
                {
                    dbCon.Execute<Grade_Tbl>(ConvertGradeToEF(grade),
                    DBConnection.ExecuteActions.Update);
                    listOfGrade_Tbl = ConvertListToModel(dbCon.GetDbSet<Grade_Tbl>().ToList());
                    return listOfGrade_Tbl.First(c => c.Grade_Code == grade.Grade_Code).Grade_Code;
                }
                catch (Exception ex)
                {
                    return 0;
                }
            return listOfGrade_Tbl.First(c => c.Grade_Code == grade.Grade_Code).Grade_Code;


        }
        public bool DeleteGrades(int gradeCode)
        {
            GradeTBLModel grade = listOfGrade_Tbl.Find(r => r.Grade_Code == gradeCode);

            if (listOfGrade_Tbl.Find(g => g.Grade_Code == grade.Grade_Code) != null)
                try
                {
                    dbCon.Execute<Grade_Tbl>(ConvertGradeToEF(grade),
                    DBConnection.ExecuteActions.Delete);
                    listOfGrade_Tbl = ConvertListToModel(dbCon.GetDbSet<Grade_Tbl>().ToList());
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }

            return false;

        }



        #region convert functions
        public Grade_Tbl ConvertGradeToEF(Models.GradeTBLModel a)
        {
            return new Grade_Tbl
            {
                Grade_Code = a.Grade_Code,
                Grade_Name = a.Grade_Name,
                Grade_Password = a.Grade_Password
            };
        }

        public static Models.GradeTBLModel ConvertGradeToModel(Grade_Tbl a)
        {
            return new Models.GradeTBLModel
            {
                Grade_Code = a.Grade_Code,
                Grade_Name = a.Grade_Name,
                Grade_Password = a.Grade_Password
            };
        }
        public static List<Models.GradeTBLModel> ConvertListToModel(List<Grade_Tbl> li)
        {
            return li.Select(l => ConvertGradeToModel(l)).ToList();
        }
        public static List<Grade_Tbl> ConvertListToEF(List<Models.GradeTBLModel> li)
        {
            List<Grade_Tbl> l = new List<Grade_Tbl>();
            Grade_Tbl a = new Grade_Tbl();
            foreach (var item in li)
            {
                a = new Grade_Tbl();
                a.Grade_Code = item.Grade_Code;
                a.Grade_Name = item.Grade_Name;
                a.Grade_Password = item.Grade_Password;
                l.Add((a));
            }
            return l;
        }
        #endregion
    }
}
