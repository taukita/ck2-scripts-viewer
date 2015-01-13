using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ck2ScriptsParser.SyntaxUnits;
using Sprache;

namespace Ck2ScriptsParser
{
	public static class Parser
	{
		public static Parser<SyntaxUnit> CommentParser =
			from comment in Parse.Regex("#.*")
			select new Comment(comment);

		public static Parser<SyntaxUnit> SymbolParser =
			from symbol in Parse.Char(c => char.IsLetterOrDigit(c) || c == '.' || c == '_', "Ожидаются только буквы, числа, '.' или '_'.").Many()
			select new Symbol(new string(symbol.ToArray()));

		public static Parser<SyntaxUnit> DoubleQuotedSymbolParser =
			from dq1 in Parse.Char('"')
			from symbol in Parse.CharExcept('"').Many()
			from dq2 in Parse.Char('"')
			select new Symbol(new string(symbol.ToArray()));

		public static Parser<SyntaxUnit> PairParser =
			from key in SymbolParser.Or(DoubleQuotedSymbolParser)
			from spaces1 in Parse.WhiteSpace.Many()
			from eq in Parse.Char('=')
			from spaces2 in Parse.WhiteSpace.Many()
			from value in SymbolParser.Or(DoubleQuotedSymbolParser).Or(Parse.Ref(() => TableParser))
			select new Pair((Symbol)key, value);

		public static Parser<SyntaxUnit> TableParser =
			from open in Parse.Char('{')
			from spaces1 in Parse.WhiteSpace.Many()
			from pairs in
				(from pairOrComment in PairParser.Or(CommentParser)
				 from spaces2 in Parse.WhiteSpace.Many()
				 select pairOrComment).Many()
			from spaces3 in Parse.WhiteSpace.Many()
			from close in Parse.Char('}')
			select new Table(pairs.OfType<Pair>());
	}
}
