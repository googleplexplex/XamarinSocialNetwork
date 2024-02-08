using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace XamarinNetworkProj.Model
{
    [Table("Posts")]
    public class Post
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }

        public int autorId { get; set; }
        public string content { get; set; }
        public int likes { get; set; }
        public DateTime postedOn { get; set; }

        public Post() { }
        public Post(int autorId, string content, int likes, DateTime postedOn, int id = 0)
        {
            this.Id = id;
            this.autorId = autorId;
            this.content = content;
            this.likes = likes;
            this.postedOn = postedOn;
        }
        
    }
}
