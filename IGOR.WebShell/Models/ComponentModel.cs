using IGOR.EntityModel.Entities;
using System.Collections.Generic;
using System.Web.Mvc;

namespace IGOR.WebShell.Models
{
	public class ComponentModel<T>
	{
		public T Target { get; set; }

		public SelectList List { get; set; }

		public SelectList List2 { get; set; }

		public IEnumerable<Component> Collection { get; set; }

		public IEnumerable<OrderItem> Collection2 { get; set; }
	}
}