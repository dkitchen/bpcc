using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace BPCCScheduler.Models
{ 
    public class AppointmentRepository : IAppointmentRepository
    {
        AppointmentContext context = new AppointmentContext();

        public IQueryable<Appointment> All
        {
            get { return context.Appointments; }
        }

        public IQueryable<Appointment> AllIncluding(params Expression<Func<Appointment, object>>[] includeProperties)
        {
            IQueryable<Appointment> query = context.Appointments;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Appointment Find(int id)
        {
            return context.Appointments.Find(id);
        }

        public void InsertOrUpdate(Appointment appointment)
        {
            if (appointment.Id == default(int)) {
                // New entity
                context.Appointments.Add(appointment);
            } else {
                // Existing entity
                context.Entry(appointment).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var appointment = context.Appointments.Find(id);
            context.Appointments.Remove(appointment);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Dispose() 
        {
            context.Dispose();
        }
    }

    public interface IAppointmentRepository : IDisposable
    {
        IQueryable<Appointment> All { get; }
        IQueryable<Appointment> AllIncluding(params Expression<Func<Appointment, object>>[] includeProperties);
        Appointment Find(int id);
        void InsertOrUpdate(Appointment appointment);
        void Delete(int id);
        void Save();
    }
}