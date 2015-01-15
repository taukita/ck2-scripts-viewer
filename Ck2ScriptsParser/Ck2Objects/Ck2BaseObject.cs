using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ck2ScriptsParser.SyntaxUnits;

namespace Ck2ScriptsParser.Ck2Objects
{
	public class Ck2BaseObject
	{
		public string Code
		{
			get;
			set;
		}

		protected void ConvertToTable(Table table)
		{
			foreach (var propertyInfo in GetType().GetProperties().Where(p => Attribute.IsDefined(p, typeof(Ck2PropertyAttribute))))
			{
				var attribute = (Ck2PropertyAttribute)propertyInfo.GetCustomAttributes(typeof(Ck2PropertyAttribute), true).First();
				var name = attribute.Name ?? propertyInfo.Name;

				if (propertyInfo.PropertyType == typeof(bool?))
				{
					if (((bool?)propertyInfo.GetValue(this)).HasValue)
					{
						table.Units.Add(((bool?)propertyInfo.GetValue(this)).Value ? new Pair(name, "yes") : new Pair(name, "no"));
					}
				}
				else if (propertyInfo.PropertyType == typeof(int?))
				{
					if (((int?)propertyInfo.GetValue(this)).HasValue)
					{
						table.Units.Add(new Pair(name, ((int?)propertyInfo.GetValue(this)).Value.ToString(CultureInfo.InvariantCulture)));
					}
				}
				else if (propertyInfo.PropertyType == typeof(decimal?))
				{
					if (((decimal?)propertyInfo.GetValue(this)).HasValue)
					{
						table.Units.Add(new Pair(name, ((decimal?)propertyInfo.GetValue(this)).Value.ToString(CultureInfo.InvariantCulture)));
					}
				}
				else if (typeof(Ck2BaseObject).IsAssignableFrom(propertyInfo.PropertyType))
				{
					var value = (Ck2BaseObject)propertyInfo.GetValue(this);
					if (value != null)
					{
						var useCode = Attribute.IsDefined(propertyInfo, typeof(UseCodeAttribute));
						table.Units.Add(useCode ? new Pair(name, value.Code) : new Pair(new Symbol(name), value.ToTable()));
					}
				}
				else if (typeof(IEnumerable<Ck2BaseObject>).IsAssignableFrom(propertyInfo.PropertyType))
				{
					var values = (IEnumerable<Ck2BaseObject>)propertyInfo.GetValue(this);
					if (values != null && values.Any())
					{
						var useCode = Attribute.IsDefined(propertyInfo, typeof(UseCodeAttribute));
						var subTable = new Table();
						if (useCode)
						{
							foreach (var ck2BaseObject in values)
							{
								subTable.Units.Add(new Symbol(ck2BaseObject.Code));
							}
						}
						else
						{
							throw new NotImplementedException();
						}
						table.Units.Add(new Pair(new Symbol(name), subTable));
					}
				}
				else
				{
					throw new InvalidOperationException();
				}
			}
		}

		public Table ToTable()
		{
			var table = new Table();
			ConvertToTable(table);
			return table;
		}

		public SyntaxUnit ToSyntaxUnit()
		{
			return string.IsNullOrEmpty(Code) ? ToTable() : (SyntaxUnit)new Pair(new Symbol(Code), ToTable());
		}
	}
}
