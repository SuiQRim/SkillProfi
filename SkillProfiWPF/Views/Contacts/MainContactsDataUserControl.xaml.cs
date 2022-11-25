﻿using SkillProfiWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace SkillProfiWPF.Views.Contacts
{
    /// <summary>
    /// Логика взаимодействия для MainContactsDataUserControl.xaml
    /// </summary>
    public partial class MainContactsDataUserControl : UserControl
    {
        public MainContactsDataUserControl(Func<bool> getLoginStatus)
        {
            InitializeComponent();
            DataContext = new MainContactsViewModel(getLoginStatus);
        }
    }
}
