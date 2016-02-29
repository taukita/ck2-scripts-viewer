using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ck2ScriptObjects;
using Ck2ScriptsParser.Ck2Objects;
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
		r = { sy }
	}
";
			var c = Parser.TableParser.Token().Parse(source);

			Console.WriteLine(c.ToString());

			var trait = new Ck2Trait()
			{
				Code = "amateurish_plotter",

				Education = true,

				Intrigue = 1,
				Stewardship = -1
			};

			var builder = new StringBuilder();
			
			trait.ToSyntaxUnit().BuildSource(builder, 0);

			Console.WriteLine();
			Console.WriteLine(builder.ToString());

            LocalizationHelper helper = new LocalizationHelper(@"E:\SteamLibrary\steamapps\common\Crusader Kings II\localisation\");

            Console.WriteLine();
            Console.WriteLine(helper.Localize("amateurish_plotter", LocalizationHelper.English));
			Console.WriteLine(helper.Localize("EVTDESC76000", LocalizationHelper.English));

			Console.ReadKey(false);
		}
	}
}
