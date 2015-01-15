using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ck2ScriptsParser.SyntaxUnits
{
	public class Pair : SyntaxUnit
	{
		public Pair(Symbol key, object value)
		{
			Key = key;
			Value = value;
		}

        public Pair(string key, string value)
            : this(new Symbol(key), new Symbol(value))
        {
        }

		public Symbol Key
		{
			get;
			private set;
		}

		public object Value
		{ 
			get;
			private set;
		}

		public override string ToString()
		{
			return string.Format("{0} = {1}", Key, Value);
		}

		public override void BuildSource(StringBuilder builder, int indentation)
		{
			builder.Append(new string('\t', indentation)).Append(Key).Append(" = ");
			if (Value is Table)
			{
				((SyntaxUnit)Value).BuildSource(builder, indentation);
			}
			else
			{
				((SyntaxUnit)Value).BuildSource(builder, 0);
			}
		}
	}
}
