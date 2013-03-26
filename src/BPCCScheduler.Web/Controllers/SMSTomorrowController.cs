using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Twilio;

namespace BPCCScheduler.Controllers
{
    public class SMSTomorrowController : SMSApiController
    {
        public IEnumerable<SMSMessage> Get()
        {
            //any appointment after tonight midnight, but before tomorrow midnight
            var tonightMidnight = DateTime.Now.AddDays(1);
            var tomorrowMidnight = tonightMidnight.AddDays(1);

            var appts = base.AppointmentContext.Appointments.ToList()    //materialize for date conversion
                .Where(i => i.Date.ToLocalTime() > tonightMidnight
                    && i.Date.ToLocalTime() < tomorrowMidnight);

            var messages = new List<SMSMessage>();
            foreach (var appt in appts)
            {
                var body = string.Format("BPCC Reminder: Appointment tomorrow at {0}",
                    appt.Date.ToLocalTime().ToShortTimeString());
                var cell = string.Format("+1{0}", appt.Cell);
                messages.Add(base.SendSmsMessage(cell, body));
            }

            return messages;
        }
    }
}
