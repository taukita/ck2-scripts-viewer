using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ck2ScriptObjects;
using Ck2ScriptsParser.Ck2Objects;
using Ck2ScriptsParser.SyntaxUnits;
using Sprache;

namespace Ck2ScriptsParser
{
	class Program
	{
		static void Main(string[] args)
		{
			string source = @"
#Liege get new character: Ibn al-Nafis
character_event = {
	id = 106036
	desc = EVTDESC106036
	picture = GFX_evt_council
	
	is_triggered_only = yes
	
	option = {
		name = EVTOPTA106036
		create_character = {
			name = Ibn
			dynasty = 1031107
			attributes = {
				learning = 11
			}
			religion = ROOT
			culture = levantine_arabic
			age = 23
			female = no
			trait = mystic
			trait = quick
			trait = charitable
			trait = scholarly_theologian
		}
		new_character = {
			set_character_flag = ibn_alnafis_flag
		}
	}
}

### Abu'l-Barakat al-Baghdadi ###
";
			/*
			var c = Parser.ListParser.Token().End().Parse(source);

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
			 */

			var list = new List<SyntaxUnit>();
			var dir = new DirectoryInfo(@"E:\SteamLibrary\steamapps\common\Crusader Kings II\events");
			var sw = new Stopwatch();
			var sw1 = new Stopwatch();
			foreach (var fn in Directory.GetFiles(dir.FullName))
			{
				sw1.Start();
				var text = File.ReadAllText(fn);
				sw1.Stop();
				sw.Start();
				var su = Parser.ListParser.Token().End().Parse(text);
				list.Add(new Pair(new Symbol(fn), new Table(su)));
				sw.Stop();
			}

			Console.WriteLine("Parsing {0} sec", sw.ElapsedMilliseconds/1000);
			Console.WriteLine("File reading {0} sec", sw1.ElapsedMilliseconds / 1000);

			/*
            LocalizationHelper helper = new LocalizationHelper(@"E:\SteamLibrary\steamapps\common\Crusader Kings II\localisation\");

            Console.WriteLine();
            Console.WriteLine(helper.Localize("amateurish_plotter", LocalizationHelper.English));
			Console.WriteLine(helper.Localize("EVTDESC76000", LocalizationHelper.English));
			 */

			Console.ReadKey(false);
		}
	}
}
