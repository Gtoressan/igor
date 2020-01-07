using IGOR.EntityModel.Derictories;
using IGOR.EntityModel.Entities;
using IGOR.WebShell.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace IGOR.WebShell.Controllers
{
	public class OrderController : Controller
	{
		readonly IDerictory<Order> Derictory = new OrderDerictory();
		readonly IDerictory<OrderItem> OrderItems = new OrderItemDerictory();
		readonly IDerictory<Component> Components = new ComponentDerictory();
		static int orderId = 0;

		public ActionResult Index(Order model)
		{
			var items = from x in Derictory.Items
						where
							x.Customer.StartsWith(string.IsNullOrWhiteSpace(model.Customer) ? "" : model.Customer) &&
							x.State.StartsWith(string.IsNullOrWhiteSpace(model.State) ? "" : model.State)
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
			return View(new Order());
		}

		[HttpPost]
		public ActionResult Create(Order item)
		{
			item.OrderMoment = DateTime.Now;
			item.State = "Открыт";

			if (item.Validate() is Exception ex) {
				return View("Error", ex);
			}

			Derictory.Add(item);

			if (Derictory.Error != null) {
				return View("Error", Derictory.Error);
			} else {
				return RedirectToAction("Index");
			}
		}

		[HttpGet]
		public ActionResult CreateOrderItem(int id)
		{
			return View(new ComponentModel<OrderItem> {
				List = new SelectList(new[] {
					new { Text = "Все компоненты", Value = -1 },
					new { Text = "Процессоры", Value = 0 },
					new { Text = "ОЗУ", Value = 1 },
					new { Text = "Видеокарты", Value = 2 },
					new { Text = "Материнские платы", Value = 3 },
					new { Text = "Накопители", Value = 4 }
				}, "Value", "Text"),
				List2 = new SelectList(Components.Items, "Id", "Name"),
				Target = new OrderItem {
					OrderId = id
				}
			});
		}

		[HttpPost]
		public ActionResult CreateOrderItem(ComponentModel<OrderItem> item)
		{
			OrderItems.Add(item.Target);

			if (OrderItems.Error != null) {
				return View("Error", OrderItems.Error);
			} else {
				return RedirectToAction("Details", new { id = item.Target.OrderId });
			}
		}

		public ActionResult Details(int id)
		{
			if (Derictory.Find(id) is Order item) {
				return View(new ComponentModel<Order> {
					Collection2 = item.OrderItems,
					Target = item
				});
			} else {
				return View("Error", Derictory.Error);
			}
		}

		[HttpGet]
		public ActionResult Delete(int id)
		{
			orderId = id;

			if (Derictory.Find(id) is Order item) {
				return View(item);
			} else {
				return RedirectToAction("Error", Derictory.Error);
			}
		}

		[HttpPost]
		public ActionResult Delete(Order item)
		{
			foreach (var x in item.OrderItems) {
				OrderItems.Drop(x);
			}

			Derictory.Drop(item);

			if (Derictory.Error != null) {
				return View("Error", Derictory.Error);
			} else {
				return RedirectToAction("Details", new { id = orderId });
			}
		}

		[HttpGet]
		public ActionResult DeleteOrderItem(int id)
		{
			if (OrderItems.Find(id) is OrderItem item) {
				return View(item);
			} else {
				return RedirectToAction("Error", OrderItems.Error);
			}
		}

		[HttpPost]
		public ActionResult DeleteOrderItem(OrderItem item)
		{
			OrderItems.Drop(item);

			if (OrderItems.Error != null) {
				return View("Error", OrderItems.Error);
			} else {
				return RedirectToAction("Index");
			}
		}

		[HttpGet]
		public ActionResult Edit(int id)
		{
			if (Derictory.Find(id) is Order item) {
				return View(item);
			} else {
				return RedirectToAction("Error", Derictory.Error);
			}
		}

		[HttpPost]
		public ActionResult Edit(Order item)
		{
			Derictory.Edit(item);

			if (Derictory.Error != null) {
				return View("Error", Derictory.Error);
			} else {
				return RedirectToAction("Index");
			}
		}

		[HttpGet]
		public ActionResult EditOrderItem(int id)
		{
			orderId = OrderItems.Find(id)?.OrderId ?? 0;

			if (OrderItems.Find(id) is OrderItem item) {
				return View(new ComponentModel<OrderItem> {
					List = new SelectList(new[] {
					new { Text = "Все компоненты", Value = -1 },
					new { Text = "Процессоры", Value = 0 },
					new { Text = "ОЗУ", Value = 1 },
					new { Text = "Видеокарты", Value = 2 },
					new { Text = "Материнские платы", Value = 3 },
					new { Text = "Накопители", Value = 4 }
				}, "Value", "Text"),
					List2 = new SelectList(Components.Items, "Id", "Name"),
					Target = item
				});
			} else {
				return RedirectToAction("Error", OrderItems.Error);
			}
		}

		[HttpPost]
		public ActionResult EditOrderItem(ComponentModel<OrderItem> item)
		{
			item.Target.OrderId = orderId;
			OrderItems.Edit(item.Target);

			if (OrderItems.Error != null) {
				return View("Error", OrderItems.Error);
			} else {
				return RedirectToAction("Details", new { id = orderId });
			}
		}

		public ActionResult GetList(int id)
		{
			switch (id) {
				default: {
					return PartialView(Components.Items);
				}

				case 0: {
					return PartialView(Components.Items.Where(x => x is Cpu));
				}

				case 1: {
					return PartialView(Components.Items.Where(x => x is Ram));
				}

				case 2: {
					return PartialView(Components.Items.Where(x => x is GraphicsCard));
				}

				case 3: {
					return PartialView(Components.Items.Where(x => x is Motherboard));
				}

				case 4: {
					return PartialView(Components.Items.Where(x => x is Drive));
				}
			};
		}
	}
}