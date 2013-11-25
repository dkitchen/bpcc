using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using WebMatrix.WebData;

namespace BPCCScheduler.Models
{
    public class CustomMembershipProvider : SimpleMembershipProvider
    {
        public class CustomProviderProvider : SimpleMembershipProvider
        {
            public override MembershipUser GetUser(string username, bool userIsOnline)
            {
                return new MembershipUser("CustomProviderProvider"
                    , username, null, null, null, null, true, false
                    , DateTime.Today.AddDays(-1)
                    , DateTime.Today, DateTime.Today, DateTime.Today, DateTime.Today);
                //return base.GetUser(username, userIsOnline);

            }

            

            public override bool ValidateUser(string username, string password)
            {
                return true; // base.ValidateUser(username, password);
            }
        }

    }
}