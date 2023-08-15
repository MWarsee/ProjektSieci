using OxyPlot;
using OxyPlot.Axes;
using System;
using System.ComponentModel;

namespace Projekt_sieci
{
    public class InformacjeSieciowe: INotifyPropertyChanged , IDataPointProvider
    {
        DateTime date;
        double dane;
        public DateTime Date {
            get { return date; }
            set 
            { 
                date = value;
                OnPropertyChanged(nameof(date));        
            } }
        
        public double Dane
        {
            get { return dane; }
            set
            {
                dane = value;
                OnPropertyChanged(nameof(dane));
            }
        }
        public InformacjeSieciowe(double dane) 
        {
            this.Date = DateTime.Now;
            this.dane = dane;
        }
        public InformacjeSieciowe() { }
        public (DateTime , double) getInformacje()
        {
            return (date, dane);
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public DataPoint GetDataPoint()
        {
            return new DataPoint(DateTimeAxis.ToDouble(date), dane);
        }
    }
}
