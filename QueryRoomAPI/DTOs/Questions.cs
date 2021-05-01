using System;

namespace QueryRoomAPI.Models
{
    public class Questions
    {
        public System.Guid QID { get; set; }
        public string QUESTION { get; set; }
        public string USERNAME { get; set; }
        public DateTime TIMESTAMP { get; set; }
        //public string USERNAME { get; set; }
    }
}