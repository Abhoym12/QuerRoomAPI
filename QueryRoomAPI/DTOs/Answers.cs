using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QueryRoomAPI.Models
{
    public class Answers
    {
        public System.Guid AID { get; set; }
        public System.Guid QID { get; set; }
        public string USERNAME { get; set; }
        public string ANSWER { get; set; }
        public DateTime DATE { get; set; }
        public int LIKES { get; set; }
        public int DISLIKES { get; set; }
        public bool ISVALIDATED { get; set; }
    }
}