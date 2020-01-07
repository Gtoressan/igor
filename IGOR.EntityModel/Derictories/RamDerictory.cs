using IGOR.EntityModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IGOR.EntityModel.Derictories
{
	public class RamDerictory : IDerictory<Ram>
	{
		public Exception Error { get; private set; }

		public IEnumerable<Ram> Items {
			get {
				try {
					Error = null;
					using (var context = new ModelContainer()) {
						return context.Components
							.Include("Manufacturer")
							.ToArray()
							.Where(x => x is Ram item && item.Type != null)
							.Select(x => (Ram)x)
							.ToArray();
					}
				} catch (Exception ex) {
					Error = ex;
					return new Ram[0];
				}
			}
		}

		public void Add(Ram item)
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

		public void Drop(Ram item)
		{
			try {
				Error = null;
				using (var context = new ModelContainer()) {
					if (context.Components.Find(item.Id) is Ram droping) {
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

		public void Edit(Ram item)
		{
			try {
				Error = null;
				using (var context = new ModelContainer()) {
					if (context.Components.Find(item.Id) is Ram editing) {
						editing.Count = item.Count;
						editing.Name = item.Name;
						editing.Price = item.Price;
						editing.ManufacturerId = item.ManufacturerId;
						editing.Capacity = item.Capacity;
						editing.Frequency = item.Frequency;
						editing.MemoryTypeId = item.MemoryTypeId;
						context.SaveChanges();
					} else {
						Error = new Exception("Редактируемые данные не найдены в базе.");
					}
				}
			} catch (Exception ex) {
				Error = ex;
			}
		}

		public Ram Find(params object[] keys)
		{
			try {
				Error = null;
				using (var context = new ModelContainer()) {
					if (context.Components.Find(keys) is Ram desired && desired.Manufacturer != null && desired.Type != null) {
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
