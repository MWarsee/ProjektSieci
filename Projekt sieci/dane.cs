using OxyPlot;
using OxyPlot.Axes;
using System;
using System.ComponentModel;

namespace Projekt_sieci
{
    public class InformacjeSieciowe: INotifyPropertyChanged , IDataPointProvider
    {
        DateTime date;
        double download;
        double upload;
        public DateTime Date {
            get { return date; }
            set 
            { 
                date = value;
                OnPropertyChanged(nameof(date));        
            } }
        public double Download 
        {
            get { return download; }
            set
            {
                download = value;
                OnPropertyChanged(nameof(download));
            }
        }
        public double Upload 
        {
            get { return upload; }
            set
            {
                upload = value;
                OnPropertyChanged(nameof(upload));
            }
        }
        public InformacjeSieciowe(double download,double upload) 
        {
            this.Date = DateTime.Now;
            this.Download = download;
            this.Upload = upload;
        }
        public InformacjeSieciowe() { }
        public (DateTime , double, double ) getInformacje()
        {
            return (date, download, upload);
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public DataPoint GetDataPoint()
        {
            return new DataPoint(DateTimeAxis.ToDouble(date), Download);
        }
        public DataPoint GetDataPoint2()
        {
            return new DataPoint(DateTimeAxis.ToDouble(date), Upload);
        }
    }
}
