using _3TL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace _3TL.Controllers
{
    public class UsersController : ApiController
    {
        [HttpGet]
        [Route("api/users")]
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

        [HttpGet]
        [Route("api/users/{id}")]
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

        [HttpPost]
        [Route("api/users")]
        public IHttpActionResult Post([FromBody] Users user)
        {
            try
            {
                int id = UsersBLL.CreateUser(user);
                return Ok(id);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpPut]
        [Route("api/users/{id}")]
        public IHttpActionResult Put(int id, [FromBody] Users user)
        {
            try
            {
                user.Id = id;
                UsersBLL.UpdateUser(user);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        [Route("api/users/{id}")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                UsersBLL.DeleteUser(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
