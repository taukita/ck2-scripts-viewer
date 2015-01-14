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
		    Children = table.Units.Where(u => !(u is Comment)).Select(FromSyntaxUnit).ToList();
		}

		public Node(string name, string value)
		{
			Name = name;
			Value = value;
			Children = new List<Node>();
		}

		public Node(string name)
		{
			Name = name;
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

        public static Node FromSyntaxUnit(SyntaxUnit su)
        {
            if (su is Symbol)
            {
                return new Node(su.ToString());
            }
            if (su is Pair)
            {
                var pair = (Pair)su;
                if (pair.Value is Symbol)
                {
                    return new Node(pair.Key.ToString(), pair.Value.ToString());
                }
                if (pair.Value is Table)
                {
                    return new Node(pair.Key.ToString(), (Table)pair.Value);
                }
                throw new InvalidOperationException();
            }
            if (su is Table)
            {
                return new Node(string.Empty, (Table)su);
            }
            throw new InvalidOperationException();
        }
	}
}
