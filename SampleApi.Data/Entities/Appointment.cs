using System;

namespace SampleApi.Data.Entities
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public string EventName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
    }
}
