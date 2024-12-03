
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Moq;
using WebbElevator.Controllers;
using Xunit;

namespace ElevatorControlSystem.Tests
{
    public class ElevatorEvoTests
    {
        private readonly Mock<ElevatorSvc> _mockService;
        private readonly Mock<ICompositeViewEngine> _mockViewEngine;
        private readonly ElevatorController _controller;

        public ElevatorEvoTests()
        {
            _mockViewEngine = new Mock<ICompositeViewEngine>();
            _controller = new ElevatorController(_mockViewEngine.Object, _mockService.Object);
        }

        [Fact]
        public void CallElevator_ValidFloor_ReturnsOk()
        {
            int floor = 3;
            var result = _controller.CallElevator(floor);
            Assert.IsType<OkResult>(result);
            _mockService.Verify(s => s.GetElevatorFloor(), Times.Once);
        }

        [Fact]
        public void PressButtonInside_ValidFloor_ReturnsOk()
        {
            int floor = 4;
            var result = _controller.PressButtonInside(floor);
            Assert.IsType<OkResult>(result);
            _mockService.Verify(s => s.GetElevatorFloor(), Times.Once);
        }
    }
}