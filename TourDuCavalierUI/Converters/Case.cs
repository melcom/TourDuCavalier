using System;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

using TourDuCavalierLib;

namespace TourDuCavalierUI.ViewModel
{
    public class Case : ViewModelBase
    {
        #region Static Fields

        private static readonly Brush blanc = new SolidColorBrush(Color.FromRgb(255, 255, 255));

        private static readonly Brush noir = new SolidColorBrush(Color.FromRgb(0, 0, 0));

        #endregion

        #region Fields

        private readonly int largeur;

        private readonly int longueur;

        private readonly Action<int, int> onClick;

        private readonly int x;

        private readonly int y;

        private int avancement;

        private CancellationTokenSource cancel;

        private IProgress<int> progress;

        #endregion

        #region Constructors and Destructors

        public Case(bool isBlanc, int x, int y, int longueur, int largeur)
        {
            this.x = x;
            this.y = y;
            this.longueur = longueur;
            this.largeur = largeur;

            Couleur = isBlanc ? blanc : noir;
            CaseClick = new RelayCommand(DoCaseClick);
        }

        #endregion

        #region Public Properties

        public int Avancement
        {
            get
            {
                return avancement;
            }
            set
            {
                RaisePropertyChanged("Avancement");
                this.avancement = value;
            }
        }

        public ICommand CaseClick { get; private set; }

        public Brush Couleur { get; private set; }

        public int ProgressMax
        {
            get
            {
                return 100;
            }
        }

        #endregion

        #region Methods

        private async void DoCaseClick()
        {
            //Cr�ation du jeton d'annulation
            //Lorsque qu'il est dans l'�tat cancel, il faut en cr�er un nouveau
            cancel = new CancellationTokenSource();
            progress = new Progress<int>(v => Avancement = v);

            TourDuCavalier tc = new TourDuCavalier(largeur, longueur, x, y, cancel.Token, progress);
            string result = await tc.Resoudre();

            MessageBox.Show(string.Format("Resultat : {0}", result));
        }

        #endregion
    }
}