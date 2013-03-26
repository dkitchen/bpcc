using BPCCScheduler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Twilio;

namespace BPCCScheduler.Controllers
{
    public class SMSTodayAMController : SMSApiController
    {
        //public IEnumerable<SMSMessage> Get()
        public string Get()
        {
            //any appointment today after last-night midnight, but before today noon
            var lastNightMidnight = DateTime.Now.Date;
            var ret = "";
            ret += lastNightMidnight.ToLongDateString();            
            var todayNoon = lastNightMidnight.AddHours(12);
            return ret;
            var appts = base.AppointmentContext.Appointments.ToList()    //materialize for date conversion
                .Where(i => i.Date.ToLocalTime() > lastNightMidnight && i.Date.ToLocalTime() < todayNoon);    

            var messages = new List<SMSMessage>();
            foreach (var appt in appts)
            {
                var body = string.Format("BPCC Reminder: Appointment this morning at {0}",
                    appt.Date.ToLocalTime().ToShortTimeString());
                var cell = string.Format("+1{0}", appt.Cell);
                messages.Add(base.SendSmsMessage(cell, body));
            }

            //return messages;
        }
    }
}
