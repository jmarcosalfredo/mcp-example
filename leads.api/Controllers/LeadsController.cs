using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using leads.api.Models;
using leads.api.Responses;
using leads.api.Services;
using Microsoft.AspNetCore.Mvc;

namespace leads.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LeadsController : ControllerBase
    {
        private readonly ILeadService _service;

        public LeadsController(ILeadService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseModel<List<Lead>>>> GetAll()
        {
            var response = await _service.FindAllAsync();

            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ResponseModel<Lead>>> GetById(int id)
        {
            var response = await _service.FindByIdAsync(id);

            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseModel<Lead>>> Create(Lead lead)
        {
            var response = await _service.CreateAsync(lead);

            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ResponseModel<Lead>>> Update(int id, Lead lead)
        {
            var response = await _service.UpdateAsync(id, lead);

            return response.Success ? Ok(response) : NotFound(response);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ResponseModel<bool>>> Delete(int id)
        {
            var response = await _service.DeleteAsync(id);

            return response.Success ? NoContent() : NotFound(response);
        }
    }
}
