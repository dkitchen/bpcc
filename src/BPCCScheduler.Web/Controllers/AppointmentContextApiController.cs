using BPCCScheduler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace BPCCScheduler.Controllers
{
    public class AppointmentContextApiController : ApiController
    {
        public AppointmentContext AppointmentContext { get; set; }

        // If you are using Dependency Injection, you can delete the following constructor
        public AppointmentContextApiController()
            : this(new AppointmentContext())
        {
        }

        public AppointmentContextApiController(AppointmentContext appointmentContext)
        {
            this.AppointmentContext = appointmentContext;
        }
    }
}