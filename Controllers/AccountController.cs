using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
using Hospitalproject.Models;

namespace Hospitalproject.Controllers
{
    public class AccountController : Controller
    {
        dbaccess _dbAccess = new dbaccess();
        // Action method to display login form
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string id, string password)
        {
            _dbAccess.opencon(); // Open the database connection

            // Check if the user is a doctor
            using (SqlDataReader doctorReader = _dbAccess.select($"SELECT * FROM Doctors WHERE DoctorID = '{id}' AND Password = '{password}'"))
            {
                if (doctorReader.Read())
                {
                    _dbAccess.closecon(); // Close the database connection

                    // Redirect to doctor's dashboard
                    // Redirect to doctor's dashboard
                    return RedirectToAction("ViewAppointments", "doctor", new { doctorID = id });

                }
            }

            // Check if the user is a patient
            using (SqlDataReader patientReader = _dbAccess.select($"SELECT * FROM Patients WHERE PatientID = '{id}' AND Password = '{password}'"))
            {
                if (patientReader.Read())
                {
                    _dbAccess.closecon(); // Close the database connection

                    // Redirect to patient's dashboard
                    // Redirect to doctor's dashboard
                    return RedirectToAction("ViewAppointments", "patient", new { patientID = id });

                }
            }

            _dbAccess.closecon(); // Close the database connection

            // If login credentials do not match, show an error message
            ViewBag.ErrorMessage = "Invalid login credentials. Please check your ID and password.";
            return View();
        }



        public ActionResult DoctorDashboard(/*string id*/)
        {
           
            return View();
        }


      
        public ActionResult PatientDashboard()
        {
            //// Retrieve patient's appointments
            //var appointments = _dbAccess.select($"SELECT * FROM Appointments WHERE PatientID = '{id}'");

            //return View(appointments);
            return View();
        }

    }
}
