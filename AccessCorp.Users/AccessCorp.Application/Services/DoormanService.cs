using AccessCorpUsers.Application.Entities;
using AccessCorpUsers.Application.Interfaces;
using AccessCorpUsers.Domain.Entities;
using AccessCorpUsers.Domain.Interfaces;
using AccessCorpUsers.Domain.Validations.DocsValidation;
using AccessCorpUsers.Infra.Repositories;
using AutoMapper;

namespace AccessCorpUsers.Application.Services
{
    public class DoormanService : IDoormanService
    {
        private readonly IDoormanRepository _doormanRepository;
        private readonly IMapper _mapper;
        private readonly IIdentityApiClient _identityApiClient;

        public DoormanService(IDoormanRepository doormanRepository, IMapper mapper, IIdentityApiClient identityApiClient)
        {
            _doormanRepository = doormanRepository;
            _mapper = mapper;
            _identityApiClient = identityApiClient;
        }

        public async Task<List<DoormanVM>> ViewAllDoorman()
        {
            var doorman = await _doormanRepository.GetAll();

            var doormanVM = _mapper.Map<List<DoormanVM>>(doorman);

            return doormanVM;
        }

        public async Task<DoormanVM> ViewDoormanById(Guid id)
        {
            var doorman = await _doormanRepository.GetById(id);

            var doormanVM = _mapper.Map<DoormanVM>(doorman);

            return doormanVM;
        }
        
        public async Task<DoormanVM> RegisterDoorman(DoormanVM request)
        {
            if (!CpfValidation.Validate(request.Cpf) || !CepValidation.Validate(request.Cep))
                return new DoormanVM();

            if (_doormanRepository.Find(a => a.Cpf == request.Cpf).Result.Any())
                return new DoormanVM();

            AdministratorIdentityRequest identityRequest = new()
            {
                Email = request.Cpf,
                Password = request.Password,
                PasswordConfirmed = request.Password
            };

            // TODO
            // Criar metodo para doorman no apiIdentity
            var resultRequest = await _identityApiClient.RegisterAdministratorAsync(identityRequest);

            if (resultRequest == null)
                return new DoormanVM();

            var doorman = _mapper.Map<Doorman>(request);

            await _doormanRepository.Add(doorman);

            return request;
        }

        public async Task<DoormanVM> UpdateDoorman(Guid id, DoormanVM request)
        {
            if (!CpfValidation.Validate(request.Cpf) || !CepValidation.Validate(request.Cep))
                return new DoormanVM();

            if (_doormanRepository.Find(a => a.Cpf == request.Cpf).Result.Any())
                return new DoormanVM();

            var doorman = _mapper.Map<Doorman>(request);

            await _doormanRepository.Update(doorman);
            // update na api de identity com client

            return request;
        }
        public async Task<DoormanVM> ExcludeDoorman(Guid id)
        {
            await _doormanRepository.Remove(id);
            // excluir na api de identity com client
            return new DoormanVM();
        }
    }
}
