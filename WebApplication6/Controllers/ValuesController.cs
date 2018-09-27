using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication6.Filters;
using WebApplication6.Models;

namespace WebApplication6.Controllers
{
   // [RoutePrefix("api")]
    [BasicAuthentication]
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<Employee> Get()
        {
            using (DBConn conn = new DBConn())
            {
                return conn.Employees.ToList();
            }
            //return new string[] { "value1", "value2" };
        }

        //// GET api/values/5
        //public string Get(int id)
        //{
        //    return "value";
        //}


        // GET api/values/5
        public IHttpActionResult Get(int id)  //berjaya yahooo
        {
            using (DBConn conn = new DBConn())
            {
                var result = conn.Employees.FirstOrDefault(x => x.EmployeeID == id);
                return Ok(result);
            }
        }

        // POST api/values
       // [HttpPost]
        //[Route("Post")]
        public HttpResponseMessage Post([FromBody] Employee employee)
        {
            try
            {
                using (DBConn conn = new DBConn())
                {
                    conn.Employees.Add(employee);
                    conn.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, employee);
                    message.Headers.Location = new Uri(Request.RequestUri + employee.EmployeeID.ToString());
                    return message;
                }
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
