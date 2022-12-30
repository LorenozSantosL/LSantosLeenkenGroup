using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntidadFederativaController : ControllerBase
    {
        // GET: api/<EntidadFederativaController>
        [EnableCors("API")]
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            ML.Result result = BL.EntidadFederativa.GetAll();

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        // GET api/<EntidadFederativaController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<EntidadFederativaController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<EntidadFederativaController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<EntidadFederativaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
