using System.Collections.ObjectModel;

using GalaSoft.MvvmLight;

namespace TourDuCavalierLib.ViewModel
{
    /// <summary>
    ///     Ligne de cases
    /// </summary>
    public class Line : ViewModelBase
    {
        #region Constructors and Destructors

        public Line()
        {
            Cases = new ObservableCollection<Case>();
        }

        #endregion

        #region Public Properties

        public ObservableCollection<Case> Cases { get; set; }

        #endregion
    }
}