using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.Storage;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace UwpApp.Views
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class PlayerPage : Page
    {
       
        public PlayerPage()
        {
            this.InitializeComponent();
            this.Loaded += (seder, e) => {
               // InitPlayerRender(_mediaPlayer);
            };
        }

        MediaPlayer _player = new MediaPlayer();

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if(e.Parameter != null && e.Parameter is String)
            {
                InitPlayer((string)e.Parameter);
               
            }
        }

        private async void InitPlayer(string filePath)
        {
            var file =await StorageFile.GetFileFromPathAsync(filePath);
            var mediaSource = MediaSource.CreateFromStorageFile(file);
            _player.Source = mediaSource;
            
            _player.Play();
            InitPlayerRender(_player);
        }


        private void InitPlayerRender(MediaPlayer mediaPlayer)
        {
            if (mediaPlayer == null) return;
            mediaPlayer.SetSurfaceSize(new Size(RenderCanvas.ActualWidth,RenderCanvas.ActualHeight));
            var compositor = ElementCompositionPreview.GetElementVisual(this).Compositor;
            MediaPlayerSurface surface = mediaPlayer.GetSurface(compositor);

            SpriteVisual spriteVisual = compositor.CreateSpriteVisual();
            spriteVisual.Size =
                new System.Numerics.Vector2((float)RenderCanvas.ActualWidth, (float)RenderCanvas.ActualHeight);

            CompositionBrush brush = compositor.CreateSurfaceBrush(surface.CompositionSurface);
            spriteVisual.Brush = brush;

            ContainerVisual container = compositor.CreateContainerVisual();
            container.Children.InsertAtTop(spriteVisual);

            ElementCompositionPreview.SetElementChildVisual(RenderCanvas, container);
        }

        
    }

    public class PlayerPageNavigationEventArgs
    {
        MediaPlayer MediaPlayer { get; set; }
    }
}
