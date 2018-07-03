using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using lesson_middleware.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace lesson_middleware.Controllers
{
    [Route("api/[controller]")]
    public class ExceptionsController : Controller
    {

        [HttpGet]
        public ActionResult Get()
        {
            throw new HttpStatusCodeException(400, "You need add params");
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            if (id != 418)
            {
                throw new Exception(id.ToString());
            }
            else
            {
                return StatusCode(418);
            }
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id)
        {
            var body = String.Empty;
            using (var ms = new MemoryStream())
            {
                this.Request.Body.CopyTo(ms);
                ms.Position = 0;
                using (var sr = new StreamReader(ms))
                {
                    body = sr.ReadToEnd();
                }
            }
            if (String.IsNullOrWhiteSpace(body))
            {
                throw new HttpStatusCodeException(400, "body is empty");
            }
            else
            {
                this.Response.StatusCode = 202;
                body = id + "-" + body;
                return Json(body);
            }
        }

    }
}
