using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace лаба_6_норм.Models
{
    public class Director : INotifyPropertyChanged
    {
        private string _name;
        private int _startActivityYear;
        private int _endActivityYear;
        private List<double> _rates = new List<double>();

        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(nameof(Name)); }
        }
        public int StartActivityYear
        {
            get => _startActivityYear;
            set { _startActivityYear = value; OnPropertyChanged(nameof(StartActivityYear)); }
        }
        public int EndActivityYear
        {
            get => _endActivityYear;
            set { _endActivityYear = value; OnPropertyChanged(nameof(EndActivityYear)); }
        }
        public int FilmCount => _rates.Count;
        public double AverageRate => _rates.Any() ? _rates.Average() : 0;

        public void AddRate(double rate)
        {
            _rates.Add(rate);
            OnPropertyChanged(nameof(FilmCount));
            OnPropertyChanged(nameof(AverageRate));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}