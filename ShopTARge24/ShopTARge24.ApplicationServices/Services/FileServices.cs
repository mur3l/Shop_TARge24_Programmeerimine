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
                if (!Directory.Exists(_webHost.ContentRootPath + "\\wwwroot\\multipleFileUpload\\"))
                {
                    Directory.CreateDirectory(_webHost.ContentRootPath + "\\wwwroot\\multipleFileUpload\\");
                }

                foreach (var file in dto.Files)
                {
                    string uploadsFolder = Path.Combine(_webHost.ContentRootPath,"wwwroot", "multipleFileUpload");
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);

                        FileToApi path = new FileToApi
                        {
                            Id = Guid.NewGuid(),
                            ExistingFilePath = uniqueFileName,
                            SpaceshipId = domain.Id
                        };

                        _context.FileToApis.AddAsync(path);
                    }
                }
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
            foreach (var dto in dtos)
            {
                var imageId = await _context.FileToApis
                    .FirstOrDefaultAsync(x => x.ExistingFilePath == dto.ExistingFilePath);
                var filepath = _webHost.ContentRootPath + "\\wwwroot\\multipleFileUpload\\"
                    + imageId 
            }

            return null;
        }
    }
}