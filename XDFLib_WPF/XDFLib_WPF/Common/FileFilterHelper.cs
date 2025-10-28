using System.Collections.Generic;
using System.IO;

namespace XDFLib_WPF.Common
{
    public class FileFilterHelper
    {
        public bool UseAllFileFilter { get; set; } = false;

        public bool CanFitAnyFile => UseAllFileFilter || _allValidExtensions.Count == 0;

        Dictionary<string, FileFilterInfo> _filters = new();
        HashSet<string> _allValidExtensions = new HashSet<string>();

        string _filterString = string.Empty;
        public string FilterString => _filterString;

        public bool SetFilterString(string filterString)
        {
            if (string.IsNullOrEmpty(filterString)) return false;
            var segments = filterString.Split('|');
            if (segments.Length <= 1 || segments.Length % 2 == 1)
            {
                return false;
            }

            _filters.Clear();
            _allValidExtensions.Clear();

            for (int i = 0; i < segments.Length; i += 2)
            {
                var filterExts = segments[i + 1].Split(';');
                if (filterExts.Length == 0) continue;

                bool isAllFile = false;
                for (int j = 0; j < filterExts.Length; j++)
                {
                    var filterExt = filterExts[j];
                    filterExt = filterExt.Remove(0, 2);
                    if (filterExt == "*")
                    {
                        isAllFile = true;
                        continue;
                    }
                    else
                    {
                        filterExts[j] = filterExt;
                    }
                }
                if (isAllFile)
                {
                    UseAllFileFilter = true;
                }
                else
                {
                    var pureNameEnd = segments[i].IndexOf('(');
                    var filterName = segments[i].Substring(0, pureNameEnd);

                    AddFilterInternal(filterName, filterExts);
                }
            }
            UpdateFilterString();
            return true;
        }

        public override string ToString()
        {
            return FilterString;
        }

        public void AddFileFilter(string filterName, params string[] extensions)
        {
            if (!_filters.ContainsKey(filterName))
            {
                FileFilterInfo ffi = new FileFilterInfo(filterName, extensions);
                AddFileFilter(ffi);
            }
        }

        public void AddFileFilter(FileFilterInfo filter)
        {
            if (!_filters.ContainsKey(filter.FilterName))
            {
                _filters.Add(filter.FilterName, filter);
                foreach (var ext in filter.Extensions)
                {
                    _allValidExtensions.Add(ext);
                }
                UpdateFilterString();
            }
        }

        public void RemoveFileFilter(string filterName)
        {
            if (_filters.Remove(filterName))
            {
                _allValidExtensions.Clear();
                foreach (var f in _filters)
                {
                    foreach (var ext in f.Value.Extensions)
                    {
                        _allValidExtensions.Add(ext);
                    }
                }
                UpdateFilterString();
            }
        }

        public void Clear()
        {
            _filters.Clear();
            _allValidExtensions.Clear();
            UpdateFilterString();
        }

        public bool IsExtensionFit(string extension)
        {
            return CanFitAnyFile || _allValidExtensions.Contains(extension.ToLower());
        }

        public bool IsFilePathFit(string filePath)
        {
            if (CanFitAnyFile) return true;

            var extension = Path.GetExtension(filePath);
            if (!string.IsNullOrEmpty(extension))
            {
                extension = extension.Remove(0, 1);
                return IsExtensionFit(extension);
            }
            return false;
        }

        void AddFilterInternal(string filterName, params string[] extensions)
        {
            FileFilterInfo ffi = new FileFilterInfo(filterName, extensions);
            _filters.Add(filterName, ffi);
            foreach (var ext in extensions)
            {
                _allValidExtensions.Add(ext.ToLower());
            }
        }

        void UpdateFilterString()
        {
            var text = _filters.Count > 0 ? string.Join('|', _filters.Values) : "";
            if (UseAllFileFilter)
            {
                if (text.Length != 0) { text += "|"; }
                text += "All files (*.*)|*.*";
            }
            _filterString = text;
        }
    }
}
