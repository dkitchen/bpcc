using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BPCCScheduler.Models
{
    public class MockAppointmentRepo : IAppointmentRepository
    {
        public List<Appointment> GetAppointments()
        {
            //mocked for now. Use DB later
            var appts = new List<Appointment>() { 
                new Appointment { ClientName = "Posh", Cell = "7165551212", Date = DateTime.Parse("2013-03-01T22:15:00Z") }
                , new Appointment { ClientName = "Baby", Cell = "7164443333", Date = DateTime.Parse("2013-02-25T15:15:00Z") }
                , new Appointment { ClientName = "Scary", Cell = "7166666666", Date = DateTime.Parse("2013-02-26T22:45:00Z") }
                , new Appointment { ClientName = "Sporty", Cell = "7165555555", Date = DateTime.Parse("2013-02-27T23:00:00Z") }
                , new Appointment { ClientName = "Ginger", Cell = "7707770770", Date = DateTime.Parse("2013-02-28T23:30:00Z") }
            };
            return appts;
        }

        public IQueryable<Appointment> All
        {
            get { throw new NotImplementedException(); }
        }

        public IQueryable<Appointment> AllIncluding(params System.Linq.Expressions.Expression<Func<Appointment, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public Appointment Find(int id)
        {
            throw new NotImplementedException();
        }

        public void InsertOrUpdate(Appointment appointment)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
