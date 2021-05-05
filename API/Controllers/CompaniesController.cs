using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Dtos;
using Services.Factory;
using System;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class CompaniesController : BaseApiController
    {
        private readonly CompanyService _service;

        public CompaniesController(IServiceFactory serviceFactory)
        {
            _service = serviceFactory.GetCompanyService();
        }

        [HttpGet]
        public async Task<IActionResult> GetCompanies()
        {
            try
            {
                var companies = await _service.GetAllCompaniesAsync();
                return Ok(companies);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCompany(int id)
        {
            try
            {
                var companies = await _service.GetCompanyByIdAsync(id);
                return Ok(companies);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Route("{isinId}/GetCompanyByIsin")]
        public async Task<IActionResult> GetCompanyByIsin(string isinId)
        {
            try
            {
                var companies = await _service.GetCompanyByIsinAsync(isinId);
                return Ok(companies);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(CompanyDto companyViewModel)
        {
            try
            {
                var createMessage = await _service.CreateCompanyAsync(companyViewModel);
                return Ok(createMessage);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(CompanyDto companyViewModel)
        {
            try
            {
                var updateMessage = await _service.UpdateCompanyAsync(companyViewModel);
                return Ok(updateMessage);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
