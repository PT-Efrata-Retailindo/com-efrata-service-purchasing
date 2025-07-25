using Com.Efrata.Service.Purchasing.Lib.ViewModels.GarmentUnitReceiptNoteViewModels.DOItems;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Efrata.Service.Purchasing.Test.RackingInventoryTest.ViewModels
{

    public class StellingViewModelsTests
    {
        public static StellingEndViewModels ToEndViewModel(StellingViewModels model)
        {
            return new StellingEndViewModels
            {
                id = model.id,
                POSerialNumber = model.POSerialNumber,
                Quantity = model.Quantity,
                Uom = model.Uom,
                Colour = model.Colour,
                Rack = model.Rack,
                Level = model.Level,
                Box = model.Box,
                Area = model.Area,
                ReceiptDate = model.ReceiptDate?.ToString("yyyy-MM-dd"),
                QuantityReceipt = model.QuantityReceipt,
                ExpenditureDate = model.ExpenditureDate?.ToString("yyyy-MM-dd"),
                QtyExpenditure = model.QtyExpenditure,
                Remaining = model.Remaining,
                Remark = model.Remark,
                User = model.User,
                Buyer = model.Buyer,
                Article = model.Article,
                Construction = model.Construction,
                Supplier = model.Supplier,
                DoNo = model.DoNo,
                RoNo = model.RoNo,
                ReceiptNo = model.ReceiptNo,
                ExpenditureNo = model.ExpenditureNo
            };
        }

        [Fact(DisplayName = "Should_Create_StellingViewModels_With_Correct_Values")]
        public void Should_Create_StellingViewModels_With_Correct_Values()
        {
            // Arrange
            var now = DateTime.Now;

            var viewModel = new StellingViewModels
            {
                id = 1,
                POSerialNumber = "PO123",
                Quantity = 10,
                Uom = "PCS",
                Colour = "Red",
                Rack = "R01",
                Level = "L1",
                Box = "B1",
                Area = "Main",
                ReceiptDate = now,
                QuantityReceipt = 9,
                ExpenditureDate = now.AddDays(1),
                QtyExpenditure = 8,
                Remaining = 1,
                Remark = "Good",
                User = "UnitTestUser",
                Buyer = "Buyer A",
                Article = "ART123",
                Construction = "Cotton",
                Supplier = "Supplier A",
                DoNo = "DO001",
                RoNo = "RO001",
                ReceiptNo = "RCPT001",
                ExpenditureNo = "EXP001",
                ProductId = 999
            };

            // Assert
            Assert.Equal("PO123", viewModel.POSerialNumber);
            Assert.Equal(10, viewModel.Quantity);
            Assert.Equal("PCS", viewModel.Uom);
            Assert.Equal("Red", viewModel.Colour);
            Assert.Equal("Main", viewModel.Area);
            Assert.Equal(9, viewModel.QuantityReceipt);
            Assert.Equal(8, viewModel.QtyExpenditure);
            Assert.Equal(1, viewModel.Remaining);
            Assert.Equal("Buyer A", viewModel.Buyer);
            Assert.Equal(999, viewModel.ProductId);
        }

        [Fact(DisplayName = "Should_Map_StellingViewModels_To_StellingEndViewModels")]
        public void Should_Map_StellingViewModels_To_StellingEndViewModels()
        {
            // Arrange
            var model = new StellingViewModels
            {
                id = 1,
                POSerialNumber = "PO123",
                Quantity = 5,
                ReceiptDate = new DateTime(2024, 1, 1),
                ExpenditureDate = new DateTime(2024, 1, 2),
            };

            // Act
            var result = ToEndViewModel(model);

            // Assert
            Assert.Equal("PO123", result.POSerialNumber);
            Assert.Equal(5, result.Quantity);
            Assert.Equal("2024-01-01", result.ReceiptDate);
            Assert.Equal("2024-01-02", result.ExpenditureDate);
        }


    }
}

