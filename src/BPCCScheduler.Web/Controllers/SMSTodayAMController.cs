using BPCCScheduler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Twilio;
using BPCCScheduler.Controllers.BaseControllers;

namespace BPCCScheduler.Controllers
{
    public class SMSTodayAMController : SMSApiController
    {
        public IEnumerable<SMSMessage> Get()        
        {
            //any appointment today after last-night midnight, but before today noon
            var lastNightMidnight = base.EasternStandardTimeNow.Date;            
            var todayNoon = lastNightMidnight.AddHours(12);
            
            var appts = base.AppointmentContext.Appointments.ToList()    //materialize for date conversion
                .Where(i => base.ToEST(i.Date) > lastNightMidnight
                    && base.ToEST(i.Date) < todayNoon);                

            var messages = new List<SMSMessage>();
            foreach (var appt in appts)
            {
                var body = string.Format("BPCC Reminder: Appointment this morning at {0}",
                    base.ToEST(appt.Date).ToShortTimeString());
                var cell = string.Format("+1{0}", appt.Cell);
                messages.Add(base.SendSmsMessage(cell, body));
            }

            return messages;            
        }
    }
}
