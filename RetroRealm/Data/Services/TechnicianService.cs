using RetroRealm.Models;

namespace RetroRealm.Data.Services
{
    public class TechnicianService : ITechnicianService
    {
        private readonly ApplicationDbContext _context;
        public TechnicianService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task Add(TechnicianModel technician)
        {
            _context.Technicians.Add(technician);
            await _context.SaveChangesAsync();
        }

        public IQueryable<TechnicianModel> GetAll()
        {
            IQueryable<TechnicianModel> technicians = _context.Technicians.Where(t => t.TechnicianModelId != -1);
            return technicians;
        }

        public TechnicianModel? GetById(int? id)
        {
            TechnicianModel? technician = _context.Technicians.FirstOrDefault(t => t.TechnicianModelId == id);
            return technician;
        }

        public async Task Remove(TechnicianModel technician)
        {
            _context.Technicians.Remove(technician);
            await _context.SaveChangesAsync();
        }

        public async Task Update(TechnicianModel technician)
        {
            _context.Technicians.Update(technician);
            await _context.SaveChangesAsync();
        }
    }
}
