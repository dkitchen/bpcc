using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BPCCScheduler.Controllers.BaseControllers;
using BPCCScheduler.Models;
using Twilio;

namespace BPCCScheduler.Controllers
{
    public class ScheduleTomorrowController : AppointmentContextApiController
    {
        public IEnumerable<Appointment> Get()
        {
            //any appointment after tonight midnight, but before tomorrow midnight
            var tonightMidnight = base.EasternStandardTimeNow.AddDays(1);
            var tomorrowMidnight = tonightMidnight.AddDays(1);

            var appts = base.AppointmentContext.Appointments.ToList()    //materialize for date conversion
                .Where(i => base.ToEST(i.Date) > tonightMidnight
                    && base.ToEST(i.Date) < tomorrowMidnight);
            
            return appts;
        }
    }
}
