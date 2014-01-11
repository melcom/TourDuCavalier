using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;

namespace TourDuCavalierUIApp.ViewModel
{
    public class Ligne : ViewModelBase
    {
        public Ligne()
        {
            Colonnes = new ObservableCollection<Case>();
        }

        public ObservableCollection<Case> Colonnes { get; set; }
    }
}