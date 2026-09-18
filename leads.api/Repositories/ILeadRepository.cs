using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using leads.api.Models;

namespace leads.api.Repositories
{
    public interface ILeadRepository
    {
        Task<IEnumerable<Lead>> GetAsync();
        Task<Lead> GetByIdAsync(int id);
        Task<Lead> CreateAsync(Lead lead);
        Task<Lead> UpdateAsync(Lead lead);
        Task<Lead> DeleteAsync(int id);
    }
}
