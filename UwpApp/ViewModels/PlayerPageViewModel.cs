using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UwpApp.Views;
using Windows.Foundation;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.Storage;
using Windows.UI.Composition;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;

namespace UwpApp.ViewModels
{
    public class PlayerPageViewModel: UWPViewModelBase
    {
        public override void OnLoaded(object obj = null)
        {
            base.OnLoaded(obj);
        }
        public override void UnLoaded()
        {
            base.UnLoaded();
            if (_player != null)
            {
                _player.Dispose();
                _player = null;
            }
        }

        #region Player
        private Canvas _renderCanvas;
        MediaPlayer _player;
        private PlayerPage _playerView;
        public async void InitPlayer(PlayerPage render, string filePath)
        {
            _playerView = render;
            _renderCanvas = _playerView.GetRenderTarget;
            if (_player == null) _player = new MediaPlayer();
            var file = await StorageFile.GetFileFromPathAsync(filePath);
            var mediaSource = MediaSource.CreateFromStorageFile(file);
            _player.Source = mediaSource;
            _player.Play();
            InitPlayerRender(_player);
        }

        private void InitPlayerRender(MediaPlayer mediaPlayer)
        {
            
            if (mediaPlayer == null || _renderCanvas == null) return;
            mediaPlayer.SetSurfaceSize(new Size((int)_renderCanvas.ActualWidth,(int) _renderCanvas.ActualHeight));
            var compositor = ElementCompositionPreview.GetElementVisual(_playerView).Compositor;
            MediaPlayerSurface surface = mediaPlayer.GetSurface(compositor);

            SpriteVisual spriteVisual = compositor.CreateSpriteVisual();
            spriteVisual.Size =
                new System.Numerics.Vector2((float)_renderCanvas.ActualWidth, (float)_renderCanvas.ActualHeight);

            CompositionBrush brush = compositor.CreateSurfaceBrush(surface.CompositionSurface);
            spriteVisual.Brush = brush;

            ContainerVisual container = compositor.CreateContainerVisual();
            container.Children.InsertAtTop(spriteVisual);

            ElementCompositionPreview.SetElementChildVisual(_renderCanvas, container);
        }
        #endregion


    }
}
