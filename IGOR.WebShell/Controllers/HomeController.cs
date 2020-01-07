using IGOR.EntityModel.Derictories;
using OfficeOpenXml;
using System.Linq;
using System.Web.Mvc;

namespace IGOR.WebShell.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}

		public void BuildSalesReport()
		{
			var excelPackage = new ExcelPackage();
			var worksheet = excelPackage.Workbook.Worksheets.Add("Продажи");
			var orderItemsDerictory = new OrderItemDerictory();

			worksheet.Cells["A1"].Value = "ФИО клиента";
			worksheet.Cells["B1"].Value = "Наименование";
			worksheet.Cells["C1"].Value = "Цена (в руб.)";
			worksheet.Cells["D1"].Value = "Количество";

			worksheet.Cells["A2"].LoadFromCollection(orderItemsDerictory.Items
				.Where(x => x.Order.State == "Выдан")
				.Select((x) => {
					return x.Order.Customer;
				}));
			worksheet.Cells["B2"].LoadFromCollection(orderItemsDerictory.Items
				.Where(x => x.Order.State == "Выдан")
				.Select((x) => {
					return x.Component.Name;
				}));
			worksheet.Cells["C2"].LoadFromCollection(orderItemsDerictory.Items
				.Where(x => x.Order.State == "Выдан")
				.Select((x) => {
					return x.Component.Price.ToString("N2");
				}));
			worksheet.Cells["D2"].LoadFromCollection(orderItemsDerictory.Items
				.Where(x => x.Order.State == "Выдан")
				.Select((x) => {
					return x.Count.ToString("N0");
				}));
			worksheet.Cells.AutoFitColumns();

			excelPackage.SaveAs(Response.OutputStream);
			Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
			Response.AddHeader("content-disposition", "attachment;  filename=Cписок выданных заказов.xlsx");
		}
	}
}