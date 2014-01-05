namespace TourDuCavalierLib
{
    public class Chessboard
    {
        #region Static Fields

        public static Move[] Moves =
        {
            new Move(2, 1),
            new Move(2, -1),
            new Move(-2, 1),
            new Move(-2, -1),
            new Move(1, 2),
            new Move(1, -2),
            new Move(-1, 2),
            new Move(-1, -2)
        };

        #endregion

        #region Fields

        private readonly int width;

        private readonly int height;

        #endregion

        #region Constructors and Destructors

        public Chessboard(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        #endregion

        #region Public Properties

        public int NbCases
        {
            get
            {
                return width * height;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Ajoute à la position les déplacements possibles
        /// </summary>
        public void AddPossibleMoves(Position current)
        {
            //Limite le nombre de déplacements possibles
            int max = 3;

            foreach (Move move in Moves)
            {
                if (max == 0) return;

                Position nPosition = IsDeplacementPossible(current, move);

                if (nPosition == null)
                {
                    continue;
                }

                max--;

                // Si l'on peut déplacer le cavalier, création du lien entre les deux positions
                nPosition.PreviousPosition = current;
                current.NextPositions.Add(nPosition);
            }
        }

        /// <summary>
        ///     Détermine si la position est sur l'échiquier ou non
        /// </summary>
        /// <returns>True si dessus, False si en dehors</returns>
        public bool IsMovePossible(int x, int y)
        {
            return x >= 0 && x < width && y >= 0 && y < height;
        }

        #endregion

        #region Methods

        private bool IsCaseEmpty(Position current, int x, int y)
        {
            do
            {
                if (current.Compare(x, y))
                {
                    return false;
                }

                current = current.PreviousPosition;
            }
            while (current != null);

            return true;
        }

        /// <summary>
        ///     Détermine si le déplacement est possible
        /// </summary>
        /// <param name="current"></param>
        /// <param name="move"></param>
        /// <returns></returns>
        private Position IsDeplacementPossible(Position current, Move move)
        {
            int x = current.X;
            int y = current.Y;

            move.Deplacer(ref x, ref y);

            if (IsMovePossible(x, y) && IsCaseEmpty(current, x, y))
            {
                return new Position(x, y);
            }
            return null;
        }

        #endregion
    }
}