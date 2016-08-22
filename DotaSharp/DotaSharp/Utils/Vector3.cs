using System;

namespace DotaSharp
{
    public struct Vector3
    {
        #region Fields

        public float X;
        public float Y;
        public float Z;

        #endregion

        #region Constructors

        public Vector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        #endregion

        #region Static Methods

        public static float Distance(Vector3 firstEntity, Vector3 secondEntity) => (float) Math.Sqrt(
            Math.Pow(firstEntity.X - secondEntity.X, 2) + Math.Pow(firstEntity.Y - secondEntity.Y, 2) +
            Math.Pow(firstEntity.Z - secondEntity.Z, 2));

        #endregion
    }
}