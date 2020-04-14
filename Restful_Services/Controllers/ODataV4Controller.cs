using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.ModelBinding;
using Microsoft.AspNet.OData;
using Restful_Services.Models;

namespace Restful_Services.Controllers
{
    public class ODataV4Controller : ODataController
    {
        private ScheduleDataContext db = new ScheduleDataContext();

        // GET: odata/ODataV4
        [EnableQuery]
        [AcceptVerbs("GET")]
        public IQueryable<EventData> GetODataV4()
        {
            return db.EventData;
        }

        // GET: odata/ODataV4(5)
        [EnableQuery]
        [AcceptVerbs("GET")]
        public IQueryable<EventData> GetODataV4(string StartDate, string EndDate)
        {
            DateTime start = DateTime.Parse(StartDate);
            DateTime end = DateTime.Parse(EndDate);
            return db.EventData.Where(evt => evt.StartTime >= start && evt.EndTime <= end);
        }

        // POST: odata/ODataV4
        [AcceptVerbs("POST", "OPTIONS")]
        public void Post([FromBody]EventData eventData)
        {
            if (ModelState.IsValid)
            {
                EventData insertData = new EventData();
                insertData.Id = (db.EventData.ToList().Count > 0 ? db.EventData.ToList().Max(p => p.Id) : 1) + 1;
                insertData.Subject = eventData.Subject;
                insertData.StartTime = Convert.ToDateTime(eventData.StartTime);
                insertData.EndTime = Convert.ToDateTime(eventData.EndTime);
                insertData.StartTimezone = eventData.StartTimezone;
                insertData.EndTimezone = eventData.EndTimezone;
                insertData.Location = eventData.Location;
                insertData.Description = eventData.Description;
                insertData.IsAllDay = eventData.IsAllDay;
                insertData.IsBlock = eventData.IsBlock;
                insertData.IsReadOnly = eventData.IsReadOnly;
                insertData.FollowingID = eventData.FollowingID;
                insertData.RecurrenceID = eventData.RecurrenceID;
                insertData.RecurrenceRule = eventData.RecurrenceRule;
                insertData.RecurrenceException = eventData.RecurrenceException;
                db.EventData.Add(insertData);
                db.SaveChanges();
            }
        }

        // PATCH: odata/ODataV4(5)
        [AcceptVerbs("PATCH", "MERGE", "OPTIONS")]
        public void Patch([FromBody]EventData eventData)
        {
            if (ModelState.IsValid)
            {
                EventData updateData = db.EventData.First(i => i.Id == Convert.ToInt32(eventData.Id));
                if (updateData != null)
                {
                    updateData.Subject = eventData.Subject;
                    updateData.StartTime = Convert.ToDateTime(eventData.StartTime);
                    updateData.EndTime = Convert.ToDateTime(eventData.EndTime);
                    updateData.StartTimezone = eventData.StartTimezone;
                    updateData.EndTimezone = eventData.EndTimezone;
                    updateData.Location = eventData.Location;
                    updateData.Description = eventData.Description;
                    updateData.IsAllDay = eventData.IsAllDay;
                    updateData.IsBlock = eventData.IsBlock;
                    updateData.IsReadOnly = eventData.IsReadOnly;
                    updateData.FollowingID = eventData.FollowingID;
                    updateData.RecurrenceID = eventData.RecurrenceID;
                    updateData.RecurrenceRule = eventData.RecurrenceRule;
                    updateData.RecurrenceException = eventData.RecurrenceException;
                    db.SaveChanges();
                }
            }
        }

        // DELETE: odata/ODataV4(5)
        [AcceptVerbs("DELETE", "OPTIONS")]
        public void Delete([FromODataUri]int key)
        {
            if (ModelState.IsValid)
            {
                EventData removeData = db.EventData.First(i => i.Id == key);
                if (removeData != null)
                {
                    db.EventData.Remove(removeData);
                    db.SaveChanges();
                }
            }
        }
    }
}
