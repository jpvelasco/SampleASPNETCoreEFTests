using System.Collections.Generic;

namespace SampleApi.Data.Entities
{
    public class Client
    {
        public int ClientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Appointment> Appointments { get; set; }
    }
}
