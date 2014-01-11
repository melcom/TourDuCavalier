using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TourDuCavalierLib
{

    public class TourDuCavalier
    {
        #region Fields

        private readonly CancellationToken cancel;

        private readonly IProgress<int> progress;

        private readonly int x;

        private readonly int width;

        private readonly int y;

        private readonly int height;

        private Chessboard chessboard;

        private Position positionInitiale;

        private int[] progressByThread;

        #endregion

        #region Constructors and Destructors

        public TourDuCavalier(int width, int height, int x, int y, CancellationToken cancel, IProgress<int> progress)
        {
            this.width = width;
            this.height = height;
            this.x = x;
            this.y = y;
            this.cancel = cancel;
            this.progress = progress;
        }

        #endregion

        #region Public Methods and Operators

        public async Task<TimeSpan> Solve()
        {
            Stopwatch parcours = new Stopwatch();
            parcours.Start();

            try
            {
                await Initialization();
                await Compute();
            }
            catch (OperationCanceledException e)
            {
                Debug.WriteLine("OperationCanceledException: Message {0}", e.Message);
            }

            parcours.Stop();

            Debug.WriteLine("Solve done in " + parcours.Elapsed);

            return parcours.Elapsed;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Cette méthode retourne l'ensemble des noeuds du niveau donné à partir de la racine
        /// </summary>
        /// <param name="racine">Racine de l'arbre</param>
        /// <param name="niveau">Niveau recherché</param>
        /// <param name="niveauCourant">Niveau courant</param>
        /// <returns></returns>
        private static IEnumerable<Position> GetNoeud(Position racine, int niveau, int niveauCourant)
        {
            if (niveau == niveauCourant)
            {
                yield return racine;
            }
            else
            {
                niveauCourant++;

                foreach (Position position in racine.NextPositions)
                {
                    if (niveau == niveauCourant)
                    {
                        yield return position;
                    }
                    else
                    {
                        foreach (Position positionSuivante in GetNoeud(position, niveau, niveauCourant))
                        {
                            yield return positionSuivante;
                        }
                    }
                }
            }
        }

        private async Task Move(Position niveau, int index)
        {
            await Task.Run(
                () =>
                {
                    for (int i = 0; i < chessboard.NbCases - 1; i++)
                    {
                        cancel.ThrowIfCancellationRequested();

                        foreach (Position position in GetNoeud(niveau, i, 0))
                        {
                            cancel.ThrowIfCancellationRequested();

                            chessboard.AddPossibleMoves(position);
                        }
                        ReportProgress(index);
                    }

                    Debug.WriteLine("Move done");

                },
                cancel);
        }

        private Task Initialization()
        {
            return Task.Run(
                () =>
                {
                    chessboard = new Chessboard(width, height);

                    positionInitiale = new Position(x, y);

                    if (!chessboard.IsMovePossible(x, y))
                    {
                        throw new Exception("La position est en dehors de l'échiquier.");
                    }

                    Debug.WriteLine("Initialization done");
                },
                cancel);
        }

        private Task Compute()
        {
            chessboard.AddPossibleMoves(positionInitiale);

            int count = positionInitiale.NextPositions.Count;

            if (count == 0)
            {
                progress.Report(100);
            }

            progressByThread = new int[count];

            Task[] tasks = new Task[count];

            for (int i = 0; i < count; i++)
            {
                tasks[i] = Move(positionInitiale.NextPositions[i], i).ContinueWith(
                    t =>
                    {
                        if (t.IsCompleted)
                            progress.Report(100);

                        Debug.WriteLine("Move done " + t.Id + " completed " + (t.IsCompleted ? "true" : "false"));
                        
                    },
                cancel,TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Current);
            }
        
            Debug.WriteLine("Parcour de l'échiquier ...");

            return Task.WhenAll(tasks);
        }

        private void ReportProgress(int index)
        {
            progressByThread[index]++;

            int progressTotal = progressByThread.Sum();

            int report = (int)(progressTotal / ((float)((chessboard.NbCases - 1) * progressByThread.Count())) * 100.0);

            progress.Report(report);
        }

        #endregion
    }
}