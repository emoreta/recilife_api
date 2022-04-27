using recilife_api.Context;
using recilife_api.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace recilife_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StateController : ControllerBase
    {
        private DBContextRecilife _dbContextRecilife;
        public StateController(DBContextRecilife context)
        {
            _dbContextRecilife = context;
        }
        [HttpGet]
        [Route("getState/")]
        public ActionResult<List<State>> Get()
        {
            return _dbContextRecilife.State.ToList();
        }
        [HttpGet]
        [Route("getbyid/{id}")]
        public ActionResult<State> Get(int id)
        {
            if (id == 0)
            {
                return NotFound("State id must be higher than zero");
            }
            State ob = _dbContextRecilife.State.FirstOrDefault(s => s.id == id);
            if (ob == null)
            {
                return NotFound(" State not found");
            }
            return Ok(ob);
        }
        [HttpPost]
        [Route("insert/")]
        public async Task<ActionResult> Post([FromBody] State state)
        {
            if (state == null)
            {
                return NotFound("state data is not supplied");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _dbContextRecilife.State.AddAsync(state);
            await _dbContextRecilife.SaveChangesAsync();
            return Ok(state);
        }
        [HttpPut]
        [Route("update/")]
        public async Task<ActionResult> Update([FromBody] State state)
        {
            if (state == null)
            {
                return NotFound("state data is not supplied");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            State ob = _dbContextRecilife.State.FirstOrDefault(s => s.id == state.id);
            if (ob == null)
            {
                return NotFound("State does not exist in the database");
            }
            ob.active = state.active;
            ob.description = state.description;
            ob.field = state.field;
            ob.id = state.id;
            ob.name = state.name;
            _dbContextRecilife.Attach(ob).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _dbContextRecilife.SaveChangesAsync();
            return Ok(_dbContextRecilife);
        }
    }
}
