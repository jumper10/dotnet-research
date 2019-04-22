
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;

namespace Wpf_research
{
    public class TreeViewItemModel: ObservableObject
    {
        public int Id { get; set; }
        string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                if (value != _title)
                {
                    Set("Title", ref _title, value);
                }
            }
        }

        bool _isExpanded;
        public bool IsExpanded
        {
            get { return _isExpanded; }
            set
            {
                if (value != _isExpanded)
                {
                    Set("IsExpanded", ref _isExpanded, value);
                }
            }
        }

        bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (value != _isSelected)
                {
                    Set("IsSelected", ref _isSelected, value);
                }
            }
        }

        public TreeViewItemModel Parent { get; set; }

        public ObservableCollection<TreeViewItemModel> Children { get; set; } = new ObservableCollection<TreeViewItemModel>();
    }
}
