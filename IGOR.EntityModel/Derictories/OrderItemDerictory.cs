using IGOR.EntityModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IGOR.EntityModel.Derictories
{
	public class OrderItemDerictory : IDerictory<OrderItem>
	{
		public Exception Error { get; private set; }

		public IEnumerable<OrderItem> Items {
			get {
				try {
					Error = null;
					using (var context = new ModelContainer()) {
						return context.OrderItems
							.Include("Component")
							.Include("Order")
							.ToArray();
					}
				} catch (Exception ex) {
					Error = ex;
					return new OrderItem[0];
				}
			}
		}

		public void Add(OrderItem item)
		{
			try {
				Error = null;
				using (var context = new ModelContainer()) {
					context.OrderItems.Add(item);
					context.SaveChanges();
				}
			} catch (Exception ex) {
				Error = ex;
			}
		}

		public void Drop(OrderItem item)
		{
			try {
				Error = null;
				using (var context = new ModelContainer()) {
					if (context.OrderItems.Find(item.Id) is OrderItem droping) {
						context.OrderItems.Remove(droping);
						context.SaveChanges();
					} else {
						Error = new Exception("Удоляемые данные не найдены в базе.");
					}
				}
			} catch (Exception ex) {
				Error = ex;
			}
		}

		public void Edit(OrderItem item)
		{
			try {
				Error = null;
				using (var context = new ModelContainer()) {
					if (context.OrderItems.Find(item.Id) is OrderItem editing) {
						editing.Count = item.Count;
						editing.ComponentId = item.ComponentId;
						editing.OrderId = item.OrderId;
						context.SaveChanges();
					} else {
						Error = new Exception("Редактируемые данные не найдены в базе.");
					}
				}
			} catch (Exception ex) {
				Error = ex;
			}
		}

		public OrderItem Find(params object[] keys)
		{
			try {
				Error = null;
				using (var context = new ModelContainer()) {
					if (context.OrderItems.Find(keys) is OrderItem desired && desired.Component != null && desired.Component.Manufacturer != null) {
						return desired;
					} else {
						Error = new Exception("Искомые данные не найден в базе.");
						return null;
					}
				}
			} catch (Exception ex) {
				Error = ex;
				return null;
			}
		}
	}
}
