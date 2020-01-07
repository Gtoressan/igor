using IGOR.EntityModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IGOR.EntityModel.Derictories
{
	public class ManufacturerDerictory : IDerictory<Manufacturer>
	{
		public Exception Error { get; private set; }

		public IEnumerable<Manufacturer> Items {
			get {
				try {
					Error = null;
					using (var context = new ModelContainer()) {
						return context.Manufacturers
							.ToArray();
					}
				} catch (Exception ex) {
					Error = ex;
					return new Manufacturer[0];
				}
			}
		}

		public void Add(Manufacturer item)
		{
			try {
				Error = null;
				using (var context = new ModelContainer()) {
					context.Manufacturers.Add(item);
					context.SaveChanges();
				}
			} catch (Exception ex) {
				Error = ex;
			}
		}

		public void Drop(Manufacturer item)
		{
			try {
				Error = null;
				using (var context = new ModelContainer()) {
					var droping = context.Manufacturers.Find(item.Id);

					if (droping != null && droping.Components.Count() == 0) {
						context.Manufacturers.Remove(droping);
						context.SaveChanges();
					} else if (droping.Components.Count() != 0) {
						Error = new Exception($"Невозможно удалить данного проиводителя, так как на него ссылаются другие записи в базе (Число записей: {droping.Components.Count()}).");
					} else {
						Error = new Exception("Удоляемые данные не найдены в базе.");
					}
				}
			} catch (Exception ex) {
				Error = ex;
			}
		}

		public void Edit(Manufacturer item)
		{
			try {
				Error = null;
				using (var context = new ModelContainer()) {
					if (context.Manufacturers.Find(item.Id) is Manufacturer editing) {
						editing.Country = item.Country;
						editing.Name = item.Name;
						context.SaveChanges();
					} else {
						Error = new Exception("Редактируемые данные не найдены в базе.");
					}
				}
			} catch (Exception ex) {
				Error = ex;
			}
		}

		public Manufacturer Find(params object[] keys)
		{
			try {
				Error = null;
				using (var context = new ModelContainer()) {
					if (context.Manufacturers.Find(keys) is Manufacturer desired) {
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
