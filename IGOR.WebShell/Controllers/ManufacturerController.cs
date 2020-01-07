using IGOR.EntityModel.Derictories;
using IGOR.EntityModel.Entities;
using System;
using System.Linq;
using System.Web.Mvc;

namespace IGOR.WebShell.Controllers
{
	public class ManufacturerController : Controller
	{
		readonly IDerictory<Manufacturer> Derictory = new ManufacturerDerictory();

		public ActionResult Index(string country)
		{
			var items = from x in Derictory.Items
						where x.Country.StartsWith(string.IsNullOrWhiteSpace(country) ? "" : country)
						select x;

			if (Derictory.Error != null) {
				return View("Error", Derictory.Error);
			} else {
				return View(items);
			}
		}

		[HttpGet]
		public ActionResult Create()
		{
			return View(new Manufacturer());
		}

		[HttpPost]
		public ActionResult Create(Manufacturer item)
		{
			if (item.Validate() is Exception ex) {
				return View("Error", ex);
			}

			Derictory.Add(item);

			if (Derictory.Error is Exception ex2) {
				return View("Error", ex2);
			} else {
				return RedirectToAction("Index");
			}
		}

		[HttpGet]
		public ActionResult Delete(int id)
		{
			if (Derictory.Find(id) is Manufacturer item) {
				return View(item);
			} else {
				return RedirectToAction("Error", Derictory.Error);
			}
		}

		[HttpPost]
		public ActionResult Delete(Manufacturer item)
		{
			Derictory.Drop(item);

			if (Derictory.Error != null) {
				return View("Error", Derictory.Error);
			} else {
				return RedirectToAction("Index");
			}
		}

		[HttpGet]
		public ActionResult Edit(int id)
		{
			if (Derictory.Find(id) is Manufacturer item) {
				return View(item);
			} else {
				return RedirectToAction("Error", Derictory.Error);
			}
		}

		[HttpPost]
		public ActionResult Edit(Manufacturer item)
		{
			Derictory.Edit(item);

			if (Derictory.Error != null) {
				return View("Error", Derictory.Error);
			} else {
				return RedirectToAction("Index");
			}
		}
	}
}