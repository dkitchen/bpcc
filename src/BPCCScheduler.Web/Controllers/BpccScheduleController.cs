using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BPCCScheduler.Models;

namespace BPCCScheduler.Controllers
{

    public class BpccScheduleController : ApiController
    {
        //Quick Guideline for REST:
        //GET /api/objects : List objects
        //GET /api/objects/1 : Get the object with id == 1
        //POST /api/objects : Create a new object with posted data (full model)
        //PUT /api/objects/1 : Update object with id == 1 with posted data (full model)
        //PATCH /api/objects/1 : Update object with id == 1 with partial data (partial model)
        //POST /api/objects/1/someprocedure : Run some procedure on object with id == 1

        
        private readonly AppointmentContext _appointmentContext;

        // If you are using Dependency Injection, you can delete the following constructor
        public BpccScheduleController()
            : this(new AppointmentContext())
        {
        }

        public BpccScheduleController(AppointmentContext appointmentContext)
        {
            _appointmentContext = appointmentContext;
        }




        //save a whole list of appointments in one go
        public HttpResponseMessage Put(IEnumerable<Appointment> schedule)
        {
            if (ModelState.IsValid)
            {
                //first, clear all the data (TODO, this is dangerous, we should be updating any existing)
                foreach (var item in _appointmentContext.Appointments)
                {
                    _appointmentContext.Appointments.Remove(item);
                }

                foreach (var appointment in schedule)
                {
                    _appointmentContext.Appointments.Add(appointment);
                }

                //save changes
                var saveResult = _appointmentContext.SaveChanges();
                return new HttpResponseMessage(HttpStatusCode.NoContent);
            }
            throw new HttpResponseException(HttpStatusCode.BadRequest);
        }
    }
}
