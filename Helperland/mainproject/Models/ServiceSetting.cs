using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mainproject.Models
{
    public class ServiceSetting
    {
        public int Id { get; set; }
        public int ActionType { get; set; }
        public int Interval { get; set; }
        public TimeSpan ScheduleTime { get; set; }
        public DateTime LastPoll { get; set; }
    }
}
