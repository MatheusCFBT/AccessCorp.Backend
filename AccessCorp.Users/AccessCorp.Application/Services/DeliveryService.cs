using AccessCorpUsers.Application.Entities;
using AccessCorpUsers.Application.Interfaces;
using AccessCorpUsers.Domain.Interfaces;
using AutoMapper;

namespace AccessCorpUsers.Application.Services
{
    public class DeliveryService : IDeliveryService
    {
        private readonly IDeliveryRepository _deliveryRepository;
        private readonly IDoormanRepository _doormanRepository;
        private readonly IMapper _mapper;

        public DeliveryService(IDeliveryRepository deliveryRepository, IDoormanRepository doormanRepository, IMapper mapper)
        {
            _deliveryRepository = deliveryRepository;
            _doormanRepository = doormanRepository;
            _mapper = mapper;
        }

        public async Task<Result> ViewAllDeliveries(string email)
        {
            var requestDoorman = await _doormanRepository.GetDoormanByEmail(email);

            var deliveries = await _deliveryRepository.GetDeliveriesByCep(requestDoorman.Cep);

            var deliveryVM = _mapper.Map<List<DeliveryVM>>(deliveries);

            return Result.Ok(deliveryVM);
        }

        Task<Result> IDeliveryService.RegisterDelivery(DeliveryVM request)
        {
            throw new NotImplementedException();
        }

        Task<Result> IDeliveryService.UpdateDelivery(string email, DeliveryVM request)
        {
            throw new NotImplementedException();
        }

        Task<DeliveryVM> IDeliveryService.ViewDeliveryById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Result> ExcludeDelivery(string email)
        {
            throw new NotImplementedException();
        }
    }
}
