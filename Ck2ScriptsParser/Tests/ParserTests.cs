using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ck2ScriptsParser.SyntaxUnits;
using NUnit.Framework;
using Sprache;

namespace Ck2ScriptsParser.Tests
{
	[TestFixture]
	internal class ParserTests
	{
		[TestCase("symbol")]
		[TestCase("123")]
		[TestCase("a1.-_:'")]
		public void SymbolTest(string source)
		{
			var parsed = Parser.SymbolParser.Token().End().Parse(source);
			Assert.IsTrue(parsed is Symbol);
			Assert.AreEqual(source, parsed.ToString());
		}

		[Test]
		public void PairTest()
		{
			const string source = "a = b";
			var parsed = Parser.PairParser.Token().End().Parse(source);
			Assert.IsTrue(parsed is Pair);
			var pair = (Pair) parsed;
			Assert.AreEqual("a", pair.Key.ToString());
			Assert.AreEqual("b", pair.Value.ToString());
		}

		[TestCase("{a b c d}", 4, typeof (Symbol))]
		[TestCase("{a = b c = d}", 2, typeof (Pair))]
		[TestCase("{ #a\r\n #b\r\n #c\r\n }", 3, typeof (Comment))]
		[TestCase("{}", 0, typeof (void))]
		public void TableTest(string source, int count, Type type)
		{
			var parsed = Parser.TableParser.Token().End().Parse(source);
			Assert.IsTrue(parsed is Table);
			var table = (Table) parsed;
			Assert.AreEqual(count, table.Units.Count);
			foreach (var unit in table.Units)
			{
				Assert.AreEqual(type, unit.GetType());
			}
		}

		[TestCase("a b c d", new[] {typeof (Symbol), typeof (Symbol), typeof (Symbol), typeof (Symbol)})]
		[TestCase("a #b\r\n c=d", new[] {typeof (Symbol), typeof (Comment), typeof (Pair)})]
		public void ListTest(string source, Type[] types)
		{
			var parsed = Parser.ListParser.Token().End().Parse(source);
			Assert.AreEqual(parsed.Count, types.Length);
			for (var i = 0; i < parsed.Count; i++)
			{
				Assert.AreEqual(types[i], parsed[i].GetType());
			}
		}

		[Test]
		public void ComplexTest()
		{
			const string source = @"
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
			var parsed = Parser.ListParser.Token().End().Parse(source);

			CheckComplex(parsed);

			var sb = new StringBuilder();
			foreach (var unit in parsed)
			{
				unit.BuildSource(sb, 0);
			}

			CheckComplex(Parser.ListParser.Token().End().Parse(sb.ToString()));
		}

		private static void CheckComplex(List<SyntaxUnit> parsed)
		{
			Assert.AreEqual(3, parsed.Count);
			Assert.IsTrue(parsed[0] is Comment);
			Assert.IsTrue(parsed[1] is Pair);
			Assert.IsTrue(parsed[2] is Comment);

			var pair = (Pair) parsed[1];

			Assert.AreEqual("character_event", pair.Key.ToString());
			Assert.IsTrue(pair.Value is Table);

			var table = (Table) pair.Value;

			Assert.AreEqual(5, table.Units.Count);
			foreach (var unit in table.Units)
			{
				Assert.IsTrue(unit is Pair);
			}
		}
	}
}
