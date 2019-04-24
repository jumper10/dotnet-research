
using Data.Local.Data;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Threading;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Input;
using UwpApp.Views;
using ViewModels;
using Windows.Devices.Enumeration;
using Windows.Media;
using Windows.Media.Core;
using Windows.Media.Devices;
using Windows.Media.Playback;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

namespace UwpApp.ViewModels
{
    public class MainShellViewModel : UWPViewModelBase
    {
        readonly SystemNavigationManager SystemNavigationManager = SystemNavigationManager.GetForCurrentView();

        private bool _canShowCommandBar =true;
        public bool CanShowCommandBar
        {
            get { return _canShowCommandBar; }
            set { Set("CanShowCommandBar", ref _canShowCommandBar,value); }
        }

        private ICommand _ItemInvokedCommand;
        public ICommand ItemInvokedCommand => 
            this._ItemInvokedCommand ?? (this._ItemInvokedCommand = new RelayCommand<NavigationViewItemInvokedEventArgs>(OnItemInvoked));
       

        private void OnItemInvoked(NavigationViewItemInvokedEventArgs args)
        {
            this.NavigationService.NavigateTo(args.InvokedItemContainer.Tag?.ToString());
            UpdateBackState();
        }
        
        public override void OnLoaded(object obj = null)
        {
            MessengerInstance.Register<Media>(this, RecieveMusic);
            SystemNavigationManager.BackRequested += SystemNavigationManager_BackRequested;
            base.OnLoaded(obj);
            this.NavigationService.CurrentFrame = obj as Frame;
            this.NavigationService.NavigateTo(ViewModelLocator.LocalPage);
            UpdateBackState();
            
            _player = new MediaPlayer();
            _player.CommandManager.IsEnabled = false;
            _mediaTimelineController = new MediaTimelineController();
            _mediaTimelineController.PositionChanged += _mediaTimelineController_PositionChanged;
            _player.TimelineController = _mediaTimelineController;
            _systemMediaTransportControls = _player.SystemMediaTransportControls;
            _systemMediaTransportControls.IsPlayEnabled = true;
            _systemMediaTransportControls.IsPauseEnabled = true;
            _systemMediaTransportControls.ButtonPressed += _systemMediaTransportControls_ButtonPressed;
            _player.MediaOpened += _player_MediaOpened;

        }
        public override void UnLoaded()
        {
            base.UnLoaded();
            this.NavigationService.CurrentFrame = null;
            MessengerInstance.Unregister<Media>(this, RecieveMusic);
            _player.MediaOpened -= _player_MediaOpened;
            _player.Dispose();
        }
        public Media CurrentMedia { get; set; }

        private void RecieveMusic(Media media)
        {
            CurrentMedia = media;
        }

        private void SystemNavigationManager_BackRequested(object sender, BackRequestedEventArgs e)
        {
            if (this.AppNavigationService.CanGoBack)
            {
                this.AppNavigationService.GoBack();
                UpdateBackState();
            }
            else if (this.NavigationService.CanGoBack)
            {
                this.NavigationService.GoBack();
                UpdateBackState();
            }
        }

        private void UpdateBackState()
        {
            SystemNavigationManager.AppViewBackButtonVisibility = this.NavigationService.CanGoBack || this.AppNavigationService.CanGoBack ? AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;
        }

        #region Player
        MediaPlayer _player;
        SystemMediaTransportControls _systemMediaTransportControls;
        public volatile bool OnChangePosition;
        MediaTimelineController _mediaTimelineController;
        private double _totalSeconds, _currentSeconds;
        public double TotalSeconds
        {
            get { return _totalSeconds; }
            set { Set("TotalSeconds", ref _totalSeconds, value); }
        }
        public double CurrentSeconds
        {
            get { return _currentSeconds; }
            set { Set("CurrentSeconds", ref _currentSeconds, value); }
        }

        private ICommand _previousCommand, _playCommand, _nextCommand, _likeCommand, _priorityCommand, _deleteCommand;

        public ICommand PreviousCommand => _previousCommand ?? (_previousCommand = new RelayCommand(Previous));
        public ICommand PlayCommand => _playCommand ?? (_playCommand = new RelayCommand(Play));
        public ICommand NextCommand => _nextCommand ?? (_nextCommand = new RelayCommand(Next));
        public ICommand LikeCommand => _likeCommand ?? (_likeCommand = new RelayCommand(Like));
        public ICommand PriorityCommand => _priorityCommand ?? (_priorityCommand = new RelayCommand(Priority));
        public ICommand DeleteCommand => _deleteCommand ?? (_deleteCommand = new RelayCommand(Delete));

        private async void _systemMediaTransportControls_ButtonPressed(SystemMediaTransportControls sender, SystemMediaTransportControlsButtonPressedEventArgs args)
        {
            switch (args.Button)
            {
                case SystemMediaTransportControlsButton.Play:
                    await DispatcherHelper.RunAsync(() =>
                    {
                        _player.Play();
                    });
                    break;
                case SystemMediaTransportControlsButton.Pause:
                    await DispatcherHelper.RunAsync(() =>
                    {
                        _player.Pause();
                    });
                    break;
                default:
                    break;
            }
        }

        private async void _mediaTimelineController_PositionChanged(MediaTimelineController sender, object args)
        {
            if (OnChangePosition) return;
            await DispatcherHelper.RunAsync(() => {
                CurrentSeconds = sender.Position.TotalSeconds;
            });
        }

        private async void _player_MediaOpened(MediaPlayer sender, object args)
        {
            await DispatcherHelper.RunAsync(() => {
                TotalSeconds = sender.PlaybackSession.NaturalDuration.TotalSeconds;
            });
        }

        public void TraceValueChanged(double newValue)
        {
            if (_player.PlaybackSession.PlaybackState == MediaPlaybackState.Playing || _player.PlaybackSession.PlaybackState == MediaPlaybackState.Opening)
            {
                _player.TimelineController.Position = new TimeSpan(0, 0, (int)newValue);
            }
        }
        private void Previous()
        {

        }
        private async void Play()
        {
            if (CurrentMedia != null)
            {
                var file = await StorageFile.GetFileFromPathAsync(CurrentMedia.FilePath);
                if (CurrentMedia is Video)
                {
                    AppNavigationService.NavigateTo(ViewModelLocator.PlayerPage, CurrentMedia.FilePath);
                    UpdateBackState();
                }
                else
                {
                    var mediaSource = MediaSource.CreateFromStorageFile(file);
                    _player.Source = mediaSource;
                    _mediaTimelineController.Start();
                }
            }

        }
        private void Next()
        {
            var mediaSource = MediaSource.CreateFromUri(new Uri(@"ms-appx:///Assets/mp3.mp3"));
            _player.Source = mediaSource;
            _player.TimelineController.Start();
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
        #endregion
    }
}
