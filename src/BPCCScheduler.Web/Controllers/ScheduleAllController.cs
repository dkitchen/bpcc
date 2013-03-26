using BPCCScheduler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BPCCScheduler.Controllers.BaseControllers;

namespace BPCCScheduler.Controllers
{
    public class ScheduleAllController : AppointmentContextApiController
    {
        public IEnumerable<Appointment> Get()
        {
            return base.AppointmentContext.Appointments;
        }

        //save a whole list of appointments in one go
        public HttpResponseMessage Put(IEnumerable<Appointment> schedule)
        {
            if (ModelState.IsValid)
            {
                //first, clear all the data (TODO, this is dangerous, we should be updating any existing)
                foreach (var item in base.AppointmentContext.Appointments)
                {
                    base.AppointmentContext.Appointments.Remove(item);
                }

                foreach (var appointment in schedule)
                {
                    base.AppointmentContext.Appointments.Add(appointment);
                }

                //save changes
                var saveResult = base.AppointmentContext.SaveChanges();
                return new HttpResponseMessage(HttpStatusCode.NoContent);
            }
            throw new HttpResponseException(HttpStatusCode.BadRequest);
        }
    }
}
