using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Wpf_research
{
    public class CircularPanel : Panel
    {
        public bool UsedRatateTransform { get; set; } = false;

        protected override Size MeasureOverride(Size availableSize)
        {
            foreach (UIElement child in Children)
            {
                child.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            }
            return base.MeasureOverride(availableSize);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            if (Children.Count > 0)
            {
                var center = new Point(finalSize.Width / 2, finalSize.Height / 2);
                var radius = Math.Min(finalSize.Width, finalSize.Height) / 2.0;

                var itemAngle = 2.0 * Math.PI / Children.Count;
                var currentAngle = 0.0;
                var angleUnit = 360 / (2.0 * Math.PI);
                foreach (UIElement child in Children)
                {
                    var chilPosition = new Point(
                        radius * Math.Cos(currentAngle) + center.X,
                        radius * Math.Sin(currentAngle) + center.Y);
                    if (UsedRatateTransform)
                    {
                        child.RenderTransform = new RotateTransform(currentAngle *angleUnit);
                    }

                    child.Arrange(new Rect(chilPosition, child.DesiredSize));
                    currentAngle += itemAngle;
                }

            }
            return finalSize;
        }

    }
}
