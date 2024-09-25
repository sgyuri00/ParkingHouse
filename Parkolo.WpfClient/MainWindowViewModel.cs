using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Parkolo.Data;
using Parkolo.Logic;
using Parkolo.WpfClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Parkolo.WpfClient
{
    class MainWindowViewModel : ObservableRecipient
    {
        ICarLogic carlogic;
        IParkolasLogic parkolaslogic;

        public MainWindowViewModel(ICarLogic carlogic, IParkolasLogic parkolaslogic)
        {
            this.carlogic = carlogic;
            this.parkolaslogic = parkolaslogic;
        }


        //restcollections 
        public RestCollection<Auto> Cars { get; set; }
        public RestCollection<Szemely> People { get; set; }
        public RestCollection<ParkoloSpots> Spots { get; set; }
        public RestCollection<Parkolas> Parkolas { get; set; }
        //non cruds
        public RestCollection<SumCost> SumCosts { get; set; }
        public RestCollection<MalePeople> Malepeople { get; set; }
        public RestCollection<UsedParkingSpots> UsedSpots { get; set; }
        public RestCollection<Over8Hours> Over8 { get; set; }
        public RestCollection<PaidMoreThan2000> MoreThen2000 { get; set; }


        private Auto selectedCar;
        private Szemely selectedPerson;
        private ParkoloSpots selectedSpot;



        public Auto SelectedCar
        {
            get { return selectedCar; }
            set
            {
                
                if (value != null)
                {
                    selectedCar = new Auto()
                    {
                        Rendszam = value.Rendszam,
                        Marka = value.Marka,
                        GyartasiEv = value.GyartasiEv,
                        Uzemanyag = value.Uzemanyag,
                    };
                    OnPropertyChanged();
                    (DeleteAutoCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }


        public Szemely SelectedSzemely
        {
            get { return selectedPerson; }
            set
            {
                if (value != null)
                {
                    selectedPerson = new Szemely()
                    {
                        SzemelyId = value.SzemelyId, //ez kell DELETE miatt
                        Nev = value.Nev,
                        Nem = value.Nem,
                        SzuletesiIdo = value.SzuletesiIdo,
                        //Parkolas = value.Parkolas,//lehet nem kell
                    };
                    OnPropertyChanged();
                    (DeletePersonCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }


        public ParkoloSpots SelectedSpot
        {
            get { return selectedSpot; }
            set
            {
                if (value != null)
                {
                    selectedSpot = new ParkoloSpots()
                    {
                        ParkolohelySzam = value.ParkolohelySzam, //ez kell DELETE miatt
                        Meret = value.Meret,
                        ElektromosE = value.ElektromosE,
                        //Parkolas = value.Parkolas,
                    };
                    OnPropertyChanged();
                    (DeleteSpotCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }


        //commands for cars
        public ICommand CreateAutoCommand { get; set; }
        public ICommand UpdateAutoCommand { get; set; }
        public ICommand DeleteAutoCommand { get; set; }

        //commands for spots
        public ICommand CreateSpotCommand { get; set; }
        public ICommand UpdateSpotCommand { get; set; }
        public ICommand DeleteSpotCommand { get; set; }

        //commands for people
        public ICommand CreatePersonCommand { get; set; }
        public ICommand UpdatePersonCommand { get; set; }
        public ICommand DeletePersonCommand { get; set; }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }


        public MainWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                Cars = new RestCollection<Auto>("http://localhost:45793/", "Auto", "hub");
                Spots = new RestCollection<ParkoloSpots>("http://localhost:45793/", "ParkoloSpots", "hub");
                People = new RestCollection<Szemely>("http://localhost:45793/", "Szemely", "hub");
                Parkolas = new RestCollection<Parkolas>("http://localhost:45793/", "Parkolas", "hub");

                SumCosts = new RestCollection<SumCost>("http://localhost:45793/", "Stat/SumCostByCars", "hub");
                Malepeople = new RestCollection<MalePeople>("http://localhost:45793/", "Stat/MalePeople", "hub");
                Over8 = new RestCollection<Over8Hours>("http://localhost:45793/", "Stat/ParkingOver8Hours", "hub");
                UsedSpots = new RestCollection<UsedParkingSpots>("http://localhost:45793/", "Stat/UsedElectricParkingSpots", "hub");
                MoreThen2000 = new RestCollection<PaidMoreThan2000>("http://localhost:45793/", "Stat/PeoplePaidMoreThan2000", "hub");

                CreateAutoCommand = new RelayCommand(() =>
                {                    
                    Cars.Add(selectedCar);                    
                });

                UpdateAutoCommand = new RelayCommand(() =>
                {
                    Cars.Update(SelectedCar);
                });

                DeleteAutoCommand = new RelayCommand(() =>
                {
                    Cars.DeleteString(selectedCar.Rendszam);
                },
                () =>
                {
                    return SelectedCar != null;
                });
                SelectedCar = new Auto();


                //commands for people
                CreatePersonCommand = new RelayCommand(() =>
                {
                    People.Add(new Szemely()
                    {
                        SzemelyId = SelectedSzemely.SzemelyId,
                        Nev = SelectedSzemely.Nev,
                        Nem = SelectedSzemely.Nem,
                        SzuletesiIdo = SelectedSzemely.SzuletesiIdo,
                    });
                });

                UpdatePersonCommand = new RelayCommand(() =>
                {
                    People.Update(SelectedSzemely);
                });

                DeletePersonCommand = new RelayCommand(() =>
                {
                    People.Delete(SelectedSzemely.SzemelyId);
                },
                () =>
                {
                    return SelectedSzemely != null;
                });
                SelectedSzemely = new Szemely();


                //commands for spots
                CreateSpotCommand = new RelayCommand(() =>
                {
                    Spots.Add(SelectedSpot);
                    //Spots.Add(new ParkoloSpots()
                    //{
                    //    ParkolohelySzam = selectedSpot.ParkolohelySzam,
                    //    Meret = selectedSpot.Meret,
                    //    ElektromosE = selectedSpot.ElektromosE,
                    //    Parkolas = selectedSpot.Parkolas//lehet nnem kell
                    //});
                });

                UpdateSpotCommand = new RelayCommand(() =>
                {
                    Spots.Update(selectedSpot);
                });

                DeleteSpotCommand = new RelayCommand(() =>
                {
                    Spots.Delete(selectedSpot.ParkolohelySzam);
                },
                () =>
                {
                    return selectedSpot != null;
                });
                selectedSpot = new ParkoloSpots();

            }
        }
    }
}
