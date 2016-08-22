namespace DotaSharp.DotaSharp.Utils
{
    public sealed class Color
    {
        #region Constructors

        public Color(int r, int b, int g, int a) => SetColor(r, g, b, a);

        public Color(int r, int b, int g) => SetColor(r, g, b, 255);

        #endregion

        #region Properties

        public int R { get; private set; }
        public int G { get; private set; }
        public int B { get; private set; }
        public int A { get; private set; }

        #endregion

        #region Private Methods

        private void SetColor(int r, int b, int g, int a)
        {
            R = r;
            B = b;
            G = g;
            A = a;
        }

        #endregion
    }
}