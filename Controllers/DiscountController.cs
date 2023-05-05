using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using EmployeeClient.Models.Domain;
using EmployeeClient.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EmployeeClient.Controllers
{
	public class DiscountController : Controller
	{
		private readonly IDiscountService discountService;
		private readonly INotyfService notification;
		public DiscountController(IDiscountService discountService, INotyfService notification)
		{
			this.discountService = discountService;
			this.notification = notification;
		}
		public IActionResult Index()
		{
			var result = discountService.GetAllDiscountList();
			return View(result);
		}
		[HttpGet]
		public IActionResult Create()
		{
			Discount discount = new Discount();
			return View(discount);
		}
		[HttpPost]
		public IActionResult Create(Discount discount)
		{
			if (ModelState.IsValid)
			{
				var result = discountService.CreateDiscount(discount);
				if (result != null)
				{
					notification.Success("Discounts Created Successfully.");
					return RedirectToAction(nameof(Index), "Discount");
				}
				else
				{
					notification.Error("Failed to Create Discount!!");
					return View(result);
				}
			}
			else
			{
				notification.Warning("Error at the Model!!");
				return View(discount);
			}
		}
		[HttpGet]
		public IActionResult Edit(int id)
		{
			var discount = discountService.GetDiscountById(id);
			return View(discount);
		}
		[HttpPost]
		public IActionResult Edit(Discount discount)
		{
			if (ModelState.IsValid)
			{
				var result = discountService.UpdateDiscount(discount);
				if (result != null)
				{
					notification.Success("Discount Successfully Updated.");
					return RedirectToAction(nameof(Index), "Discount");
				}
				else
				{
					notification.Error("Error Occurred while Updating, Check the Field Correctly!!");
					return View(result);
				}
			}
			else
			{
				notification.Warning("Error Occorred at the Model!!");
				return View(discount);
			}
		}
		public IActionResult Delete(int id)
		{
			var result = discountService.DeleteDiscount(id);
			if (result)
			{
				notification.Success("Discount Deleted Successfully.");
				return RedirectToAction(nameof(Index), "Discount");
			}
			{
				notification.Error("Error Occurred while Deleting, Please Check your Details!!");
				return RedirectToAction("Index");
			}
		}
	}
}