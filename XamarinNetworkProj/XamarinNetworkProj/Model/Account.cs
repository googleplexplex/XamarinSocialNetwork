using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace XamarinNetworkProj.Model
{
    [Table("Accounts")]
    public class Account
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }

        public string nickname { get; set; }
        public string password { get; set; }
        public string desc { get; set; }
        public string likedPosts { get; set; }
    }
}
