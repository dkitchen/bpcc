using BPCCScheduler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BPCCScheduler.Controllers
{
    public class ScheduleTodayAMController : ApiController
    {
        private readonly AppointmentContext _appointmentContext;

        // If you are using Dependency Injection, you can delete the following constructor
        public ScheduleTodayAMController()
            : this(new AppointmentContext())
        {
        }

        public ScheduleTodayAMController(AppointmentContext appointmentContext)
        {
            _appointmentContext = appointmentContext;
        }

        public IEnumerable<Appointment> Get ()
        {
            //any appointment today after last-night midnight, but before today noon
            var lastNightMidnight = DateTime.Now.Date;
            var todayNoon = lastNightMidnight.AddHours(12);
            return _appointmentContext.Appointments.ToList()    //materialize for date conversion
                .Where(i => i.Date.ToLocalTime() > lastNightMidnight && i.Date.ToLocalTime() < todayNoon);            
        }
    }
}
