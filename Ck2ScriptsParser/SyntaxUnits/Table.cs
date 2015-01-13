using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ck2ScriptsParser.SyntaxUnits
{
	public class Table : SyntaxUnit
	{
		public List<Pair> Pairs
		{
			get;
			private set;
		}

		public Table(IEnumerable<Pair> pairs)
		{
			Pairs = pairs as List<Pair> ?? pairs.ToList();
		}

		public override string ToString()
		{
			return "{ " + string.Join(" ", Pairs.Select(p => p.ToString())) + " }";
		}
	}
}
