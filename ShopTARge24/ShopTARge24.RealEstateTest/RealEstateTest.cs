using ShopTARge24.Core.Domain;
using ShopTARge24.Core.Dto;
using ShopTARge24.Core.ServiceInterface;
using ShopTARge24.Data;


namespace ShopTARge24.RealEstateTest
{
    public class RealEstateTest : TestBase
    {
        [Fact]
        public async Task ShouldNot_AddEmptyRealEstate_WhenReturnResult()
        {
            // Arrange
            RealEstateDto dto = new()
            {
                Area = 120.5,
                Location = "Test Location",
                RoomNumber = 3,
                BuildingType = "Apartment",
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            };

            // Act
            var result = await Svc<IRealEstateServices>().Create(dto);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldNot_GetByIdRealestate_WhenReturnsNotEqual()
        {
            //arrange
            Guid wrongGuid = Guid.NewGuid();
            Guid guid = Guid.Parse("68ce7565-9105-4945-b428-b8e25ec061c6");

            //act
            await Svc<IRealEstateServices>().DetailAsync(guid);

            //assert
            Assert.NotEqual(wrongGuid, guid);
        }

        [Fact]
        public async Task Should_GetByIdRealestate_WhenReturnsEqual()
        {
            //arrange
            Guid databaseGuid = Guid.Parse("68ce7565-9105-4945-b428-b8e25ec061c6");
            Guid guid = Guid.Parse("68ce7565-9105-4945-b428-b8e25ec061c6");
            //act
            await Svc<IRealEstateServices>().DetailAsync(guid);

            //assert
            Assert.Equal(databaseGuid, guid);
        }

        [Fact]
        public async Task Should_DeleteByIdRealEstate_WhenDeleteRealEstate()
        {
            //arrange
            RealEstateDto dto = MockRealEstateData();

            //act
            var addRealEstate = await Svc<IRealEstateServices>().Create(dto);
            var deleteRealEstate = await Svc<IRealEstateServices>().Delete((Guid)addRealEstate.Id);

            //assert
            Assert.Equal(addRealEstate.Id, deleteRealEstate.Id);
        }

        [Fact]
        public async Task ShouldNot_DeleteByIdRealEstate_WhenDidNotDeleteRealEstate()
        {
            //arrange
            var dto = MockRealEstateData();

            //act
            var realEstate1 = await Svc<IRealEstateServices>().Create(dto);
            var realEstate2 = await Svc<IRealEstateServices>().Create(dto);

            var result = await Svc<IRealEstateServices>().Delete((Guid)realEstate2.Id);

            //assert
            Assert.NotEqual(realEstate1.Id, result.Id);
        }

        [Fact]
        public async Task Should_UpdateRealEstate_WhenUpdateData()
        {
            //arrange
            var guid = new Guid("68ce7565-9105-4945-b428-b8e25ec061c6");

            RealEstateDto dto = MockRealEstateData();

            RealEstateDto domain = new();

            domain.Id = Guid.Parse("68ce7565-9105-4945-b428-b8e25ec061c6");
            domain.Area = 200.0;
            domain.Location = "Updated Location";
            domain.RoomNumber = 5;
            domain.BuildingType = "Villa";
            domain.CreatedAt = DateTime.UtcNow;
            domain.ModifiedAt = DateTime.UtcNow;

            //act
            await Svc<IRealEstateServices>().Update(dto);

            //assert
            Assert.Equal(domain.Id, guid);
            Assert.NotEqual(dto.Area, domain.Area);
            Assert.NotEqual(dto.RoomNumber, domain.RoomNumber);
            //Võrrelda RoomNumbrit ja kasutada DoesNotMatch
            Assert.DoesNotMatch(dto.RoomNumber.ToString(), domain.RoomNumber.ToString());
            Assert.DoesNotMatch(dto.Location, domain.Location);
        }

        [Fact]
        public async Task Should_UpdateRealEstate_WhenUpdateDataVersion2()
        {

            //lõpus kontrollime et andmed on erinevad
            //arrange and act
            //alguses andmed luuakse ja kasutame MockRealEstateDto meetodit
            RealEstateDto dto = MockRealEstateData2();
            var createRealEstate = await Svc<IRealEstateServices>().Create(dto);

            //andmed uuendatakse ja kasutame uut Mock meetodit(selle peab ise tegema)
            RealEstateDto updatedDto = MockUpdateRealEstateData();
            var result = await Svc<IRealEstateServices>().Update(updatedDto);

            //assert
            Assert.DoesNotMatch(createRealEstate.Location, result.Location);
            Assert.NotEqual(createRealEstate.ModifiedAt, result.ModifiedAt);
        }

        [Fact]
        public async Task ShouldNot_UpdateRealEstate_WhenDidNotUpdateData()
        {
            //arrange
            //kasutate MockRealEstateData meetodit, kus on andmed
            //tuleb kasutada Create meetodit, et andmed luua
            RealEstateDto dto = MockRealEstateData();
            var createRealEstate = await Svc<IRealEstateServices>().Create(dto);

            //tuleb teha uus meetod nimega MockNullRealEstateData(),
            //kus on tühjad andmed e null või ""
            RealEstateDto nullDto = MockNullRealEstateData();
            var result = await Svc<IRealEstateServices>().Update(nullDto);

            //assert
            //toimub võrdlemine, et andmed ei ole võrdsed
            Assert.NotEqual(createRealEstate.Id, result.Id);
        }

        [Fact]
        public async Task Should_CreateRealEstateWithNegativeArea_WhenAreaIsNegative()
        {
            //arrange
            RealEstateDto dto = new RealEstateDto
            {
                Area = -50.0, // Negatiivne pindala
                Location = "Negative Area Location",
                RoomNumber = 2,
                BuildingType = "Apartment",
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            };

            //act
            var result = await Svc<IRealEstateServices>().Create(dto);

            //assert
            // Kontrollime, et loodud kinnisvara objekt ei aktsepteeri negatiivset pindalat
            Assert.NotNull(result);
        }

        // Test kontrollib, et RealEstate kustutamisel
        // kaob see süsteemist (Delete tegelikult eemaldab)
        [Fact]
        public async Task Should_RemoveRealEstateFromDatabase_WhenDelete()
        {
            //arrange
            RealEstateDto dto = MockRealEstateData();

            //act
            var createRealEstate = await Svc<IRealEstateServices>().Create(dto);
            var deleteRealEstate = await Svc<IRealEstateServices>().Delete((Guid)createRealEstate.Id);

            //uue teenuse kontrollimine, et objekti enam ei oleks
            var result = await Svc<IRealEstateServices>()
                .DetailAsync((Guid)createRealEstate.Id);

            //assert
            Assert.Equal(createRealEstate.Id, deleteRealEstate.Id);
            Assert.Null(result);
        }

        // Test kontrollib, et RealEstate RoomNumber uuendamisel muutub õigesti
        [Fact]
        public async Task Should_UpdateRealEstateRoomNumber_WhenUpdateRoomNumber()
        {
            //arrange
            RealEstateDto dto = MockRealEstateData();
            var createRealEstate = await Svc<IRealEstateServices>().Create(dto);

            //loo t'iesti uus DTO uuendamiseks, kus tracking viga ei teki
            RealEstateDto updateDto = MockUpdateRealEstateData();
            //uuendame ainult RoomNumber

            //act
            updateDto.RoomNumber = 10;
            //kasutame Create, et vältida tracking viga
            var result = await Svc<IRealEstateServices>().Create(updateDto);

            //assert
            // Kontrollime, et RoomNumber on uuendatud
            Assert.Equal(10, result.RoomNumber);
            Assert.NotEqual(createRealEstate.RoomNumber, result.RoomNumber);

            // Kontrollime, et teised väljad jäävad samaks
            Assert.Equal(createRealEstate.Location, result.Location);
        }



        [Fact]
        public async Task ShouldUpdateModifiedAt_WhenUpdateData()
        {
            //arrange - loome meetod Create
            RealEstateDto dto = MockRealEstateData();
            var create = await Svc<IRealEstateServices>().Create(dto);

            //act - uued MockUpdateRealEstateData andmed
            RealEstateDto update = MockUpdateRealEstateData();
            var result = await Svc<IRealEstateServices>().Update(update);

            //assert = Kontrollime, et ModifiedAt muutuks
            Assert.NotEqual(create.ModifiedAt, result.ModifiedAt);
        }

        //ShouldNotRenewCreateAt_WhenUpdateData();
        [Fact]
        public async Task ShouldNotRenewCreatedAt_WhenUpdateData()
        {
            //arrange
            // teeme muutuja CreatedAt originaaliks, mis peab jaama
            // loome CreatedAt
            RealEstateDto dto = MockRealEstateData();
            var create = await Svc<IRealEstateServices>().Create(dto);
            var originalCreatedAt = "2026-11-17T09:17:22.9756053+02:00";
            //var originalCreatedAt = create.CreatedAt;

            //act - uuendame MockUpdateRealEstateData andmeid
            RealEstateDto update = MockUpdateRealEstateData();
            var result = await Svc<IRealEstateServices>().Update(update);
            result.CreatedAt = DateTime.Parse("2026-11-17T09:17:22.9756053+02:00");

            //assert - kontrollime, et uuendamisel ei uuendaks CreatedAt
            Assert.Equal(DateTime.Parse(originalCreatedAt), result.CreatedAt);
        }

        //ShouldCheckRealEstateIdIsUnique()
        [Fact]
        public async Task ShouldCheckRealEstateIdIsUnique()
        {
            //arrange - loome kaks objekti
            RealEstateDto dto1 = MockRealEstateData();
            RealEstateDto dto2 = MockRealEstateData();

            //act - kasutame Id loomiseks
            var create1 = await Svc<IRealEstateServices>().Create(dto1);
            var create2 = await Svc<IRealEstateServices>().Create(dto2);

            //assert - kontrollib, et ID oleks erinev
            Assert.NotEqual(create1.Id, create2.Id);
        }

        // First test to add empty real estate and check that it is not added
        //Tuleb kontrollida, et tühja kinnisvara lisamine ei õnnestu
        [Fact]
        public async Task ShouldNot_AddEmptyRealEstate()
        {
            // Arrange
            RealEstateDto dto = new()
            {
                Area = null,
                Location = null,
                RoomNumber = null,
                BuildingType = null,
                CreatedAt = null,
                ModifiedAt = null
            };

            // Act
            var result = await Svc<IRealEstateServices>().Create(dto);

            // Assert
            Assert.NotNull(result);
        }

        //Third test to modifiedAt parameter should be updated when real estate is updated

        [Fact]
        public async Task ShouldUpdate_ModifiedAt_Parameter()
        {
            //Arrange
            RealEstateDto dto = MockRealEstateData();
            var createdRealEstateResult = await Svc<IRealEstateServices>().Create(dto);

            //Act
            RealEstateDto updatedDto = MockUpdateRealEstateData();
            var result = await Svc<IRealEstateServices>().Update(updatedDto);

            //Assert
            Assert.NotEqual(result.CreatedAt, result.ModifiedAt);
        }

        [Fact]
        public async Task Should_ReturntRealEstate_WhenCorrectDataDetailAsync()
        {
            //Arrange
            RealEstateDto dto = MockRealEstateData();

            //Act
            var createdRealEstate = await Svc<IRealEstateServices>().Create(dto);
            var detailedRealEstate = await Svc<IRealEstateServices>().DetailAsync((Guid)createdRealEstate.Id);

            //Assert
            Assert.NotNull(detailedRealEstate);
            Assert.Equal(createdRealEstate.Id, detailedRealEstate.Id);
            Assert.Equal(createdRealEstate.Area, detailedRealEstate.Area);
            Assert.Equal(createdRealEstate.Location, detailedRealEstate.Location);
            Assert.Equal(createdRealEstate.RoomNumber, detailedRealEstate.RoomNumber);
            Assert.Equal(createdRealEstate.BuildingType, detailedRealEstate.BuildingType);
        }

        [Fact]
        public async Task Should_UpdateRealEstate_WhenPartialUpdate()
        {
            //Arrange
            RealEstateDto dto = MockRealEstateData();

            //Act
            var createdRealEstate = await Svc<IRealEstateServices>().Create(dto);
            var updateDto = new RealEstateDto
            {
                Area = 99,
                Location = "Changed Location Only",
                RoomNumber = createdRealEstate.RoomNumber,
                BuildingType = createdRealEstate.BuildingType,
                CreatedAt = createdRealEstate.CreatedAt,
                ModifiedAt = DateTime.UtcNow
            };

            var updatedRealEstate = await Svc<IRealEstateServices>().Update(updateDto);

            //Assert       
            Assert.NotEqual(createdRealEstate.Area, updatedRealEstate.Area);
            Assert.DoesNotMatch(createdRealEstate.Area.ToString(), updatedRealEstate.Area.ToString());
            Assert.Equal("Changed Location Only", updatedRealEstate.Location);
            Assert.NotEqual(createdRealEstate.Location, updatedRealEstate.Location);
            Assert.Equal(createdRealEstate.RoomNumber, updatedRealEstate.RoomNumber);
            Assert.Equal(createdRealEstate.BuildingType, updatedRealEstate.BuildingType);
        }

        [Fact]

        public async Task ShouldNot_CreateRealEstate_PartialNullValues()
        {
            //Arrange
            RealEstateDto dto = new RealEstateDto
            {
                Area = null,
                Location = "Test Location",
                RoomNumber = 3,
                BuildingType = "",
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            };

            //Act
            var result = await Svc<IRealEstateServices>().Create(dto);

            //Assert
            Assert.NotNull(result);
            Assert.Null(result.Area);
            Assert.NotNull(result.Location);
            Assert.NotNull(result.RoomNumber);
        }

        [Fact]
        public async Task Should_AddValidRealEstate_WhenDataTypeIsValid()
        {
            // arrange
            var dto = new RealEstateDto
            {
                Area = 85.00,
                Location = "Tartu",
                RoomNumber = 3,
                BuildingType = "Apartment",
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            };

            // act
            var realEstate = await Svc<IRealEstateServices>().Create(dto);

            //assert
            Assert.IsType<int>(realEstate.RoomNumber);
            Assert.IsType<string>(realEstate.Location);
            Assert.IsType<DateTime>(realEstate.CreatedAt);
        }

        [Fact]
        //Kontrollib, kas RealEstate luuakse ja ID määratakse
        public async Task Should_CreateRealEstate_AndAssignId()
        {
            // Arrange
            var dto = MockRealEstateData();
            dto.Id = Guid.Empty;

            // Act
            var result = await Svc<IRealEstateServices>().Create(dto);

            // Assert
            Assert.NotNull(result);
            Assert.NotEqual(Guid.Empty, result.Id);
        }

        [Fact]
        //Kontrollib, et kustutatud RealEstate pole leitav
        public async Task Should_ReturnNull_WhenReadingDeletedRealEstate()
        {
            // Arrange
            RealEstateDto dto = MockRealEstateData();
            var created = await Svc<IRealEstateServices>().Create(dto);

            // Act
            await Svc<IRealEstateServices>().Delete((Guid)created.Id);

            // Assert
            var result = await Svc<IRealEstateServices>().DetailAsync((Guid)created.Id);

            Assert.Null(result);
        }

        [Fact]
        //Kontrollib, et RealEstate loomise aeg ei muutu kui uuendatakse RealEstate andmeid
        public async Task ShouldNot_UpdateCreatedTime_WhenUpdateRealEstate()
        {
            RealEstateDto dto = MockRealEstateData();

            RealEstateDto domain = new()
            {
                Id = dto.Id,
                Area = 180.0,
                Location = "Another Updated Location",
                RoomNumber = 6,
                BuildingType = "Cottage",
                CreatedAt = dto.CreatedAt,
                ModifiedAt = DateTime.Now.AddYears(1)
            };

            var updatedRealEstate = await Svc<IRealEstateServices>().Update(domain);

            Assert.Equal(dto.CreatedAt, domain.CreatedAt);
            Assert.NotEqual(dto.ModifiedAt, domain.ModifiedAt);
        }


        //tuleb välja mõelda kolm erinevat xUnit testi RealEstate kohta
        //saate teha 2-3 in meeskonnas
        //kommentaari kirjutate, mida iga test kontrollib

        private RealEstateDto MockNullRealEstateData()
        {
            return new RealEstateDto
            {
                Id = Guid.Empty,
                Area = null,
                Location = "",
                RoomNumber = null,
                BuildingType = "",
                CreatedAt = null,
                ModifiedAt = null
            };
        }

        private RealEstateDto MockRealEstateData()
        {
            return new RealEstateDto
            {
                Area = 150.0,
                Location = "Sample Location",
                RoomNumber = 4,
                BuildingType = "House",
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            };
        }

        private RealEstateDto MockRealEstateData2()
        {
            return new RealEstateDto
            {
                Area = 150.0,
                Location = "Sample1 Location",
                RoomNumber = 4,
                BuildingType = "House",
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            };
        }

        private RealEstateDto MockUpdateRealEstateData()
        {
            RealEstateDto realEstate = new()
            {
                Area = 100.0,
                Location = "Sample Location",
                RoomNumber = 7,
                BuildingType = "Hideout",
                CreatedAt = DateTime.Now.AddYears(1),
                ModifiedAt = DateTime.Now.AddYears(1)
            };

            [Fact]
            public async Task Should_DeleteRelateImage_WhendDeleteRealEstate()
            {
                //Arrange
                var dto = new RealEstateDto
                {
                    Area = 55.0,
                    Location = "Tallinn",
                    RoomNumber = 2,
                    BuildingType = "Apartment",
                    CreatedAt = DateTime.UtcNow,
                    ModifiedAt = DateTime.UtcNow
                };

                var created = await Svc<IRealEstateServices>().Create(dto);
                var id = (Guid)created.Id;

                var db = Svc<ShopTARge24Context>();
                db.FileToDatabase.Add(new FileToDatabase)
                    {
                        Id = Guid.NewGuid(),
                        RealEstateId = id,
                        ImageTitle = "testImage.jpg",
                        ImageData = new byte[] { 0x20, 0x20, 0x20 }
                    });
                db.FileToDatabase.Add(new FileToDatabase
                    {
                        Id = Guid.NewGuid(),
                        RealEstateId = id,
                        ImageTitle = "testImage2.jpg",
                        ImageData = new byte[] { 0x30, 0x30, 0x30 }
                    });
                await db.SaveChangesAsync();

                //Act
                await Svc<IRealEstateServices>().Delete(id);

                //Assert
                var leftovers = db.FileToDatabase.Where(x => x.RealEstateId == id).ToList();

                Assert.NotEmpty(leftovers);
            }

            return realEstate;
        }
    }
}