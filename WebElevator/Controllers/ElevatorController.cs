// using WebbElevator.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace WebbElevator.Controllers
{
    [Route("[controller]")]
    public class ElevatorController : Controller
    {
        private readonly ICompositeViewEngine _viewEngine;
        private readonly ElevatorSvc _elevatorSvc;

        public ElevatorController(ICompositeViewEngine viewEngine, ElevatorSvc elevatorSvc)
        {
            _viewEngine = viewEngine;
            _elevatorSvc = elevatorSvc;
        }

        [HttpPost("init")]
        public IActionResult InitElevator()
        {
            var outsideElevatorView = PartialView("_outsideElevator", new ElevatorVM { CurrentFloor = _elevatorSvc.GetElevatorFloor() });
            var outsideElevatorHtml = ConvertViewToString(this.ControllerContext, outsideElevatorView, _viewEngine);
            return Ok(Json(new { outsideElevatorHtml }));
        }

        [HttpPost("call")]
        public IActionResult CallElevator(int floor)
        {
            try
            {
                _elevatorSvc.RequestFloor(floor);
                var insideElevatorView = PartialView("_insideElevator", new ElevatorVM { CurrentFloor = _elevatorSvc.GetElevatorFloor() });
                var insideElevatorHtml = ConvertViewToString(this.ControllerContext, insideElevatorView, _viewEngine);
                return Ok(Json(new { insideElevatorHtml }));
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("inside")]
        public IActionResult PressButtonInside(int floor)
        {
            try
            {
                _elevatorSvc.RequestFloor(floor);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        public string ConvertViewToString(ControllerContext controllerContext, PartialViewResult partialViewResult, ICompositeViewEngine _viewEngine)
        {
            try
            {
                using var writer = new StringWriter();
                var viewEngineResult = _viewEngine.FindView(controllerContext, partialViewResult.ViewName, false);
                var viewContext = new ViewContext(controllerContext, viewEngineResult.View, partialViewResult.ViewData, partialViewResult.TempData, writer, new HtmlHelperOptions());
                viewEngineResult.View.RenderAsync(viewContext);
                return writer.GetStringBuilder().ToString();
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}