using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace TourDuCavalierLib.ViewModel
{
    /// <summary>
    ///     Main page view model
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        #region Fields

        private CancellationTokenSource cancel;

        private int length;
        private int width;
        private readonly SynchronizationContext sc;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            //On capture le SynchronizationContext courant
            sc = SynchronizationContext.Current;
            width = 5;
            length = 5;

            Lines = new ObservableCollection<Line>();

            //Gestion du log
            Log = new ObservableCollection<string>();
            //Log.CollectionChanged += (sender, args) => RaisePropertyChanged(() => Log);
            Messenger.Default.Register<string>(this, s =>
            {

                sc.Send((p)=>Log.Add(s),s);
            });
            AnnulerClick = new RelayCommand(() => cancel.Cancel());
            CleanLogClick = new RelayCommand(() => Log.Clear());

            AllCasesClick = new RelayCommand(DoAllCasesClick);

            FillChessboard();

            
        }

        #endregion

        #region Public Properties

        public ICommand AllCasesClick { get; private set; }
        public ICommand AnnulerClick { get; private set; }
        public ICommand CleanLogClick { get; private set; }

        public int Width
        {
            get { return width; }
            set
            {
                width = value;
                FillChessboard();
            }
        }

        public ObservableCollection<Line> Lines { get; set; }

        public ObservableCollection<string> Log { get; set; }

        public int Length
        {
            get { return length; }
            set
            {
                length = value;
                FillChessboard();
            }
        }

        #endregion

        #region Methods

        private async void DoAllCasesClick()
        {
            List<Task> tasks = Lines.SelectMany(l => l.Cases, (l, c) => c.InnerCaseClick()).ToList();

            while (tasks.Count > 0)
            {
                Task task = await Task.WhenAny(tasks);

                Debug.WriteLine("Remove " + task.Id);

                tasks.Remove(task);
            }
        }

        private void FillChessboard()
        {
            cancel = new CancellationTokenSource();

            Lines.Clear();
            for (int i = 0; i < length; i++)
            {
                Line l = new Line();

                for (int j = 0; j < width; j++)
                {
                    l.Cases.Add(new Case(i%2 == 0 ? j%2 == 0 : j%2 != 0, j, i, length, width, GetNewToken));
                }

                Lines.Add(l);
            }

            //Notification
            RaisePropertyChanged(() => Lines);
        }

        private CancellationTokenSource GetNewToken()
        {
            if (cancel == null || cancel.IsCancellationRequested)
                cancel = new CancellationTokenSource();
            return cancel;
        }

        #endregion
    }
}