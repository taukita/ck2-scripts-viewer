using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ck2ScriptsParser.SyntaxUnits
{
	public class Table : SyntaxUnit
	{
		public List<SyntaxUnit> Units
		{
			get;
			private set;
		}

		public Table(IEnumerable<SyntaxUnit> pairs)
		{
			Units = pairs as List<SyntaxUnit> ?? pairs.ToList();
		}

		public override string ToString()
		{
			return "{ " + string.Join(" ", Units.Select(p => p.ToString())) + " }";
		}
	}
}
