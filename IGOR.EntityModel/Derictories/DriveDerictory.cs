using IGOR.EntityModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IGOR.EntityModel.Derictories
{
	public class DriveDerictory : IDerictory<Drive>
	{
		public Exception Error { get; private set; }

		public IEnumerable<Drive> Items {
			get {
				try {
					Error = null;
					using (var context = new ModelContainer()) {
						return context.Components
							.Include("Manufacturer")
							.ToArray()
							.Where(x => x is Drive)
							.Select(x => (Drive)x);
					}
				} catch (Exception ex) {
					Error = ex;
					return new Drive[0];
				}
			}
		}

		public void Add(Drive item)
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

		public void Drop(Drive item)
		{
			try {
				Error = null;
				using (var context = new ModelContainer()) {
					if (context.Components.Find(item.Id) is Drive droping) {
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

		public void Edit(Drive item)
		{
			try {
				Error = null;
				using (var context = new ModelContainer()) {
					if (context.Components.Find(item.Id) is Drive editing) {
						editing.Count = item.Count;
						editing.Name = item.Name;
						editing.Price = item.Price;
						editing.ManufacturerId = item.ManufacturerId;
						editing.Capacity = item.Capacity;
						editing.Type = item.Type;
						context.SaveChanges();
					} else {
						Error = new Exception("Редактируемые данные не найдены в базе.");
					}
				}
			} catch (Exception ex) {
				Error = ex;
			}
		}

		public Drive Find(params object[] keys)
		{
			try {
				Error = null;
				using (var context = new ModelContainer()) {
					if (context.Components.Find(keys) is Drive desired && desired.Manufacturer != null) {
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
