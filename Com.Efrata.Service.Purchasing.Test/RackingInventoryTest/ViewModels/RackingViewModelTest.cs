using Com.Efrata.Service.Purchasing.Lib.ViewModels.GarmentUnitReceiptNoteViewModels.DOItems;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Efrata.Service.Purchasing.Test.RackingInventoryTest.ViewModels
{
    public class RackingViewModelTest
    {
        [Fact(DisplayName = "Should_Create_RackingViewModels_With_Correct_Values")]
        public void Should_Create_RackingViewModels_With_Correct_Values()
        {
            // Arrange
            var expectedColour = "Blue";
            var expectedRack = "R-01";
            var expectedLevel = "L-2";
            var expectedBox = "BX-12";
            var expectedQuantity = 25.5m;
            var expectedArea = "A1";

            // Act
            var viewModel = new RackingViewModels
            {
                Colour = expectedColour,
                Rack = expectedRack,
                Level = expectedLevel,
                Box = expectedBox,
                Quantity = expectedQuantity,
                Area = expectedArea
            };

            // Assert
            Assert.Equal(expectedColour, viewModel.Colour);
            Assert.Equal(expectedRack, viewModel.Rack);
            Assert.Equal(expectedLevel, viewModel.Level);
            Assert.Equal(expectedBox, viewModel.Box);
            Assert.Equal(expectedQuantity, viewModel.Quantity);
            Assert.Equal(expectedArea, viewModel.Area);
        }
    }
}
