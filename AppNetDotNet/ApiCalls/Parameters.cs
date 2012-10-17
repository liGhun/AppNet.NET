using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppNetDotNet.ApiCalls
{
    public class Parameters
    {
        public string since_id { get; set; }
        public string before_id { get; set; }
        public int count { get; set; }
        public bool include_muted { get; set; }
        private string _include_muted
        {
            get
            {
                if (include_muted)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
        }
        public bool include_deleted { get; set; }
        private string _include_deleted
        {
            get
            {
                if (include_deleted)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
        }
        public bool include_directed_posts { get; set; }
        private string _include_directed_posts
        {
            get
            {
                if (include_directed_posts)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
        }
        public bool include_machine { get; set; }
        private string _include_machine
        {
            get
            {
                if (include_machine)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
        }
        public bool include_annotations { get; set; }
        private string _include_annotations
        {
            get
            {
                if (include_annotations)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
        }
        public bool include_starred_by { get; set; }
        private string _include_starred_by
        {
            get
            {
                if (include_starred_by)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
        }
        public bool include_reposters { get; set; }
        private string _include_reposters
        {
            get
            {
                if (include_reposters)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
        }
        public bool include_user { get; set; }
        private string _include_user
        {
            get
            {
                if (include_user)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
        }

        public Parameters()
        {
            since_id = "";
            before_id = "";
            count = 0;
            // except on request for muted user
            include_muted = false;
            include_deleted = true;
            // except on "My stream"
            include_directed_posts = true;
            include_machine = false;
            include_annotations = false;
            include_starred_by = false;
            include_reposters = false;
            include_user = true;
        }

        public string getQueryString()
        {
            string queryString = "";
            if (!string.IsNullOrEmpty(since_id))
            {
                queryString += "since_id=" + since_id + "&";
            }
            if (!string.IsNullOrEmpty(before_id))
            {
                queryString += "before_id=" + before_id + "&";
            }
            if (count != 0)
            {
                queryString += "count=" + count.ToString() + "&";
            }
            queryString += string.Format("include_muted={0}&include_deleted={1}&include_directed_posts={2}&include_machine={3}&include_annotations={4}&include_starred_by={5}&include_reposters={6}&include_user={7}", 
                _include_muted, 
                _include_deleted, 
                _include_directed_posts, 
                _include_machine, 
                _include_annotations, 
                _include_starred_by, 
                _include_reposters, 
                _include_user);
            return queryString;
        }
    }

    public class ParametersMyStream : Parameters
    {
        public ParametersMyStream()
        {
            include_directed_posts = false;
        }
    }
    public class ParametersMutedUsersRequest : Parameters
    {
        public ParametersMutedUsersRequest()
        {
            include_muted = true;
        }
    }

}
