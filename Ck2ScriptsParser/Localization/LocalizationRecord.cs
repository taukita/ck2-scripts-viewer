using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ck2ScriptObjects
{
    class LocalizationRecord
    {
        private readonly Dictionary<string, string> _content = new Dictionary<string, string>();
        private readonly string _defaultValue;
        private readonly string _code;

        public LocalizationRecord(List<string> header, List<string> body)
        {
            _code = body[0];

            for (int i = 1; i < header.Count; i++)
            {
                if (!string.IsNullOrEmpty(header[i]) && header[i] != "x")
                {
                    _content[header[i]] = body[i];
                    if (string.IsNullOrEmpty(_defaultValue))
                    {
                        _defaultValue = body[i];
                    }
                }
            }
        }

        public string Code
        {
            get
            {
                return _code;
            }
        }

        public string Localize(string language)
        {
            return _content.ContainsKey(language) ? _content[language] : _defaultValue;
        }
    }
}
