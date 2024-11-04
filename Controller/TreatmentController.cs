using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HospitalPatientAPI.DTO;
using HospitalPatientAPI.Context;
using HospitalPatientAPI.Entities;

namespace HospitalPatientAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class TreatmentController(HospitalContext context) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TreatmentDTO treatmentDTO)
        {
            if (treatmentDTO is null) return BadRequest("Empty input");

            var treatment = new Treatment
            {
                PatientId = treatmentDTO.PatientId,
                DoctorId = treatmentDTO.DoctorId,
                TreatmentDetails = treatmentDTO.TreatmentDetails!,
                StartDate = treatmentDTO.StartDate,
                EndDate = treatmentDTO.EndDate,
            };

            await context.Treatments.AddAsync(treatment);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = treatment.Id }, treatment);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var treatments = await context.Treatments.Include(r => r.Doctor).Include(c => c.Patient).ToListAsync();
            if (treatments is null) return NotFound("No treatments found");

            return Ok(treatments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var treatment = await context.Treatments.FirstOrDefaultAsync(t => t.Id == id);
            if (treatment is null) return NotFound("No treatment found");

            return Ok(treatment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Treatment treatment)
        {
            var treatmentToUpdate = await context.Treatments.FirstOrDefaultAsync(t => t.Id == id);
            if (treatmentToUpdate == null) return NotFound("Treatment not found");

            treatmentToUpdate.StartDate = treatment.StartDate;
            treatmentToUpdate.EndDate = treatment.EndDate;

            await context.SaveChangesAsync();
            return Ok("Updated successfuly");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var treatment = await context.Treatments.FirstOrDefaultAsync(r => r.Id == id);
            if (treatment == null) return NotFound("Treatments not found");

            context.Treatments.Remove(treatment);
            await context.SaveChangesAsync();
            return Ok("Deleted successfuly");
        }
    }
}
