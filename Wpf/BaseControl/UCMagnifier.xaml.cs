using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Wpf_research
{
    /// <summary>
    /// UCMagnifier.xaml 的交互逻辑
    /// </summary>
    public partial class UCMagnifier : UserControl
    {
        public static readonly double MaxZoomFact = 80;
        public static DependencyProperty CornerRadiusProperty;
        public static DependencyProperty ZoomFactorProperty;
        public static DependencyProperty MagnifierWidthProperty;
        public static DependencyProperty MagnifierHeightProperty;
        public static DependencyProperty ImageSourceProperty;

        private static void OnZoomFactorChanged(DependencyObject o, DependencyPropertyChangedEventArgs dp)
        {
            var obj = o as UCMagnifier;
            var newValue = (double)dp.NewValue;

            if (obj == null) return;
            obj.ZoomFactor = newValue;
            if (newValue <= 0 && newValue > MaxZoomFact)
            {
                obj.ZoomFactor = 1;
            }
        }

        static UCMagnifier()
        {
            UCMagnifier.ImageSourceProperty =
                DependencyProperty.Register("ImageSource", typeof(ImageSource), typeof(UCMagnifier), new UIPropertyMetadata((default(ImageSource))));
            UCMagnifier.CornerRadiusProperty =
                DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(UCMagnifier), new PropertyMetadata(default(CornerRadius)));
            UCMagnifier.ZoomFactorProperty =
                DependencyProperty.Register("ZoomFactor", typeof(double), typeof(UCMagnifier), new UIPropertyMetadata(1.0d, new PropertyChangedCallback(UCMagnifier.OnZoomFactorChanged)));
            UCMagnifier.MagnifierWidthProperty =
                DependencyProperty.Register("MagnifierWidth", typeof(double), typeof(UCMagnifier), new UIPropertyMetadata(60d, new PropertyChangedCallback(UCMagnifier.OnZoomFactorChanged)));
            UCMagnifier.MagnifierHeightProperty =
                           DependencyProperty.Register("MagnifierHeight", typeof(double), typeof(UCMagnifier), new UIPropertyMetadata(60d, new PropertyChangedCallback(UCMagnifier.OnZoomFactorChanged)));

        }
        public UCMagnifier()
        {
            InitializeComponent();
            this.Unloaded += (obj, e) => {
                baseImage.MouseMove -= BaseImage_MouseMove;
            };
        }

        public ImageSource ImageSource
        {
            get
            {
                return (ImageSource)base.GetValue(UCMagnifier.ImageSourceProperty);
            }
            set
            {
                base.SetValue(UCMagnifier.ImageSourceProperty, value);
            }
        }
        public CornerRadius CornerRadius
        {
            get
            {
                return (CornerRadius)base.GetValue(UCMagnifier.CornerRadiusProperty);
            }
            set
            {
                base.SetValue(UCMagnifier.CornerRadiusProperty, value);
            }
        }

        public double ZoomFactor
        {
            get
            {
                return (double)base.GetValue(UCMagnifier.ZoomFactorProperty);
            }
            set
            {
                base.SetValue(UCMagnifier.ZoomFactorProperty, value);
            }
        }
        public double MagnifierWidth
        {
            get
            {
                return (double)base.GetValue(UCMagnifier.MagnifierWidthProperty);
            }
            set
            {
                base.SetValue(UCMagnifier.MagnifierWidthProperty, value);
            }
        }

        public double MagnifierHeight
        {
            get
            {
                return (double)base.GetValue(UCMagnifier.MagnifierHeightProperty);
            }
            set
            {
                base.SetValue(UCMagnifier.MagnifierHeightProperty, value);
            }
        }

        private void BaseImage_MouseMove(object sender, MouseEventArgs e)
        {
            var currentPosition = e.GetPosition(Canvas);
            var viewWidth = Magnifier.ActualWidth;

            Rect viewbox = new Rect(
                currentPosition.X,
                currentPosition.Y,
                viewWidth, viewWidth);
            MagnifierBrush.Viewbox = viewbox;

            var xp = currentPosition.X;
            var yp = currentPosition.Y;
            if (xp + Magnifier.Width > Canvas.ActualWidth)
            {
                xp = xp - Magnifier.Width;
            }
            if (yp + Magnifier.Height > Canvas.ActualHeight)
            {
                yp = yp - Magnifier.Height;
            }

            Canvas.SetLeft(Magnifier, xp);
            Canvas.SetTop(Magnifier, yp);
        }
    }
}
