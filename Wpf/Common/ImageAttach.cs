using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace Wpf_research.Common
{
    public static class ImageAttach
    {
        #region attached properties
        [AttachedPropertyBrowsableForType(typeof(Image))]
        public static ImageSource GetAnimatedSource(Image obj)
        {
            return (ImageSource)obj.GetValue(AnimatedSourceProperty);
        }

        public static void SetAnimatedSource(Image obj, ImageSource value)
        {
            obj.SetValue(AnimatedSourceProperty, value);
        }

        public static readonly DependencyProperty AnimatedSourceProperty =
            DependencyProperty.RegisterAttached(
                "AnimatedSource",
                typeof(ImageSource),
                typeof(ImageAttach),
                new PropertyMetadata(
                    null,
                   AnimatedSourceChanged));
        #endregion


        private static void AnimatedSourceChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var image = o as Image;
            if (image == null) return;

            var oldValue = e.OldValue as ImageSource;
            var newValue = e.NewValue as ImageSource;

            if (ReferenceEquals(oldValue, newValue))
            {
                if (image.IsLoaded)
                {
                    //var isAnimLoaded = GetIsAnimationLoaded(image);
                    //if (!isAnimLoaded)
                    //{
                    InitAnimation(image);
                    //  }
                }
                return;
            }

            if (oldValue != null)
            {
                image.Loaded -= Image_Loaded;
                image.Unloaded -= Image_Unloaded;
                image.Source = null;
            }

            if (newValue != null)
            {
                image.Loaded += Image_Loaded;
                image.Unloaded += Image_Unloaded;

                if (image.IsLoaded)
                {
                    InitAnimation(image);
                }
            }
        }

        private static void Image_Unloaded(object sender, RoutedEventArgs e)
        {
            var image = sender as Image;
            if (image == null) return;

        }

        private static void Image_Loaded(object sender, RoutedEventArgs e)
        {
            var image = sender as Image;
            if (image == null) return;
            InitAnimation(image);
        }

        private static void InitAnimation(Image image)
        {
            var source = GetAnimatedSource(image) as BitmapSource;

            if (source == null) return;

            if (source.IsDownloading)
            {
                EventHandler handler = null;
                handler = (sender, args) =>
                {
                    source.DownloadCompleted -= handler;
                    InitAnimation(image);
                };
                source.DownloadCompleted += handler;
                image.Source = source;
                return;
            }

            var animation = GetAnimation(image, source);

            if (animation != null)
            {
                if (animation.KeyFrames.Count > 0)
                {
                    image.Source = (ImageSource)animation.KeyFrames[0].Value;
                }
                else
                {
                    image.Source = source;
                }
                animation.RepeatBehavior = RepeatBehavior.Forever;
                image.BeginAnimation(Image.SourceProperty, animation);
                return;
            }

            image.Source = source;
        }

        private static ObjectAnimationUsingKeyFrames GetAnimation(Image image, BitmapSource source)
        {
            var decoder = GetDecoder(source, image);

            var animation = new ObjectAnimationUsingKeyFrames();

            if (decoder != null && decoder.Frames.Count > 1)
            {
                var keyFrames = new ObjectKeyFrameCollection();
                var totalDuration = TimeSpan.Zero;
                BitmapSource baseFrame = null;

                foreach (var rawFrame in decoder.Frames)
                {
                    var delay = TimeSpan.FromMilliseconds(rawFrame.Metadata.GetFrameMetaDataValue<int>("/grctlext/Delay") * 10);
                    if (delay <= TimeSpan.Zero) delay = TimeSpan.FromMilliseconds(100);
                    var width = decoder.Metadata.GetFrameMetaDataValue<int>("/logscrdesc/Width");
                    var height = decoder.Metadata.GetFrameMetaDataValue<int>("/logscrdesc/Height");
                    var disposalMethod = (FrameDisposalMethod)rawFrame.Metadata.GetFrameMetaDataValue<byte>("/grctlext/Disposal");

                    var rFrame = MakeFrame(new Size((double)width, (double)height), rawFrame, baseFrame);
                    keyFrames.Add(new DiscreteObjectKeyFrame(rFrame
                      ,
                      totalDuration));

                    switch (disposalMethod)
                    {
                        case FrameDisposalMethod.None:
                        case FrameDisposalMethod.DoNotDispose:
                            //if (IsFullSize(rawFrame.Metadata, new Size(width, height)))
                            //{
                            baseFrame = rFrame;
                            //}
                            break;
                        case FrameDisposalMethod.RestoreBackground:
                            baseFrame = null;
                            break;
                        case FrameDisposalMethod.RestorePrevious:
                            break;
                    }

                    totalDuration += delay;
                }

                animation.KeyFrames = keyFrames;
            }

            return animation;
        }

        private static BitmapDecoder GetDecoder(BitmapSource source, Image image)
        {
            BitmapDecoder decoder = null;
            Stream stream = null;
            Uri uri = null;
            BitmapCreateOptions createOptions = BitmapCreateOptions.None;

            var bmp = source as BitmapImage;
            if (bmp != null)
            {
                createOptions = bmp.CreateOptions;
                if (bmp.StreamSource != null)
                {
                    stream = bmp.StreamSource;
                }
                else if (bmp.UriSource != null)
                {
                    uri = bmp.UriSource;
                    if (uri.IsAbsoluteUri)
                    {
                        var baseUri = bmp.BaseUri ?? (image as IUriContext).BaseUri;
                        if (baseUri != null)
                        {
                            uri = new Uri(baseUri, uri);
                        }
                    }
                }
            }
            else
            {
                BitmapFrame frame = source as BitmapFrame;
                if (frame != null)
                {
                    decoder = frame.Decoder;
                    Uri.TryCreate(frame.BaseUri, frame.ToString(), out uri);
                }
            }

            if (decoder == null)
            {
                if (stream != null)
                {
                    stream.Position = 0;
                    decoder = BitmapDecoder.Create(stream, createOptions, BitmapCacheOption.OnLoad);
                }
                else if (uri != null && uri.IsAbsoluteUri)
                {
                    decoder = BitmapDecoder.Create(uri, createOptions, BitmapCacheOption.OnLoad);
                }
            }

            return decoder;
        }

        private static BitmapSource MakeFrame(Size size, BitmapSource rawFrame,
            BitmapSource baseFrame)
        {
            if (baseFrame == null && IsFullSize(rawFrame.Metadata, size))
            {
                return rawFrame;
            }

            DrawingVisual visual = new DrawingVisual();
            using (var context = visual.RenderOpen())
            {
                var rect = rawFrame.Metadata.GetFrameRect();

                if (baseFrame != null)
                {
                    var fullRect = new Rect(0, 0, size.Width, size.Height);

                    context.DrawImage(baseFrame, fullRect);
                    context.DrawImage(baseFrame, fullRect);
                    //清楚重复区域
                    var clip = Geometry.Combine(
                        new RectangleGeometry(rect),
                        new RectangleGeometry(fullRect),
                        GeometryCombineMode.Intersect,
                        null);
                    context.PushClip(clip);

                    context.DrawImage(rawFrame, rect);
                }
            }

            var bitmap = new RenderTargetBitmap(
                (int)size.Width, (int)size.Height,
                96, 96,
                PixelFormats.Pbgra32);
            bitmap.Render(visual);

            var writeableBitmap = new WriteableBitmap(bitmap);

            if (writeableBitmap.CanFreeze && !writeableBitmap.IsFrozen)
            {
                writeableBitmap.Freeze();
            }

            return writeableBitmap;
        }

        private static bool IsFullSize(this ImageMetadata metadata, Size size)
        {
            var rect = metadata.GetFrameRect();

            return rect.Top == 0
                && rect.Left == 0
                && rect.Width == size.Width
                && rect.Height == size.Height;
        }

        private static Rect GetFrameRect(this ImageMetadata metadata)
        {
            return new Rect(
                metadata.GetFrameMetaDataValue<int>("/imgdesc/Left"),
                metadata.GetFrameMetaDataValue<int>("/imgdesc/Top"),
                metadata.GetFrameMetaDataValue<int>("/imgdesc/Width"),
                metadata.GetFrameMetaDataValue<int>("/imgdesc/Height")
                );
        }

        private static T GetFrameMetaDataValue<T>(this ImageMetadata metadata, string query)
        {
            if (metadata != null && metadata is BitmapMetadata bitMeta)
            {
                if (bitMeta.ContainsQuery(query))
                {
                    var value = bitMeta.GetQuery(query);
                    if (value is object && value is T tvalue)
                    {
                        return tvalue;
                    }
                    else
                    {
                        return (T)Convert.ChangeType(value, typeof(T));
                    }

                }
            }
            return default(T);
        }


        private enum FrameDisposalMethod
        {
            None = 0,
            DoNotDispose = 1,
            RestoreBackground = 2,
            RestorePrevious = 3
        }
    }
}
