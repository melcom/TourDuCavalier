using Windows.UI;
using Windows.UI.Xaml.Media;
using GalaSoft.MvvmLight;

namespace TourDuCavalierUIApp.ViewModel
{
    public class Colonne : ViewModelBase
    {
        #region Static Fields

        private static readonly Brush blanc = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));

        private static readonly Brush noir = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));

        #endregion

        public Colonne(bool isBlanc)
        {
            Couleur = isBlanc ? blanc : noir;
        }

        public Brush Couleur { get; set; }

        public int ProgressMax { get; set; }

        public int Avancement { get; set; }

        public int LargeurCase
        {
            get { return 100; }
        }

        public int HauteurCase
        {
            get { return 100; }
        }
    }
}