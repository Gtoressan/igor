using IGOR.EntityModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IGOR.EntityModel.Derictories
{
	public class GraphicsCardDerictory : IDerictory<GraphicsCard>
	{
		public Exception Error { get; private set; }

		public IEnumerable<GraphicsCard> Items {
			get {
				try {
					Error = null;
					using (var context = new ModelContainer()) {
						return context.Components
							.Include("Manufacturer")
							.ToArray()
							.Where(x => x is GraphicsCard)
							.Select(x => (GraphicsCard)x);
					}
				} catch (Exception ex) {
					Error = ex;
					return new GraphicsCard[0];
				}
			}
		}

		public void Add(GraphicsCard item)
		{
			try {
				Error = null;
				using (var context = new ModelContainer()) {
					context.Components.Add(item);
					context.SaveChanges();
				}
			} catch (Exception ex) {
				Error = ex;
			}
		}

		public void Drop(GraphicsCard item)
		{
			try {
				Error = null;
				using (var context = new ModelContainer()) {
					if (context.Components.Find(item.Id) is GraphicsCard droping) {
						if (droping.OrderItems.Count() != 0) {
							throw new Exception($"Удаление данного компонента невозможно, так как на данный элемент ссылаются в заказах. (Число ссылок: {droping.OrderItems.Count()})");
						}

						context.Components.Remove(droping);
						context.SaveChanges();
					} else {
						Error = new Exception("Удоляемые данные не найдены в базе.");
					}
				}
			} catch (Exception ex) {
				Error = ex;
			}
		}

		public void Edit(GraphicsCard item)
		{
			try {
				Error = null;
				using (var context = new ModelContainer()) {
					if (context.Components.Find(item.Id) is GraphicsCard editing) {
						editing.Count = item.Count;
						editing.Name = item.Name;
						editing.Price = item.Price;
						editing.ManufacturerId = item.ManufacturerId;
						editing.Capacity = item.Capacity;
						editing.GraphicalMemoryType = item.GraphicalMemoryType;
						context.SaveChanges();
					} else {
						Error = new Exception("Редактируемые данные не найдены в базе.");
					}
				}
			} catch (Exception ex) {
				Error = ex;
			}
		}

		public GraphicsCard Find(params object[] keys)
		{
			try {
				Error = null;
				using (var context = new ModelContainer()) {
					if (context.Components.Find(keys) is GraphicsCard desired && desired.Manufacturer != null) {
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
