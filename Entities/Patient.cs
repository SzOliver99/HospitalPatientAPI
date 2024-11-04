namespace HospitalPatientAPI.Entities
{
    public class Patient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string ContactInfo { get; set; }
    }
}
