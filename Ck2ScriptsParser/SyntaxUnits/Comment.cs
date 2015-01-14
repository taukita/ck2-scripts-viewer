using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ck2ScriptsParser.SyntaxUnits
{
	public class Comment : SyntaxUnit
	{
		private readonly string _value;

		public Comment(string value)
		{
			_value = value;
		}

		public override string ToString()
		{
			return _value;
		}
	}
}
