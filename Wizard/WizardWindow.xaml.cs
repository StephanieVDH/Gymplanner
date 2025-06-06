﻿using Gymplanner.CS;
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
using System.Windows.Shapes;

namespace Gymplanner.Wizard
{
    /// <summary>
    /// Interaction logic for WizardWindow.xaml
    /// </summary>
    public partial class WizardWindow : Window
    {
        public WizardWindow()
        {
            InitializeComponent();
        }

        public WizardWindow(int userId)
        {
            InitializeComponent();
            var vm = new WizardViewModel(userId);
            this.DataContext = vm;

            // Let the VM call back here when finished
            vm.CloseAction = () => this.Dispatcher.Invoke(this.Close);
        }

    }
}
