using ShopTARge24.Core.Domain;
using ShopTARge24.Core.Dto;

namespace ShopTARge24.Core.ServiceInterface
{
    public interface IFileServices
    {
        void FilesToApi(SpaceshipDto dto, Spaceships domain);

        Task<FileToApi> RemoveImageFromApi(FileToApiDto dto);
    }
}