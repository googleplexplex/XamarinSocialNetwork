using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinNetworkProj.Model
{
    public class PostShared : Post
    {
        public string autorName { get; set; }
        public string likedByUser { get; set; }

        public PostShared() : base() { }
        public PostShared(string autorName, int autorId, string content, int likes, DateTime postedOn, int id = 0) : base(autorId, content, likes, postedOn, id)
        {
            this.autorName = autorName;
        }

        static public PostShared getFromPost(Post p)
        {
            return new PostShared("", p.autorId, p.content, p.likes, p.postedOn, p.Id);
        }
    }
}
