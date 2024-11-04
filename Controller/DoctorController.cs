using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HospitalPatientAPI.Context;
using HospitalPatientAPI.Entities;

namespace HospitalPatientAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController(HospitalContext context) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Doctor doctor)
        {
            if (doctor is null) return BadRequest("Empty input");

            await context.Doctors.AddAsync(doctor);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = doctor.Id }, doctor);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var doctors = await context.Doctors.ToListAsync();
            if (doctors is null) return NotFound("No doctors found");

            return Ok(doctors);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var doctor = await context.Doctors.FirstOrDefaultAsync(v => v.Id == id);
            if (doctor is null) return NotFound("No doctor found");

            return Ok(doctor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Doctor doctor)
        {
            var doctorToUpdate = await context.Doctors.FirstOrDefaultAsync(d => d.Id == id);
            if (doctorToUpdate == null) return NotFound("Doctor not found");

            doctorToUpdate.Name = doctor.Name;
            doctorToUpdate.Specialty = doctor.Specialty;
            doctorToUpdate.ContactInfo = doctor.ContactInfo;

            await context.SaveChangesAsync();
            return Ok("Updated successfuly");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var doctor = await context.Doctors.FirstOrDefaultAsync(d => d.Id == id);
            if (doctor == null) return NotFound("Doctor not found");

            context.Doctors.Remove(doctor);
            await context.SaveChangesAsync();
            return Ok("Deleted successfuly");
        }
    }
}
