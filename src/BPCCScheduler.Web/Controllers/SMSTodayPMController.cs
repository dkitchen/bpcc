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
    public class SMSTodayPMController : SMSApiController
    {

        public IEnumerable<SMSMessage> Get()
        {
            //any appointment today after noon, but before tonight midnight
            var tonightMidnight = base.EasternStandardTimeNow.Date.AddDays(1);
            var todayNoon = tonightMidnight.AddHours(-12);
            var appts = base.AppointmentContext.Appointments.ToList()    //materialize for date convert
                .Where(i => base.ToEST(i.Date) > todayNoon && base.ToEST(i.Date) < tonightMidnight); 

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
