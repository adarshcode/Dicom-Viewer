using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp3
{
    public partial class ExceptionWindow : Window
    {
        public string ExceptionDetails { get; set; }

        public ExceptionWindow(Exception exception)
        {
            InitializeComponent();
            ExceptionDetails = exception.ToString();
            DataContext = this;
        }
    }
}
