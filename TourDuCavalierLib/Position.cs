using System.Collections.Generic;

namespace TourDuCavalierLib
{
    public class Position
    {
        #region Fields

        private List<Position> nextPositions;

        #endregion

        #region Public Properties

        public int X { get; protected set; }

        public int Y { get; protected set; }

        #endregion

        #region Constructors and Destructors

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        #endregion

        #region Public Properties

        public Position PreviousPosition { get; set; }

        public List<Position> NextPositions
        {
            get
            {
                if (nextPositions == null)
                {
                    nextPositions = new List<Position>();
                }

                return nextPositions;
            }
            set { nextPositions = value; }
        }

        #endregion

        #region Public Methods and Operators

        public bool Compare(int x, int y)
        {
            return X == x && Y == y;
        }

        #endregion
    }
}