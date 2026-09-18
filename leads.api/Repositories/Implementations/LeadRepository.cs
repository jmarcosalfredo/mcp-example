using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using leads.api.Context;
using leads.api.Models;
using Microsoft.EntityFrameworkCore;

namespace leads.api.Repositories.Implementations
{
    public class LeadRepository : ILeadRepository
    {
        private readonly AppDbContext _context;

        public LeadRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Lead>> GetAllAsync()
        {
            return await _context.Leads.AsNoTracking().ToListAsync();
        }

        public async Task<Lead> GetByIdAsync(int id)
        {
            var lead = await _context.Leads.AsNoTracking().FirstOrDefaultAsync(l => l.LeadId == id);

            if (lead is null)
            {
                throw new ArgumentNullException(nameof(lead));
            }

            return lead;
        }

        public async Task<Lead> CreateAsync(Lead lead)
        {
            if (lead is null)
            {
                throw new ArgumentNullException(nameof(lead));
            }

            await _context.Leads.AddAsync(lead);
            await _context.SaveChangesAsync();

            return lead;
        }

        public async Task<Lead> UpdateAsync(Lead lead)
        {
            if (lead is null)
            {
                throw new ArgumentNullException(nameof(lead));
            }

            _context.Entry(lead).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return lead;
        }

        public async Task<Lead> DeleteAsync(int id)
        {
            var lead = _context.Leads.Find(id);

            if (lead is null)
            {
                throw new ArgumentNullException(nameof(lead));
            }

            _context.Remove(lead);
            await _context.SaveChangesAsync();

            return lead;
        }
    }
}
