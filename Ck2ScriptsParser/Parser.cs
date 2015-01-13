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
			from symbol in
				Parse.Char(c => char.IsLetterOrDigit(c) || c == '.' || c == '_', "Ожидаются только буквы, числа, '.' или '_'.")
				     .Many()
			select new Symbol(new string(symbol.ToArray()));

		public static Parser<SyntaxUnit> DoubleQuotedSymbolParser =
			from dq1 in Parse.Char('"')
			from symbol in Parse.CharExcept('"').Many()
			from dq2 in Parse.Char('"')
			select new Symbol(new string(symbol.ToArray()));

		public static Parser<SyntaxUnit> PairParser = null;

		public static Parser<SyntaxUnit> TableParser = null;
	}
}
