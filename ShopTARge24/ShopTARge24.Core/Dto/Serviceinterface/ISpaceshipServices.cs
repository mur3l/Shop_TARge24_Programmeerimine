using ShopTARge24.Core.Domain;
using ShopTARge24.Core.Dto;


namespace ShopTARge24.Core.ServiceInterface
{
    public interface ISpaceshipServices
    {
        Task<Spaceships> Create(SpaceshipDto dto);
        Task<Spaceships> Update(SpaceshipDto dto);
        Task<Spaceships> Delete(Guid id);
        Task<Spaceships> DetailAsync(Guid id);
    }
}