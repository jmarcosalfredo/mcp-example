using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using leads.api.Models;
using leads.api.Repositories;
using leads.api.Responses;

namespace leads.api.Services.Implementations
{
    public class LeadService : ILeadService
    {
        private readonly ILeadRepository _repository;

        public LeadService(ILeadRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResponseModel<List<Lead>>> FindAllAsync()
        {
            try
            {
                var result = await _repository.GetAllAsync();
                return ResponseModel<List<Lead>>.WithSuccess(result.ToList(), "List found with success!");
            }
            catch (Exception ex)
            {
                return ResponseModel<List<Lead>>.WithFail(ex.Message);
            }
        }

        public async Task<ResponseModel<Lead>> FindByIdAsync(int id)
        {
            try
            {
                var result = await _repository.GetByIdAsync(id);

                if (result == null)
                {
                    return ResponseModel<Lead>.WithFail("Lead not found!");
                }

                return ResponseModel<Lead>.WithSuccess(result, "Lead found with success!");
            }
            catch (Exception ex)
            {
                return ResponseModel<Lead>.WithFail(ex.Message);
            }
        }

        public async Task<ResponseModel<Lead>> CreateAsync(Lead lead)
        {
            try
            {
                await _repository.CreateAsync(lead);

                return ResponseModel<Lead>.WithSuccess(lead, "Lead created with succes!");
            }
            catch (Exception ex)
            {
                return ResponseModel<Lead>.WithFail(ex.Message);
            }
        }

        public async Task<ResponseModel<Lead>> UpdateAsync(Lead lead)
        {
            try
            {
                var leadToUpdate = await _repository.GetByIdAsync(lead.LeadId);

                if (leadToUpdate == null)
                {
                    return ResponseModel<Lead>.WithFail("Lead to update not found!");
                }

                await _repository.UpdateAsync(lead);

                return ResponseModel<Lead>.WithSuccess(lead, "Lead Updated!");
            }
            catch (Exception ex)
            {
                return ResponseModel<Lead>.WithFail(ex.Message);
            }
        }

        public async Task<ResponseModel<bool>> DeleteAsync(int id)
        {
            try
            {
                var leadToDelete = await _repository.GetByIdAsync(id);

                if (leadToDelete == null)
                {
                    return ResponseModel<bool>.WithFail("Lead to delete not found!");
                }

                await _repository.DeleteAsync(id);

                return ResponseModel<bool>.WithSuccess(true, "Lead Deleted!");
            }
            catch (Exception ex)
            {
                return ResponseModel<bool>.WithFail(ex.Message);
            }
        }
    }
}
