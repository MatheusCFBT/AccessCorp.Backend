using AccessCorpUsers.Application.Entities;
using AccessCorpUsers.Application.Interfaces;
using AccessCorpUsers.Domain.Entities;
using AccessCorpUsers.Domain.Interfaces;
using AccessCorpUsers.Domain.Validations.DocsValidation;
using AccessCorpUsers.Infra.Repositories;
using AutoMapper;

namespace AccessCorpUsers.Application.Services
{
    public class GuestService : IGuestService
    {
        private readonly IMapper _mapper;
        private readonly IGuestRepository _guestRepository;
        private readonly IDoormanRepository _doormanRepository;

        public GuestService(IMapper mapper, IGuestRepository guestRepository, IDoormanRepository doormanRepository)
        {
            _mapper = mapper;
            _guestRepository = guestRepository;
            _doormanRepository = doormanRepository;
        }

        public async Task<Result> ExcludeGuest(string email)
        {
            var guest = await _guestRepository.GetGuestByEmail(email);

            await _guestRepository.Remove(guest.Id);

            return Result.Ok("Usúario deletado");
        }

        public async Task<Result> RegisterGuest(GuestVM request)
        {
            if (!CpfValidation.Validate(request.Cpf) || !CepValidation.Validate(request.Cep))
                return Result.Fail("CPF ou CEP inválidos");

            if (_guestRepository.Find(a => a.Cpf == request.Cpf).Result.Any() ||
                _guestRepository.Find(a => a.Email == request.Email).Result.Any())
                return Result.Fail("Já existe um residente com esse CPF ou email");

            var guest = _mapper.Map<Guest>(request);

            await _guestRepository.Add(guest);

            return Result.Ok(guest);
        }

        public async Task<Result> UpdateGuest(string email, GuestVM request)
        {
            if (!CpfValidation.Validate(request.Cpf) || !CepValidation.Validate(request.Cep))
                return Result.Fail("CPF ou CEP inválidos");

            var existingGuest = await _guestRepository.GetGuestByEmail(email);

            if (existingGuest == null)
                return Result.Fail("Usuário não existe");

            _mapper.Map(request, existingGuest);

            await _guestRepository.Update(existingGuest);

            return Result.Ok("Usuário alterado");
        }

        public async Task<Result> ViewAllGuests(string email)
        {
            var requestAdmin = await _doormanRepository.GetDoormanByEmail(email);

            var guest = await _guestRepository.GetGuestByCep(requestAdmin.Cep);

            var guestVM = _mapper.Map<List<GuestVM>>(guest);

            return Result.Ok(guestVM);
        }

        public async Task<GuestVM> ViewGuestById(Guid id)
        {
            var guest = await _guestRepository.GetById(id);

            var guestVM = _mapper.Map<GuestVM>(guest);

            return guestVM;
        }
    }
}
