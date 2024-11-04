namespace HospitalPatientAPI.DTO
{
    public class TreatmentDTO
    {
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public string? TreatmentDetails { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime? EndDate { get; set; }
    }
}
