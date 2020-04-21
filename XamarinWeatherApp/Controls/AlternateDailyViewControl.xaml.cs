using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace XamarinWeatherApp.Controls
{
    public partial class AlternateDailyViewControl : ContentView
    {
        public AlternateDailyViewControl()
        {
            InitializeComponent();
            if (Settings.Settings.IsDefaultGridVewVisible)
            {
                GridLayout.TextColor = Color.White;
                ListLayout.TextColor = Color.Gray;
            }
            else
            {
                GridLayout.TextColor = Color.Gray;
                ListLayout.TextColor = Color.White;
            }
        }

        public static readonly BindableProperty IsCheckedProperty = BindableProperty.Create(nameof(IsChecked), typeof(bool), typeof(DegreeSwitchControl));

        public bool IsChecked
        {
            get => (bool)GetValue(IsCheckedProperty);
            set => SetValue(IsCheckedProperty, value);
        }

        private void OnTapped(object sender, EventArgs e)
        {
            IsChecked = !IsChecked;
            if (IsChecked)
            {
                GridLayout.TextColor = Color.White;
                ListLayout.TextColor = Color.Gray;
            }
            else
            {
                GridLayout.TextColor = Color.Gray;
                ListLayout.TextColor = Color.White;
            }
        }
    }
}
