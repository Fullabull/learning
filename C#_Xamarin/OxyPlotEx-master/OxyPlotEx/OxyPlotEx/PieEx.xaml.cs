﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace OxyPlotEx
{
    public partial class PieEx : ContentPage
    {
        public PieEx()
        {
            InitializeComponent();
            BindingContext = new OxyExData();
        }
    }
}
