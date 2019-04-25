using Data.Local.Data;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UwpApp.Comon;
using ViewModels;
using Windows.Storage.Pickers;

namespace UwpApp.ViewModels
{
    public class MVPageViewModel : UWPViewModelBase
    {
        public ObservableCollection<Video> Videos { get; set; } = new ObservableCollection<Video>();

        private ICommand _selectDirectory;
        public ICommand SelectDirectoryCommand
        {
            get
            {
                if (_selectDirectory == null)
                {
                    _selectDirectory = new RelayCommand(SelectDirectory);
                }
                return _selectDirectory;
            }
        }

        private ICommand _addFile;
        public ICommand AddFileCommand
        {
            get
            {
                if (_addFile == null)
                {
                    _addFile = new RelayCommand(AddFile);
                }
                return _addFile;
            }
        }

        private void SelectDirectory()
        {

        }

        public async void AddFile()
        {
            var file = await FileHelper.SelectFileAsync();
            if (file != null)
            {
                Windows.Storage.AccessCache.StorageApplicationPermissions
                    .FutureAccessList.AddOrReplace(file.Name, file);

                var video = new Video { FilePath = file.Path, FileName = file.DisplayName };
                Videos.Add(video);
                this.MessengerInstance.Send<Media>(video);
            }
        }
        public override void OnLoaded(object obj = null)
        {
            base.OnLoaded(obj);
        }
        public override void UnLoaded()
        {
            base.UnLoaded();
        }
    }
}
