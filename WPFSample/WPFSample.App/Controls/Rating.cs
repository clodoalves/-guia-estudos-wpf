using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WPFSample.App.Controls
{
    public sealed class Rating : Control
    {
        public int Value
        {
            get { return (int)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public Rating()
        {
            this.DefaultStyleKey = typeof(Rating);
        }

        // Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(int), typeof(Rating), new PropertyMetadata(1, OnValuePropertyChanged));

        private static void OnValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var self = (Rating)d;
            self.UpdateRatingState();
        }

        private void UpdateRatingState()
        {
            var val = Value.Clamp(1, 5);
            var state = $"Rating{val}";
            var result = VisualStateManager.GoToState(this, state, true);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            UpdateRatingState();
        }
    }

    public static class RatingExtensions
    {
        public static T Clamp<T>(this T val, T min, T max) where T : IComparable<T>
        {
            if (val.CompareTo(min) < 0) return min;
            else if (val.CompareTo(max) > 0) return max;
            else return val;
        }
    }
}