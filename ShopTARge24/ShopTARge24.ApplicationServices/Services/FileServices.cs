using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using ShopTARge24.Core.Domain;
using ShopTARge24.Core.Dto;
using ShopTARge24.Core.ServiceInterface;
using ShopTARge24.Data;


namespace ShopTARge24.ApplicationServices.Services
{
    public class FileServices : IFileServices
    {
        private readonly IHostEnvironment _webHost;
        private readonly ShopTARge24Context _context;

        public FileServices
            (
                IHostEnvironment webHost,
                ShopTARge24Context context
            )
        {
            _webHost = webHost;
            _context = context;
        }

        public void FilesToApi(SpaceshipDto dto, Spaceships domain)
        {
            if (dto.Files != null && dto.Files.Count > 0)
            {
                string uploadDir = Path.Combine(_webHost.ContentRootPath, "wwwroot", "multipleFileUpload");
                if (!Directory.Exists(uploadDir))
                {
                    Directory.CreateDirectory(uploadDir);
                }

                foreach (var file in dto.Files)
                {
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                    string filePath = Path.Combine(uploadDir, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    using (var memoryStream = new MemoryStream())
                    {
                        file.CopyTo(memoryStream);

                        FileToApi path = new FileToApi
                        {
                            Id = Guid.NewGuid(),
                            ExistingFilePath = Path.Combine("multipleFileUpload", uniqueFileName),
                            SpaceshipId = domain.Id,
                            ImageTitle = file.FileName,
                            ImageData = memoryStream.ToArray()
                        };

                        _context.FileToApis.Add(path);
                    }
                }

                _context.SaveChanges();
            }
        }

        public void FilesToApi(RealEstateDto dto, RealEstate domain)
        {
            if (dto.Files != null && dto.Files.Count > 0)
            {
                string uploadDir = Path.Combine(_webHost.ContentRootPath, "wwwroot", "multipleFileUpload");
                if (!Directory.Exists(uploadDir))
                {
                    Directory.CreateDirectory(uploadDir);
                }

                foreach (var file in dto.Files)
                {
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                    string filePath = Path.Combine(uploadDir, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    using (var memoryStream = new MemoryStream())
                    {
                        file.CopyTo(memoryStream);

                        FileToApi path = new FileToApi
                        {
                            Id = Guid.NewGuid(),
                            ExistingFilePath = Path.Combine("multipleFileUpload", uniqueFileName),
                            RealEstateId = domain.Id,
                            ImageTitle = file.FileName,
                            ImageData = memoryStream.ToArray()
                        };

                        _context.FileToApis.Add(path);
                    }
                }

                _context.SaveChanges();
            }
        }

        public async Task FilesToApi(KindergartenDto dto, Kindergarten domain)
        {
            if (dto.Files != null && dto.Files.Count > 0)
            {
                string uploadDir = Path.Combine(_webHost.ContentRootPath, "wwwroot", "multipleFileUpload");

                if (!Directory.Exists(uploadDir))
                {
                    Directory.CreateDirectory(uploadDir);
                }

                foreach (var file in dto.Files)
                {
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                    string filePath = Path.Combine(uploadDir, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }

                    byte[] imageData;
                    using (var memoryStream = new MemoryStream())
                    {
                        await file.CopyToAsync(memoryStream);
                        imageData = memoryStream.ToArray();
                    }

                    var path = new FileToApi
                    {
                        Id = Guid.NewGuid(),
                        ExistingFilePath = uniqueFileName,
                        KindergartenId = domain.Id,
                        ImageData = imageData,
                        ImageTitle = file.FileName
                    };

                    await _context.FileToApis.AddAsync(path);
                }

                await _context.SaveChangesAsync();
            }
        }





        public async Task<FileToApi> RemoveImageFromApi(FileToApiDto dto)
        { 
            //kui soovin kustutada siis pean läbi Id pildi ülesse otsima
            var imageId = await _context.FileToApis
                .FirstOrDefaultAsync(x => x.Id == dto.Id);

            //kus asuvad pildid mida hakatakse kustutama
            var filePath = _webHost.ContentRootPath + "\\wwwroot\\"
                + imageId.ExistingFilePath;

            if (File.Exists(filePath))
            { 
                File.Delete(filePath);
            }

            _context.FileToApis.Remove(imageId);
            await _context.SaveChangesAsync();

            return null;
        }

        public async Task<List<FileToApi>> RemoveImagesFromApi(FileToApiDto[] dtos)
        {
            //Mitme pildi korraga kustutamine
            foreach(var dto in dtos)
            {
                var imageId = await _context.FileToApis
                    .FirstOrDefaultAsync(x => x.ExistingFilePath == dto.ExistingFilePath);

                var filePath = _webHost.ContentRootPath + "\\wwwroot\\multipleFileUpload\\"
                    + imageId.ExistingFilePath;

                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                _context.FileToApis.Remove(imageId);
                await _context.SaveChangesAsync();
            }

            return null;
        }

        public void UploadFilesToDataBase(RealEstateDto dto, RealEstate domain)
        {
            //Toimub kontroll kas on vähemalt üks fail või mitu
            if (dto.Files != null && dto.Files.Count > 0)
            {

                //Tuleb kasutada foreach, et mitu faili ülesse laadida
                foreach (var file in dto.Files)
                {
                    //Foreachi sees tuleb kasutada using'ut
                    using (var target = new MemoryStream())
                    {
                        FileToDatabase files = new FileToDatabase()
                        {
                            Id = Guid.NewGuid(),
                            ImageTitle = file.FileName,
                            RealEstateId = domain.Id
                        };
                    }

                    //Andmed salvestada andmebaasi

                }
            }
        }

        public void UploadFilesToDatabase(RealEstateDto dto, RealEstate realEstate)
        {
            if (dto.Files != null && dto.Files.Count > 0)
            {
                foreach (var file in dto.Files)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        file.CopyTo(memoryStream);

                        FileToApi image = new FileToApi
                        {
                            Id = Guid.NewGuid(),
                            RealEstateId = realEstate.Id,
                            ImageTitle = file.FileName,
                            ImageData = memoryStream.ToArray()
                        };

                        _context.FileToApis.Add(image);
                    }
                }

                _context.SaveChanges();
            }
        }
    }
}