using QueryRoomAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace QueryRoom.Controllers.API_controllers
{

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class LoginAPIController : ApiController
    {
        QueryRoomDBEntities db = new QueryRoomDBEntities();

        //API for logging in
        public IHttpActionResult PostLogin(LoginData obj)
        {
            var user = db.TBL_SIGNUP.ToList().Find(x=>x.USERNAME== obj.USERNAME);
            
            if (user!=null && user.PASSWORD==Crypto.Hash(obj.PASSWORD))
            {
                var userRole = db.TBL_USERROLE.ToList().Find(x => x.USERNAME == user.USERNAME);
                return Ok(userRole.ROLE);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
