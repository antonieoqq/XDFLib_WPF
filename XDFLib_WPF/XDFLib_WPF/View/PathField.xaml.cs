using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Media;
using XDFLib_WPF.Common;

namespace XDFLib_WPF.View
{
    public partial class PathField : FieldBase
    {
        public string PathValue
        {
            get { return (string)GetValue(PathValueProperty); }
            set
            {
                SetValue(PathValueProperty, value);
                UpdatePathValidation();
                //DialogInitDirectory = Path.GetDirectoryName(value);
            }
        }
        public static readonly DependencyProperty PathValueProperty =
            DependencyProperty.Register("PathValue", typeof(string), typeof(PathField),
                new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    OnPathValuePropertyChanged));
        static void OnPathValuePropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var s = sender as PathField;
            s.InvokeOnValueChanged(e.OldValue, e.NewValue);
        }

        public bool DirectoryMode
        {
            get { return (bool)GetValue(DirectoryModeProperty); }
            set { SetValue(DirectoryModeProperty, value); }
        }
        public static readonly DependencyProperty DirectoryModeProperty =
            DependencyProperty.Register("DirectoryMode", typeof(bool), typeof(PathField),
                new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public SolidColorBrush Background_Normal
        {
            get { return (SolidColorBrush)GetValue(BackgroundWheNormalProperty); }
            set { SetValue(BackgroundWheNormalProperty, value); }
        }
        public static readonly DependencyProperty BackgroundWheNormalProperty =
            DependencyProperty.Register("Background_Normal", typeof(SolidColorBrush), typeof(PathField),
                new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromRgb(40, 40, 40)), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public SolidColorBrush Background_MouseOver
        {
            get { return (SolidColorBrush)GetValue(BackgroundWhenMouseOverProperty); }
            set { SetValue(BackgroundWhenMouseOverProperty, value); }
        }
        public static readonly DependencyProperty BackgroundWhenMouseOverProperty =
            DependencyProperty.Register("Background_MouseOver", typeof(SolidColorBrush), typeof(PathField),
                new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromRgb(86, 134, 187)), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public SolidColorBrush Background_Invalid
        {
            get { return (SolidColorBrush)GetValue(BackgroundWhenInvalidProperty); }
            set { SetValue(BackgroundWhenInvalidProperty, value); }
        }
        public static readonly DependencyProperty BackgroundWhenInvalidProperty =
            DependencyProperty.Register("Background_Invalid", typeof(SolidColorBrush), typeof(PathField),
                new FrameworkPropertyMetadata(new SolidColorBrush(Colors.IndianRed), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public string DialogTitle
        {
            get { return (string)GetValue(DialogTitleProperty); }
            set { SetValue(DialogTitleProperty, value); }
        }
        public static readonly DependencyProperty DialogTitleProperty =
            DependencyProperty.Register("DialogTitle", typeof(string), typeof(PathField),
                new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public string DialogInitDirectory
        {
            get { return (string)GetValue(DialogInitDirectoryProperty); }
            set { SetValue(DialogInitDirectoryProperty, value); }
        }
        public static readonly DependencyProperty DialogInitDirectoryProperty =
            DependencyProperty.Register("DialogInitDirectory", typeof(string), typeof(PathField),
                new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public bool DialogSupportAllFileFilter
        {
            get { return (bool)GetValue(DialogSupportAllFileFilterProperty); }
            set { SetValue(DialogSupportAllFileFilterProperty, value); }
        }
        public static readonly DependencyProperty DialogSupportAllFileFilterProperty =
            DependencyProperty.Register("DialogSupportAllFileFilter", typeof(bool), typeof(PathField),
                new FrameworkPropertyMetadata(
                    false,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    OnDialogSupportAllFileFilterPropertyChanged));
        static void OnDialogSupportAllFileFilterPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var s = sender as PathField;
            s._filterHelper.UseAllFileFilter = (bool)e.NewValue;
            s.UpdatePathValidation();
        }

        public string FilterString
        {
            get { return (string)GetValue(FilterStringProperty); }
            set
            {
                SetValue(FilterStringProperty, value);
                _filterHelper.SetFilterString(value);
            }
        }
        public static readonly DependencyProperty FilterStringProperty =
            DependencyProperty.Register("FilterString", typeof(string), typeof(PathField),
                new FrameworkPropertyMetadata(
                    string.Empty,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    OnFilterStringPropertyChanged));
        static void OnFilterStringPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var s = sender as PathField;
            s._filterHelper.SetFilterString(e.NewValue.ToString());
        }

        public bool IsPathValid
        {
            get { return (bool)GetValue(IsPathValidProperty); }
            set { throw new ReadOnlyException("An attempt ot modify Read-Only property"); }
        }
        public static readonly DependencyProperty IsPathValidProperty =
            DependencyProperty.Register("IsPathValid", typeof(bool), typeof(PathField),
                new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnIsPathValidPropertyChanged));
        static void OnIsPathValidPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var s = sender as PathField;
            s.InvokeOnValueChanged(e.OldValue, e.NewValue);
        }

        public FileFilterHelper _filterHelper { get; private set; } = new();

        bool _isMouseOver = false;

        public PathField()
        {
            InitializeComponent();
            DataContextChanged += OnDataContextChanged;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            UpdatePathValidation();
        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            UpdatePathValidation();
        }

        public void AddFileFilter(string filterName, params string[] extensions)
        {
            _filterHelper.AddFileFilter(filterName, extensions);
        }

        private void TextBlock_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var arr = (Array)e.Data.GetData(DataFormats.FileDrop);
                if (arr == null || arr.Length == 0) return;

                string? newPath = arr.GetValue(0).ToString();
                if (newPath != null)
                {
                    var isDirectory = Directory.Exists(newPath);
                    if ((isDirectory && DirectoryMode)
                        || (!isDirectory && !DirectoryMode && _filterHelper.IsFilePathFit(newPath)))
                    {
                        PathValue = newPath;
                    }
                }
            }
        }

        private void PathBlock_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (DirectoryMode)
            {
                var dlg = new CommonOpenFileDialog();
                dlg.IsFolderPicker = true;
                dlg.Title = DialogTitle;

                var dir = string.IsNullOrEmpty(DialogInitDirectory) ?
                    PathValue : DialogInitDirectory;

                dlg.InitialDirectory = dir;
                dlg.DefaultDirectory = dir;

                var result = dlg.ShowDialog();
                if (result == CommonFileDialogResult.Ok)
                {
                    PathValue = dlg.FileName;
                }
            }
            else
            {
                var dlg = new OpenFileDialog();
                dlg.Title = DialogTitle;

                var dir = string.IsNullOrEmpty(DialogInitDirectory) ?
                    Path.GetDirectoryName(PathValue) : DialogInitDirectory; ;

                dlg.InitialDirectory = dir;
                dlg.Filter = _filterHelper.FilterString;

                bool? result = dlg.ShowDialog();

                if (result == true)
                {
                    PathValue = dlg.FileName;
                }
            }
        }

        private void PathBlock_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            _isMouseOver = true;
            UpdatePathValidation();
        }

        private void PathBlock_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            _isMouseOver = false;
            UpdatePathValidation();
        }

        void UpdatePathValidation()
        {
            var isPathValid = DirectoryMode ? Directory.Exists(PathValue) : File.Exists(PathValue);
            SetValue(IsPathValidProperty, isPathValid);
            UpdatePathBlockBackground();
        }

        void UpdatePathBlockBackground()
        {
            if (_isMouseOver)
            {
                PathBlock.Background = Background_MouseOver;
            }
            else
            {
                PathBlock.Background = IsPathValid ? Background_Normal : Background_Invalid;
            }
        }
    }
}
