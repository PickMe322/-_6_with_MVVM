using System;
using System.ComponentModel;
using лаба_6_норм.Models;

namespace лаба_6_норм.Models 
{
    public class Movie : INotifyPropertyChanged
    {
        private string _name;
        private Genre _genre;
        private int _publishYear;
        private double _rate;
        private Director _director;

        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(nameof(Name)); }
        }
        public Genre Genre
        {
            get => _genre;
            set { _genre = value; OnPropertyChanged(nameof(Genre)); }
        }
        public int PublishYear
        {
            get => _publishYear;
            set { _publishYear = value; OnPropertyChanged(nameof(PublishYear)); }
        }
        public double Rate
        {
            get => _rate;
            set { _rate = value; OnPropertyChanged(nameof(Rate)); }
        }
        public Director Director
        {
            get => _director;
            set { _director = value; OnPropertyChanged(nameof(Director)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}