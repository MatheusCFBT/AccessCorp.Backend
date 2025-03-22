﻿using AccessCorpUsers.Application.Entities;

namespace AccessCorpUsers.Application.Interfaces
{
    public interface IDeliveryService
    {
        public Task<Result> ViewAllDeliveries(string email);
        public Task<DeliveryVM> ViewDeliveryById(Guid id);
        public Task<Result> RegisterDelivery(DeliveryVM request);
        public Task<Result> UpdateDelivery(string email, DeliveryVM request);
        public Task<Result> ExcludeDelivery(string email);
    }
}
