using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using лаба_6_норм.Models;

namespace лаба_6_норм.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Movie> Movies { get; } = new ObservableCollection<Movie>();
        public ObservableCollection<object> DirectorsSummary { get; } = new ObservableCollection<object>();
        public ObservableCollection<Genre> Genres { get; } = new ObservableCollection<Genre>(Enum.GetValues(typeof(Genre)).Cast<Genre>());
        private ObservableCollection<Movie> _allMovies = new ObservableCollection<Movie>();

        private string _filmName;
        public string FilmName
        {
            get => _filmName;
            set { _filmName = value; OnPropertyChanged(nameof(FilmName)); }
        }

        private int _publishYear;
        public int PublishYear
        {
            get => _publishYear;
            set { _publishYear = value; OnPropertyChanged(nameof(PublishYear)); }
        }

        private string _directorName;
        public string DirectorName
        {
            get => _directorName;
            set { _directorName = value; OnPropertyChanged(nameof(DirectorName)); }
        }

        private double _rate;
        public double Rate
        {
            get => _rate;
            set { _rate = value; OnPropertyChanged(nameof(Rate)); }
        }

        private Genre _selectedGenre;
        public Genre SelectedGenre
        {
            get => _selectedGenre;
            set { _selectedGenre = value; OnPropertyChanged(nameof(SelectedGenre)); }
        }


        private Genre _filterGenre;
        public Genre FilterGenre
        {
            get => _filterGenre;
            set { _filterGenre = value; OnPropertyChanged(nameof(FilterGenre)); }
        }

        private int _relStart;
        public int RelStart
        {
            get => _relStart;
            set { _relStart = value; OnPropertyChanged(nameof(RelStart)); }
        }

        private int _relEnd;
        public int RelEnd
        {
            get => _relEnd;
            set { _relEnd = value; OnPropertyChanged(nameof(RelEnd)); }
        }

        private Movie _selectedMovie = new Movie();
        public Movie SelectedMovie
        {
            get => _selectedMovie;
            set { _selectedMovie = value; OnPropertyChanged(nameof(SelectedMovie)); }
        }

        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand ShowAllCommand { get; }
        public ICommand FilterGenreCommand { get; }
        public ICommand FilterReleaseCommand { get; }
        public ICommand SortAscCommand { get; }
        public ICommand SortDescCommand { get; }
        public ICommand ShowDirectorsCommand { get; }

        public MainWindowViewModel()
        {
            AddCommand = new RelayCommand(_ => AddMovie());
            DeleteCommand = new RelayCommand(_ => DeleteMovie());
            ShowAllCommand = new RelayCommand(_ => ShowAll());
            FilterGenreCommand = new RelayCommand(_ => FilterByGenre());
            FilterReleaseCommand = new RelayCommand(_ => FilterByRelease());
            SortAscCommand = new RelayCommand(_ => SortByRate(true));
            SortDescCommand = new RelayCommand(_ => SortByRate(false));
            ShowDirectorsCommand = new RelayCommand(_ => ShowDirectors());
        }

        private void AddMovie()
        {
            var exists = _allMovies.FirstOrDefault(m =>
                m.Name == FilmName &&
                m.PublishYear == PublishYear &&
                m.Rate == Rate &&
                m.Genre == SelectedGenre &&
                m.Director.Name == DirectorName);

            if (exists == null)
            {
                var newMovie = new Movie
                {
                    Name = FilmName,
                    Genre = SelectedGenre,
                    PublishYear = PublishYear,
                    Rate = Rate,
                    Director = new Director
                    {
                        Name = DirectorName,
                        StartActivityYear = PublishYear,
                        EndActivityYear = PublishYear
                    }
                };

                _allMovies.Add(newMovie);
            }
        }


        private void DeleteMovie()
        {
            if (SelectedMovie == null) return;

            var movie = _allMovies.FirstOrDefault(m =>
                m.Name == SelectedMovie.Name &&
                m.PublishYear == SelectedMovie.PublishYear &&
                m.Rate == SelectedMovie.Rate &&
                m.Genre == SelectedMovie.Genre &&
                m.Director.Name == SelectedMovie.Director.Name);

            if (movie != null)
            {
                _allMovies.Remove(movie);
            }
        }

        private void ShowAll()
        {
            Movies.Clear();
            foreach (var m in _allMovies)
                Movies.Add(m);
        }

        private void FilterByGenre()
        {
            var filtered = _allMovies.Where(m => m.Genre == FilterGenre).ToList();
            Movies.Clear();
            foreach (var m in filtered)
                Movies.Add(m);
        }

        private void FilterByRelease()
        {
            var filtered = _allMovies
                .Where(m => m.PublishYear >= RelStart && m.PublishYear <= RelEnd)
                .ToList();

            Movies.Clear();
            foreach (var m in filtered)
                Movies.Add(m);
        }

        private void SortByRate(bool asc)
        {
            var sorted = (asc ? Movies.OrderBy(m => m.Rate) : Movies.OrderByDescending(m => m.Rate)).ToList();
            Movies.Clear();
            foreach (var m in sorted)
                Movies.Add(m);
        }

        private void ShowDirectors()
        {
            DirectorsSummary.Clear();
            var stats = Movies.GroupBy(m => m.Director.Name)
                .Select(g => new
                {
                    Name = g.Key,
                    FilmCount = g.Count(),
                    AverageRate = g.Average(m => m.Rate),
                    StartActivityYear = g.Min(m => m.PublishYear),
                    EndActivityYear = g.Max(m => m.PublishYear)
                });
            foreach (var s in stats) DirectorsSummary.Add(s);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string prop) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
