﻿namespace RetroRealm.Models
{
    public class IncidentVM
    {
        public string SearchType { get; set; }
        public string Operation { get; set; }
        public List<CustomerModel> Customers { get; set; }
        public List<GameModel> Games { get; set; }
        public List<TechnicianModel> Technicians { get; set; }
        public List<IncidentModel> Incidents { get; set; }
    }
}
