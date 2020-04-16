using System;
using System.Collections.Generic;
using Xamarin.Forms;
using XamarinWeatherApp.Settings;

namespace XamarinWeatherApp.Controls
{
    public partial class DegreeSwitchControl : ContentView
    {
        public DegreeSwitchControl()
        {
            InitializeComponent();
            if (Settings.Settings.IsCelsius)
            {
                CDegree.TextColor = Color.White;
                FDegree.TextColor = Color.LightGray;
            }
            else
            {
                CDegree.TextColor = Color.LightGray;
                FDegree.TextColor = Color.White;
            }
            //CDegree.SetBinding(Label.TextColorProperty, new Binding("CTextColor", source: this));
            //FDegree.SetBinding(Label.TextColorProperty, new Binding("FTextColor", source: this));
        }

        //public static readonly BindableProperty CDegreeProperty = BindableProperty.Create("CTextColor", typeof(Color), typeof(DegreeSwitchControl));

        //public string CTextColor
        //{
        //    get => (string)GetValue(CDegreeProperty);
        //    set => SetValue(CDegreeProperty, value);
        //}

        //public static readonly BindableProperty FDegreeProperty = BindableProperty.Create("FTextColor", typeof(Color), typeof(DegreeSwitchControl));

        //public string FTextColor
        //{
        //    get => (string)GetValue(FDegreeProperty);
        //    set => SetValue(FDegreeProperty, value);
        //}

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
                CDegree.TextColor = Color.White;
                FDegree.TextColor = Color.LightGray;
            }
            else
            {
                CDegree.TextColor = Color.LightGray;
                FDegree.TextColor = Color.White;
            }
        }
    }
}
