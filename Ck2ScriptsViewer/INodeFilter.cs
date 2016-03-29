using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ck2ScriptsParser.TreeModel;

namespace Ck2ScriptsViewer
{
	public interface INodeFilter
	{
		IEnumerable<Node> Filter(IEnumerable<Node> nodes);
	}
}
