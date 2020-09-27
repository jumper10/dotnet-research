using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Wpf_research.BaseControl
{
    /// <summary>
    /// UCLoading.xaml 的交互逻辑
    /// </summary>
    public partial class UCLoading : UserControl
    {
        Storyboard storyboard;

        bool initial = false;
        public UCLoading()
        {
            InitializeComponent();          
        }

        private void CalcLocation()
        {
            const double offset = Math.PI;
            const double step = Math.PI * 2.0 / 8.0;
            double R = Math.Min(Height * 1.0, Width * 1.0) / 2.0-7;

            var rota = Canvas.RenderTransform as RotateTransform;
            if (rota != null)
            {
                rota.CenterX = R+7;
                rota.CenterY = R+7;
            }

            SetPosition(C0, offset, 0.0, step, R);
            SetPosition(C1, offset, 1.0, step, R);
            SetPosition(C2, offset, 2.0, step, R);
            SetPosition(C3, offset, 3.0, step, R);
            SetPosition(C4, offset, 4.0, step, R);
            SetPosition(C5, offset, 5.0, step, R);
            SetPosition(C6, offset, 6.0, step, R);
            SetPosition(C7, offset, 7.0, step, R);
        }

        private void SetPosition(Ellipse ellipse, double offset,
           double posOffSet, double step, double r)
        {
            ellipse.SetValue(Canvas.LeftProperty, r
                + Math.Sin(offset + posOffSet * step) * r);

            ellipse.SetValue(Canvas.TopProperty, r
                + Math.Cos(offset + posOffSet * step) * r);
        }

        private void UserControl_IsVisibleChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            if(sender is UserControl uc)
            {
                if (uc.IsVisible)
                {
                    if (IsLoaded && !initial)
                    {
                        initial = true;
                        storyboard = FindResource("round") as Storyboard;
                        CalcLocation();
                    }

                    storyboard?.Begin();
                }
                else
                {
                    storyboard?.Stop();
                }
            }
        }
    }
}
