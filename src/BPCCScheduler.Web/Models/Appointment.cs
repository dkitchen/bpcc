using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BPCCScheduler.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public string ClientName { get; set; }
        public string Cell { get; set; }
        public DateTime Date { get; set; }
    }
}