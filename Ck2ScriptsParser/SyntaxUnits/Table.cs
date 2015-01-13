using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ck2ScriptsParser.SyntaxUnits
{
	class Table : SyntaxUnit
	{
		private readonly List<Pair> _pairs;

		public Table(IEnumerable<Pair> pairs)
		{
			_pairs = pairs as List<Pair> ?? pairs.ToList();
		}

		public override string ToString()
		{
			return "{ " + string.Join(" ", _pairs.Select(p => p.ToString())) + " }";
		}
	}
}
