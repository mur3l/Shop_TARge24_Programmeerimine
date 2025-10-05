﻿using ShopTARge24.Core.Domain;
using ShopTARge24.Core.Dto;

namespace ShopTARge24.Core.ServiceInterface
{
    public interface IRealEstateServices
    {
        Task<RealEstate> Create(RealEstateDto dto);
        Task<RealEstate> DetailAsync(Guid id);
        Task<RealEstate> Delete(Guid id);
        Task<RealEstate> Update(RealEstateDto dto);
    }
}
