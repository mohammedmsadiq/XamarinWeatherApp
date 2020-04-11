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
            InnerHeaderText.SetBinding(Label.TextProperty, new Binding("HeaderText", source: this));
        }

        public static readonly BindableProperty HeaderTitleProperty = BindableProperty.Create("HeaderText", typeof(string), typeof(AlternateNavigationBar), string.Empty);

        public string HeaderText
        {
            get => (string)GetValue(HeaderTitleProperty);
            set => SetValue(HeaderTitleProperty, value);
        }
    }
}
