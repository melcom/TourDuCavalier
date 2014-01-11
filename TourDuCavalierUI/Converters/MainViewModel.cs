using System.Collections.ObjectModel;
using System.Windows.Input;

using GalaSoft.MvvmLight;

namespace TourDuCavalierUI.ViewModel
{
    /// <summary>
    ///     This class contains properties that the main View can data bind to.
    ///     <para>
    ///         Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    ///     </para>
    ///     <para>
    ///         You can also use Blend to data bind with the tool's support.
    ///     </para>
    ///     <para>
    ///         See http://www.galasoft.ch/mvvm
    ///     </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        #region Fields

        private ObservableCollection<Case> echiquier;

        private int largeur;

        private int longueur;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}

            largeur = 5;
            longueur = 5;

            Echiquier = new ObservableCollection<Case>();

            RemplirEchiquier();
        }

        #endregion

        #region Public Properties

        public ICommand DoAnnuler { get; private set; }

        public ObservableCollection<Case> Echiquier
        {
            get
            {
                return echiquier;
            }
            set
            {
                RaisePropertyChanged("Echiquier");
                echiquier = value;
            }
        }

        public int Largeur
        {
            get
            {
                return largeur;
            }
            set
            {
                RaisePropertyChanged("Largeur");
                largeur = value;
                RemplirEchiquier();
            }
        }

        public int Longueur
        {
            get
            {
                return longueur;
            }
            set
            {
                RaisePropertyChanged("Longueur");
                longueur = value;
                RemplirEchiquier();
            }
        }

        #endregion

        #region Methods

        private void RemplirEchiquier()
        {
            Echiquier.Clear();
            for (int i = 0; i < longueur; i++)
            {
                for (int j = 0; j < largeur; j++)
                {
                    Echiquier.Add(new Case(i % 2 == 0 ? j % 2 == 0 : j % 2 != 0, j, i, longueur, largeur));
                }
            }
        }

        #endregion
    }
}