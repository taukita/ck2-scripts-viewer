using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;

namespace Ck2ScriptObjects
{
    public class LocalizationHelper
    {
        public const string English = "ENGLISH";
        public const string French = "FRENCH";
        public const string German = "GERMAN";
        public const string Spanish = "SPANISH";

        private readonly Dictionary<string, LocalizationRecord> _cache = new Dictionary<string, LocalizationRecord>();

        private readonly string _path;

        private readonly List<string> _defaultHeader = new List<string> {"#CODE", "ENGLISH", "FRENCH", "GERMAN", "", "SPANISH"};

        public LocalizationHelper(string path)
        {
            _path = path;
            Cache();
        }

        public string Localize(string id, string language)
        {
	        return id != null && _cache.ContainsKey(id) ? _cache[id].Localize(language) : null;
        }

        private void Cache()
        {
            foreach (var file in Directory.GetFiles(_path))
            {
                using (var textFieldParser = new TextFieldParser(file, Encoding.ASCII))
                {
                    textFieldParser.Delimiters = new[] { ";" };
                    textFieldParser.HasFieldsEnclosedInQuotes = false;

                    List<string> header;

                    var values = textFieldParser.ReadFields();

	                if (values == null)
	                {
		                continue;
	                }

                    if (values[0] == "#CODE")
                    {
                        header = values.ToList();
                    }
                    else
                    {
                        header = _defaultHeader;
                        AddRecord(header, values);
                    }

                    while (!textFieldParser.EndOfData)
                    {
                        values = textFieldParser.ReadFields();
                        AddRecord(header, values);
                    }
                }
            }
        }

        private void AddRecord(List<string> header, string[] values)
        {
            _cache[values[0]] = new LocalizationRecord(header, values.ToList());
        }
    }
}
