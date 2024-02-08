using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace Hospitalproject.Models
{
    public class dbaccess
    {
        static string constring = @"Data Source=DESKTOP-PNNBCSS\SQLEXPRESS; initial catalog=hospital; integrated security=true";
        public SqlConnection conn = new SqlConnection(constring);

        public void opencon()
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
        }

        public void closecon()
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }

        public SqlCommand cmd = null;
        public SqlDataReader sdr = null;

        public void IUD(string query)
        {
            opencon();
            cmd = new SqlCommand(query, conn);
            cmd.ExecuteNonQuery();
            closecon();
        }

        public SqlDataReader select(string query)
        {
            opencon();
            cmd = new SqlCommand(query, conn);
            sdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            return sdr;
        }

        // Method to retrieve all available doctors
        public List<doc> GetAllDoctors()
        {
            List<doc> doctors = new List<doc>();

            try
            {
                opencon();
                string query = "SELECT * FROM Doctors";
                cmd = new SqlCommand(query, conn);
                sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                   doc doctor = new doc
                    {
                        DoctorID = sdr["DoctorID"].ToString(),
                        DoctorName = sdr["DoctorName"].ToString(),
                        // Add other properties accordingly
                    };

                    doctors.Add(doctor);
                }
            }
            finally
            {
                closecon();
            }

            return doctors;
        }

        // Method to schedule an appointment
        public void ScheduleAppointment(string doctorID, string patientID, DateTime appointmentDateTime)
        {
            string query = $"INSERT INTO Appointments (DoctorID, PatientID, AppointmentDateTime) " +
                           $"VALUES ('{doctorID}', '{patientID}', '{appointmentDateTime.ToString("yyyy-MM-dd HH:mm:ss")}')";
            IUD(query);
        }

        // Method to retrieve doctor's appointments
        public List<app> GetDoctorAppointments(string doctorID)
        {
            List<app> appointments = new List<app>();

            try
            {
                opencon();
                string query = $"SELECT * FROM Appointments WHERE DoctorID = '{doctorID}'";
                cmd = new SqlCommand(query, conn);
                sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    app appointment = new app
                    {
                        AppointmentID = int.Parse(sdr["AppointmentID"].ToString()),
                        // Add other properties accordingly
                    };

                    appointments.Add(appointment);
                }
            }
            finally
            {
                closecon();
            }

            return appointments;
        }

        // Method to retrieve patient's appointments
        public List<app> GetPatientAppointments(string patientID)
        {
            List<app> appointments = new List<app>();

            try
            {
                opencon();
                string query = $"SELECT * FROM Appointments WHERE PatientID = '{patientID}'";
                cmd = new SqlCommand(query, conn);
                sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                   app appointment = new app
                    {
                        AppointmentID = int.Parse(sdr["AppointmentID"].ToString()),
                        // Add other properties accordingly
                    };

                    appointments.Add(appointment);
                }
            }
            finally
            {
                closecon();
            }

            return appointments;
        }
    }
}
