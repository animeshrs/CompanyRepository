using AutoMapper;
using Domain;
using Persistence;
using Repositories;
using Services.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class CompanyService
    {
        private readonly DataContext _context;
        private readonly IGenericRepository<Company> _iRepository;
        private readonly IMapper _iMapper;
        public CompanyService(DataContext context, IMapper iMapper)
        {
            _context = context;
            _iRepository ??= new GenericRepository<Company>(context);
            _iMapper = iMapper;
        }

        public async Task<List<CompanyDto>> GetAllCompaniesAsync()
        {
            var companies = await _iRepository.GetAllAsync();
            var companyDtos = _iMapper.Map<List<CompanyDto>>(companies);
            return companyDtos;
        }

        public async Task<CompanyDto> GetCompanyByIdAsync(int id)
        {
            var company = await _iRepository.GetByIdAsync(id);
            var companyDto = _iMapper.Map<CompanyDto>(company);
            return companyDto;
        }

        public async Task<CompanyDto> GetCompanyByIsinAsync(string isin)
        {
            var company = await _context.Companies
                .Where(c => c.ISIN == isin)
                .Select(c => _iMapper.Map<CompanyDto>(c))
                .FirstOrDefaultAsync();
            var companyDto = _iMapper.Map<CompanyDto>(company);
            return companyDto;
        }

        public async Task<string> CreateCompanyAsync(CompanyDto companyDto)
        {
            var existingCompanies = await GetAllCompaniesAsync();
            if (existingCompanies
                .Where(c => !string.IsNullOrWhiteSpace(c.ISIN))
                .Any(c => (bool)c.ISIN?.Trim().Equals(companyDto.ISIN?.Trim())))
                return "Can't create- The Company with given ISIN already exists";

            var company = _iMapper.Map<CompanyDto, Company>(companyDto);

            await _iRepository.CreateAsync(company);
            return "Company Successfully Created";
        }


        public async Task<string> UpdateCompanyAsync(CompanyDto companyDto)
        {
            if (companyDto == null)
                return "Dto is null";

            var existingCompanies = await GetAllCompaniesAsync();
            if (existingCompanies
                .Where(c => !string.IsNullOrWhiteSpace(c.ISIN) && c.Id != companyDto.Id)
                .Any(c => (bool)c.ISIN?.Trim().Equals(companyDto.ISIN?.Trim())))
                return "Can't update- The Company with given ISIN already exists";

            var originalCompany = await _context.Companies.FindAsync(companyDto.Id);
            _context.Entry(originalCompany).CurrentValues.SetValues(companyDto);

            await _context.SaveChangesAsync();

            return "Company Successfully Updated";
        }
    }
}
