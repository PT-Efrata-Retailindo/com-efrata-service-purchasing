using Com.Efrata.Service.Purchasing.Lib.Models.GarmentUnitReceiptNoteModel;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Efrata.Service.Purchasing.Test.RackingInventoryTest.Models
{
    public class GarmentDOItemModelTest
    {
        [Fact(DisplayName = "Should_Create_GarmentDOItems_With_Correct_Values")]
        public void Should_Create_GarmentDOItems_With_Correct_Values()
        {
            // Arrange
            var garmentDOItem = new GarmentDOItems
            {
                DOItemNo = "DO123",
                UId = "UID456",
                UnitId = 1,
                UnitCode = "UC001",
                UnitName = "Unit Name A",
                StorageId = 2,
                StorageCode = "SC002",
                StorageName = "Storage Name B",
                POId = 3,
                POItemId = 4,
                PRItemId = 5,
                EPOItemId = 6,
                POSerialNumber = "POSN789",
                ProductId = 7,
                ProductCode = "PCODE123",
                ProductName = "Product ABC",
                DesignColor = "Black",
                SmallQuantity = 100.5m,
                RemainingQuantity = 50.25m,
                SmallUomId = 8,
                SmallUomUnit = "PCS",
                DOCurrencyRate = 14500.5,
                DetailReferenceId = 9,
                URNItemId = 10,
                RO = "RO987",
                CustomsCategory = "IMPORT",
                Colour = "Red",
                Rack = "R01",
                Level = "L2",
                Box = "BX-1",
                Area = "A1",
                SplitQuantity = 75.5m
            };

            // Assert
            Assert.Equal("DO123", garmentDOItem.DOItemNo);
            Assert.Equal("UID456", garmentDOItem.UId);
            Assert.Equal(1, garmentDOItem.UnitId);
            Assert.Equal("UC001", garmentDOItem.UnitCode);
            Assert.Equal("Unit Name A", garmentDOItem.UnitName);
            Assert.Equal(2, garmentDOItem.StorageId);
            Assert.Equal("SC002", garmentDOItem.StorageCode);
            Assert.Equal("Storage Name B", garmentDOItem.StorageName);
            Assert.Equal(3, garmentDOItem.POId);
            Assert.Equal(4, garmentDOItem.POItemId);
            Assert.Equal(5, garmentDOItem.PRItemId);
            Assert.Equal(6, garmentDOItem.EPOItemId);
            Assert.Equal("POSN789", garmentDOItem.POSerialNumber);
            Assert.Equal(7, garmentDOItem.ProductId);
            Assert.Equal("PCODE123", garmentDOItem.ProductCode);
            Assert.Equal("Product ABC", garmentDOItem.ProductName);
            Assert.Equal("Black", garmentDOItem.DesignColor);
            Assert.Equal(100.5m, garmentDOItem.SmallQuantity);
            Assert.Equal(50.25m, garmentDOItem.RemainingQuantity);
            Assert.Equal(8, garmentDOItem.SmallUomId);
            Assert.Equal("PCS", garmentDOItem.SmallUomUnit);
            Assert.Equal(14500.5, garmentDOItem.DOCurrencyRate);
            Assert.Equal(9, garmentDOItem.DetailReferenceId);
            Assert.Equal(10, garmentDOItem.URNItemId);
            Assert.Equal("RO987", garmentDOItem.RO);
            Assert.Equal("IMPORT", garmentDOItem.CustomsCategory);
            Assert.Equal("Red", garmentDOItem.Colour);
            Assert.Equal("R01", garmentDOItem.Rack);
            Assert.Equal("L2", garmentDOItem.Level);
            Assert.Equal("BX-1", garmentDOItem.Box);
            Assert.Equal("A1", garmentDOItem.Area);
            Assert.Equal(75.5m, garmentDOItem.SplitQuantity);
        }
    }
}
