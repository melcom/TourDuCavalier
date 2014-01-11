using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace TourDuCavalierLib.ViewModel
{
    /// <summary>
    ///     Case de l'échiquier
    /// </summary>
    public class Case : ViewModelBase
    {
        #region Static Fields

        private static readonly Color Blanc = new Color(255, 255, 255);

        private static readonly Color Noir = new Color(0, 0, 0);

        #endregion

        #region Fields

        private readonly Func<CancellationTokenSource> cancel;

        private readonly int height;

        private readonly int width;

        private readonly int x;

        private readonly int y;

        private int computeProgress;

        private IProgress<int> progress;

        #endregion

        #region Constructors and Destructors

        public Case(bool isBlanc, int x, int y, int height, int width, Func<CancellationTokenSource> cancel)
        {
            this.x = x;
            this.y = y;
            this.height = height;
            this.width = width;
            this.cancel = cancel;

            Color = isBlanc ? Blanc : Noir;
            CaseClick = new RelayCommand(DoCaseClick);
        }

        #endregion

        #region Public Properties

        public ICommand CaseClick { get; private set; }

        public int CaseHeight
        {
            get { return 100; }
        }

        public int CaseWidth
        {
            get { return 100; }
        }

        public Color Color { get; private set; }

        public int ComputeProgress
        {
            get { return computeProgress; }
            set
            {
                computeProgress = value;
                RaisePropertyChanged();
            }
        }

        public int ProgressMax
        {
            get { return 100; }
        }

        #endregion

        #region Methods

        private async void DoCaseClick()
        {
            await InnerCaseClick();
        }

        public async Task InnerCaseClick()
        {
            //Obtention du jeton d'annulation
            CancellationTokenSource c = cancel();

            progress = new Progress<int>(v => ComputeProgress = v);

            TourDuCavalier tc = new TourDuCavalier(width, height, x, y, c.Token, progress);

            await tc.Solve().ContinueWith(t =>
            {
                Debug.WriteLine("End task " + t.Id);

                Messenger.Default.Send(c.Token.IsCancellationRequested
                    ? "Tâche abandonnée"
                    : String.Format("Tâche terminée en {0} ms", (int) t.Result.TotalMilliseconds));
            }, TaskContinuationOptions.ExecuteSynchronously);
        }

        #endregion
    }
}