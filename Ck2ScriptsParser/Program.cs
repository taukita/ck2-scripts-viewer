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
	#ololo
";
			var c = Parser.CommentParser.Token().Parse(source);

			Console.WriteLine(c.ToString());
			Console.ReadKey(false);
		}
	}
}
