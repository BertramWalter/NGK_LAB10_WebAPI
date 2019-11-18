using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NGK_LAB10_WebAPI.Models
{
    public class LoginClient
    {
        [MaxLength(16)]
        public string SerialNumber { get; set; }

        [MaxLength(12)]
        public string Password { get; set; }
    }

}
