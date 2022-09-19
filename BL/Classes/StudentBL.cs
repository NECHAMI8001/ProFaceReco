using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Models;









namespace BL.Classes
{
    public class StudentBL : FaceRecognitionBL
    {
        DBConnection dbCon;
        string Mis = "";
        List<Models.StudentTBLModel> listOfStudent_Tbl;
        GeneralBL generalBL = new GeneralBL();

        public StudentBL()
        {

            dbCon = new DBConnection();
            listOfStudent_Tbl = ConvertListToModel(dbCon.GetDbSet<Student_Tbl>().ToList());

        }


        public List<Models.StudentTBLModel> GetAllStudent()
        {
            return listOfStudent_Tbl;
        }
        public string InsertStudent(Models.StudentTBLModel student)
        {
            if (listOfStudent_Tbl.Find(s => s.Student_ID == student.Student_ID) == null)
                try
                {
                    dbCon.Execute<Student_Tbl>(ConvertStudentToEF(student),
                    DBConnection.ExecuteActions.Insert);
                    listOfStudent_Tbl = ConvertListToModel(dbCon.GetDbSet<Student_Tbl>().ToList());
                    return listOfStudent_Tbl.First(s => s.Student_ID == student.Student_ID).Student_ID;
                }
                catch (Exception ex)
                {
                    return "";
                }
            return listOfStudent_Tbl.First(s => s.Student_ID == student.Student_ID).Student_ID;
        }

        public string UpDateStudents(Models.StudentTBLModel student)
        {
            if (listOfStudent_Tbl.Find(s => s.Student_ID == student.Student_ID) != null)
                try
                {
                    dbCon.Execute<Student_Tbl>(ConvertStudentToEF(student),
                    DBConnection.ExecuteActions.Update);
                    listOfStudent_Tbl = ConvertListToModel(dbCon.GetDbSet<Student_Tbl>().ToList());
                    return listOfStudent_Tbl.First(s => s.Student_ID == student.Student_ID).Student_ID;
                }
                catch (Exception ex)
                {
                    return "";
                }
            return listOfStudent_Tbl.First(s => s.Student_ID == student.Student_ID).Student_ID;
        }


        public bool DeleteStudent(string studentID)
        {
            StudentTBLModel student = listOfStudent_Tbl.Find(s => s.Student_ID == studentID);

            if (listOfStudent_Tbl.Find(s => s.Student_ID == student.Student_ID) != null)
                try
                {
                    dbCon.Execute<Student_Tbl>(ConvertStudentToEF(student),
                    DBConnection.ExecuteActions.Delete);
                    listOfStudent_Tbl = ConvertListToModel(dbCon.GetDbSet<Student_Tbl>().ToList());
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }

            return false;

        }

        public void GetPath(string imPath)
        {
            //  File.Create(@"C:\Users\User\Pictures\imagePATH.txt");
            File.AppendAllText(@"C:\Users\User\Pictures\imagePATH.txt", imPath);
            //File.WriteAllText(@"C:\Users\User\Pictures\imagePATH.txt", imPath);
            //Mis = imPath;
        }

        #region convert functions

        public Student_Tbl ConvertStudentToEF(Models.StudentTBLModel a)
        {
            //Mis=File.ReadAllText()
            string pathIMG = File.ReadAllText(@"C:\Users\User\Pictures\imagePATH.txt");
            File.Delete(@"C:\Users\User\Pictures\imagePATH.txt");
            return new Student_Tbl
            {
                Student_ID = a.Student_ID,
                Student_Name = a.Student_Name,
                //  Student_Image = a.Student_Image,

                Student_Image = pathIMG,
                Student_Grade_Code = a.Student_Grade_Code,
                //DistancePoint=a.DistancePoint,

                DistPoint = generalBL.landmarkDetection(pathIMG)

                //  DistPoint = a.DistPoint
            };


        }

        public static List<Student_Tbl> ConvertListToEF(List<Models.StudentTBLModel> li)
        {
            List<Student_Tbl> l = new List<Student_Tbl>();
            Student_Tbl a = new Student_Tbl();
            foreach (var item in li)
            {
                a = new Student_Tbl();
                a.Student_ID = item.Student_ID;
                a.Student_Name = item.Student_Name;
                a.Student_Image = item.Student_Image;
                a.Student_Grade_Code = item.Student_Grade_Code;
                a.DistancePoint = item.DistancePoint;
                a.DistPoint = item.DistPoint;
                l.Add((a));
            }
            return l;
        }

        public static List<Models.StudentTBLModel> ConvertListToModel(List<Student_Tbl> li)
        {
            return li.Select(l => ConvertStudentToModel(l)).ToList();
        }

        public static Models.StudentTBLModel ConvertStudentToModel(Student_Tbl a)
        {
            return new Models.StudentTBLModel
            {
                Student_ID = a.Student_ID,
                Student_Name = a.Student_Name,
                Student_Image = a.Student_Image,
                Student_Grade_Code = a.Student_Grade_Code,
                DistancePoint = a.DistancePoint,
                DistPoint = a.DistPoint

            };
        }
        #endregion
    }
}
