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
    public class ScheduleTodayPMController : AppointmentContextApiController
    {

        public IEnumerable<Appointment> Get()
        {
            //any appointment today after noon, but before tonight midnight
            var tonightMidnight = base.EasternStandardTimeNow.Date.AddDays(1);
            var todayNoon = tonightMidnight.AddHours(-12);
            var appts = base.AppointmentContext.Appointments.ToList()    //materialize for date convert
                .Where(i => base.ToEST(i.Date) > todayNoon && base.ToEST(i.Date) < tonightMidnight); 

            return appts;
        }
    }
}
