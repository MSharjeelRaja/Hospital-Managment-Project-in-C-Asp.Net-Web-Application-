using Hospitalproject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hospitalproject.Controllers
{
    public class doctorController : Controller
    {
        private readonly dbaccess _dbAccess = new dbaccess();

        // Action method to display doctor's appointments
        public ActionResult ViewAppointments(string doctorID)
        {
            // Retrieve doctor's appointments
            var appointments = new List<app>();
            using (var doctorReader = _dbAccess.select($"SELECT * FROM Appointments WHERE DoctorID = '{doctorID}'"))
            {
                while (doctorReader.Read())
                {
                    appointments.Add(new app
                    { DoctorID = doctorID,
                        AppointmentID = (int)doctorReader["AppointmentID"],
                        PatientID = (string)doctorReader["PatientID"],
                        AppointmentDateTime = (DateTime)doctorReader["AppointmentDateTime"]
                    }); 
                }
            }

            return View(appointments);
        }
    }
}