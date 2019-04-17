using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_research
{
    public class TreeViewItemModel:ViewModelBase
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
                    _title = value;
                    this.RaisePropertyChanged();
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
                    _isExpanded = value;
                    this.RaisePropertyChanged("IsExpanded");
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
                    _isSelected = value;
                    this.RaisePropertyChanged("IsSelected");
                }
            }
        }

        public TreeViewItemModel Parent { get; set; }

        public ObservableCollection<TreeViewItemModel> Children { get; set; } = new ObservableCollection<TreeViewItemModel>();
    }
}
