using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BPCCScheduler.Models;

namespace BPCCScheduler.Controllers
{   
    public class AppointmentController : Controller
    {
		private readonly IAppointmentRepository appointmentRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public AppointmentController() : this(new AppointmentRepository())
        {
        }

        public AppointmentController(IAppointmentRepository appointmentRepository)
        {
			this.appointmentRepository = appointmentRepository;
        }

        //
        // GET: /Appointment/

        public ViewResult Index()
        {
            return View(appointmentRepository.All);
        }

        //
        // GET: /Appointment/Details/5

        public ViewResult Details(int id)
        {
            return View(appointmentRepository.Find(id));
        }

        //
        // GET: /Appointment/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Appointment/Create

        [HttpPost]
        public ActionResult Create(Appointment appointment)
        {
            if (ModelState.IsValid) {
                appointmentRepository.InsertOrUpdate(appointment);
                appointmentRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }
        
        //
        // GET: /Appointment/Edit/5
 
        public ActionResult Edit(int id)
        {
             return View(appointmentRepository.Find(id));
        }

        //
        // POST: /Appointment/Edit/5

        [HttpPost]
        public ActionResult Edit(Appointment appointment)
        {
            if (ModelState.IsValid) {
                appointmentRepository.InsertOrUpdate(appointment);
                appointmentRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }

        //
        // GET: /Appointment/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(appointmentRepository.Find(id));
        }

        //
        // POST: /Appointment/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            appointmentRepository.Delete(id);
            appointmentRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                appointmentRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

