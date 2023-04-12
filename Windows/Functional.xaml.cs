using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Test.Models;
using Test.UserControls;

namespace Test.Windows
{
    /// <summary>
    /// Логика взаимодействия для Functional.xaml
    /// </summary>
    public partial class Functional : Window
    {
        int count = tradeContext.GetContext().Product.Count(); 
        public Functional()
        {
            InitializeComponent();
            TbNameUser.Text = $"{App.CurrentUser.UserSurname} {App.CurrentUser.UserName} {App.CurrentUser.UserPatronymic}\n" +
                $"{App.CurrentUser.UserRoleNavigation.RoleName}";
            FillList();
            Fill_CBFiltr();
        }

        public void FillList()
        {
            LbMain.Items.Clear();
            //Парсим данные с базы
            var products = tradeContext.GetContext().Product.Include(p => p.IdmanufacturerNavigation).ToList();
            // Поиск 
            if (TbSearch.Text != "".TrimEnd())
                products = products.Where(p => p.ProductName.ToLower().Contains(TbSearch.Text.ToLower()) 
                || p.ProductDescription.ToLower().Contains(TbSearch.Text.ToLower())).ToList();
            // Сортировка
            if(CbSort.SelectedIndex > 0)
            {
                if (CbSort.SelectedIndex == 1)
                    products = products.OrderBy(p => p.ProductCost).ToList();
                else
                    products = products.OrderByDescending(p => p.ProductCost).ToList();
            }
            // Фильтрация 
            if(CbFiltr.SelectedIndex > 0)
                products = products.Where(p => p.Idmanufacturer == CbFiltr.SelectedIndex + 1).ToList();
            // Вывод
            foreach (var p in products)
            {
                LbMain.Items.Add(new ProductControl(p));
            }
            TbAllCount.Text = $"{products.Count()} / {count}";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Auth a = new Auth();
            a.Show();
            this.Close();
        }

        public void Fill_CBFiltr()
        {
            var manufacs = tradeContext.GetContext().Manufacturer.ToList();
            CbFiltr.Items.Add("Все производители");
            foreach (var m in manufacs)
            {
                CbFiltr.Items.Add(m.Manufacname);
            }
            CbFiltr.SelectedIndex = 0; 
            CbSort.SelectedIndex = 0;
        }

        private void TbSearch_TextChanged(object sender, TextChangedEventArgs e) { FillList(); }

        private void CbSort_SelectionChanged(object sender, SelectionChangedEventArgs e) { FillList(); }

        private void CbFiltr_SelectionChanged(object sender, SelectionChangedEventArgs e) { FillList(); }
    }
}
