using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Windows.Controls;
using System.Windows.Threading;
using SharpPcap;
using PacketDotNet;

namespace Projekt_sieci
{
    /// <summary>
    /// Logika interakcji dla klasy ObserwowanieAplikacji.xaml
    /// </summary>
    public partial class ObserwowanieAplikacji : UserControl
    {
        ObservableCollection<InformacjeSieciowe> informacjeDownload = new();
        ObservableCollection<InformacjeSieciowe> informacjeUpload = new();
        PlotModel plotModel = new();
        LineSeries seriesDownload = new();
        LineSeries seriesUpload = new();
        Process Process { get; set; }
        NetworkInterface NetworkInterface { get; set; }

        DispatcherTimer timer = new DispatcherTimer();

        public ObserwowanieAplikacji()
        {
            InitializeComponent();
            TworzenieWykresu();

        }

        void TworzenieWykresu()
        {
            var linearAxis = new LinearAxis { Position = AxisPosition.Left };
            var dateTimeAxis = new DateTimeAxis { Position = AxisPosition.Bottom };

            plotModel.Axes.Add(linearAxis);
            plotModel.Axes.Add(dateTimeAxis);

            seriesDownload.XAxisKey = dateTimeAxis.Key;
            seriesDownload.YAxisKey = linearAxis.Key;

            seriesDownload.ItemsSource = informacjeDownload;
            seriesDownload.Title = "Download";

            seriesUpload.XAxisKey = dateTimeAxis.Key;
            seriesUpload.YAxisKey = linearAxis.Key;

            seriesUpload.ItemsSource = informacjeUpload;
            seriesUpload.Title = "Upload";
        }

        void ResetWykresu()
        {
            plotModel.Series.Clear();
            plotModel.Series.Add(seriesUpload);
            plotModel.Series.Add(seriesDownload);

            MaxDonload.Text = informacjeDownload.Max(item => item.Dane).ToString();
            MinDonload.Text = informacjeDownload.Min(item => item.Dane).ToString();
            AvgDonload.Text = informacjeDownload.Average(item => item.Dane).ToString();

            MaxUpload.Text = informacjeUpload.Max(item => item.Dane).ToString();
            MinUpload.Text = informacjeUpload.Min(item => item.Dane).ToString();
            AvgUpload.Text = informacjeUpload.Average(item => item.Dane).ToString();

            Wykres.Model = plotModel;
        }

        private void WybierzApp_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            OknoWyburuProcesu oknoWyburuProcesu = new OknoWyburuProcesu();

            oknoWyburuProcesu.ShowDialog();

            int wybrany = oknoWyburuProcesu.cbProcesy.SelectedIndex;
            if (wybrany < 0) { return; }
            WybierzApp.Content = oknoWyburuProcesu.processes[wybrany].ProcessName;
            Process = oknoWyburuProcesu.processes[wybrany];

            informacjeDownload.Clear();
            informacjeUpload.Clear();
        }
        

        private void Start_Click(object sender, System.Windows.RoutedEventArgs e)
        {

            Start.Content = "Stop";
            Start.Click -= Start_Click;
            Start.Click += Stop_Click;
            TworzenieWykresu();
            //Odpalanie Timeru (pobieranie informacji sieciowych)
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }
        private void Stop_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Start.Content = "Start";
            Start.Click += Start_Click;
            Start.Click -= Stop_Click;
            timer.Stop();

        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (Process != null)
            {
                if (NetworkInterface != null)
                {
                    IPv4InterfaceStatistics stats = NetworkInterface.GetIPv4Statistics();

                    // Pobierz ilość danych odebranych i wysłanych przez interfejs sieciowy
                    long bytesReceived = stats.BytesReceived;
                    long bytesSent = stats.BytesSent;

                    // Dodaj dane do kolekcji
                    informacjeDownload.Add(new InformacjeSieciowe { Dane = bytesReceived, Date = DateTime.Now });
                    informacjeUpload.Add(new InformacjeSieciowe { Dane = bytesSent, Date = DateTime.Now });

                    // Odśwież wykres
                    ResetWykresu();
                }
            }
        }

    }

}
