using AccessCorpUsers.Application.Entities;
using AccessCorpUsers.Application.Interfaces;
using AccessCorpUsers.Domain.Entities;
using AccessCorpUsers.Domain.Interfaces;
using AccessCorpUsers.Domain.Validations.DocsValidation;
using AutoMapper;

namespace AccessCorpUsers.Application.Services
{
    public class AdministratorService : IAdministratorService
    {
        private readonly IAdministratorRepository _administratorRepository;
        private readonly IIdentityApiClient _identityApiClient;
        private readonly IMapper _mapper;

        public AdministratorService(IAdministratorRepository administratorRepository, IIdentityApiClient client, IMapper mapper)
        {
            _administratorRepository = administratorRepository;
            _identityApiClient = client;
            _mapper = mapper;
        }
        public async Task<List<AdministratorVM>> ViewAllAdministrators()
        {
            var administrators = await _administratorRepository.GetAll();

            var adminVM = _mapper.Map<List<AdministratorVM>>(administrators);

            return adminVM;
        }

        public async Task<AdministratorVM> ViewAdministratorById(Guid id)
        {
            var administrators = await _administratorRepository.GetById(id);

            var adminVM = _mapper.Map<AdministratorVM>(administrators);

            return adminVM;
        }

        public async Task<AdministratorVM> RegisterAdministrator(AdministratorVM request)
        {
            if (!CpfValidation.Validate(request.Cpf) || !CepValidation.Validate(request.Cep))
                return new AdministratorVM();

            if (_administratorRepository.Find(a => a.Cpf == request.Cpf).Result.Any())
                return new AdministratorVM();

            AdministratorIdentityRequest identityRequest = new()
            {
                Email = request.Cpf,
                Password = request.Password,
                PasswordConfirmed = request.Password
            };

            var resultRequest = await _identityApiClient.RegisterAdministratorAsync(identityRequest);

            if (resultRequest == null)
                return new AdministratorVM();

            var admin = _mapper.Map<Administrator>(request);

            await _administratorRepository.Add(admin);

            return request;
        }

        public async Task<AdministratorVM> UpdateAdministrator(Guid id, AdministratorVM request)
        {
            if (!CpfValidation.Validate(request.Cpf) || !CepValidation.Validate(request.Cep))
                return new AdministratorVM();

            if (_administratorRepository.Find(a => a.Cpf == request.Cpf).Result.Any())
                return new AdministratorVM();

            var admin = _mapper.Map<Administrator>(request);

            await _administratorRepository.Update(admin);
            // update na api de identity com client

            return request;
        }

        public async Task<AdministratorVM> ExcludeAdministrator(Guid id)
        {
            await _administratorRepository.Remove(id);
            // excluir na api de identity com client
            return new AdministratorVM();
        }

        public void Dispose()
        {
            _administratorRepository.Dispose();
        }

    }
}
