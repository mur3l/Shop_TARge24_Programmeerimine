using ShopTARge24.Core.Domain;
using ShopTARge24.Core.Dto;


namespace ShopTARge24.Core.ServiceInterface
{
    public interface IKindergartenServices
    {
        Task<Kindergarten> Create(KindergartenDto dto);
        Task<Kindergarten> Update(KindergartenDto dto);
        Task<Kindergarten> Delete(Guid id);
        Task<Kindergarten> DetailAsync(Guid id);

    }
}
