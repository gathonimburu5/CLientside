using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeClient.Models.Domain;

namespace EmployeeClient.Services.Interface
{
	public interface IDiscountService
	{
		Discount CreateDiscount(Discount discount);
		Discount UpdateDiscount(Discount discount);
		bool DeleteDiscount(int id);
		Discount GetDiscountById(int id);
		List<Discount> GetAllDiscountList();
	}
}