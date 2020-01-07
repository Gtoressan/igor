using IGOR.EntityModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IGOR.EntityModel.Derictories
{
	public class MotherboardDerictory : IDerictory<Motherboard>
	{
		public Exception Error { get; private set; }

		public IEnumerable<Motherboard> Items {
			get {
				try {
					Error = null;
					using (var context = new ModelContainer()) {
						return context.Components
							.Include("Manufacturer")
							.ToArray()
							.Where(x => x is Motherboard item && item.MemoryType != null)
							.Select(x => (Motherboard)x)
							.ToArray();
					}
				} catch (Exception ex) {
					Error = ex;
					return new Motherboard[0];
				}
			}
		}

		public void Add(Motherboard item)
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

		public void Drop(Motherboard item)
		{
			try {
				Error = null;
				using (var context = new ModelContainer()) {
					if (context.Components.Find(item.Id) is Motherboard droping) {
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

		public void Edit(Motherboard item)
		{
			try {
				Error = null;
				using (var context = new ModelContainer()) {
					if (context.Components.Find(item.Id) is Motherboard editing) {
						editing.Count = item.Count;
						editing.Name = item.Name;
						editing.Price = item.Price;
						editing.ManufacturerId = item.ManufacturerId;
						editing.Chipset = item.Chipset;
						editing.Socket = item.Socket;
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

		public Motherboard Find(params object[] keys)
		{
			try {
				Error = null;
				using (var context = new ModelContainer()) {
					if (context.Components.Find(keys) is Motherboard desired && desired.Manufacturer != null && desired.MemoryType != null) {
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
