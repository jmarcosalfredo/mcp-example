using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using leads.api.Models;
using leads.api.Responses;

namespace leads.api.Services
{
    public interface ILeadService
    {
        Task<ResponseModel<Lead>> FindByIdAsync(int id);
        Task<ResponseModel<List<Lead>>> FindAllAsync();
        Task<ResponseModel<Lead>> CreateAsync(Lead lead);
        Task<ResponseModel<Lead>> UpdateAsync(int id, Lead lead);
        Task<ResponseModel<bool>> DeleteAsync(int id);
    }
}
