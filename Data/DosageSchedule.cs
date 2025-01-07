namespace MyMedCalendar.Data
{
    public class DosageSchedule : BaseEntity
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public int IntervalDays { get; set; } // Number of days between doses
        public TimeSpan DoseTime { get; set; } // Time of day for the dose
        public DateTime? EndDate { get; set; } // Optional end date for the schedule

        // FK
        public int DrugId { get; set; }
        public virtual Drug Drug { get; set; } = null!;

        // Navigation
        public virtual ICollection<MedicationEvent> MedicationEvents { get; set; } = new List<MedicationEvent>();
    }
}
