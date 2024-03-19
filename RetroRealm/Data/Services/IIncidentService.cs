using RetroRealm.Models;

namespace RetroRealm.Data.Services
{
    public interface IIncidentService
    {
        IQueryable<IncidentModel> GetAll();

        IncidentModel? GetById(int id);

        Task AddIncident(IncidentModel incident);
        Task UpdateIncident(IncidentModel incident);
        Task DeleteIncident(IncidentModel incident);
    }
}
