namespace RetroRealm.Models
{
    public class IncidentVM
    {
        public string? Filter { get; set; }
        public string? Action { get; set; }
        public List<CustomerModel>? Customers { get; set; }
        public List<GameModel>? Games { get; set; }
        public List<TechnicianModel>? Technicians { get; set; }
        public List<IncidentModel>? Incidents { get; set; }
        public IncidentModel? CurrentIncident { get; set; }
    }
}
