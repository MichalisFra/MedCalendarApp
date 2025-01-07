namespace MyMedCalendar.Data
{
    public class Patient : BaseEntity
    {
        public int Id { get; set; }
        public string AMKA { get; set; } = null!;
        public DateOnly DateOfBirth { get; set; }

        // FK
        public int UserId { get; set; }
        public virtual User User { get; set; } = null!;

        // Navigation
        public virtual ICollection<DoctorPatient> DoctorPatients { get; set; } = new List<DoctorPatient>();
        public virtual ICollection<Drug> Drugs { get; set; } = new List<Drug>();
        public virtual ICollection<Calendar> Calendars { get; set; } = new List<Calendar>(); // Multiple calendars for flexibility
    }
}
