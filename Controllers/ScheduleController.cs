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
    public class ScheduleController : ControllerBase
    {
        private DBContextRecilife _dbContextRecilife;
        public ScheduleController(DBContextRecilife context)
        {
            _dbContextRecilife = context;
        }
        [HttpGet]
        [Route("getSchedule/")]
        public ActionResult<List<Schedule>> Get()
        {
            return _dbContextRecilife.Schedule.ToList();
        }
        [HttpGet]
        [Route("getbyid/{id}")]
        public ActionResult<Schedule> Get(int id)
        {
            if (id == 0)
            {
                return NotFound("Schedule id must be higher than zero");
            }
            Schedule ob = _dbContextRecilife.Schedule.FirstOrDefault(s => s.Id == id);
            if (ob == null)
            {
                return NotFound(" Schedule not found");
            }
            return Ok(ob);
        }
        [HttpPost]
        [Route("insert/")]
        public async Task<ActionResult> Post([FromBody] Schedule schedule)
        {
            if (schedule == null)
            {
                return NotFound("schedule data is not supplied");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _dbContextRecilife.Schedule.AddAsync(schedule);
            await _dbContextRecilife.SaveChangesAsync();
            return Ok(schedule);
        }
        [HttpPut]
        [Route("update/")]
        public async Task<ActionResult> Update([FromBody] Schedule schedule)
        {
            if (schedule == null)
            {
                return NotFound("schedule data is not supplied");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Schedule ob = _dbContextRecilife.Schedule.FirstOrDefault(s => s.Id == schedule.Id);
            if (ob == null)
            {
                return NotFound("Schedule does not exist in the database");
            }
            ob.Active = schedule.Active;
            ob.Id = schedule.Id;
            ob.Start = schedule.Start;
            ob.End = schedule.End;
            _dbContextRecilife.Attach(ob).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _dbContextRecilife.SaveChangesAsync();
            return Ok(ob);
        }
    }
}
