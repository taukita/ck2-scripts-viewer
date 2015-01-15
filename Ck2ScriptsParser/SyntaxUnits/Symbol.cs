using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ck2ScriptsParser.SyntaxUnits
{
	public class Symbol : SyntaxUnit
	{
		private readonly string _value;

		public Symbol(string value)
		{
			_value = value;
		}

		public override string ToString()
		{
			return _value;
		}

		public override void BuildSource(StringBuilder builder, int indentation)
		{
			builder.Append(new string('\t', indentation)).AppendLine(_value);
		}
	}
}
