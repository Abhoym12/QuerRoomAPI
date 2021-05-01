using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using QueryRoom.DTOs;
using System.Web.Http;
using QueryRoomAPI.Models;
using System.Web.Http.Cors;

namespace QueryRoom.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AccountAPIController : ApiController
    {
        QueryRoomDBEntities dbObj = new QueryRoomDBEntities();
        //Registration of user 
        public IHttpActionResult accRegForm(accountClass obj)                   
        {
            TBL_SIGNUP tbObj = new TBL_SIGNUP();
            if(ModelState.IsValid)
            {
                tbObj.NAME = obj.NAME;
                tbObj.EMAIL = obj.EMAIL;
                tbObj.USERNAME = obj.USERNAME;
                tbObj.PASSWORD = Crypto.Hash(obj.PASSWORD);
                tbObj.PHONE_NO = obj.PHONE_NO;
                tbObj.POINTS =5;
                dbObj.TBL_SIGNUP.Add(tbObj);
                dbObj.SaveChanges();

                var roles = new TBL_USERROLE();
                roles.ROLE = "User";
                roles.USERNAME = obj.USERNAME;
                dbObj.TBL_USERROLE.Add(roles);
                dbObj.SaveChanges();
                return Ok();
            }
            ModelState.Clear();
            return BadRequest();
            
        }

        //List of all users
        [HttpGet]
        public IEnumerable<accountClass> GetData()                                  
        {
            return dbObj.TBL_SIGNUP.ToList().Select(AutoMapper.Mapper.Map<TBL_SIGNUP,accountClass>);
        }

        //Get the role of user for WebRoleProvider class
        [HttpGet]
        [Route("GetRole/{username}")]
        public IHttpActionResult GetUserRole(string username)                   
        {
            if (String.IsNullOrEmpty(username))
            {
                return NotFound();
            }
            var userRole = dbObj.TBL_USERROLE.ToList().Find(x=>x.USERNAME==username);
            var role = AutoMapper.Mapper.Map<TBL_USERROLE,UserRoleDTO>(userRole);
            if (role==null)
            {
                return NotFound();
            }
            return Ok(role.ROLE);
        }
    }
}
