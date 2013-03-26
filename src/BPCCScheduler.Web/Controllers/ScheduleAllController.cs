using BPCCScheduler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BPCCScheduler.Controllers
{
    public class ScheduleAllController : AppointmentContextApiController
    {
        public IEnumerable<Appointment> Get()
        {
            return base.AppointmentContext.Appointments;
        }
    }
}
