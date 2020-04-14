using System;
using System.Collections.Generic;

namespace Restful_Services.Models
{
    public partial class EventData
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Location { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string StartTimezone { get; set; }
        public string EndTimezone { get; set; }
        public bool? IsAllDay { get; set; }
        public bool? IsBlock { get; set; }
        public bool? IsReadOnly { get; set; }
        public int? FollowingID { get; set; }
        public string RecurrenceRule { get; set; }
        public int? RecurrenceID { get; set; }
        public string RecurrenceException { get; set; }
        public string Description { get; set; }
    }
}
