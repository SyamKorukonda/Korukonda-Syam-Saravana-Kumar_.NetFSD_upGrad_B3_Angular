using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApplication19.Controllers;
using WebApplication19.Models;
using WebApplication19.Repositories;

namespace ProductApi.Nunit.Tests
{
    public class ProductControllerTests
    {
        [TestFixture]
        public class ProductsControllerTests
        {
            private Mock<IProductRepository> _mockRepository;
            private ProductsController _controller;

            [SetUp]
            public void Setup()
            {
                _mockRepository = new Mock<IProductRepository>();
                _controller = new ProductsController(_mockRepository.Object);
            }

            [Test]
            public async Task GetAll_WhenProductsExist_ReturnsOkWithProducts()
            {
                // Arrange
                var products = new List<Product>
            {
                new Product { Id = 1, Name = "Laptop", Price = 999.99, Category = "Electronics" },
                new Product { Id = 2, Name = "Book", Price = 19.99, Category = "Books" }
            };

                _mockRepository.Setup(repo => repo.GetAllAsync())
                               .ReturnsAsync(products);

                // Act
                var result = await _controller.GetAll();

                // Assert
                var okResult = result as OkObjectResult;
                Assert.That(okResult, Is.Not.Null);

                var returnValue = okResult.Value as IEnumerable<Product>;
                Assert.That(returnValue, Is.Not.Null);
                Assert.AreEqual(2, returnValue.Count());

                _mockRepository.Verify(repo => repo.GetAllAsync(), Times.Once);
            }

            [Test]
            public async Task GetById_ExistingId_ReturnsOkWithProduct()
            {
                // Arrange
                var product = new Product { Id = 1, Name = "Laptop", Price = 999.99, Category = "Electronics" };

                _mockRepository.Setup(repo => repo.GetByIdAsync(1))
                               .ReturnsAsync(product);

                // Act
                var result = await _controller.GetById(1);

                // Assert
                var okResult = result as OkObjectResult;
                Assert.That(okResult, Is.Not.Null);

                var returnValue = okResult.Value as Product;
                Assert.That(returnValue, Is.Not.Null);
                Assert.AreEqual("Laptop", returnValue.Name);

                _mockRepository.Verify(repo => repo.GetByIdAsync(1), Times.Once);
            }

            [Test]
            public async Task Create_ValidProduct_ReturnsCreatedAtAction()
            {
                // Arrange
                var product = new Product { Id = 1, Name = "Laptop", Price = 999.99, Category = "Electronics" };

                _mockRepository.Setup(repo => repo.AddAsync(It.IsAny<Product>()))
                               .ReturnsAsync(product);

                // Act
                var result = await _controller.Create(product);

                // Assert
                var createdResult = result as CreatedAtActionResult;
                Assert.That(createdResult, Is.Not.Null);

                Assert.AreEqual("GetById", createdResult.ActionName);
                Assert.AreEqual(1, createdResult.RouteValues["id"]);

                var returnValue = createdResult.Value as Product;
                Assert.That(returnValue, Is.Not.Null);
                Assert.AreEqual("Laptop", returnValue.Name);

                _mockRepository.Verify(repo => repo.AddAsync(It.Is<Product>(p => p.Name == "Laptop")), Times.Once);
            }

            [Test]
            public async Task Delete_ExistingId_ReturnsNoContent()
            {
                // Arrange
                _mockRepository.Setup(repo => repo.DeleteAsync(1))
                               .ReturnsAsync(true);

                // Act
                var result = await _controller.Delete(1);

                // Assert
                Assert.That(result, Is.TypeOf<NoContentResult>());

                _mockRepository.Verify(repo => repo.DeleteAsync(1), Times.Once);
            }
        }
    }
}
