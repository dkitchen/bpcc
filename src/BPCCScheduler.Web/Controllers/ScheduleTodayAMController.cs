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
    public class ScheduleTodayAMController : AppointmentContextApiController
    {
        public IEnumerable<Appointment> Get()        
        {
            //any appointment today after last-night midnight, but before today noon
            var lastNightMidnight = base.EasternStandardTimeNow.Date;            
            var todayNoon = lastNightMidnight.AddHours(12);
            
            var appts = base.AppointmentContext.Appointments.ToList()    //materialize for date conversion
                .Where(i => base.ToEST(i.Date) > lastNightMidnight
                    && base.ToEST(i.Date) < todayNoon);            

            return appts;
        }
    }
}
