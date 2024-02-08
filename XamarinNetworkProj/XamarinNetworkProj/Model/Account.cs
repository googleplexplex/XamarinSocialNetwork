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
        public string phone { get; set; }
        public string desc { get; set; }

        public Account() { }
        public Account(string nickname, string phone, string desc, int id = 0)
        {
            Id = id;
            this.nickname = nickname;
            this.phone = phone;
            this.desc = desc;
        }
    }
}
