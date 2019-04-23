
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
            if(CurrentMedia != null)
            {
                var file = await StorageFile.GetFileFromPathAsync(CurrentMedia.FilePath);
               
                var mediaSource = MediaSource.CreateFromStorageFile(file);
                _player.Source = mediaSource;
                mediaSource.OpenOperationCompleted += MediaSource_OpenOperationCompleted;
                //_player.TimelineController = _mediaTimelineController;
                _mediaTimelineController.Start();
            }

        }

        private void MediaSource_OpenOperationCompleted(MediaSource sender, MediaSourceOpenOperationCompletedEventArgs args)
        {
            //throw new NotImplementedException();
        }

        private void Next()
        {
            var mediaSource = MediaSource.CreateFromUri(new Uri(@"ms-appx:///Assets/mp3.mp3"));
            _player.Source = mediaSource;
            //_player.TimelineController = _mediaTimelineController;
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
        public override void OnLoaded(object obj = null)
        {
            SystemNavigationManager.BackRequested += SystemNavigationManager_BackRequested;
            base.OnLoaded(obj);
            this.NavigationService.CurrentFrame = obj as Frame;
            this.NavigationService.NavigateTo(ViewModelLocator.LocalPage);
            UpdateBackState();

            MessengerInstance.Register<Music>(this,RecieveMusic);
            _player.CommandManager.IsEnabled = false;
            _mediaTimelineController.PositionChanged += _mediaTimelineController_PositionChanged;
            _player.TimelineController = _mediaTimelineController;
            _systemMediaTransportControls = _player.SystemMediaTransportControls;
            _systemMediaTransportControls.IsPlayEnabled = true;
            _systemMediaTransportControls.IsPauseEnabled = true;
            _systemMediaTransportControls.ButtonPressed += _systemMediaTransportControls_ButtonPressed;
            _player.MediaOpened += _player_MediaOpened;
        }

        private async void _systemMediaTransportControls_ButtonPressed(SystemMediaTransportControls sender, SystemMediaTransportControlsButtonPressedEventArgs args)
        {
            switch (args.Button)
            {
                case SystemMediaTransportControlsButton.Play:
                    await DispatcherHelper.RunAsync( () =>
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

        SystemMediaTransportControls _systemMediaTransportControls;
        public volatile bool OnChangePosition;
        MediaTimelineController _mediaTimelineController = new MediaTimelineController();
        private double _totalSeconds, _currentSeconds;
        public double TotalSeconds
        {
            get { return _totalSeconds; }
            set { Set("TotalSeconds", ref _totalSeconds,value); }
        }
        public double CurrentSeconds
        {
            get { return _currentSeconds; }
            set { Set("CurrentSeconds", ref _currentSeconds, value); }
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
            await DispatcherHelper.RunAsync(()=> {
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

        public override void UnLoaded()
        {
            base.UnLoaded();
            MessengerInstance.Unregister<Media>(this,RecieveMusic);
            _player.MediaOpened += _player_MediaOpened;
            _player.Dispose();
        }

        public Media CurrentMedia { get; set; }

        private void RecieveMusic(Media media)
        {
            CurrentMedia = media;
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
