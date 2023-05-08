using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestfulServices.Models;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Formatter;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RestfulServices.Controllers
{
    //[Route("api/")]
    //[ApiController]
    public class EventsController : ODataController
    {
        private ScheduleDataContext _db;
        // GET: api/<EventsController>
        public EventsController(ScheduleDataContext context)
        {
            _db = context;
            if (context.EventsData.Count() == 0)
            {
                foreach (var b in DataSource.GetEvents())
                {
                    context.EventsData.Add(b);
                }
                context.SaveChanges();
            }

        }
        [HttpGet]
        [EnableQuery]
        public IActionResult Get()
        {
            return Ok(_db.EventsData);
        }

        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        public async Task Post([FromBody] EventData events)
        {
            _db.EventsData.Add(events);
            await _db.SaveChangesAsync();
        }

        public async Task Put([FromODataUri] int key, [FromBody] EventData events)
        {
            var entity = await _db.EventsData.FindAsync(events.Id);
            if (entity != null)
            {
                _db.Entry(entity).CurrentValues.SetValues(events);
                await _db.SaveChangesAsync();
            }
        }

        public async Task Patch([FromODataUri] int key, [FromBody] EventData events)
        {
            var entity = await _db.EventsData.FindAsync(key);
            //events.Patch(entity);
            if (entity != null)
            {
                _db.Entry(entity).CurrentValues.SetValues(events);
                await _db.SaveChangesAsync();
            }
        }

        public async Task Delete([FromODataUri] int key)
        {
            var od = _db.EventsData.Find(key);
            if (od != null)
            {
                _db.EventsData.Remove(od);
                await _db.SaveChangesAsync();
            }
        }
    }
}
