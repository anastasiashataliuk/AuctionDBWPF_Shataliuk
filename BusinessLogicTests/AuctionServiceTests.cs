using Moq;
using NUnit.Compatibility;
using BusinessLogic.Services;
using BusinessLogic.Interfaces;
using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BusinessLogic.Tests
{
    [TestFixture]
    public class AuctionServiceTests
    {
        private Mock<IAuctionService> _auctionServiceMock;
        private AuctionService _auctionService;

        [SetUp]
        public void SetUp()
        {
            // Arrange: Create a mock instance of AuctionService, using a mock connection string
            var connectionString = "MockConnectionString";
            _auctionServiceMock = new Mock<IAuctionService>();
            _auctionService = new AuctionService(connectionString);
        }

        [Test]
        public void GetAllAuctions_ShouldReturnListOfAuctions()
        {
            // Arrange: Set up mock data
            var mockAuctions = new List<Auction>
            {
                new Auction
                {
                    Auction_Id = 1,
                    Start_Date = DateTime.Now,
                    End_Date = DateTime.Now.AddDays(2),
                    Starting_Price = 100.0m,
                    Buyout_Price = 150.0m,
                    Status = "Active"
                },
                new Auction
                {
                    Auction_Id = 2,
                    Start_Date = DateTime.Now.AddDays(1),
                    End_Date = DateTime.Now.AddDays(3),
                    Starting_Price = 200.0m,
                    Buyout_Price = 250.0m,
                    Status = "Active"
                }
            };

            _auctionServiceMock.Setup(service => service.GetAllAuctions()).Returns(mockAuctions);

            // Act: Call the method being tested
            var result = _auctionService.GetAllAuctions();

            // Assert: Verify the result
            Assert.NotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(1, result[0].Auction_Id);
            Assert.AreEqual(2, result[1].Auction_Id);
        }

        [Test]
        public void GetAuctionById_ShouldReturnAuction_WhenAuctionExists()
        {
            // Arrange: Set up mock data
            var mockAuction = new Auction
            {
                Auction_Id = 1,
                Start_Date = DateTime.Now,
                End_Date = DateTime.Now.AddDays(2),
                Starting_Price = 100.0m,
                Buyout_Price = 150.0m,
                Status = "Active"
            };

            _auctionServiceMock.Setup(service => service.GetAuctionById(1)).Returns(mockAuction);

            // Act: Call the method being tested
            var result = _auctionService.GetAuctionById(1);

            // Assert: Verify the result
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Auction_Id);
            Assert.AreEqual("Active", result.Status);
        }

        [Test]
        public void CreateAuction_ShouldExecuteWithoutError()
        {
            // Arrange: Set up a mock Auction to be created
            var newAuction = new Auction
            {
                Auction_Id = 3,
                Start_Date = DateTime.Now,
                End_Date = DateTime.Now.AddDays(2),
                Starting_Price = 100.0m,
                Buyout_Price = 150.0m,
                Status = "Active"
            };

            _auctionServiceMock.Setup(service => service.CreateAuction(It.IsAny<Auction>())).Verifiable();

            // Act: Call the method being tested
            _auctionService.CreateAuction(newAuction);

            // Assert: Verify that CreateAuction was called
            _auctionServiceMock.Verify(service => service.CreateAuction(It.IsAny<Auction>()), Times.Once);
        }
    }
}
