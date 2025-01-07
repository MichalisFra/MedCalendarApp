namespace MyMedCalendar.Data
{
    public class Doctor : BaseEntity
    {
        public int Id { get; set; }
        public string Specialty { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;

        // FK
        public int UserId { get; set; }
        public virtual User User { get; set; } = null!;

        // Navigation
        public virtual ICollection<DoctorPatient> DoctorPatients { get; set; } = new List<DoctorPatient>();
    }

}
