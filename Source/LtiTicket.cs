using System;
using System.Collections.Specialized;

namespace LtiLaunchDemo
{
    public class LtiTicket
    {
        #region Properties

        public Uri Url { get; private set; }
        public string Signature { get; private set; }
        public NameValueCollection Parameters { get; private set; }

        #endregion

        #region Construction

        public LtiTicket(Uri url, string signature, NameValueCollection parameters)
        {
            Url = url;
            Signature = signature;
            Parameters = parameters;
        }

        #endregion
    }
}