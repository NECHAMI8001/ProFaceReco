using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BL.Classes;
using Intercom.Core;
using BL;

namespace API.Controllers
{

    [RoutePrefix("api/studentsClassroom")]
    public class StudentsClassroomController : ApiController
    {
        BL.Classes.StudentsClassroomBL studentsClassroomBL = new BL.Classes.StudentsClassroomBL();

        [AcceptVerbs("GET", "POST")]

        [HttpPost]
        [Route("signup")]
        public string SignUp(Models.StudentsClassroomTBLModel studentsClassroom)
        {
            return studentsClassroomBL.InsertStudentsClassroom(studentsClassroom);
        }
        [Route("updateStudentsClassroom")]
        [HttpPost]
        public string UpDateStudentsClassroom(Models.StudentsClassroomTBLModel studentsClassroom)
        {
            return studentsClassroomBL.UpdateStudentsClassroom(studentsClassroom);
        }

        [Route("delete/{studentsClassroom}")]
        [HttpGet]
        public void Delete(string studentsClassroom)
        {
            studentsClassroomBL.DeleteStudentsClassroom(studentsClassroom);
        }

        [HttpGet]
        [Route("getStudentsClassroom/{studentID}")]
        public Models.StudentsClassroomTBLModel GetStudentsClassroom(string studentID)
        {
            return studentsClassroomBL.GetAllStudentsClassroom().First(sc => sc.Student_ID == studentID);
        }































        //// GET: api/StudentsClassroom
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET: api/StudentsClassroom/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST: api/StudentsClassroom
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT: api/StudentsClassroom/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/StudentsClassroom/5
        //public void Delete(int id)
        //{
        //}
    }
}
