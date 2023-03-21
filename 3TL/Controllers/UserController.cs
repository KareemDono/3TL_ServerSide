using _3TL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace _3TL.Controllers
{
    public class UserController : ApiController
    {
        public IHttpActionResult Get()
        {
            try
            {
                UserRole ur = UserRole.ADMIN;
                Users[] users = UsersBLL.GetAllUsersFromDB(ur);
                return Ok(users);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        public IHttpActionResult Get(int id)
        {
            try
            {
                Users user = UsersBLL.GetUserById(id);
                return Ok(user);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        public IHttpActionResult Post(int id)
        {
            try
            {
                return Ok(UsersBLL.GetUsersGreaterThenId(id));
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.Conflict, e);
            }
        }
    }
}
