using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ck2ScriptsParser.Ck2Objects
{
	class Ck2Condition : Ck2BaseObject
	{
		#region Operators

		[Ck2Property("AND")]
		public Ck2BaseObject And
		{
			get;
			set;
		}

		[Ck2Property("OR")]
		public Ck2BaseObject Or
		{
			get;
			set;
		}

		[Ck2Property("NOT")]
		public Ck2BaseObject Not
		{
			get;
			set;
		}

		#endregion
	}
}
