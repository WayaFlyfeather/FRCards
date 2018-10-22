﻿using FRCards.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FRCards.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DeckSetView : ContentView
    {
        public DeckSetViewModel ViewModel => this.BindingContext as DeckSetViewModel;
        public DeckSetView()
        {
            InitializeComponent();
        }
    }
}