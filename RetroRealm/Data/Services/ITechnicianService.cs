using RetroRealm.Models;

namespace RetroRealm.Data.Services
{
    public interface ITechnicianService
    {
        IQueryable<TechnicianModel> GetAll();
        TechnicianModel? GetById(int id);
        Task Add(TechnicianModel technician);
        Task Remove(TechnicianModel technician);
        Task Update(TechnicianModel technician);
    }
}
