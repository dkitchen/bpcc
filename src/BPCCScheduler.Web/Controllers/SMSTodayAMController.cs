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
        public IEnumerable<SMSMessage> Get()        
        {
            //any appointment today after last-night midnight, but before today noon
            var tzi = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            var easternNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tzi);
            
            var lastNightMidnight = easternNow.Date;
            
            var todayNoon = lastNightMidnight.AddHours(12);
            
            var appts = base.AppointmentContext.Appointments.ToList()    //materialize for date conversion
                .Where(i => TimeZoneInfo.ConvertTimeFromUtc(i.Date, tzi) > lastNightMidnight 
                    && TimeZoneInfo.ConvertTimeFromUtc(i.Date, tzi) < todayNoon);                

            var messages = new List<SMSMessage>();
            foreach (var appt in appts)
            {
                var body = string.Format("BPCC Reminder: Appointment this morning at {0}",
                    TimeZoneInfo.ConvertTimeFromUtc(appt.Date, tzi).ToShortTimeString());
                var cell = string.Format("+1{0}", appt.Cell);
                messages.Add(base.SendSmsMessage(cell, body));
            }

            return messages;            
        }
    }
}
