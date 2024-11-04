using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HospitalPatientAPI.Context;
using HospitalPatientAPI.Entities;

namespace HospitalPatientAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController(HospitalContext context) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Patient patient)
        {
            if (patient is null) return BadRequest("Empty input");

            await context.Patients.AddAsync(patient);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = patient.Id }, patient);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var patients = await context.Patients.ToListAsync();
            if (patients is null) return NotFound("No patients found");

            return Ok(patients);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var patient = await context.Patients.FirstOrDefaultAsync(p => p.Id == id);
            if (patient is null) return NotFound("No patient found");

            return Ok(patient);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Patient patient)
        {
            var patientToUpdate = await context.Patients.FirstOrDefaultAsync(p => p.Id == id);
            if (patientToUpdate == null) return NotFound("Patient not found");

            patientToUpdate.Name = patient.Name;
            patientToUpdate.DateOfBirth = patient.DateOfBirth;
            patientToUpdate.Gender = patient.Gender;
            patientToUpdate.ContactInfo = patient.ContactInfo;

            await context.SaveChangesAsync();
            return Ok("Updated successfuly");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var patient = await context.Patients.FirstOrDefaultAsync(p => p.Id == id);
            if (patient == null) return NotFound("Patient not found");

            context.Patients.Remove(patient);
            await context.SaveChangesAsync();
            return Ok("Deleted successfuly");
        }
    }
}
