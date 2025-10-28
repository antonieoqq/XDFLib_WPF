using System;
using System.Collections.Generic;
using XDFLib.Collections;

namespace XDFLib_WPF.Common
{
    public class FileFilterInfo
    {
        public string FilterName;
        Deque<string> _extentions = new();
        public ReadOnlySpan<string> Extensions => _extentions.AsSpan();

        public FileFilterInfo(string filterName, params string[] extensions)
        {
            FilterName = filterName;
            _extentions.Capacity = extensions.Length;
            foreach (var ext in extensions)
            {
                AddExtension(ext);
            }
        }

        public void AddExtension(string extension)
        {
            var extLower = extension.ToLower();
            if (!_extentions.Contains(extLower)) ;
            {
                _extentions.Add(extLower);
            }
        }

        public void RemoveExtension(string extension)
        {
            var extLower = extension.ToLower();
            _extentions.Remove(extLower);
        }

        public override string ToString()
        {
            if (_extentions.Count > 0)
            {
                string exts = $"*.{string.Join(";*.", _extentions)}";
                var curr = $"{FilterName}({exts})|{exts}";
                return curr;
            }
            return "";
        }

        public static string GetFilterString(ICollection<FileFilterInfo> fileFilters, bool addAllFileFilter)
        {
            string result;
            if (fileFilters.Count > 0)
            {
                result = string.Join('|', fileFilters);
            }
            else
            {
                result = "";
            }

            if (addAllFileFilter)
            {
                if (result.Length != 0) { result += "|"; }
                result += "All files (*.*)|*.*";
            }
            return result;
        }
    }
}
