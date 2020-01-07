﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IGOR.EntityModel.Entities
{
	using System;
	using System.ComponentModel;

	public partial class GraphicsCard : Component
    {
		[DisplayName("Объем (в Гб)")]
		public float Capacity { get; set; }

		[DisplayName("Тип графической памяти")]
		public string GraphicalMemoryType { get; set; }

		public override Exception Validate()
		{
			if (base.Validate() is Exception ex) {
				return ex;
			} else if (Capacity <= 0) {
				return new Exception($"Объем памяти не моет быть меньше или равен 0 ({Capacity}).");
			} else if (string.IsNullOrWhiteSpace(GraphicalMemoryType)) {
				return new Exception($"Не указан тип графической памяти ({GraphicalMemoryType}).");
			}

			return null;
		}
	}
}
