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

        protected void NotifyPropertyChanged(string v)
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
            LastPageButtonIsEnabled = true;
            PreviousPageButtonIsEnabled = false;
            FirstPageButtonIsEnabled = false;
            EventComboBoxIsEnabled = false;
            SportComboBoxIsEnabled = false;
            SelectedRPP = 10;
            PageNumber = 1;
            UpdateData();
        }

        private string _filtraNome;

        public string FiltraNome
        {
            get { return _filtraNome; }
            set { _filtraNome = value; UpdateAndReturnToTheFirstPage(); NotifyPropertyChanged("FiltraNome"); }
        }

        private List<string> _listaSessi;
        public List<string> ListaSessi
        {
            get { return _listaSessi; }
            set { _listaSessi = value; }
        }

        internal void PreviousPage()
        {
            PageNumber--;
            checkButton();
            UpdateData();
        }

        internal void FirstPage()
        {
            UpdateAndReturnToTheFirstPage();
        }

        internal void LastPage()
        {
            PageNumber = TotalPages;
            checkButton();
            UpdateData();
        }

        internal void NextPage()
        {
            
            PageNumber++;
            checkButton();
            UpdateData();
            
        }

        internal void checkButton()
        {
            if(PageNumber == 1)
            {
                PreviousPageButtonIsEnabled = false;
                FirstPageButtonIsEnabled = false;
            }
            else
            {
                PreviousPageButtonIsEnabled = true;
                FirstPageButtonIsEnabled = true;
            }

            if (PageNumber >= TotalPages)
            {
                if (TotalPages == 0)
                {       
                    PageNumber = 0;
                    PreviousPageButtonIsEnabled = false;
                    FirstPageButtonIsEnabled = false;
                }

                NextPageButtonIsEnabled = false;
                LastPageButtonIsEnabled = false;
                
            }
            else
            {
                NextPageButtonIsEnabled = true;
                LastPageButtonIsEnabled = true;
            }
                
        }

        private char? _selectedSex;
        public char? SelectedSex
        {
            get { return _selectedSex; }
            set { _selectedSex = value; UpdateAndReturnToTheFirstPage(); NotifyPropertyChanged("SelectedSex"); }
        }

        internal void ResetFiltri()
        {
            SelectedGames = null;
            SelectedMedal = null;
            SelectedSex = null;
            FiltraNome = null;
            EventComboBoxIsEnabled = false;
            SportComboBoxIsEnabled = false;
            SelectedRPP = 10;
            UpdateAndReturnToTheFirstPage();
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
                EventComboBoxIsEnabled = false;
                SportComboBoxIsEnabled = true;
                ListaSport = Partecipations.GetSports(SelectedGames);
                UpdateAndReturnToTheFirstPage();
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
                EventComboBoxIsEnabled = true;
                ListaEvent = Partecipations.GetEvents(SelectedGames,SelectedSport);
                UpdateAndReturnToTheFirstPage();
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
            set { _selectedEvent = value; UpdateAndReturnToTheFirstPage(); NotifyPropertyChanged("SelectedEvent"); }
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
            set { _selectedMedal = value; UpdateAndReturnToTheFirstPage(); NotifyPropertyChanged("SelectedMedal"); }
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
            set { _selectedRPP = value; UpdateAndReturnToTheFirstPage(); UpdateData(); NotifyPropertyChanged("SelectedRPP"); }
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

        private bool _lastPageButtonIsEnabled;
        public bool LastPageButtonIsEnabled
        {
            get { return _lastPageButtonIsEnabled; }
            set { _lastPageButtonIsEnabled = value; NotifyPropertyChanged("LastPageButtonIsEnabled"); }
        }

        private bool _previousPageButtonIsEnabled;
        public bool PreviousPageButtonIsEnabled

        {
            get { return _previousPageButtonIsEnabled; }
            set { _previousPageButtonIsEnabled = value; NotifyPropertyChanged("PreviousPageButtonIsEnabled"); }
        }

        private bool _firstPageButtonIsEnabled;
        public bool FirstPageButtonIsEnabled

        {
            get { return _firstPageButtonIsEnabled; }
            set { _firstPageButtonIsEnabled = value; NotifyPropertyChanged("FirstPageButtonIsEnabled"); }
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

        private bool _eventComboBoxIsEnabled;
        public bool EventComboBoxIsEnabled

        {
            get { return _eventComboBoxIsEnabled; }
            set { _eventComboBoxIsEnabled = value; NotifyPropertyChanged("EventComboBoxIsEnabled"); }
        }

        private bool _sportComboBoxIsEnabled;
        public bool SportComboBoxIsEnabled

        {
            get { return _sportComboBoxIsEnabled; }
            set { _sportComboBoxIsEnabled = value; NotifyPropertyChanged("SportComboBoxIsEnabled"); }
        }



        public void UpdateData()
        {
            ListaPartecipation = Partecipations.LoadDataGrid(PageNumber, SelectedRPP,FiltraNome,SelectedSex,SelectedGames,SelectedSport,SelectedEvent, SelectedMedal);
            TotalPages = Partecipations.GetTotalPages(SelectedRPP, FiltraNome, SelectedSex, SelectedGames, SelectedSport, SelectedEvent, SelectedMedal);
            checkButton();
            StringLabelPagina = Partecipations.StringLabelPagina(PageNumber, TotalPages);
        }

        public void UpdateAndReturnToTheFirstPage()
        {
            PageNumber = 1;
            UpdateData();
        }

        
    }
}
