using System;
using System.Threading;
using System.Windows.Input;
using Windows.UI;
using Windows.UI.Xaml.Media;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using TourDuCavalierLib;

namespace TourDuCavalierUIApp.ViewModel
{
    public class Case : ViewModelBase
    {
        #region Static Fields

        private static readonly Brush blanc = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));

        private static readonly Brush noir = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));

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
            get { return this.avancement; }
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
            get { return 100; }
        }

        public int LargeurCase
        {
            get { return 100; }
        }

        public int HauteurCase
        {
            get { return 100; }
        }

        #endregion

        #region Methods

        private async void DoCaseClick()
        {
            //Création du jeton d'annulation
            //Lorsque qu'il est dans l'état cancel, il faut en créer un nouveau
            cancel = new CancellationTokenSource();
            progress = new Progress<int>(v => Avancement = v);

            TourDuCavalier tc = new TourDuCavalier(largeur, longueur, x, y, cancel.Token, progress);
            string result = await tc.Resoudre().ContinueWith(() =>
            {
                string log = "";
            });

            //MessageBox.Show(string.Format("Resultat : {0}", result));
        }

        #endregion
    }
}