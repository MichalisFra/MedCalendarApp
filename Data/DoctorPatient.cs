namespace MyMedCalendar.Data
{
    public class DoctorPatient
    {
        public int DoctorId { get; set; }
        public virtual Doctor Doctor { get; set; } = null!;

        public int PatientId { get; set; }
        public virtual Patient Patient { get; set; } = null!;
    }
}
