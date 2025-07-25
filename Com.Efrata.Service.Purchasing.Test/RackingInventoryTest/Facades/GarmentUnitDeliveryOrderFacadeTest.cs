using Com.Efrata.Service.Purchasing.Lib;
using Com.Efrata.Service.Purchasing.Lib.Facades.GarmentUnitDeliveryOrderFacades;
using Com.Efrata.Service.Purchasing.Lib.Models.GarmentUnitDeliveryOrderModel;
using Com.Efrata.Service.Purchasing.Lib.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Com.Efrata.Service.Purchasing.Lib.ViewModels.GarmentUnitDeliveryOrderViewModel;
using Com.Efrata.Service.Purchasing.Lib.ViewModels.NewIntegrationViewModel;

namespace Com.Efrata.Service.Purchasing.Test.RackingInventoryTest.Facades
{
    public class GarmentUnitDeliveryOrderFacadeTest
    {
        private GarmentUnitDeliveryOrderFacade CreateFacade(string testName, string username = "UnitTest")
        {
            var options = new DbContextOptionsBuilder<PurchasingDbContext>()
                .UseInMemoryDatabase(databaseName: testName)
                .ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;

            var dbContext = new PurchasingDbContext(options);

            var serviceProviderMock = new Mock<IServiceProvider>();
            serviceProviderMock
                .Setup(x => x.GetService(typeof(IdentityService)))
                .Returns(new IdentityService { Username = username });

            return new GarmentUnitDeliveryOrderFacade(dbContext, serviceProviderMock.Object);
        }

        [Fact]
        public void Read_Returns_Data()
        {
            // Arrange
            string testName = nameof(Read_Returns_Data);
            var options = new DbContextOptionsBuilder<PurchasingDbContext>()
                .UseInMemoryDatabase(databaseName: testName)
                .ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;
            var dbContext = new PurchasingDbContext(options);

            var createdBy = "UnitTest";
            var unitDO = new GarmentUnitDeliveryOrder
            {
                Id = 1,
                UnitDONo = "UDO001",
                UnitDODate = DateTimeOffset.Now,
                UnitDOType = "PROSES",
                UnitRequestCode = "REQ01",
                UnitRequestName = "Unit Request",
                UnitSenderCode = "SEND01",
                UnitSenderName = "Unit Sender",
                StorageName = "Storage A",
                StorageCode = "ST01",
                StorageRequestCode = "SR01",
                StorageRequestName = "Storage Request",
                IsUsed = false,
                RONo = "RONO1",
                Article = "Article1",
                CreatedBy = createdBy,
                LastModifiedUtc = DateTimeOffset.Now.DateTime,
                UENFromNo = "UEN001",
                UENFromId = 2,
                Items = new List<GarmentUnitDeliveryOrderItem>
                {
                    new GarmentUnitDeliveryOrderItem
                    {
                        Id = 10,
                        DesignColor = "Black",
                        ProductId = 100,
                        ProductCode = "P100",
                        ProductName = "Product 100",
                        Area = "A1",
                        Colour = "Red",
                        Box = "B1",
                        Level = "L1",
                        Rack = "R1"
                    }
                }
            };
            dbContext.GarmentUnitDeliveryOrders.Add(unitDO);
            dbContext.SaveChanges();

            var serviceProviderMock = new Mock<IServiceProvider>();
            serviceProviderMock
                .Setup(x => x.GetService(typeof(IdentityService)))
                .Returns(new IdentityService { Username = createdBy });

            var facade = CreateFacade(testName, createdBy);

            // Act
            var result = facade.Read();

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Data.Count > 0);
            var data = result.Data.First();
            Assert.Equal(unitDO.UnitDONo, data.GetType().GetProperty("UnitDONo").GetValue(data, null));
            Assert.Equal(unitDO.RONo, data.GetType().GetProperty("RONo").GetValue(data, null));
            Assert.Equal(unitDO.UnitDOType, data.GetType().GetProperty("UnitDOType").GetValue(data, null));
            Assert.Equal(unitDO.Article, data.GetType().GetProperty("Article").GetValue(data, null));
            Assert.Equal(unitDO.UnitRequestName, data.GetType().GetProperty("UnitRequestName").GetValue(data, null));
            Assert.Equal(unitDO.StorageName, data.GetType().GetProperty("StorageName").GetValue(data, null));
            Assert.Equal(unitDO.CreatedBy, data.GetType().GetProperty("CreatedBy").GetValue(data, null));
            Assert.Equal(unitDO.UnitSenderName, data.GetType().GetProperty("UnitSenderName").GetValue(data, null));
            Assert.NotNull(data.GetType().GetProperty("Items").GetValue(data, null));
        }

        [Fact]
        public void ReadForUnitExpenditureNote_Returns_Data()
        {
            // Arrange
            string testName = nameof(ReadForUnitExpenditureNote_Returns_Data);
            var options = new DbContextOptionsBuilder<PurchasingDbContext>()
                .UseInMemoryDatabase(databaseName: testName)
                .ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;
            var dbContext = new PurchasingDbContext(options);

            var createdBy = "UnitTest";
            var unitDO = new GarmentUnitDeliveryOrder
            {
                Id = 1,
                UnitDONo = "UDO001",
                UnitDODate = DateTimeOffset.Now,
                UnitDOType = "PROSES",
                UnitRequestCode = "REQ01",
                UnitRequestName = "Unit Request",
                UnitSenderCode = "SEND01",
                UnitSenderName = "Unit Sender",
                StorageName = "Storage A",
                StorageCode = "ST01",
                StorageRequestCode = "SR01",
                StorageRequestName = "Storage Request",
                IsUsed = false,
                CreatedBy = createdBy,
                LastModifiedUtc = DateTimeOffset.Now.DateTime,
                Items = new List<GarmentUnitDeliveryOrderItem>
                {
                    new GarmentUnitDeliveryOrderItem
                    {
                        Id = 10,
                        DesignColor = "Black",
                        ProductId = 100,
                        ProductCode = "P100",
                        ProductName = "Product 100",
                        Area = "A1",
                        Colour = "Red",
                        Box = "B1",
                        Level = "L1",
                        Rack = "R1"
                    }
                }
            };
            dbContext.GarmentUnitDeliveryOrders.Add(unitDO);
            dbContext.SaveChanges();

            var serviceProviderMock = new Mock<IServiceProvider>();
            serviceProviderMock
                .Setup(x => x.GetService(typeof(IdentityService)))
                .Returns(new IdentityService { Username = createdBy });

            // Setup IMapper to map GarmentUnitDeliveryOrder to GarmentUnitDeliveryOrderViewModel
            var mapperMock = new Mock<AutoMapper.IMapper>();
            mapperMock.Setup(m => m.Map<List<Com.Efrata.Service.Purchasing.Lib.ViewModels.GarmentUnitDeliveryOrderViewModel.GarmentUnitDeliveryOrderViewModel>>(It.IsAny<List<GarmentUnitDeliveryOrder>>()))
                .Returns((List<GarmentUnitDeliveryOrder> src) =>
                    src.Select(x => new Com.Efrata.Service.Purchasing.Lib.ViewModels.GarmentUnitDeliveryOrderViewModel.GarmentUnitDeliveryOrderViewModel
                    {
                        Id = x.Id,
                        UnitDONo = x.UnitDONo,
                        UnitDOType = x.UnitDOType,
                        IsUsed = x.IsUsed,
                        Storage = new Lib.ViewModels.IntegrationViewModel.StorageViewModel
                        {
                            name = x.StorageName,
                            code = x.StorageCode
                        },
                        UnitDODate = x.UnitDODate,
                        StorageRequest = new Lib.ViewModels.IntegrationViewModel.StorageViewModel
                        {
                            name = x.StorageRequestName,
                            code = x.StorageRequestCode
                        },
                        UnitRequest = new UnitViewModel
                        {
                            Name = x.UnitRequestName,
                            Code = x.UnitRequestCode
                        },
                        UnitSender = new UnitViewModel
                        {
                            Name = x.UnitSenderName,
                            Code = x.UnitSenderCode
                        },
                        CreatedBy = x.CreatedBy,
                        LastModifiedUtc = x.LastModifiedUtc,
                        Items = x.Items.Select(i => new GarmentUnitDeliveryOrderItemViewModel
                        {
                            Id = i.Id,
                            ProductId = i.ProductId,
                            ProductCode = i.ProductCode,
                            ProductName = i.ProductName,
                            ProductRemark = i.ProductRemark,
                            Quantity = i.Quantity,
                            DefaultDOQuantity = i.DefaultDOQuantity,
                            DODetailId = i.DODetailId,
                            EPOItemId = i.EPOItemId,
                            FabricType = i.FabricType,
                            PricePerDealUnit = i.PricePerDealUnit,
                            POSerialNumber = i.POSerialNumber,
                            POItemId = i.POItemId,
                            PRItemId = i.PRItemId,
                            UomId = i.UomId,
                            UomUnit = i.UomUnit,
                            RONo = i.RONo,
                            URNItemId = i.URNItemId,
                            DesignColor = i.DesignColor,
                            DOCurrency = null,
                            CustomsCategory = i.CustomsCategory,
                            Colour = i.Colour,
                            Box = i.Box,
                            Level = i.Level,
                            Rack = i.Rack,
                            Area = i.Area
                        }).ToList()
                    }).ToList()
                );
            serviceProviderMock
                .Setup(x => x.GetService(typeof(AutoMapper.IMapper)))
                .Returns(mapperMock.Object);

            var facade = new GarmentUnitDeliveryOrderFacade(dbContext, serviceProviderMock.Object);

            // Act
            var result = facade.ReadForUnitExpenditureNote();

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Data.Count > 0);
            var data = result.Data.First();
            Assert.NotNull(data.GetType().GetProperty("UnitDONo").GetValue(data, null));
            Assert.NotNull(data.GetType().GetProperty("Items").GetValue(data, null));
        }
    }
}