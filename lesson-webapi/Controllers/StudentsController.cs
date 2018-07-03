using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lesson_webapi.Models;
using lesson_webapi.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace lesson_webapi.Controllers
{
    [Route("api/[controller]")]
    public class StudentsController : Controller
    {
        private IHttpContextAccessor _accessor { get; }
        private SQLContext _context { get; }

        public StudentsController(IHttpContextAccessor accessor, SQLContext context)
        {
            _accessor = accessor;
            _context = context;
        }

        [HttpGet]
        public IList<Student> Get()
        {
            return _context.Students.Where(x => x.isDel != true).ToList();
        }


        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            var student = _context.Students.Where(x => x.Id == id).FirstOrDefault();
            if (id > 0 || student != null)
            {
                return Json(student);
            }
            else
            {
                this.Response.StatusCode = 404;
                return Json("student not found");
            }
        }
        [HttpPost]
        public ActionResult Post([FromBody]Student value)
        {
            if (value != null)
            {
                var ip = _accessor.HttpContext.Connection.RemoteIpAddress.ToString();
                var row = _context.Students.Where(x => x.IpAddress == ip).FirstOrDefault();
                if (!String.IsNullOrWhiteSpace(row.Surname)
                    ||!String.IsNullOrWhiteSpace(row.Firstname)
                    ||!String.IsNullOrWhiteSpace(row.Middlename))
                {
                    return StatusCode(400, "Student is alredy created");
                }
                if (row != null)
                {
                    row.Firstname = value.Firstname;
                    row.Middlename = value.Middlename;
                    row.Surname = value.Surname;
                    row.isDel = false;
                    _context.SaveChanges();
                    return Json(row);
                }
                else
                {
                    return StatusCode(400, "the first task was not done correctly");
                }
            }
            else
            {
                this.Response.StatusCode = 400;
                return Json("input student is null");
            }
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody]Student value)
        {
            if (value != null)
            {
                var ip = _accessor.HttpContext.Connection.RemoteIpAddress.ToString();
                var row = _context.Students.Where(x => x.IpAddress == ip).FirstOrDefault();
                if (row != null && row.Id == id)
                {
                    row.Firstname = value.Firstname;
                    row.Middlename = value.Middlename;
                    row.Surname = value.Surname;
                    row.isDel = false;
                    _context.SaveChanges();
                    return Json(row);
                }
                else
                {
                    return StatusCode(401, "you didn't have rules");
                }
            }
            else
            {
                this.Response.StatusCode = 400;
                return Json("input student is null");
            }

            // value.IpAddress = _accessor.HttpContext.Connection.RemoteIpAddress.ToString();
            // _context.Entry(value).State = EntityState.Modified;
            // _context.SaveChanges();

        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var student = _context.Students.Where(x => x.Id == id).FirstOrDefault();
            if (student != null)
            {
                student.isDel = true;
                _context.SaveChanges();
                return StatusCode(202, "delete success");
            }
            else
            {
                return StatusCode(400, "student not found");
            }

        }

    }
}