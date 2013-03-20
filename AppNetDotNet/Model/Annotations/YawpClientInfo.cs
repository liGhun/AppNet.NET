using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppNetDotNet.Model.Annotations
{
    public class YawpClientInfo
    {
        /// <summary>
        /// A formatted date string. 
        /// </summary>
        public DateTime build_date
        {
            get
            {
                return _build_date;
            }
            set
            {
                _build_date = value.ToLocalTime();
            }
        }
        /// <summary>
        /// User supplied text of the post.
        /// </summary>
        private DateTime _build_date { get; set; }
        /// <summary>
        /// The application's internal build number
        /// </summary>
        public string build_number { get; set; }
        /// <summary>
        /// The bundle ID of the application.
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// The locale identifier of the user.
        /// </summary>
        public string locale { get; set; }
        /// <summary>
        /// The version of the application in dotted notation.
        /// </summary>
        public string  version { get; set; }
    }
}
