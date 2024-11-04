using HospitalPatientAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalPatientAPI.Context
{
    public class HospitalContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors {  get; set; } 
        public DbSet<Treatment> Treatments { get; set; }
    }
}
