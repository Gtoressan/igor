using IGOR.EntityModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IGOR.EntityModel.Derictories
{
	public class MemoryTypeDerictory : IDerictory<MemoryType>
	{
		public Exception Error { get; private set; }

		public IEnumerable<MemoryType> Items {
			get {
				try {
					Error = null;
					using (var context = new ModelContainer()) {
						return context.MemoryTypes
							.ToArray();
					}
				} catch (Exception ex) {
					Error = ex;
					return new MemoryType[0];
				}
			}
		}

		public void Add(MemoryType item)
		{
			try {
				Error = null;
				using (var context = new ModelContainer()) {
					context.MemoryTypes.Add(item);
					context.SaveChanges();
				}
			} catch (Exception ex) {
				Error = ex;
			}
		}

		public void Drop(MemoryType item)
		{
			try {
				Error = null;
				using (var context = new ModelContainer()) {
					var droping = context.MemoryTypes.Find(item.Id);

					if (droping != null && droping.Motherboards.Count() == 0 && droping.Rams.Count() == 0) {
						context.MemoryTypes.Remove(droping);
						context.SaveChanges();
					} else if (droping.Motherboards.Count() != 0 && droping.Rams.Count() != 0) {
						Error = new Exception($"Удаление данной информации невозможно, так как на нее ссылаются в других местах. Число ссылок: {droping.Motherboards.Count() + droping.Rams.Count()}");
					} else {
						Error = new Exception("Удоляемые данные не найдены в базе.");
					}
				}
			} catch (Exception ex) {
				Error = ex;
			}
		}

		public void Edit(MemoryType item)
		{
			try {
				Error = null;
				using (var context = new ModelContainer()) {
					if (context.MemoryTypes.Find(item.Id) is MemoryType editing) {
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

		public MemoryType Find(params object[] keys)
		{
			try {
				Error = null;
				using (var context = new ModelContainer()) {
					if (context.MemoryTypes.Find(keys) is MemoryType desired) {
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
