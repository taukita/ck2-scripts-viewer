using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprache;

namespace Ck2ScriptsParser
{
	class Program
	{
		static void Main(string[] args)
		{
			string source = @"
	{
		#ololo comment
		a = b
		c = 5.0.12#this is comment too
		d = {d = d}
	}
";
			var c = Parser.TableParser.Token().Parse(source);

			Console.WriteLine(c.ToString());
			Console.ReadKey(false);
		}
	}
}
