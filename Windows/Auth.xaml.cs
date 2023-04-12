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

namespace Test.Windows
{
    /// <summary>
    /// Логика взаимодействия для Auth.xaml
    /// </summary>
    public partial class Auth : Window
    {
        public Auth()
        {
            InitializeComponent();
        }

        private void BtLogin_Click(object sender, RoutedEventArgs e)
        {
            var user = tradeContext.GetContext().User.Include(u => u.UserRoleNavigation).FirstOrDefault(u => u.UserLogin == Login.Text
                                                                       && u.UserPassword == Password.Password); 
            if (user == null) { MessageBox.Show("Неверные данные!"); return; }
            App.CurrentUser = user;
            Functional f = new Functional();
            f.Show();
            this.Close();
        }
    }
}
