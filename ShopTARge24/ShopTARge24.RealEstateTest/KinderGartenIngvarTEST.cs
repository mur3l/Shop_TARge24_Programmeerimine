using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using ShopTARge24.ApplicationServices.Services;
using ShopTARge24.Core.Domain;
using ShopTARge24.Core.Dto;
using ShopTARge24.Data;
using Xunit;

namespace ShopTARge24.RealEstateTest
{
    public class KinderGartenIngvarTEST
    {
        //Databaasi mällu loomine
        private ShopTARge24Context GetInMemoryDb()
        {
            var options = new DbContextOptionsBuilder<ShopTARge24Context>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new ShopTARge24Context(options);
        }

        //Lisa KinderGarten "Õiged andmed" - 1
        [Fact]
        public async Task Create_ValidData_CreateKindergarten()
        {
            //Arrange
            var db = GetInMemoryDb();
            var service = new KindergartenServices(db);

            var dto = new KindergartenDto
            {
                Id = Guid.NewGuid(),
                GroupName = "Pääsukesed",
                ChildrenCount = 15,
                KindergartenName = "Männiku lasteaed",
                TeacherName = "Irina"
            };

            //Act
            var result = await service.Create(dto);

            //Assert
            Assert.NotNull(result);
            Assert.Equal("Pääsukesed", result.GroupName);
            Assert.Equal("Männiku lasteaed", result.KindergartenName);
        }

        //Kontrolli andmeid "Irina" - 2
        [Fact]
        public async Task DetailAsync_ShouldReturnCorrect()
        {
            //Arrange
            var db = GetInMemoryDb();
            var id = Guid.NewGuid();

            db.Kindergarten.Add(new Kindergarten
            {
                Id = id,
                GroupName = "Pääsukesed",
                ChildrenCount = 15,
                KindergartenName = "Männiku lasteaed",
                TeacherName = "Irina"
            });

            await db.SaveChangesAsync();

            var service = new KindergartenServices(db);

            //Act
            var result = await service.DetailAsync(id);

            //Assert
            Assert.NotNull(result);
            Assert.Equal("Irina", result.TeacherName);
        }

        //Uuenda lasteaeda "Mari" - 3
        [Fact]
        public async Task Update_TeacherName()
        {
            //Arrange
            var db = GetInMemoryDb();
            var id = Guid.NewGuid();

            var existing = new Kindergarten
            {
                Id = Guid.NewGuid(),
                GroupName = "Pääsukesed",
                ChildrenCount = 15,
                KindergartenName = "Männiku lasteaed",
                TeacherName = "Irina"
            };

            db.Kindergarten.Add(existing);
            await db.SaveChangesAsync();

            var service = new KindergartenServices(db);

            var dto = new KindergartenDto
            {
                Id = existing.Id,
                GroupName = existing.GroupName,
                ChildrenCount = existing.ChildrenCount,
                KindergartenName = existing.KindergartenName,
                TeacherName = "Mari"
            };

            //Act
            var result = await service.Update(dto);

            //Assert
            Assert.NotNull(result);
            Assert.Equal("Mari", result.TeacherName);
        }

        //Lisa lasteaed "Valed andmed" - 4
        [Fact]
        public async Task Create_InvalidDataValidation()
        {
            //Arrange
            var db = GetInMemoryDb();
            var service = new KindergartenServices(db);

            var dto = new KindergartenDto
            {
                Id = Guid.NewGuid(),
                GroupName = "Pääsukesed",
                //Null ega negatiivne ei tohi olla, peab tagastama veateate
                ChildrenCount = -5,
                KindergartenName = "Männiku lasteaed",
                TeacherName = "Irina"
            };

            //Act and Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await service.Create(dto);
            });
        }

        //Uuenda lasteaeda "Valed andmed" - 5
        [Fact]
        public async Task Update_InvalidDataValidation()
        {
            //Arrange
            var db = GetInMemoryDb();
            var id = Guid.NewGuid();

            var existing = new Kindergarten
            {
                Id = id,
                GroupName = "Pääsukesed",
                ChildrenCount = 15,
                KindergartenName = "Männiku lasteaed",
                TeacherName = "Irina"
            };

            db.Kindergarten.Add(existing);
            await db.SaveChangesAsync();

            var service = new KindergartenServices(db);
            var dto = new KindergartenDto
            {
                Id = id,
                GroupName = existing.GroupName,
                //Null ega negatiivne ei tohi olla, peab tagastama veateate
                ChildrenCount = 0,
                KindergartenName = existing.KindergartenName,
                TeacherName = existing.TeacherName
            };

            //Act and Assert
            await Assert.ThrowsAsync<ArgumentException>(() => service.Update(dto));


            //Assert - Kontrolli, et andmed pole muutunud
            var stillExisting = await db.Kindergarten.FirstAsync(k => k.Id == id);
            Assert.Equal(15, stillExisting.ChildrenCount);

        }

        //Kustuta lasteaed - 6
        [Fact]
        public async Task Delete_RemoveKindergarten()
        {
            //Arrange
            var db = GetInMemoryDb();
            var id = Guid.NewGuid();

            db.Kindergarten.Add(new Kindergarten
            {
                Id = id,
                GroupName = "Pääsukesed",
                ChildrenCount = 15,
                KindergartenName = "Männiku lasteaed",
                TeacherName = "Mari"
            });

            db.FileToApis.Add(new FileToApi
            {
                Id = Guid.NewGuid(),
                KindergartenId = id,
                //Kujutletav pilt
                ImageTitle = "Ekskursioon.jpg",
                ImageData = new byte[] { 1, 2, 3 }
            });

            await db.SaveChangesAsync();

            var service = new KindergartenServices(db);

            //Act
            var deleted = await service.Delete(id);

            //Assert
            Assert.NotNull(deleted);
            Assert.False(await db.Kindergarten.AnyAsync(k => k.Id == id));
            Assert.False(await db.FileToApis.AnyAsync(f => f.KindergartenId == id));
        }
    }
}
