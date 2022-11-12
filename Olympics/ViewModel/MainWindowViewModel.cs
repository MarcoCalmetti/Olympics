using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Olympics.Controllers;
using Olympics.Models;

namespace Olympics.ViewModel
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string v) // utilizziamo un metodo/funzione per semplificare il lavoro
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
        }
        public MainWindowViewModel()
        {
            _listaSessi = Partecipations.GetDistinctValueList("Sex");
            _listaGames = Partecipations.GetDistinctValueList("Games");
            _listaRPP = new List<int> { 10, 20, 50 };
            _listaMedal = Partecipations.GetMedal();
            NextPageButtonIsEnabled = true;
            SelectedRPP = 10;
            PageNumber = 1;
            UpdateData();
        }

        private string _filtraNome;

        public string FiltraNome
        {
            get { return _filtraNome; }
            set { _filtraNome = value; UpdateData(); NotifyPropertyChanged("FiltraNome"); }
        }

        private List<string> _listaSessi;
        public List<string> ListaSessi
        {
            get { return _listaSessi; }
            set { _listaSessi = value; }
        }

        internal void NextPage()
        {
            PageNumber++;
            UpdateData();
        }

        private char? _selectedSex;
        public char? SelectedSex
        {
            get { return _selectedSex; }
            set { _selectedSex = value; UpdateData(); NotifyPropertyChanged("SelectedSex"); }
        }

        internal void ResetFiltri()
        {
            SelectedGames = null;
            SelectedMedal = null;
            SelectedSex = null;
            FiltraNome = null;
            SelectedRPP = 10;
            PageNumber = 1;
            UpdateData();
        }

        private List<string> _listaGames;
        public List<string> ListaGames
        {
            get { return _listaGames; }
            set { _listaGames = value; }
        }

        private string _selectedGames;
        public string SelectedGames
        {
            get { return _selectedGames; }
            set { _selectedGames = value;
                SelectedSport = null;
                SelectedEvent = null;
                ListaSport = Partecipations.GetSports(SelectedGames);
                UpdateData();
                NotifyPropertyChanged("SelectedGames"); }
        }

        private List<string> _listaSport;
        public List<string> ListaSport
        {
            get { return _listaSport; }
            set { _listaSport = value; NotifyPropertyChanged("ListaSport"); }
        }

        private string _selectedSport;

        public string SelectedSport
        {
            get { return _selectedSport; }
            set { _selectedSport = value;
                SelectedEvent = null;
                ListaEvent = Partecipations.GetEvents(SelectedGames,SelectedSport);
                UpdateData();
                NotifyPropertyChanged("SelectedSport"); }
        }

        private List<string> _listaEvent;
        public List<string> ListaEvent
        {
            get { return _listaEvent; }
            set { _listaEvent = value; NotifyPropertyChanged("ListaEvent"); }
        }

        private string _selectedEvent;
        public string SelectedEvent
        {
            get { return _selectedEvent; }
            set { _selectedEvent = value; UpdateData(); NotifyPropertyChanged("SelectedEvent"); }
        }

        private List<string> _listaMedal;
        public List<string> ListaMedal
        {
            get { return _listaMedal; }
            set { _listaMedal = value; }
        }

        private string _selectedMedal;
        public string SelectedMedal
        {
            get { return _selectedMedal; }
            set { _selectedMedal = value; UpdateData(); NotifyPropertyChanged("SelectedMedal"); }
        }

        private List<Partecipation> _listaPartecipation;
        public List<Partecipation> ListaPartecipation
        {
            get { return _listaPartecipation; }
            set { _listaPartecipation = value; NotifyPropertyChanged("ListaPartecipation"); }
        }

        private List<int> _listaRPP;
        public List<int> ListaRPP
        {
            get { return _listaRPP; }
            set { _listaRPP = value; NotifyPropertyChanged("ListaRPP"); }
        }

        private int _selectedRPP;
        public int SelectedRPP
        {
            get { return _selectedRPP; }
            set { _selectedRPP = value; PageNumber = 1; UpdateData(); NotifyPropertyChanged("SelectedRPP"); }
        }

        private int? _pageNumber;
        public int? PageNumber
        {
            get { return _pageNumber; }
            set { _pageNumber = value; NotifyPropertyChanged("PageNumber"); }
        }

        private bool _nextPageButtonIsEnabled;
        public bool NextPageButtonIsEnabled
        {
            get { return _nextPageButtonIsEnabled; }
            set { _nextPageButtonIsEnabled = value; NotifyPropertyChanged("NextPageButtonIsEnabled"); }
        }
        private int _totalPages;
        public int TotalPages
        {
            get { return _totalPages; }
            set { _totalPages = value; NotifyPropertyChanged("TotalPages"); }
        }
        private string _stringLabelPagina;
        public string StringLabelPagina
        {
            get { return _stringLabelPagina; }
            set { _stringLabelPagina = value; NotifyPropertyChanged("StringLabelPagina"); }
        }

        public void UpdateData()
        {
            ListaPartecipation = Partecipations.LoadDataGrid(PageNumber, SelectedRPP,FiltraNome,SelectedSex,SelectedGames,SelectedSport,SelectedEvent, SelectedMedal);
            TotalPages = Partecipations.GetTotalPages(SelectedRPP);
            StringLabelPagina = Partecipations.StringLabelPagina(PageNumber, TotalPages);
        }
    }
}
