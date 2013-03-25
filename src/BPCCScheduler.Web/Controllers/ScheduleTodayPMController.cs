using BPCCScheduler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BPCCScheduler.Controllers
{
    public class ScheduleTodayPMController : ApiController
    {
        private readonly AppointmentContext _appointmentContext;

        // If you are using Dependency Injection, you can delete the following constructor
        public ScheduleTodayPMController()
            : this(new AppointmentContext())
        {
        }

        public ScheduleTodayPMController(AppointmentContext appointmentContext)
        {
            _appointmentContext = appointmentContext;
        }

        public IEnumerable<Appointment> Get ()
        {
            //any appointment today after noon, but before tonight midnight
            var tonightMidnight = DateTime.Now.Date.AddDays(1);
            var todayNoon = tonightMidnight.AddHours(-12);
            return _appointmentContext.Appointments.ToList()    //materialize for date convert
                .Where(i => i.Date.ToLocalTime() > todayNoon && i.Date.ToLocalTime() < tonightMidnight);            
        }
    }
}
