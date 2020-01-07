using IGOR.EntityModel.Derictories;
using IGOR.EntityModel.Entities;
using IGOR.WebShell.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace IGOR.WebShell.Controllers
{
	public class DriveController : Controller
	{
		readonly IDerictory<Drive> Derictory = new DriveDerictory();
		readonly IDerictory<Manufacturer> Manufacturers = new ManufacturerDerictory();

		public ActionResult Index(ComponentSearchModel model)
		{
			var items = from x in Derictory.Items
						where
							x.Manufacturer.Name.StartsWith(string.IsNullOrWhiteSpace(model.Manufacturer) ? "" : model.Manufacturer) &&
							x.Name.StartsWith(string.IsNullOrWhiteSpace(model.Name) ? "" : model.Name) && (
								model.LowPriceBorder <= 0 ||
								x.Price >= model.LowPriceBorder
							) && (
								model.HighPriceBorder <= model.LowPriceBorder ||
								x.Price <= model.HighPriceBorder
							) && (
								model.IsAvailable == false ||
								x.Count > 0
							)
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
			return View(new ComponentModel<Drive> {
				List = new SelectList(Manufacturers.Items, "Id", "Name"),
				Target = new Drive()
			});
		}

		[HttpPost]
		public ActionResult Create(ComponentModel<Drive> item)
		{
			if (item.Target.Validate() is Exception ex) {
				return View("Error", ex);
			}

			Derictory.Add(item.Target);

			if (Derictory.Error != null) {
				return View("Error", Derictory.Error);
			} else {
				return RedirectToAction("Index");
			}
		}

		[HttpGet]
		public ActionResult Delete(int id)
		{
			if (Derictory.Find(id) is Drive item) {
				return View(item);
			} else {
				return RedirectToAction("Error", Derictory.Error);
			}
		}

		[HttpPost]
		public ActionResult Delete(Drive item)
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
			if (Derictory.Find(id) is Drive item) {
				return View(new ComponentModel<Drive> {
					List = new SelectList(Manufacturers.Items, "Id", "Name"),
					Target = item
				});
			} else {
				return RedirectToAction("Error", Derictory.Error);
			}
		}

		[HttpPost]
		public ActionResult Edit(ComponentModel<Drive> item)
		{
			Derictory.Edit(item.Target);

			if (Derictory.Error != null) {
				return View("Error", Derictory.Error);
			} else {
				return RedirectToAction("Index");
			}
		}
	}
}