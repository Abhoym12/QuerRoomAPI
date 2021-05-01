using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QueryRoom.DTOs
{
    public class UserDetails
    {
        public string USERNAME { get; set; }
        public string NAME { get; set; }
        public string EMAIL { get; set; }
        public long PHONE_NO { get; set; }
        public int POINTS { get; set; }
    }
}