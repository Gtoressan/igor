using IGOR.EntityModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IGOR.EntityModel.Derictories
{
	public class OrderDerictory : IDerictory<Order>
	{
		public Exception Error { get; private set; }

		public IEnumerable<Order> Items {
			get {
				try {
					Error = null;
					using (var context = new ModelContainer()) {
						return context.Orders
							.Include("OrderItems")
							.Include("OrderItems.Component")
							.Include("OrderItems.Order")
							.ToArray();
					}
				} catch (Exception ex) {
					Error = ex;
					return new Order[0];
				}
			}
		}

		public void Add(Order item)
		{
			try {
				Error = null;
				using (var context = new ModelContainer()) {
					context.Orders.Add(item);
					context.SaveChanges();
				}
			} catch (Exception ex) {
				Error = ex;
			}
		}

		public void Drop(Order item)
		{
			try {
				Error = null;
				using (var context = new ModelContainer()) {
					if (context.Orders.Find(item.Id) is Order droping) {
						context.Orders.Remove(droping);
						context.SaveChanges();
					} else {
						Error = new Exception("Удоляемые данные не найдены в базе.");
					}
				}
			} catch (Exception ex) {
				Error = ex;
			}
		}

		public void Edit(Order item)
		{
			try {
				Error = null;
				using (var context = new ModelContainer()) {
					if (context.Orders.Find(item.Id) is Order editing) {
						editing.Customer = item.Customer;
						editing.OrderMoment = item.OrderMoment;
						editing.State = item.State;
						context.SaveChanges();
					} else {
						Error = new Exception("Редактируемые данные не найдены в базе.");
					}
				}
			} catch (Exception ex) {
				Error = ex;
			}
		}

		public Order Find(params object[] keys)
		{
			try {
				Error = null;
				using (var context = new ModelContainer()) {
					if (context.Orders.Find(keys) is Order desired && desired.OrderItems != null && desired.OrderItems.Count(x => x.Component != null && x.Component.Manufacturer != null) >= 0) {
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
