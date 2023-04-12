﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Test.Models;

namespace Test.UserControls
{
    /// <summary>
    /// Логика взаимодействия для ProductControl.xaml
    /// </summary>
    public partial class ProductControl : UserControl
    {
        public ProductControl(Product pr)
        {
            InitializeComponent();
            data.DataContext = pr;
            TbManufac.Text = pr.IdmanufacturerNavigation.Manufacname;
            if(pr.ProductPhoto!= null) 
                Img.Source = new BitmapImage(new Uri($"/Resources/photos/{pr.ProductPhoto}", UriKind.Relative)); 
        }
    }
}
