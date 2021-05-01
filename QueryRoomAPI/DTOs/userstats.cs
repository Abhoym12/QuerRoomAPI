using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QueryRoomAPI.Models.DTOs
{
    public class userstats
    {
        public int validated_answers { get; set; }
        public int answers { get; set; }
        public int questions { get; set; }

        public int credibility { get; set; }

        public int for_admin { get; set; }
    }
}