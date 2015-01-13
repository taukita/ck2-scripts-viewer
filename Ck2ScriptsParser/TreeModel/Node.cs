using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ck2ScriptsParser.SyntaxUnits;

namespace Ck2ScriptsParser.TreeModel
{
	public class Node
	{
		public Node(string name, Table table)
		{
			Name = name;
			Children = table.Pairs.Select(p =>
				{
					if (p.Value is Symbol)
					{
						return new Node(p.Key.ToString(), p.Value.ToString());
					}
					return new Node(p.Key.ToString(), (Table)p.Value);
				}).ToList();
		}

		public Node(string name, string value)
		{
			Name = name;
			Value = value;
			Children = new List<Node>();
		}

		public string Name
		{
			get;
			set;
		}

		public string Value
		{
			get;
			set;
		}

		public string FullName
		{
			get
			{
				return string.IsNullOrEmpty(Value) ? Name : Name + " = " + Value;
			}
		}

		public List<Node> Children
		{
			get;
			set;
		}
	}
}
