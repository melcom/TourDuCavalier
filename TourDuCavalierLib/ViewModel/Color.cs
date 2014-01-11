namespace TourDuCavalierLib.ViewModel
{
    /// <summary>
    ///     Stockage pour une couleur RGB
    /// </summary>
    public struct Color
    {
        #region Constructors and Destructors

        public Color(byte r, byte g, byte b)
            : this()
        {
            R = r;
            G = g;
            B = b;
        }

        #endregion

        #region Public Properties

        public byte B { get; private set; }
        public byte G { get; private set; }
        public byte R { get; private set; }

        #endregion
    }
}