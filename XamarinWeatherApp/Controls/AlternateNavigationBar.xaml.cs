using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace XamarinWeatherApp.Controls
{
    public partial class AlternateNavigationBar : ContentView
    {
        public AlternateNavigationBar()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty HeaderTextProperty =
            BindableProperty.Create(nameof(HeaderText), typeof(string), typeof(AlternateNavigationBar), string.Empty);

        public string HeaderText
        {
            get => (string)GetValue(HeaderTextProperty);
            set => SetValue(HeaderTextProperty, value);
        }
    }
}
