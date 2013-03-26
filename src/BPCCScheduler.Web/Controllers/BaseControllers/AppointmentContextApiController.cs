using BPCCScheduler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace BPCCScheduler.Controllers.BaseControllers
{
    public class AppointmentContextApiController : ApiController
    {
        public AppointmentContext AppointmentContext { get; set; }

        //Server is UTC, so this helps with queries for EST
        public TimeZoneInfo EasternTimeZoneInfo { get; set; }
        public DateTime EasternStandardTimeNow
        {
            get
            {
                return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, EasternTimeZoneInfo);
            }
        }


        /// <summary>
        /// Converts UTC date to Eastern Standard Time
        /// </summary>
        /// <param name="utc"></param>
        /// <returns></returns>
        public DateTime ToEST(DateTime utc)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(utc, EasternTimeZoneInfo);
        }

        // If you are using Dependency Injection, you can delete the following constructor
        public AppointmentContextApiController()
            : this(new AppointmentContext())
        {
        }

        public AppointmentContextApiController(AppointmentContext appointmentContext)
        {
            this.AppointmentContext = appointmentContext;
            this.EasternTimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");            
        }
    }
}