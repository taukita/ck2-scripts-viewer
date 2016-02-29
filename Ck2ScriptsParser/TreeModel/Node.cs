using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ck2ScriptObjects;
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

		public bool IsLocalizable
		{
			get
			{
				//return Name == "desc" || Name == "name";
				return true;
			}
		}

		public string Localize(LocalizationHelper helper, string language = null)
		{
			if (Name == "desc" || Name == "name" || Name == "tooltip") //Events
			{
				return helper.Localize(Value, language ?? LocalizationHelper.English);
			}
			//Decisions or traits
			{
				var name = helper.Localize(Name, language ?? LocalizationHelper.English);
				if (name != null)
				{
					var nameDesc = helper.Localize(Name + "_desc", language ?? LocalizationHelper.English);
					if (nameDesc != null)
					{
						var nameDeath = helper.Localize(Name + "_death", language ?? LocalizationHelper.English);
						return name + Environment.NewLine + nameDesc + Environment.NewLine + nameDeath;
					}
				}
			}
			//Objectives
			{
				var nameTitle = helper.Localize(Name + "_title", language ?? LocalizationHelper.English);
				if (nameTitle != null)
				{
					var nameDesc = helper.Localize(Name + "_desc", language ?? LocalizationHelper.English);
					if (nameDesc != null)
					{
						return nameTitle + Environment.NewLine + nameDesc;
					}
				}
			}
			return string.Empty;
		}
	}
}
