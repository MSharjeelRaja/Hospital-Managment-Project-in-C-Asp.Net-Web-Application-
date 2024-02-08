using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hospitalproject.Models
{
    public class doc
    {
                   public string DoctorID { get; set; }
            public string DoctorName { get; set; }
            public string Country { get; set; }
            public string Gender { get; set; }
            public string Experience { get; set; }
            public string Email { get; set; }
            public string Specialization { get; set; }
            public string Description { get; set; }
            public string Password { get; set; }

            // Corrected property name to match the class representing appointments
            public virtual ICollection<app> app { get; set; }
        }
    
}