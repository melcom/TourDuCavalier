using System.Collections.ObjectModel;
using System.Windows.Input;
using GalaSoft.MvvmLight;

namespace TourDuCavalierUIApp.ViewModel
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

        private int largeur;

        private int longueur;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            largeur = 5;
            longueur = 5;

            Lignes = new ObservableCollection<Ligne>();

            RemplirEchiquier();
        }

        #endregion

        #region Public Properties

        public ICommand DoAnnuler { get; private set; }

        public ObservableCollection<Ligne> Lignes { get; set; }

        public int Largeur
        {
            get { return largeur; }
            set
            {
                largeur = value;
                RaisePropertyChanged("Largeur");
                RemplirEchiquier();
            }
        }

        public int Longueur
        {
            get { return longueur; }
            set
            {
                longueur = value;
                RaisePropertyChanged("Longueur");
                RemplirEchiquier();
            }
        }

        #endregion

        #region Methods

        private void RemplirEchiquier()
        {
            Lignes.Clear();
            for (int i = 0; i < longueur; i++)
            {
                Ligne l = new Ligne();

                for (int j = 0; j < largeur; j++)
                {
                    l.Colonnes.Add(new Case(i%2 == 0 ? j%2 == 0 : j%2 != 0, j, i, longueur, largeur));
                }

                Lignes.Add(l);
            }

            //Notification
            RaisePropertyChanged("Lignes");
        }

        #endregion
    }
}