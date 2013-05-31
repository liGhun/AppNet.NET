using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppNetDotNet.Model.Annotations
{
    public class Issue
    {
        /// <summary>
        /// a unique guid for this issue
        /// </summary>
        public string guid { get; set; }

        /// <summary>
        /// a short title for the bug
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// a human readable description of the issue
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// a stacktrace
        /// </summary>
        public string stacktrace { get; set; }

        /// <summary>
        /// current state of the bug (examples): new, open, assigned, in_progress, fixed, duplicate, wont_fix
        /// </summary>
        public string state { get; set; }

        /// <summary>
        /// a comment by the user
        /// </summary>
        public string user_comment { get; set; }

        /// <summary>
        /// a comment by the developer
        /// </summary>
        public string developer_comment { get; set; }
    }
}
