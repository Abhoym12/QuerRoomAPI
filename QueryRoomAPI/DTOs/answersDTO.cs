using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QueryRoomAPI.Models.DTOs
{
    public class answersDTO
    {
        
        public Guid QID { get; set; }
        public string USERNAME { get; set; }
        public string ANSWER { get; set; }
        public DateTime DATE { get; set; }
        public bool ISVALIDATED { get; set; }
       
    }
}