using MyMedCalendar.Core.Enums;
using System.Globalization;

namespace MyMedCalendar.Data
{
    public class User : BaseEntity
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Firstname { get; set; } = null!;
        public string Lastname { get; set; } = null!;
        public UserRole UserRole { get; set; }

        //Navigation
        public virtual Patient? PatientProfile { get; set; }
        public virtual Doctor? DoctorProfile { get; set; }





    }
}
