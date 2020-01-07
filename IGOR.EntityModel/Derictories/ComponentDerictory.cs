using IGOR.EntityModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IGOR.EntityModel.Derictories
{
	public class ComponentDerictory : IDerictory<Component>
	{
		public Exception Error { get; private set; }

		public IEnumerable<Component> Items {
			get {
				try {
					Error = null;
					using (var context = new ModelContainer()) {
						return context.Components
							.Include("Manufacturer")
							.ToArray();
					}
				} catch (Exception ex) {
					Error = ex;
					return new Component[0];
				}
			}
		}

		public void Add(Component item)
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

		public void Drop(Component item)
		{
			try {
				Error = null;
				using (var context = new ModelContainer()) {
					if (context.Components.Find(item.Id) is Component droping) {
						if (droping.OrderItems.Count() != 0) {
							throw new Exception($"Удаление данного компонента невозможно, так как на данный элемент ссылаются в заказах. (Число заказов: {droping.OrderItems.Count()})");
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

		public void Edit(Component item)
		{
			try {
				Error = null;
				using (var context = new ModelContainer()) {
					if (context.Components.Find(item.Id) is Component editing) {
						editing.Count = item.Count;
						editing.Name = item.Name;
						editing.Price = item.Price;
						editing.ManufacturerId = item.ManufacturerId;
						context.SaveChanges();
					} else {
						Error = new Exception("Редактируемые данные не найдены в базе.");
					}
				}
			} catch (Exception ex) {
				Error = ex;
			}
		}

		public Component Find(params object[] keys)
		{
			try {
				Error = null;
				using (var context = new ModelContainer()) {
					if (context.Components.Find(keys) is Component desired && desired.Manufacturer != null) {
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
