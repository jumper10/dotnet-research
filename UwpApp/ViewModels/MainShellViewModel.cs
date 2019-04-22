
using Data.Local.Data;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using Microsoft.Toolkit.Uwp.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Input;
using UwpApp.Views;
using ViewModels;
using Windows.Devices.Enumeration;
using Windows.Media.Core;
using Windows.Media.Devices;
using Windows.Media.Playback;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;

namespace UwpApp.ViewModels
{
    public class MainShellViewModel : UWPViewModelBase
    {
        readonly SystemNavigationManager SystemNavigationManager = SystemNavigationManager.GetForCurrentView();

        MediaPlayer _player = new MediaPlayer(); 
        

        private ICommand _ItemInvokedCommand;
        public ICommand ItemInvokedCommand => 
            this._ItemInvokedCommand ?? (this._ItemInvokedCommand = new RelayCommand<NavigationViewItemInvokedEventArgs>(OnItemInvoked));
        private ICommand _previousCommand, _playCommand, _nextCommand, _likeCommand, _priorityCommand, _deleteCommand;

        public ICommand PreviousCommand => _previousCommand ?? (_previousCommand= new RelayCommand(Previous));
        public ICommand PlayCommand => _playCommand ??(_playCommand= new RelayCommand(Play));
        public ICommand NextCommand => _nextCommand ??(_nextCommand= new RelayCommand(Next));
        public ICommand LikeCommand => _likeCommand ??(_likeCommand= new RelayCommand(Like));
        public ICommand PriorityCommand => _priorityCommand ??(_priorityCommand= new RelayCommand(Priority));
        public ICommand DeleteCommand => _deleteCommand ??(_deleteCommand= new RelayCommand(Delete));

        private void OnItemInvoked(NavigationViewItemInvokedEventArgs args)
        {
            // could also use a converter on the command parameter if you don't like
            // the idea of passing in a NavigationViewItemInvokedEventArgs
            this.NavigationService.NavigateTo(args.InvokedItemContainer.Tag?.ToString());
            UpdateBackState();
        }
        private void Previous()
        {

        }
        private async void Play()
        {
            if(CurrentMusic != null)
            {
                var mediaSource = MediaSource.CreateFromUri(new Uri(CurrentMusic.FilePath,UriKind.RelativeOrAbsolute));
                _player.Source = mediaSource;
                //string audioSelector = MediaDevice.GetAudioRenderSelector();
                //var outputDevices = await DeviceInformation.FindAllAsync(audioSelector);
               // _player.AudioDevice = outputDevices[0];
                _player.Volume = 50;
                _player.AutoPlay = true;
                _player.Play();
            }

        }
        private void Next()
        {
            var mediaSource = MediaSource.CreateFromUri(new Uri(@"ms-appx:///Assets/mp3.mp3"));
            _player.Source = mediaSource;
            //string audioSelector = MediaDevice.GetAudioRenderSelector();
            //var outputDevices = await DeviceInformation.FindAllAsync(audioSelector);
            // _player.AudioDevice = outputDevices[0];
           // _player.Volume = 50;
          //  _player.AutoPlay = true;
            _player.Play();

        }
        private void Like()
        {

        }
        private void Priority()
        {

        }
        private void Delete()
        {

        }
        public override void OnLoaded(object obj = null)
        {
            SystemNavigationManager.BackRequested += SystemNavigationManager_BackRequested;
            base.OnLoaded(obj);
            this.NavigationService.CurrentFrame = obj as Frame;
            this.NavigationService.NavigateTo(ViewModelLocator.LocalPage);
            UpdateBackState();

            MessengerInstance.Register<Music>(this,RecieveMusic);
        }

        public override void UnLoaded()
        {
            base.UnLoaded();
            MessengerInstance.Unregister<Music>(this,RecieveMusic);
            _player.Dispose();
        }

        public Music CurrentMusic { get; set; }

        private void RecieveMusic(Music music)
        {
            CurrentMusic = music;
        }

        private void SystemNavigationManager_BackRequested(object sender, BackRequestedEventArgs e)
        {
            if (this.NavigationService.CanGoBack)
            {
                this.NavigationService.GoBack();
                UpdateBackState();
            }
        }

        private void UpdateBackState()
        {
            SystemNavigationManager.AppViewBackButtonVisibility = this.NavigationService.CanGoBack ? AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;
        }
    }
}
