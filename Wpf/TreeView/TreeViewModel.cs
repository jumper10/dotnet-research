
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Wpf_research
{
    public class TreeViewModel:ViewModelBase
    {
        public ObservableCollection<TreeViewItemModel> TreeViews { get; set; } = new ObservableCollection<TreeViewItemModel>();


        public void Loaded()
        {
            CreateTestData();
        }

        public int Id { get; set; }

        public ICommand SearchCommand
        {
            get
            {
                return new RelayCommand<object>((obj) =>
                {
                    var item = SearchById(Id);
                    if (item.Parent != null) item.Parent.IsExpanded = true;

                    if (item != null)
                        item.IsSelected = true;
                },
                    (obj) => { return true; }, true
               );
            }
        }



        public TreeViewItemModel SearchById(int id)
        {
            return SearchById(TreeViews, id);

        }

        private TreeViewItemModel SearchById(IList<TreeViewItemModel> nodes, int id)
        {
            if (nodes == null) return null;
            var node = nodes.FirstOrDefault(n => n.Id == id);
            if (node != null) return node;
            foreach (var n in nodes)
            {
                node = SearchById(n.Children, id);
                if (node != null) return node;
            }
            return null;
        }


        private void CreateTestData()
        {
            var seed = new Random();
            int id = 0;
            for(int i = 0; i < 5; i++)
            {
                var node = new TreeViewItemModel { Id = id, Title = $"Id {id++}" };
                var childCount = seed.Next(5, 100);
                for(int c = 0; c < childCount; c++)
                {
                    var child = new TreeViewItemModel { Id = id, Title = $"Id {id++}",Parent =node };
                    node.Children.Add(child);
                }
                TreeViews.Add(node);
            }
        }
    }
}
