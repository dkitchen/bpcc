using BPCCScheduler.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Twilio;

namespace BPCCScheduler.Controllers.BaseControllers
{
    public class SMSApiController : AppointmentContextApiController
    {
        private TwilioRestClient _twilioRestClient;
        private NameValueCollection _twilioConfig 
            = ConfigurationManager.GetSection("TwilioConfig") as NameValueCollection;

        public TwilioRestClient TwilioRestClient
        {
            get
            {
                if (_twilioRestClient == null)
                {
                    string AccountSid = _twilioConfig["AccountSid"];
                    string AuthToken = _twilioConfig["AuthToken"];
                    _twilioRestClient = new TwilioRestClient(AccountSid, AuthToken);
                }
                return _twilioRestClient;
            }
        }

        public SMSMessage SendSmsMessage(string cell, string body)
        {
            string fromCell = _twilioConfig["FromCell"];
            return TwilioRestClient.SendSmsMessage(fromCell, cell, body);
        }


    }
}
