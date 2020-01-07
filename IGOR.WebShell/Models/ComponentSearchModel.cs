using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IGOR.WebShell.Models
{
	public class ComponentSearchModel
	{
		public string Manufacturer { get; set; }

		public string Name { get; set; }

		public float LowPriceBorder { get; set; }

		public float HighPriceBorder { get; set; }

		public bool IsAvailable { get; set; }
	}
}