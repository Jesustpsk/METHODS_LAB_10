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

namespace METHODS_LAB_10
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            TBInput.Text = "y' + xy = 0.5 * (x - 1) * e^x * y^2" +
                           "\ny(0) = 2" +
                           "\na = 0" +
                           "\nb = 2";
        }

        private void BStart_OnClick(object sender, RoutedEventArgs e)
        {
            NumericMethods.RungeKutta(TBRKStep, WpfPlot);
            NumericMethods.Euler(TBEStep, WpfPlot);
            NumericMethods.Exact(TBMaxDevRK, TBMaxDevE, WpfPlot, dataGrid);
        }
    }
}