namespace MyMedCalendar.Data
{
    public abstract class BaseEntity
    {
        public DateTime InsertedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }
    }
}
