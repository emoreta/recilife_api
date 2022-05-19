using recilife_api.Context;
using recilife_api.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace recilife_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeesionTypeController : ControllerBase
    {
        private DBContextRecilife _dbContextRecilife;

        public SeesionTypeController(DBContextRecilife context)
        {
            _dbContextRecilife = context;
        }

        [HttpGet]
        [Route("getSeesionType/")]
        public ActionResult<List<SeesionType>> Get()
        {
            return _dbContextRecilife.SeesionType.ToList();
        }
        [HttpGet]
        [Route("getbyid/{id}")]
        public ActionResult<SeesionType> Get(int id)
        {
            if (id == 0)
            {
                return NotFound("SeesionType id must be higher than zero");
            }
            SeesionType ob = _dbContextRecilife.SeesionType.FirstOrDefault(s => s.Id == id);
            if (ob == null)
            {
                return NotFound(" SeesionType not found");
            }
            return Ok(ob);
        }

        [HttpPost]
        [Route("insert/")]
        public async Task<ActionResult> Post([FromBody] SeesionType seesionType)
        {
            if (seesionType == null)
            {
                return NotFound("seesionType data is not supplied");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _dbContextRecilife.SeesionType.AddAsync(seesionType);
            await _dbContextRecilife.SaveChangesAsync();
            return Ok(seesionType);
        }

        [HttpPut]
        [Route("update/")]
        public async Task<ActionResult> Update([FromBody] SeesionType seesionType)
        {
            if (seesionType == null)
            {
                return NotFound("seesionType data is not supplied");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            SeesionType ob = _dbContextRecilife.SeesionType.FirstOrDefault(s => s.Id == seesionType.Id);
            if (ob == null)
            {
                return NotFound("SeesionType does not exist in the database");
            }
            ob.description = seesionType.description;
            ob.name = seesionType.name;
            _dbContextRecilife.Attach(ob).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _dbContextRecilife.SaveChangesAsync();
            return Ok(ob);
        }


    }
}
