using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp3
{
    public partial class WarningWindow : Window
    {
        public string WarningDetails { get; set; }

        public WarningWindow(String warning)
        {
            InitializeComponent();

            WarningDetails = warning.ToUpper();
            DataContext = this;
        }
    }
}
