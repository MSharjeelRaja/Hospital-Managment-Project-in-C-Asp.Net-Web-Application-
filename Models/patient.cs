using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hospitalproject.Models
{
    public class patient
    {
        public string PatientID { get; set; }
        public string PatientName { get; set; }
        public string Country { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Disease { get; set; }
        public string Password { get; set; }
        public virtual ICollection<app> app { get; set; }
    }
}