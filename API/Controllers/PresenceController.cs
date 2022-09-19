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
    [RoutePrefix("api/presence")]

    public class PresenceController : ApiController
    {
        BL.Classes.PresenceBL presenceBL = new BL.Classes.PresenceBL();

        [AcceptVerbs("GET", "POST")]

        [HttpPost]
        [Route("signup")]
        public int SignUp(Models.PresenceTBLModel presence)
        {
            return presenceBL.InsertPresence(presence);
        }

        [Route("updatePresence")]
        [HttpPost]
        public int UpDatePresence(Models.PresenceTBLModel presence)
        {
            return presenceBL.UpdatePresence(presence);
        }

        [Route("delete/{grade}")]
        [HttpGet]
        public void Delete(int presence)
        {
            presenceBL.DeletePresence(presence);
        }

        //[HttpGet]
        //[Route("getPresence/{presenceCode}")]
        //public Models.PresenceTBLModel GetAllPresence(int presenceCode)
        //{
        //    return presenceBL.GetAllPresence().First(p => p.Presence_Code == presenceCode);
        //}

        [Route("GetAllPresence")]
        [HttpGet]
        //הפונקציה מביאה את כל המשתמשים הקימים במערכת
        public List<Models.PresenceTBLModel> GetAllPresence()
        {
            try
            {
                return presenceBL.GetAllPresence();
            }
            catch (Exception)
            {
                return null;
            }
        }




        [HttpPost]
        [Route("insertPresence")]
        public int insertPresence(Models.PresenceTBLModel presence)
        {
            try
            {
                presenceBL.InsertPresence(presence);
                return presence.Presence_Code;
            }
            catch (Exception)
            {
                return 0;
            }
        }















        //// GET: api/Presence
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET: api/Presence/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST: api/Presence
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT: api/Presence/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/Presence/5
        //public void Delete(int id)
        //{
        //}
    }
}
