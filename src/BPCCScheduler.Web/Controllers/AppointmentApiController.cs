using BPCCScheduler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BPCCScheduler.Controllers
{
    public class AppointmentApiController : ApiController
    {

        private readonly IAppointmentRepository _appointmentRepository;
        public AppointmentApiController(IAppointmentRepository appointmentRepository)
        {
            this._appointmentRepository = appointmentRepository;
        }

        // GET /api/appointmentapi
        public IQueryable<Appointment> Get()
        {
            return _appointmentRepository.All;
        }

        // GET /api/appointmentapi/5
        public Appointment Get(int id)
        {
            var appt = _appointmentRepository.Find(id);
            if (appt == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            return appt;
        }

        // POST /api/appointmentapi
        public HttpResponseMessage Post(Appointment value)
        {
            if (ModelState.IsValid)
            {
                _appointmentRepository.InsertOrUpdate(value);
                _appointmentRepository.Save();

                //Created!
                //var response = new  HttpResponseMessage<Appointment>(value, HttpStatusCode.Created);
                var response = Request.CreateResponse<Appointment>(HttpStatusCode.Created, value);


                //Let them know where the new appointment is
                string uri = Url.Route(null, new { id = value.Id });
                response.Headers.Location = new Uri(Request.RequestUri, uri);

                return response;

            }
            throw new HttpResponseException(HttpStatusCode.BadRequest);
        }

        // PUT /api/appointmentapi/5
        public HttpResponseMessage Put(int id, Appointment value)
        {
            if (ModelState.IsValid)
            {
                _appointmentRepository.InsertOrUpdate(value);
                _appointmentRepository.Save();
                return new HttpResponseMessage(HttpStatusCode.NoContent);
            }
            throw new HttpResponseException(HttpStatusCode.BadRequest);
        }

        // DELETE /api/appointmentapi/5
        public void Delete(int id)
        {
            var appt = _appointmentRepository.Find(id);
            if (appt == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _appointmentRepository.Delete(id);
        }


    }
}
