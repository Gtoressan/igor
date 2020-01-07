using IGOR.EntityModel.Derictories;
using IGOR.EntityModel.Entities;
using IGOR.WebShell.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace IGOR.WebShell.Controllers
{
	public class GraphicsCardController : Controller
	{
		readonly IDerictory<GraphicsCard> Derictory = new GraphicsCardDerictory();
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
			return View(new ComponentModel<GraphicsCard> {
				List = new SelectList(Manufacturers.Items, "Id", "Name"),
				Target = new GraphicsCard()
			});
		}

		[HttpPost]
		public ActionResult Create(ComponentModel<GraphicsCard> item)
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
			if (Derictory.Find(id) is GraphicsCard item) {
				return View(item);
			} else {
				return RedirectToAction("Error", Derictory.Error);
			}
		}

		[HttpPost]
		public ActionResult Delete(GraphicsCard item)
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
			if (Derictory.Find(id) is GraphicsCard item) {
				return View(new ComponentModel<GraphicsCard> {
					List = new SelectList(Manufacturers.Items, "Id", "Name"),
					Target = item
				});
			} else {
				return RedirectToAction("Error", Derictory.Error);
			}
		}

		[HttpPost]
		public ActionResult Edit(ComponentModel<GraphicsCard> item)
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