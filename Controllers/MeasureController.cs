using AspNetCoreHero.ToastNotification.Abstractions;
using EmployeeClient.Models.Domain;
using EmployeeClient.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeClient.Controllers
{
	public class MeasureController : Controller
	{
		private readonly IMeasureService measureService;
		private readonly INotyfService notyfService;
		public MeasureController(IMeasureService measureService, INotyfService notyfService)
		{
			this.measureService = measureService;
			this.notyfService = notyfService;
		}
		public IActionResult Index()
		{
			var measure = measureService.GetAllMeasure();
			return View(measure);
		}
		[HttpGet]
		public IActionResult Create()
		{
			MeasureUnit measure = new MeasureUnit();
			return View(measure);
		}
		[HttpPost]
		public IActionResult Create([FromBody] MeasureUnit measure)
		{
			if (ModelState.IsValid)
			{
				var result = measureService.CreateMeasure(measure);
				if (result != null)
				{
					notyfService.Success("Measure Unit Created Successfully.");
					return RedirectToAction("Index");
				}
				else
				{
					notyfService.Error("Failed to Create Measure Unit!!");
					return View(result);
				}
			}
			else
			{
				notyfService.Warning("Error at the Model End!!");
				return View(measure);
			}
		}
		[HttpGet]
		public IActionResult Edit(int id)
		{
			var measure = measureService.GetMeasureById(id);
			return View(measure);
		}
		[HttpPost]
		public IActionResult Edit(MeasureUnit measure)
		{
			if (ModelState.IsValid)
			{
				var result = measureService.UpdateMeasure(measure);
				if (result != null)
				{
					notyfService.Success("Measure Unit Updated Successfully.");
					return RedirectToAction("Index");
				}
				else
				{
					notyfService.Error("Failed to Update Measure Unit!!");
					return View(result);
				}
			}
			else
			{
				notyfService.Warning("Error at the Model End!!");
				return View(measure);
			}
		}
		public IActionResult Delete(int id)
		{
			var result = measureService.DeleteMeasure(id);
			if(result)
			{
				notyfService.Success("Measure Unit Deleted Successfully.");
				return RedirectToAction("Index");
			}else
			{
				notyfService.Error("Error Occurred while Deleting Measure Unit!!!");
				return RedirectToAction("Index");
			}
		}
	}
}
