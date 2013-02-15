using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppNetDotNet.Model
{
    public class Post
    {
        public override string ToString()
        {
            if (user != null && text != null)
            {
                return user.ToString() + ": " +  text;
            }
            return "Incomplete post";
        }

        /// <summary>
        /// Primary identifier for a post. This will be an integer, but it is always expressed as a string to avoid limitations with the way JavaScript integers are expressed.
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// This is an embedded version of the User object. Note: In certain cases (e.g., when a user account has been deleted), this key may be omitted.
        /// </summary>
        public User user { get; set; }
        /// <summary>
        /// The time at which the post was create in ISO 8601 format.
        /// </summary>
        public DateTime created_at { get; set; }
        /// <summary>
        /// User supplied text of the post.
        /// </summary>
        public string text { get; set; }
        /// <summary>
        /// Server-generated annotated HTML rendering of post text.
        /// </summary>
        public string html { get; set; }
        /// <summary>
        /// the client having sent this post
        /// </summary>
        public Source source { get; set; }
        /// <summary>
        /// The id of the post this post is replying to (or null if not a reply).
        /// </summary>
        public string reply_to { get; set; }
        /// <summary>
        /// The URL of the post's detail page on Alpha.
        /// </summary>
        public string canonical_url { get; set; }
        /// <summary>
        /// The id of the post at the root of the thread that this post is a part of. If thread_id==id than this property does not guarantee that the thread has > 1 post. Please see num_replies.
        /// </summary>
        public string thread_id { get; set; }
        /// <summary>
        /// The number of posts created in reply to this post.
        /// </summary>
        public int num_replies { get; set; }
        /// <summary>
        /// The number of users who have starred this post.
        /// </summary>
        public int num_stars { get; set; }
        /// <summary>
        /// The number of users who have reposted this post.
        /// </summary>
        public int num_reposts { get; set; }
        /// <summary>
        /// Metadata about the entire post. See the Annotations documentation.
        /// </summary>
        public List<Annotation> annotations { get; set; }
        /// <summary>
        /// Rich text information for this post. See the Entities documentation.
        /// </summary>
        public Entities entities { get; set; }
        /// <summary>
        /// Has this post been deleted? For non-deleted posts, this key may be omitted instead of being false. If a post has been deleted, the text, html, and entities properties will be empty and may be omitted.
        /// </summary>
        public bool is_deleted { get; set; }
        /// <summary>
        /// Is this Post meant for humans or other apps? See Machine only Posts for more information.
        /// </summary>
        public bool machine_only { get; set; }
        /// <summary>
        /// Have you starred this Post? May be omitted if this is not an authenticated request.
        /// </summary>
        public bool you_starred { get; set; }
        /// <summary>
        /// A partial list of users who have starred this post. This is not comprehensive and is meant to be a sample of users who have starred this post giving preference to users the current user follows. This is only included if include_starred_by=1 is passed to App.net. May be omitted if this is not an authenticated request.
        /// </summary>
        public List<User> starred_by { get; set; }
        /// <summary>
        /// Have you reposted this Post? May be omitted if this is not an authenticated request.
        /// </summary>
        public bool you_reposted { get; set; }
        /// <summary>
        /// A partial list of users who have reposted this post. This is not comprehensive and is meant to be a sample of users who have starred this post giving preference to users the current user follows. This is only included if include_reposters=1 is passed to App.net. May be omitted if this is not an authenticated request.
        /// </summary>
        public List<User> reposters { get; set; }
        /// <summary>
        /// If this post is a repost, this key will contain the complete original Post.
        /// </summary>
        public Post repost_of { get; set; }

        public class Source
        {
            /// <summary>
            /// Description of the API consumer that created this post.
            /// </summary>
            public string name { get; set; }
            /// <summary>
            /// Link provided by the API consumer that created this post.
            /// </summary>
            public string link { get; set; }
            /// <summary>
            /// The public client_id of the API consumer that created this post.
            /// </summary>
            public string client_id { get; set; }
        }

        
    }
}
