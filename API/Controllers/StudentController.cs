using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BL.Classes;
using Intercom.Core;
using BL;
using System.Web;

namespace API.Controllers
{

    [RoutePrefix("api/student")]
    public class StudentController : ApiController
    {
        BL.Classes.StudentBL studentBL = new BL.Classes.StudentBL();


        // [AcceptVerbs("GET", "POST")]

        //[HttpPost]
        //[Route("InsertStudent")]
        //public string InsertStudent(Models.StudentTBLModel student)
        //{
        //    return studentBL.InsertStudent(student);
        //}

        [Route("updateStudent")]
        [HttpPost]
        public string UpDateStudent(Models.StudentTBLModel student)
        {
            return studentBL.UpDateStudents(student);
        }

        [Route("delete/{student}")]
        [HttpDelete]

        public void Delete(string student)
        {
            studentBL.DeleteStudent(student);
        }

        //[HttpGet]
        //[Route("getStudent/{studentID}")]
        //public Models.StudentTBLModel GetStudent(string studentID)
        //{
        //    return studentBL.GetAllStudent().First(s => s.Student_ID == studentID);
        //}
        [Route("GetAllStudent")]
        [HttpGet]
        //הפונקציה מביאה את כל המשתמשים הקימים במערכת
        public List<Models.StudentTBLModel> GetAllStudent()
        {
            try
            {
                return studentBL.GetAllStudent();
            }
            catch (Exception)
            {
                return null;
            }
        }
        [Route("n/{num}")]
        [HttpPost]
        public int n(int num)
        {
            return num;
        }

        [HttpPost]
        [Route("insertStudent")]
        public string insertStudent(Models.StudentTBLModel student)
        {
            try
            {
                studentBL.InsertStudent(student);
                return student.Student_ID;
            }
            catch (Exception)
            {
                return "";
            }
        }
        //  StudentBL student = new StudentBL();

        [HttpPost]
        [Route("imageStudent")]
        public void ImageStudent()
        {
            HttpPostedFile file = HttpContext.Current.Request.Files[0];
            string path = HttpContext.Current.Server.MapPath("~/Content/Files/" + file.FileName);
            file.SaveAs(path);
            //  int GradeCode = int.Parse(HttpContext.Current.Request.Params["GradeCode"]);
            studentBL.GetPath(path);  //(path);
        }

    }
}
