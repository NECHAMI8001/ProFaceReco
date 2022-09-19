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
    [RoutePrefix("api/grade")]

    public class GradeController : ApiController
    {
        BL.Classes.GradeBL gradeBL = new BL.Classes.GradeBL();

        [AcceptVerbs("GET", "POST")]

        [HttpPost]
        [Route("signup")]
        public int SignUp(Models.GradeTBLModel grade)
        {
            List<Models.GradeTBLModel> g = gradeBL.GetAllGrades();
            Models.GradeTBLModel c1 = new Models.GradeTBLModel();
            try
            {
                c1 = g.Find(r => r.Grade_Code == grade.Grade_Code);
            }
            catch (Exception ex)
            {
                return 0;
            }
            if (c1 == null)
                return gradeBL.InsertGrades(grade);
            else
                return 0;
        }
        //[HttpGet]
        //[Route("insertGrade")]
        //public bool InsertGrade(int gradeCode)
        //{

        //}
        //[HttpGet]
        //[Route("login/{gradeCode}/{password}")]
        //public long Login(int gradeCode, int password)
        //{
        //    return gradeBL.Login(gradeCode, password);
        //}


        [Route("updateGrades")]
        [HttpPost]
        public int UpDateGrade(Models.GradeTBLModel grade)
        {
            return gradeBL.UpdateGrades(grade);
        }

        [Route("delete/{grade}")]
        [HttpGet]
        public void Delete(int grade)
        {
            gradeBL.DeleteGrades(grade);
        }

        //[HttpGet]
        //[Route("getGrade/{gradeCode}")]
        //public Models.GradeTBLModel GetGrade(int gradeCode)
        //{
        //    return gradeBL.GetAllGrades().First(r => r.Grade_Code == gradeCode);
        //}
        [HttpGet]
        [Route("getGrade/{gradeCode}")]
        public bool GetGrade(int gradeCode, Models.GradeTBLModel gr)
        {
            List<Models.GradeTBLModel> g = gradeBL.GetAllGrades();
            Models.GradeTBLModel c1;
            try
            {
                c1 = g.First(r => r.Grade_Code == gradeCode);
            }
            catch
            {
                return false;
            }
            if (c1 != null)
            {
                if (SignUp(gr) != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }


            }

            return false;


            // gradeBL.GetAllGrades().First(r => r.Grade_Code == gradeCode);
        }

        //[HttpGet]
        //[Route("getGradeNameByCode/{gradeCode}")]
        //public Models.GradeTBLModel GetGradeNameByCode(int gradeCode)
        //{
        //    return gradeBL.GetGradeNameByCode(gradeCode);//.First(r => r.Grade_Code == GradeCode);
        //}

        [HttpGet]
        [Route("Login/{GradeCode}/{password}")]
        public string Login(int GradeCode, int password)
        {
            List<Models.GradeTBLModel> g1 = gradeBL.GetAllGrades();
            Models.GradeTBLModel c1 = new Models.GradeTBLModel();
            //   gradeBL.GetAllGrades().First(g => g.Grade_Code == GradeCode).Grade_Name;
            try
            {
                c1 = g1.FirstOrDefault(g => g.Grade_Code == GradeCode && g.Grade_Password == password);

            }
            catch
            {
                return "";
            }
            if (c1 != null)
                return c1.Grade_Name;
            return "";
        }
        //public string GetAllGrades(int GradeCode)
        //{
        //    return gradeBL.GetAllGrades().First(g => g.Grade_Code == GradeCode).Grade_Name;
        //}
        GeneralBL general = new GeneralBL();
        [HttpPost]
        [Route("loadDataStudent")]
        public void LoadDataStudent()
        {
            HttpPostedFile file = HttpContext.Current.Request.Files[0];
            string path = HttpContext.Current.Server.MapPath("~/Content/Files/" + file.FileName);
            file.SaveAs(path);
            //  int GradeCode = int.Parse(HttpContext.Current.Request.Params["GradeCode"]);
            general.PresenceStudent(path);
        }
    }
}
