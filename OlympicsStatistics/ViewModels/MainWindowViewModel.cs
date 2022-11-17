using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OlympicsStatistics.Models;
using OlympicsStatistics.Controllers;
using System.Configuration;

namespace OlympicsStatistics.ViewModels
{
    class MainWindowViewModel : BaseViewModel
    {
        public MainWindowViewModel()
        {
            InitializeData();//nei costruttori await non può essere utilizzato
        }

        private async Task LoadData()
        {
            await LoadData(SelectedNoc);
        }

        private async Task LoadData(string noc)
        {
            IsDataLoaded = false;
            Athletes = await OlympicsController.getAthletes(noc, OnlyMedalists, page, pageSize);
            IsDataLoaded = true;
        }

        private async Task InitializeData()
        {
            string noc = ConfigurationManager.AppSettings["defaultNOC"];
            var t = OlympicsController.getAllNocs();
            LoadData(noc); // sono in parallelo
            Nocs = await t; // sono in parallelo
            _selectedNoc = noc; //in questo modo non fa eseguire il LoadData() all'interno del set
            NotifyPropertyChanged("SelectedNoc");
        }

        public async Task AddNew()
        {
            await OlympicsController.AddNew();
        }


        #region Binding Properties
        private List<string> _nocs;
        public List<string> Nocs
        {
            get { return _nocs; }
            set { _nocs = value; NotifyPropertyChanged("Nocs"); }
        }

        private string _selectedNoc;
        public string SelectedNoc
        {
            get { return _selectedNoc; }
            set { _selectedNoc = value; LoadData(); NotifyPropertyChanged("SelectedNoc"); }
        }

        //??????????
        private List<AthletesWithMedals> _athletes;
        public List<AthletesWithMedals> Athletes
        {
            get { return _athletes; }
            set { _athletes = value; NotifyPropertyChanged("Athletes"); }
        }

        private bool _onlyMedalists;
        public bool OnlyMedalists
        {
            get { return _onlyMedalists; }
            set { _onlyMedalists = value; LoadData(); NotifyPropertyChanged("OnlyMedalists"); }
        }

        private bool _isDataLoaded;

        public bool IsDataLoaded
        {
            get { return _isDataLoaded; }
            set { _isDataLoaded = value; NotifyPropertyChanged("IsDataLoaded"); }
        }


        #endregion

        #region EventHandling
        public void NextPage()
        {
            page++;
            LoadData();
        }

        public void PreviousPage()
        {
            page--;
            if (page <= 0) page = 1;
            LoadData();
        }
        #endregion

        #region Private fields
        private int page = 1;
        private int pageSize = 50;
        #endregion
    }
}
