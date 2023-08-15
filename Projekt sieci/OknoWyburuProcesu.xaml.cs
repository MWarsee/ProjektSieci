using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace Projekt_sieci
{
    /// <summary>
    /// Logika interakcji dla klasy OknoWyburuProcesu.xaml
    /// </summary>
    public partial class OknoWyburuProcesu : Window
    {
        public List<Process> processes;
        public OknoWyburuProcesu()
        {
            InitializeComponent();
            var processestemp = Process.GetProcesses();
            processes= processestemp.OrderBy(ro=>ro.ProcessName).ToList();
            cbProcesy.ItemsSource = processes;
            cbProcesy.DisplayMemberPath = "ProcessName";
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        
        
    }
}
