using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lesson_di.Services;
using Microsoft.AspNetCore.Mvc;

namespace lesson_di.Controllers
{
    [Route("api/[controller]")]
    public class DiController : Controller
    {
        
        private RandomSingleton _singleton { get; }
        private RandomScoped _scoped { get; }
        private RandomTransient _transient { get; }

        public DiController(
            RandomSingleton randomSingleton,
            RandomScoped randomScoped,
            RandomTransient randomTransient)
        {
            _singleton = randomSingleton;
            _scoped = randomScoped;
            _transient = randomTransient;
        }

        [HttpGet]
        public JsonResult Get(
            [FromServices]RandomSingleton randomSingleton,
            [FromServices]RandomScoped randomScoped,
            [FromServices]RandomTransient randomTransient)
        {
            return Json(new Result(_singleton, _scoped, _transient, randomSingleton, randomScoped, randomTransient));
        }

        private class Result
        {
            public Result(
                RandomSingleton singl,
                RandomScoped sco,
                RandomTransient tran,
                RandomSingleton singl2,
                RandomScoped sco2,
                RandomTransient tran2)
            {
                SingletonResult = $"{singl.Value}-{singl2.Value}";
                ScopedResult = $"{sco.Value}-{sco2.Value}";
                TransientResult = $"{tran.Value}-{tran2.Value}";
            }
            public string SingletonResult { get; }
            public string ScopedResult { get; }
            public string TransientResult { get; }
        }




    }
}
