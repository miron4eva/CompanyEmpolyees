﻿using Entities.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ComEmp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Rep repository = new Rep();
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Login_Click(object sender, RoutedEventArgs e)
        {
            var login = new UserForAuthenticationDto
            {
                UserName = UsernameTextBox.Text,
                Password = PasswordTextBox.Text
            };
            HttpResponseMessage response = await repository.PostAuthenticationLogin(login);
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Успех");
            }
            else
            {
                MessageBox.Show(response.StatusCode.ToString());
            }
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
