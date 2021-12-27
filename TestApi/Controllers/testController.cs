using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Test1API.Models;
using System.Reflection;

namespace Test1API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class testController : ControllerBase
    {
        private static List<test> testList = new List<test>
        {
            new test { 
                Id  = 1, 
                Name = "test1", 
                Description = "Descriptio test1", 
                Other = "Other info test1" 
            },
            new test { 
                Id  = 2, 
                Name = "test2", 
                Description = "Descriptio test2", 
                Other = "Other info test2" 
            } 
        };

        [HttpGet]
        public async Task<ActionResult<List<test>>> Get()
        {
            return Ok(testList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<test>> Get(int id)
        {
            var test = testList.Find(t => t.Id == id);
            if (test == null)
                return BadRequest("test not found");
            return Ok(test);
        }

        [HttpPost]
        public async Task<ActionResult<List<test>>> AddTest(test otest)
        {
            testList.Add(otest);
            return Ok(otest);
        }

        [HttpPut]
        public async Task<ActionResult<List<test>>> UpdateTest(test request)
        {
            var test = testList.Find(t => t.Id == request.Id);
            if (test == null)
                return BadRequest("test not found");
            Type t = typeof(test);
            PropertyInfo[] arrProps = t.GetProperties();
            foreach (var prop in arrProps)
                if (prop.GetValue(test) != prop.GetValue(request))
                    prop.SetValue(test, prop.GetValue(request));
            return Ok(test);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<test>> Get(int id)
        {
            var test = testList.Find(t => t.Id == id);
            if (test == null)
                return BadRequest("test not found");
            testList.Remove(test);
            return Ok(testList);
        }
    }
}