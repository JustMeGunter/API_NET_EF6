using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Test1API.Models;
using System.Reflection;

namespace Test1API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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

        private readonly DataContext _context;
        public testController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<test>>> Get()
        {
            return Ok(await _context.test.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<test>> Get(int id)
        {
            var test = await _context.test.FindAsync(id);
            if (test == null)
                return BadRequest("test not found");
            return Ok(test);
        }

        [HttpPost]
        public async Task<ActionResult<List<test>>> AddTest(test otest)
        {
            _context.test.Add(otest);
            await _context.SaveChangesAsync();
            return Ok(await _context.test.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<test>>> UpdateTest(test request)
        {
            var dbTest = await _context.test.FindAsync(request.Id);
            if (dbTest == null)
                return BadRequest("test not found");
            Type t = typeof(test);
            PropertyInfo[] arrProps = t.GetProperties();
            foreach (var prop in arrProps)
                if (prop.GetValue(dbTest) != prop.GetValue(request))
                    prop.SetValue(dbTest, prop.GetValue(request));
            await _context.SaveChangesAsync();
            return Ok(await _context.test.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<test>> DeleteTest(int id)
        {
            var test = await _context.test.FindAsync(id);
            if (test == null)
                return BadRequest("test not found");
            _context.test.Remove(test);
            await _context.SaveChangesAsync();
            return Ok(await _context.test.ToListAsync());
        }
    }
}