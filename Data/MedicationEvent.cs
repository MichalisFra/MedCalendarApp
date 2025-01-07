namespace MyMedCalendar.Data
{
    public class MedicationEvent : BaseEntity
    {
        public int Id { get; set; }
        public DateTime EventDateTime { get; set; }
        public bool IsTaken { get; set; }
        public DateTime? TakenAt { get; set; }

        public int DosageScheduleId { get; set; }
        public virtual DosageSchedule DosageSchedule { get; set; } = null!;
    }
}
