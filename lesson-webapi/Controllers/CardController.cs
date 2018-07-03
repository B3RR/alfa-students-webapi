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
    public class CardController : Controller
    {
        private IHttpContextAccessor _accessor { get; }
        private SQLContext _context { get; }

        public CardController(IHttpContextAccessor accessor, SQLContext context)
        {
            _accessor = accessor;
            _context = context;
        }

        [HttpPut("{cvv}")]
        public ActionResult Put(int cvv,[FromBody] string number)
        {
                var card=_context.Cards.Where(x=>x.Number==number).FirstOrDefault();
                if (card!=null)
                {
                    if (card.CVV==cvv)
                    {
                        return Json("OK");
                    }
                    else
                    {
                        return StatusCode(401,"no rules");
                    }
                }
                else
                {
                    return StatusCode(400,"Card not found");
                }
        }
    }
}