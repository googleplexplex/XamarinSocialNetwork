using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinNetworkProj.Model
{
    public class PostSharedOldDepricated : Post
    {
        public string autorName { get; set; }
        public string likedByUser { get; set; }

        public PostSharedOldDepricated() : base() { }
        public PostSharedOldDepricated(string autorName, int autorId, string content, int likes, int id = 0) : base(autorId, content, likes, id)
        {
            this.autorName = autorName;
        }

        static public PostSharedOldDepricated getFromPost(Post p)
        {
            return new PostSharedOldDepricated("", p.autorId, p.content, p.likes, p.Id);
        }
    }
}
