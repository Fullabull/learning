﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace OxyPlotEx
{
    public partial class BarEx : ContentPage
    {
        public BarEx()
        {
            InitializeComponent();
            BindingContext = new OxyExData();
        }
    }
}
