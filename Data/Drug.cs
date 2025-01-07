namespace MyMedCalendar.Data
{
    public class Drug 
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;
        public string Company { get; set; } = null!;
        public int PackageSize { get; set; }
        public int DoseQuantity { get; set; }

        // FK

        public int PatientId { get; set; }
        public virtual Patient Patient { get; set; } = null!;

        // Navigation
        public virtual ICollection<DosageSchedule> DosageSchedules { get; set; } = new List<DosageSchedule>();
    }
}
