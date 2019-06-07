using System;
using System.Collections.Generic;
using System.Text;

namespace Fluent {
	public class FluentListProperties {

		public string Name { get; set; }

		public string Description { get; set; }

		public string Icon { get; set; }

		public List<string> Columns { get; set; }

		public List<string> ColumnNames { get; set; }

		public string GroupBy { get; set; }

		

	}
}
