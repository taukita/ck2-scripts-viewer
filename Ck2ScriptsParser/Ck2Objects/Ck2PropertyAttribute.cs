using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ck2ScriptsParser.Ck2Objects
{
	public class Ck2PropertyAttribute : Attribute
	{
		public Ck2PropertyAttribute(string name)
		{
			Name = name;
		}

		public Ck2PropertyAttribute()
		{
		}

		public string Name
		{
			get;
			private set;
		}
	}
}
