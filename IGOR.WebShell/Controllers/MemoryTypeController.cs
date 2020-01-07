using IGOR.EntityModel.Derictories;
using IGOR.EntityModel.Entities;
using System;
using System.Linq;
using System.Web.Mvc;

namespace IGOR.WebShell.Controllers
{
	public class MemoryTypeController : Controller
	{
		readonly IDerictory<MemoryType> Derictory = new MemoryTypeDerictory();

		public ActionResult Index()
		{
			var items = Derictory.Items;

			if (Derictory.Error != null) {
				return View("Error", Derictory.Error);
			} else {
				return View(items);
			}
		}

		[HttpGet]
		public ActionResult Create()
		{
			return View(new MemoryType());
		}

		[HttpPost]
		public ActionResult Create(MemoryType item)
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
			if (Derictory.Find(id) is MemoryType item) {
				return View(item);
			} else {
				return RedirectToAction("Error", Derictory.Error);
			}
		}

		[HttpPost]
		public ActionResult Delete(MemoryType item)
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
			if (Derictory.Find(id) is MemoryType item) {
				return View(item);
			} else {
				return RedirectToAction("Error", Derictory.Error);
			}
		}

		[HttpPost]
		public ActionResult Edit(MemoryType item)
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