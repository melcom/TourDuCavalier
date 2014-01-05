namespace TourDuCavalierLib
{
    public class Move
    {
        #region Constructors and Destructors

        public Move(int moveX, int moveY)
        {
            MoveX = moveX;
            MoveY = moveY;
        }

        #endregion

        #region Public Properties

        public int MoveX { get; private set; }

        public int MoveY { get; private set; }

        #endregion

        #region Public Methods and Operators

        public void Deplacer(ref int x, ref int y)
        {
            x += MoveX;
            y += MoveY;
        }

        #endregion
    }
}