using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        // GET: api/<EmpleadoController>
        [EnableCors("API")]
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            ML.Result result = BL.Empleado.GetAll();

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        // GET api/<EmpleadoController>/5
        [EnableCors("API")]
        [HttpGet("GetById/{idempleado}")]
        public IActionResult Get(int idempleado)
        {
            ML.Result result = BL.Empleado.GetById(idempleado);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        // POST api/<EmpleadoController>
        [EnableCors("API")]
        [HttpPost("Add")]
        public IActionResult Post([FromBody] ML.Empleado empleado)
        {
            ML.Result result = BL.Empleado.Add(empleado);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        // PUT api/<EmpleadoController>/5
        [EnableCors("API")]
        [HttpPut("Update/{idempleado}")]
        public IActionResult Put(int idempleado, [FromBody] ML.Empleado empleado)
        {
            empleado.IdEmpleado = idempleado;

            ML.Result result = BL.Empleado.Update(empleado);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        // DELETE api/<EmpleadoController>/5
        [EnableCors("API")]
        [HttpDelete("Delete/{idempleado}")]
        public IActionResult Delete(int idempleado)
        {
            if (idempleado > 0)
            {
                ML.Result result = BL.Empleado.Delete(idempleado);
                if (result.Correct)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return NotFound();
            }

        }
    }
}
