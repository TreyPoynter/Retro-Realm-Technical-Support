using Microsoft.EntityFrameworkCore;
using RetroRealm.Models;

namespace RetroRealm.Data.Services
{
    public class IncidentService : IIncidentService
    {
        private readonly ApplicationDbContext _context;
        public IncidentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddIncident(IncidentModel incident)
        {
            _context.Incidents.Add(incident);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteIncident(IncidentModel incident)
        {
            _context.Incidents.Remove(incident);
            await _context.SaveChangesAsync();
        }

        public IQueryable<IncidentModel> GetAll()
        {
            IQueryable<IncidentModel> incidentModels = _context.Incidents
                .Include(i => i.Customer)
                .Include(i => i.Game)
                .Include(i => i.Technician);
            return incidentModels;
        }

        public IncidentModel? GetById(int id)
        {
            IncidentModel? incident = _context.Incidents
                .Include(i => i.Customer)
                .Include(i => i.Game)
                .Include(i => i.Technician)
                .FirstOrDefault(i => i.IncidentModelId == id);

            return incident;
        }

        public async Task UpdateIncident(IncidentModel incident)
        {
            _context.Incidents.Update(incident);
            await _context.SaveChangesAsync();
        }
    }
}
