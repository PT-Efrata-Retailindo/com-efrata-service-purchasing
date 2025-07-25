using Com.Efrata.Service.Purchasing.Lib.Interfaces;
using Com.Efrata.Service.Purchasing.Lib.Models.GarmentUnitReceiptNoteModel;
using Com.Efrata.Service.Purchasing.Lib.Services;
using Com.Efrata.Service.Purchasing.Lib.ViewModels.GarmentUnitReceiptNoteViewModels;
using Com.Efrata.Service.Purchasing.Lib.ViewModels.GarmentUnitReceiptNoteViewModels.DOItems;
using Com.Efrata.Service.Purchasing.WebApi.Controllers.v1.GarmentUnitReceiptNoteControllers;
using Com.Moonlay.NetCore.Lib.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Efrata.Service.Purchasing.Test.RackingInventoryTest.Controllers
{
    public class GarmentDOItemsControllerTest
    {
        private Mock<IServiceProvider> GetServiceProvider()
        {
            var serviceProvider = new Mock<IServiceProvider>();
            serviceProvider
                .Setup(x => x.GetService(typeof(IdentityService)))
                .Returns(new IdentityService() { Token = "Token", Username = "Test" });

            return serviceProvider;
        }

        private GarmentDOItemController GetController(Mock<IGarmentDOItemFacade> facadeM)
        {
            var user = new Mock<ClaimsPrincipal>();
            var claims = new Claim[]
            {
                new Claim("username", "unittestusername")
            };
            user.Setup(u => u.Claims).Returns(claims);

            var servicePMock = GetServiceProvider();

            var controller = new GarmentDOItemController(servicePMock.Object, facadeM.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                    {
                        User = user.Object
                    }
                }
            };
            controller.ControllerContext.HttpContext.Request.Headers["Authorization"] = "Bearer unittesttoken";
            controller.ControllerContext.HttpContext.Request.Path = new PathString("/v1/unit-test");
            controller.ControllerContext.HttpContext.Request.Headers["x-timezone-offset"] = "7";

            return controller;
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void GetDOItemsByPO_Success_And_Error(bool isError)
        {
            var facadeMock = new Mock<IGarmentDOItemFacade>();
            if (isError)
                facadeMock.Setup(f => f.GetByPO(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Throws(new System.Exception("error"));
            else
                facadeMock.Setup(f => f.GetByPO(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(new List<DOItemsViewModels>());

            var controller = GetController(facadeMock);

            var result = controller.GetDOItemsByPO("code", "po", "rack");
            if (isError)
            {
                var objResult = Assert.IsType<ObjectResult>(result);
                Assert.Equal(500, objResult.StatusCode);
            }
            else
            {
                Assert.IsType<OkObjectResult>(result);
            }
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void GetXls_Success_And_Error(bool isError)
        {
            var facadeMock = new Mock<IGarmentDOItemFacade>();
            if (isError)
                facadeMock.Setup(f => f.GenerateExcel(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Throws(new System.Exception("error"));
            else
                facadeMock.Setup(f => f.GenerateExcel(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(new MemoryStream(new byte[] { 1, 2, 3 }));

            var controller = GetController(facadeMock);

            var result = controller.GetXls("code", "po", "unitcode");
            if (isError)
            {
                var objResult = Assert.IsType<ObjectResult>(result);
                Assert.Equal(500, objResult.StatusCode);
            }
            else
            {
                Assert.IsType<FileContentResult>(result);
            }
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void Get_Success_And_Error(bool isError)
        {
            var facadeMock = new Mock<IGarmentDOItemFacade>();
            if (isError)
                facadeMock.Setup(f => f.ReadById(It.IsAny<int>())).Returns((GarmentDOItems)null);
            else
                facadeMock.Setup(f => f.ReadById(It.IsAny<int>())).Returns(new GarmentDOItems());

            var controller = GetController(facadeMock);

            var result = controller.Get(1);
            if (isError)
            {
                var objResult = Assert.IsType<ObjectResult>(result);
                Assert.Equal(500, objResult.StatusCode);
            }
            else
            {
                Assert.IsType<OkObjectResult>(result);
            }
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task Put_Success_And_BadRequest(bool isError)
        {
            var facadeMock = new Mock<IGarmentDOItemFacade>();
            if (isError)
            {
                facadeMock.Setup(f => f.Update(It.IsAny<int>(), It.IsAny<DOItemsRackingViewModels>()))
                   .ThrowsAsync(new ServiceValidationExeption(null, new List<ValidationResult>()));

            }
            else
            {
                facadeMock.Setup(f => f.Update(It.IsAny<int>(), It.IsAny<DOItemsRackingViewModels>()))
                    .ReturnsAsync(1);
            }

            var controller = GetController(facadeMock);

            var viewModel = new DOItemsRackingViewModels { Items = new List<RackingViewModels>() };

            var result = await controller.Put(1, viewModel);

            if (isError)
            {
                Assert.IsType<BadRequestObjectResult>(result);
            }
            else
            {
                Assert.IsType<NoContentResult>(result);
            }
        }
        [Fact]
        public async Task Put_Exception_Returns_InternalServerError()
        {
            var facadeMock = new Mock<IGarmentDOItemFacade>();
            facadeMock.Setup(f => f.Update(It.IsAny<int>(), It.IsAny<DOItemsRackingViewModels>()))
                .ThrowsAsync(new Exception("Unexpected error"));

            var controller = GetController(facadeMock);

            var viewModel = new DOItemsRackingViewModels { Items = new List<RackingViewModels>() };

            var result = await controller.Put(1, viewModel);

            var objResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, objResult.StatusCode);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void GetStelling_Success_And_Error(bool isError)
        {
            var facadeMock = new Mock<IGarmentDOItemFacade>();
            if (isError)
                facadeMock.Setup(f => f.GetStellingQuery(It.IsAny<int>(), It.IsAny<int>())).Throws(new System.Exception("error"));
            else
                facadeMock.Setup(f => f.GetStellingQuery(It.IsAny<int>(), It.IsAny<int>())).Returns(Task.FromResult(new List<StellingEndViewModels> { new StellingEndViewModels { POSerialNumber = "PO123" } }));

            var controller = GetController(facadeMock);

            var result = controller.GetStelling(1);
            if (isError)
            {
                var objResult = Assert.IsType<ObjectResult>(result);
                Assert.Equal(500, objResult.StatusCode);
            }
            else
            {
                Assert.IsType<OkObjectResult>(result);
            }
        }

        [Fact]
        public void GetStelling_Returns_FileStreamResult_When_AcceptPdf()
        {
            // Arrange
            var facadeMock = new Mock<IGarmentDOItemFacade>();
            var stellingList = new List<StellingEndViewModels>
            {
                new StellingEndViewModels { POSerialNumber = "PO123" }
            };
            var memoryStream = new MemoryStream(new byte[] { 1, 2, 3 });

            facadeMock.Setup(f => f.GetStellingQuery(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(stellingList);
            facadeMock.Setup(f => f.GeneratePdf(It.IsAny<List<StellingEndViewModels>>()))
                .Returns(memoryStream);

            var controller = GetController(facadeMock);

            // Simulate Accept header for PDF
            controller.ControllerContext.HttpContext.Request.Headers["Accept"] = "application/pdf";
            controller.ControllerContext.HttpContext.Request.Headers["x-timezone-offset"] = "7";

            // Act
            var result = controller.GetStelling(1);

            // Assert
            var fileResult = Assert.IsType<FileStreamResult>(result);
            Assert.Equal("application/pdf", fileResult.ContentType);
            Assert.Equal("Racking - PO123.pdf", fileResult.FileDownloadName);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void GetBarcode_Success_And_Error(bool isError)
        {
            var facadeMock = new Mock<IGarmentDOItemFacade>();
            if (isError)
                facadeMock.Setup(f => f.GetStellingQuery(It.IsAny<int>(), It.IsAny<int>())).Throws(new System.Exception("error"));
            else
                facadeMock.Setup(f => f.GetStellingQuery(It.IsAny<int>(), It.IsAny<int>())).Returns(Task.FromResult(new List<StellingEndViewModels> { new StellingEndViewModels { POSerialNumber = "PO123" } }));

            var controller = GetController(facadeMock);

            var result = controller.GetBarcode(1);
            if (isError)
            {
                var objResult = Assert.IsType<ObjectResult>(result);
                Assert.Equal(500, objResult.StatusCode);
            }
            else
            {
                Assert.IsType<OkObjectResult>(result);
            }
        }

        [Fact]
        public void GetBarcode_Returns_FileStreamResult_When_AcceptPdf()
        {
            // Arrange
            var facadeMock = new Mock<IGarmentDOItemFacade>();
            var stellingList = new List<StellingEndViewModels>
            {
                new StellingEndViewModels { POSerialNumber = "PO123" }
            };
            var memoryStream = new MemoryStream(new byte[] { 1, 2, 3 });

            facadeMock.Setup(f => f.GetStellingQuery(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(stellingList);
            facadeMock.Setup(f => f.GenerateBarcode(It.IsAny<List<StellingEndViewModels>>()))
                .Returns(memoryStream);

            var controller = GetController(facadeMock);

            // Simulate Accept header for PDF
            controller.ControllerContext.HttpContext.Request.Headers["Accept"] = "application/pdf";
            controller.ControllerContext.HttpContext.Request.Headers["x-timezone-offset"] = "7";

            // Act
            var result = controller.GetBarcode(1);

            // Assert
            var fileResult = Assert.IsType<FileStreamResult>(result);
            Assert.Equal("application/pdf", fileResult.ContentType);
            Assert.Equal("Racking - PO123.pdf", fileResult.FileDownloadName);
        }
    }
}
