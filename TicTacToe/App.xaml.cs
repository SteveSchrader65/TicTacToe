﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TicTacToe
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            MessagingCenter.Send<object>(this, "Loading ...");
        }

        protected override void OnSleep()
        {
            MessagingCenter.Send<object>(this, "Sleeping ...");
        }

        protected override void OnResume()
        {
        }
    }
}
