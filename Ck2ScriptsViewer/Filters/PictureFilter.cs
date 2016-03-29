using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ck2ScriptsParser.TreeModel;

namespace Ck2ScriptsViewer.Filters
{
	internal class PictureFilter : INodeFilter
	{
		public IEnumerable<Node> Filter(IEnumerable<Node> nodes)
		{
			var result = new List<Node>();
			Collect(nodes, result);
			return result;
		}

		private static void Collect(IEnumerable<Node> input, ICollection<Node> output)
		{
			foreach (var node in input)
			{
				if (node.IsPair && node.Name == "picture")
				{
					output.Add(node);
				}
				Collect(node.Children, output);
			}
		}
	}
}
