using AccessCorpUsers.Application.Entities;

namespace AccessCorpUsers.Application.Interfaces
{
    public interface IDoormanService
    {
        public Task<List<DoormanVM>> ViewAllDoorman();
        public Task<DoormanVM> ViewDoormanById(Guid id);
        public Task<DoormanVM> RegisterDoorman(DoormanVM request);
        public Task<DoormanVM> UpdateDoorman(Guid id, DoormanVM request);
        public Task<DoormanVM> ExcludeDoorman(Guid id);
    }
}
