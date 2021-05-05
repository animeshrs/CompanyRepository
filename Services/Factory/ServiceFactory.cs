using System.ComponentModel.Design;
using AutoMapper;
using Persistence;

namespace Services.Factory
{
    public class ServiceFactory : IServiceFactory
    {
        private readonly ServiceContainer _serviceContainer;
        private readonly DataContext _context;
        private readonly IMapper _iMapper;

        public ServiceFactory(DataContext context, IMapper iMapper)
        {
            _context = context;
            _serviceContainer = new ServiceContainer();
            _iMapper = iMapper;
        }
        public CompanyService GetCompanyService()
        {
            var service = (CompanyService)_serviceContainer.GetService(typeof(CompanyService));

            if (service == null)
            {
                service = new CompanyService(_context, _iMapper);
                _serviceContainer.AddService(typeof(CompanyService), service);
            }

            return service;
        }
    }
}
