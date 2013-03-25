using BPCCScheduler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BPCCScheduler.Controllers
{
    public class ScheduleTomorrowController : ApiController
    {
        private readonly AppointmentContext _appointmentContext;

        // If you are using Dependency Injection, you can delete the following constructor
        public ScheduleTomorrowController()
            : this(new AppointmentContext())
        {
        }

        public ScheduleTomorrowController(AppointmentContext appointmentContext)
        {
            _appointmentContext = appointmentContext;
        }

        public IEnumerable<Appointment> Get ()
        {
            //any appointment after tonight midnight, but before tomorrow midnight
            var tonightMidnight = DateTime.UtcNow.Date.AddDays(1);
            var tomorrowMidnight = tonightMidnight.AddDays(1);
            
            return _appointmentContext.Appointments
                .Where(i => i.Date > tonightMidnight && i.Date < tomorrowMidnight);            
        }
    }
}
