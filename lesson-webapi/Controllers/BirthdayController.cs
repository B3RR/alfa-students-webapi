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
    public class BirthdayController : Controller
    {
        private IHttpContextAccessor _accessor { get; }
        private SQLContext _context { get; }

        public BirthdayController(IHttpContextAccessor accessor, SQLContext context)
        {
            _accessor = accessor;
            _context = context;
        }

        [HttpGet]
        public JsonResult Get([FromQuery]int date)
        {
            try
            {
                var birthday = DateTime.ParseExact(date.ToString(), "yyyyMMdd",
                                       System.Globalization.CultureInfo.InvariantCulture);
                var ip=_accessor.HttpContext.Connection.RemoteIpAddress.ToString();
                var student = new Student()
                {
                    IpAddress = ip,
                    Birthday = birthday
                };
                var exist=_context.Students.Where(x => x.IpAddress == ip).FirstOrDefault();
                if (exist!=null)
                {
                    throw new Exception($"{birthday.ToString("dd.MM.yyyy")} - is exist in database, you id {exist.Id}");
                }
                else
                {
                    _context.Students.Add(student);
                    var rnd=new Random();
                    var cardNumber="12345678"+birthday.ToString("yyyyMMdd");
                    _context.Cards.Add(new Card{Number=cardNumber,CVV=rnd.Next(1,99)});
                    _context.SaveChanges();
                    return Json($"Дергая данное webapi вы соглашаетесь на передачу персональных данных. Ваш id: {student.Id}");
                }
            }
            catch (Exception ex)
            {
                this.Response.StatusCode = 400;
                return Json(ex.Message);
            }
        }
    }
}