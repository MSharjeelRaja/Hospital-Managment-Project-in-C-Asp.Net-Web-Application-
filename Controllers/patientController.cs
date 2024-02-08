using Hospitalproject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hospitalproject.Controllers
{
    public class patientController : Controller
    {
        // GET: patient
       private readonly dbaccess _dbAccess = new dbaccess();

        // Action method to display patient's appointments
        public ActionResult ViewAppointments(string patientID)
        {
            // Retrieve patient's appointments
            var appointments = new List<app>();
            using (var appointmentReader = _dbAccess.select($"SELECT * FROM Appointments WHERE PatientID = '{patientID}'"))
            {
                while (appointmentReader.Read())
                {
                    appointments.Add(new app
                    {
                        AppointmentID = (int)appointmentReader["AppointmentID"],
                        PatientID = (string)appointmentReader["PatientID"],
                        DoctorID = (string)appointmentReader["DoctorID"],
                        AppointmentDateTime = (DateTime)appointmentReader["AppointmentDateTime"]
                    });
                }
            }

            // Check if the patient has no appointments, and if so, redirect to the CreateAppointment action
            if (appointments.Count == 0)
            {
                return RedirectToAction("CreateAppointment", new { patientID });
            }

            return View(appointments);
        }


        public ActionResult CreateAppointment(string patientID)
        {
            // Retrieve available doctors
            var doctors = new List<doc>();
            using (var doctorReader = _dbAccess.select("SELECT * FROM Doctors"))
            {
                while (doctorReader.Read())
                {
                    doctors.Add(new doc
                    {
                        DoctorID = (string)doctorReader["DoctorID"],
                        DoctorName = (string)doctorReader["DoctorName"],
                        // Add other properties as needed
                    });
                }
            }

            ViewBag.PatientID = patientID; // Set the patientID in ViewBag
            return View(doctors);
        }
     
        int id = 16;
        [HttpPost]
        public ActionResult CreateAppointmentPost(string doctorID, string patientID, DateTime appointmentDateTime)
        {
            // Create a new appointment in the database
            string query = $"INSERT INTO Appointments (AppointmentID, DoctorID, PatientID, AppointmentDateTime) VALUES ('{id}', '{doctorID}', '{patientID}', '{appointmentDateTime.ToString("yyyy-MM-dd HH:mm:ss")}')";
            _dbAccess.IUD(query);

            // Redirect to the ViewAppointments action with the patientID parameter
            return RedirectToAction("ViewAppointments", new { patientID });
            id++;
        }


        public ActionResult ShowAllDoctors(string patientID)
        {
            var doctors = new List<doc>();
            using (var doctorReader = _dbAccess.select("SELECT * FROM Doctors"))
            {
                while (doctorReader.Read())
                {
                    doctors.Add(new doc
                    {
                        DoctorID = (string)doctorReader["DoctorID"],
                        DoctorName = (string)doctorReader["DoctorName"],
                        // Add other properties as needed
                    });
                }
            }

            ViewBag.PatientID = patientID;
            return View(doctors);
        }



    }
}