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
			from comment in Parse.Regex("#.*(\r\n|$)")
			select new Comment(comment);

		public static Parser<SyntaxUnit> SymbolParser =
			from symbol in Parse.Char(c => char.IsLetterOrDigit(c) || c == '.' || c == '-' || c == '_' || c == ':' || c == '\'', "Ожидаются только буквы, числа, '.', '-', '_', ':' или '\''.").Many()
			select new Symbol(new string(symbol.ToArray()));

		public static Parser<SyntaxUnit> DoubleQuotedSymbolParser =
			from dq1 in Parse.Char('"')
			from symbol in Parse.CharExcept('"').Many()
			from dq2 in Parse.Char('"')
			select new Symbol(new string(symbol.ToArray()));

		public static Parser<SyntaxUnit> BracketedSymbolParser =
			from open in Parse.Char('[')
			from symbol in SymbolParser
			from close in Parse.Char(']')
			select new Symbol('[' + symbol.ToString() + ']');

		public static Parser<SyntaxUnit> PairParser =
			from key in SymbolParser.Or(DoubleQuotedSymbolParser)
			from spaces1 in Parse.WhiteSpace.Many()
			from eq in Parse.Char('=')
			from spaces2 in Parse.WhiteSpace.Many()
			from value in SymbolParser.Or(BracketedSymbolParser).Or(DoubleQuotedSymbolParser).Or(Parse.Ref(() => TableParser))
			select new Pair((Symbol)key, value);

	    public static Parser<List<SyntaxUnit>> ListParser =
	        from syntaxUnits in
	            (from syntaxUnit in PairParser.Or(CommentParser).Or(SymbolParser)
	             from spaces2 in Parse.WhiteSpace.Many()
	             select syntaxUnit).Many()
	        select syntaxUnits.ToList();

	    public static Parser<SyntaxUnit> TableParser =
	        from open in Parse.Char('{')
	        from spaces1 in Parse.WhiteSpace.Many()
	        from syntaxUnits in ListParser
	        from spaces3 in Parse.WhiteSpace.Many()
	        from close in Parse.Char('}')
	        select new Table(syntaxUnits);
	}
}
