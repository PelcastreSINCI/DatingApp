using System;
using System.Collections.Generic;

namespace API.DTOs
{
    public class MemberDto
    {
        public int Id { get; set; }

        public string UserName { get; set; }

       
        public int  Age{get;set;}
        public string KnownAs{get;set;}
        public DateTime Created{get;set;};
        public DateTime LastActive {get;set;} = DateTime.Now;
        public string Introduction {get;set;}
        public string LookinFor{get;set;}
        public string Interest{get;set;}
        public string City{get;set;}
        public string Country{get;set;}
        public ICollection<PhotoDto>Photos{get;set;}

       
    }
}