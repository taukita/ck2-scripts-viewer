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
	}
}
