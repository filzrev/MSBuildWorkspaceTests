// MIT License - Copyright (C) The Mono.Xna Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Microsoft.Xna.Framework
{
    /// <summary>
    /// Describes a 2D-vector.
    /// </summary>
#if XNADESIGNPROVIDED
    [System.ComponentModel.TypeConverter(typeof(Microsoft.Xna.Framework.Design.Vector2TypeConverter))]
#endif
    [DataContract]
    [DebuggerDisplay("{DebugDisplayString,nq}")]
    public struct Vector2 : IEquatable<Vector2>
    {
        #region Private Fields

        private static readonly Vector2 zeroVector = new Vector2(0f, 0f);
        private static readonly Vector2 unitVector = new Vector2(1f, 1f);
        private static readonly Vector2 unitXVector = new Vector2(1f, 0f);
        private static readonly Vector2 unitYVector = new Vector2(0f, 1f);

        #endregion

        #region Public Fields

        /// <summary>
        /// The x coordinate of this <see cref="Vector2"/>.
        /// </summary>
        [DataMember]
        public float X;

        /// <summary>
        /// The y coordinate of this <see cref="Vector2"/>.
        /// </summary>
        [DataMember]
        public float Y;

        #endregion

        #region Properties

        /// <summary>
        /// Returns a <see cref="Vector2"/> with components 0, 0.
        /// </summary>
        public static Vector2 Zero
        {
            get { return zeroVector; }
        }

        /// <summary>
        /// Returns a <see cref="Vector2"/> with components 1, 1.
        /// </summary>
        public static Vector2 One
        {
            get { return unitVector; }
        }

        /// <summary>
        /// Returns a <see cref="Vector2"/> with components 1, 0.
        /// </summary>
        public static Vector2 UnitX
        {
            get { return unitXVector; }
        }

        /// <summary>
        /// Returns a <see cref="Vector2"/> with components 0, 1.
        /// </summary>
        public static Vector2 UnitY
        {
            get { return unitYVector; }
        }

        #endregion

        #region Internal Properties

        internal string DebugDisplayString
        {
            get
            {
                return string.Concat(
                    this.X.ToString(), "  ",
                    this.Y.ToString()
                );
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs a 2d vector with X and Y from two values.
        /// </summary>
        /// <param name="x">The x coordinate in 2d-space.</param>
        /// <param name="y">The y coordinate in 2d-space.</param>
        public Vector2(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }

        /// <summary>
        /// Constructs a 2d vector with X and Y set to the same value.
        /// </summary>
        /// <param name="value">The x and y coordinates in 2d-space.</param>
        public Vector2(float value)
        {
            this.X = value;
            this.Y = value;
        }

        #endregion

        #region Operators

        /// <summary>
        /// Converts a <see cref="System.Numerics.Vector2"/> to a <see cref="Vector2"/>.
        /// </summary>
        /// <param name="value">The converted value.</param>
        public static implicit operator Vector2(System.Numerics.Vector2 value)
        {
            return new Vector2(value.X, value.Y);
        }

        /// <summary>
        /// Inverts values in the specified <see cref="Vector2"/>.
        /// </summary>
        /// <param name="value">Source <see cref="Vector2"/> on the right of the sub sign.</param>
        /// <returns>Result of the inversion.</returns>
        public static Vector2 operator -(Vector2 value)
        {
            value.X = -value.X;
            value.Y = -value.Y;
            return value;
        }

        /// <summary>
        /// Adds two vectors.
        /// </summary>
        /// <param name="value1">Source <see cref="Vector2"/> on the left of the add sign.</param>
        /// <param name="value2">Source <see cref="Vector2"/> on the right of the add sign.</param>
        /// <returns>Sum of the vectors.</returns>
        public static Vector2 operator +(Vector2 value1, Vector2 value2)
        {
            value1.X += value2.X;
            value1.Y += value2.Y;
            return value1;
        }

        /// <summary>
        /// Subtracts a <see cref="Vector2"/> from a <see cref="Vector2"/>.
        /// </summary>
        /// <param name="value1">Source <see cref="Vector2"/> on the left of the sub sign.</param>
        /// <param name="value2">Source <see cref="Vector2"/> on the right of the sub sign.</param>
        /// <returns>Result of the vector subtraction.</returns>
        public static Vector2 operator -(Vector2 value1, Vector2 value2)
        {
            value1.X -= value2.X;
            value1.Y -= value2.Y;
            return value1;
        }

        /// <summary>
        /// Multiplies the components of two vectors by each other.
        /// </summary>
        /// <param name="value1">Source <see cref="Vector2"/> on the left of the mul sign.</param>
        /// <param name="value2">Source <see cref="Vector2"/> on the right of the mul sign.</param>
        /// <returns>Result of the vector multiplication.</returns>
        public static Vector2 operator *(Vector2 value1, Vector2 value2)
        {
            value1.X *= value2.X;
            value1.Y *= value2.Y;
            return value1;
        }

        /// <summary>
        /// Multiplies the components of vector by a scalar.
        /// </summary>
        /// <param name="value">Source <see cref="Vector2"/> on the left of the mul sign.</param>
        /// <param name="scaleFactor">Scalar value on the right of the mul sign.</param>
        /// <returns>Result of the vector multiplication with a scalar.</returns>
        public static Vector2 operator *(Vector2 value, float scaleFactor)
        {
            value.X *= scaleFactor;
            value.Y *= scaleFactor;
            return value;
        }

        /// <summary>
        /// Multiplies the components of vector by a scalar.
        /// </summary>
        /// <param name="scaleFactor">Scalar value on the left of the mul sign.</param>
        /// <param name="value">Source <see cref="Vector2"/> on the right of the mul sign.</param>
        /// <returns>Result of the vector multiplication with a scalar.</returns>
        public static Vector2 operator *(float scaleFactor, Vector2 value)
        {
            value.X *= scaleFactor;
            value.Y *= scaleFactor;
            return value;
        }

        /// <summary>
        /// Divides the components of a <see cref="Vector2"/> by the components of another <see cref="Vector2"/>.
        /// </summary>
        /// <param name="value1">Source <see cref="Vector2"/> on the left of the div sign.</param>
        /// <param name="value2">Divisor <see cref="Vector2"/> on the right of the div sign.</param>
        /// <returns>The result of dividing the vectors.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 operator /(Vector2 value1, Vector2 value2)
        {
            value1.X /= value2.X;
            value1.Y /= value2.Y;
            return value1;
        }

        /// <summary>
        /// Divides the components of a <see cref="Vector2"/> by a scalar.
        /// </summary>
        /// <param name="value1">Source <see cref="Vector2"/> on the left of the div sign.</param>
        /// <param name="divider">Divisor scalar on the right of the div sign.</param>
        /// <returns>The result of dividing a vector by a scalar.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 operator /(Vector2 value1, float divider)
        {
            float factor = 1 / divider;
            value1.X *= factor;
            value1.Y *= factor;
            return value1;
        }

        /// <summary>
        /// Compares whether two <see cref="Vector2"/> instances are equal.
        /// </summary>
        /// <param name="value1"><see cref="Vector2"/> instance on the left of the equal sign.</param>
        /// <param name="value2"><see cref="Vector2"/> instance on the right of the equal sign.</param>
        /// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
        public static bool operator ==(Vector2 value1, Vector2 value2)
        {
            return value1.X == value2.X && value1.Y == value2.Y;
        }

        /// <summary>
        /// Compares whether two <see cref="Vector2"/> instances are not equal.
        /// </summary>
        /// <param name="value1"><see cref="Vector2"/> instance on the left of the not equal sign.</param>
        /// <param name="value2"><see cref="Vector2"/> instance on the right of the not equal sign.</param>
        /// <returns><c>true</c> if the instances are not equal; <c>false</c> otherwise.</returns>	
        public static bool operator !=(Vector2 value1, Vector2 value2)
        {
            return value1.X != value2.X || value1.Y != value2.Y;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Performs vector addition on <paramref name="value1"/> and <paramref name="value2"/>.
        /// </summary>
        /// <param name="value1">The first vector to add.</param>
        /// <param name="value2">The second vector to add.</param>
        /// <returns>The result of the vector addition.</returns>
        public static Vector2 Add(Vector2 value1, Vector2 value2)
        {
            value1.X += value2.X;
            value1.Y += value2.Y;
            return value1;
        }

        /// <summary>
        /// Performs vector addition on <paramref name="value1"/> and
        /// <paramref name="value2"/>, storing the result of the
        /// addition in <paramref name="result"/>.
        /// </summary>
        /// <param name="value1">The first vector to add.</param>
        /// <param name="value2">The second vector to add.</param>
        /// <param name="result">The result of the vector addition.</param>
        public static void Add(ref Vector2 value1, ref Vector2 value2, out Vector2 result)
        {
            result.X = value1.X + value2.X;
            result.Y = value1.Y + value2.Y;
        }

        /// <summary>
        /// Returns the distance between two vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <returns>The distance between two vectors.</returns>
        public static float Distance(Vector2 value1, Vector2 value2)
        {
            float v1 = value1.X - value2.X, v2 = value1.Y - value2.Y;
            return MathF.Sqrt((v1 * v1) + (v2 * v2));
        }

        /// <summary>
        /// Returns the distance between two vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <param name="result">The distance between two vectors as an output parameter.</param>
        public static void Distance(ref Vector2 value1, ref Vector2 value2, out float result)
        {
            float v1 = value1.X - value2.X, v2 = value1.Y - value2.Y;
            result = MathF.Sqrt((v1 * v1) + (v2 * v2));
        }

        /// <summary>
        /// Returns the squared distance between two vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <returns>The squared distance between two vectors.</returns>
        public static float DistanceSquared(Vector2 value1, Vector2 value2)
        {
            float v1 = value1.X - value2.X, v2 = value1.Y - value2.Y;
            return (v1 * v1) + (v2 * v2);
        }

        /// <summary>
        /// Returns the squared distance between two vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <param name="result">The squared distance between two vectors as an output parameter.</param>
        public static void DistanceSquared(ref Vector2 value1, ref Vector2 value2, out float result)
        {
            float v1 = value1.X - value2.X, v2 = value1.Y - value2.Y;
            result = (v1 * v1) + (v2 * v2);
        }

        /// <summary>
        /// Divides the components of a <see cref="Vector2"/> by the components of another <see cref="Vector2"/>.
        /// </summary>
        /// <param name="value1">Source <see cref="Vector2"/>.</param>
        /// <param name="value2">Divisor <see cref="Vector2"/>.</param>
        /// <returns>The result of dividing the vectors.</returns>
        public static Vector2 Divide(Vector2 value1, Vector2 value2)
        {
            value1.X /= value2.X;
            value1.Y /= value2.Y;
            return value1;
        }

        /// <summary>
        /// Divides the components of a <see cref="Vector2"/> by the components of another <see cref="Vector2"/>.
        /// </summary>
        /// <param name="value1">Source <see cref="Vector2"/>.</param>
        /// <param name="value2">Divisor <see cref="Vector2"/>.</param>
        /// <param name="result">The result of dividing the vectors as an output parameter.</param>
        public static void Divide(ref Vector2 value1, ref Vector2 value2, out Vector2 result)
        {
            result.X = value1.X / value2.X;
            result.Y = value1.Y / value2.Y;
        }

        /// <summary>
        /// Divides the components of a <see cref="Vector2"/> by a scalar.
        /// </summary>
        /// <param name="value1">Source <see cref="Vector2"/>.</param>
        /// <param name="divider">Divisor scalar.</param>
        /// <returns>The result of dividing a vector by a scalar.</returns>
        public static Vector2 Divide(Vector2 value1, float divider)
        {
            float factor = 1 / divider;
            value1.X *= factor;
            value1.Y *= factor;
            return value1;
        }

        /// <summary>
        /// Divides the components of a <see cref="Vector2"/> by a scalar.
        /// </summary>
        /// <param name="value1">Source <see cref="Vector2"/>.</param>
        /// <param name="divider">Divisor scalar.</param>
        /// <param name="result">The result of dividing a vector by a scalar as an output parameter.</param>
        public static void Divide(ref Vector2 value1, float divider, out Vector2 result)
        {
            float factor = 1 / divider;
            result.X = value1.X * factor;
            result.Y = value1.Y * factor;
        }

        /// <summary>
        /// Returns a dot product of two vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <returns>The dot product of two vectors.</returns>
        public static float Dot(Vector2 value1, Vector2 value2)
        {
            return (value1.X * value2.X) + (value1.Y * value2.Y);
        }

        /// <summary>
        /// Returns a dot product of two vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <param name="result">The dot product of two vectors as an output parameter.</param>
        public static void Dot(ref Vector2 value1, ref Vector2 value2, out float result)
        {
            result = (value1.X * value2.X) + (value1.Y * value2.Y);
        }

        /// <summary>
        /// Compares whether current instance is equal to specified <see cref="Object"/>.
        /// </summary>
        /// <param name="obj">The <see cref="Object"/> to compare.</param>
        /// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
        public override bool Equals(object obj)
        {
            if (obj is Vector2)
            {
                return Equals((Vector2)obj);
            }

            return false;
        }

        /// <summary>
        /// Compares whether current instance is equal to specified <see cref="Vector2"/>.
        /// </summary>
        /// <param name="other">The <see cref="Vector2"/> to compare.</param>
        /// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
        public bool Equals(Vector2 other)
        {
            return (X == other.X) && (Y == other.Y);
        }

        /// <summary>
        /// Round the members of this <see cref="Vector2"/> towards negative infinity.
        /// </summary>
        public void Floor()
        {
            X = MathF.Floor(X);
            Y = MathF.Floor(Y);
        }

        /// <summary>
        /// Creates a new <see cref="Vector2"/> that contains members from another vector rounded towards negative infinity.
        /// </summary>
        /// <param name="value">Source <see cref="Vector2"/>.</param>
        /// <returns>The rounded <see cref="Vector2"/>.</returns>
        public static Vector2 Floor(Vector2 value)
        {
            value.X = MathF.Floor(value.X);
            value.Y = MathF.Floor(value.Y);
            return value;
        }

        /// <summary>
        /// Creates a new <see cref="Vector2"/> that contains members from another vector rounded towards negative infinity.
        /// </summary>
        /// <param name="value">Source <see cref="Vector2"/>.</param>
        /// <param name="result">The rounded <see cref="Vector2"/>.</param>
        public static void Floor(ref Vector2 value, out Vector2 result)
        {
            result.X = MathF.Floor(value.X);
            result.Y = MathF.Floor(value.Y);
        }

        /// <summary>
        /// Gets the hash code of this <see cref="Vector2"/>.
        /// </summary>
        /// <returns>Hash code of this <see cref="Vector2"/>.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                return (X.GetHashCode() * 397) ^ Y.GetHashCode();
            }
        }


        /// <summary>
        /// Returns the length of this <see cref="Vector2"/>.
        /// </summary>
        /// <returns>The length of this <see cref="Vector2"/>.</returns>
        public float Length()
        {
            return MathF.Sqrt((X * X) + (Y * Y));
        }

        /// <summary>
        /// Returns the squared length of this <see cref="Vector2"/>.
        /// </summary>
        /// <returns>The squared length of this <see cref="Vector2"/>.</returns>
        public float LengthSquared()
        {
            return (X * X) + (Y * Y);
        }

       

        /// <summary>
        /// Creates a new <see cref="Vector2"/> that contains subtraction of on <see cref="Vector2"/> from a another.
        /// </summary>
        /// <param name="value1">Source <see cref="Vector2"/>.</param>
        /// <param name="value2">Source <see cref="Vector2"/>.</param>
        /// <returns>The result of the vector subtraction.</returns>
        public static Vector2 Subtract(Vector2 value1, Vector2 value2)
        {
            value1.X -= value2.X;
            value1.Y -= value2.Y;
            return value1;
        }

        /// <summary>
        /// Creates a new <see cref="Vector2"/> that contains subtraction of on <see cref="Vector2"/> from a another.
        /// </summary>
        /// <param name="value1">Source <see cref="Vector2"/>.</param>
        /// <param name="value2">Source <see cref="Vector2"/>.</param>
        /// <param name="result">The result of the vector subtraction as an output parameter.</param>
        public static void Subtract(ref Vector2 value1, ref Vector2 value2, out Vector2 result)
        {
            result.X = value1.X - value2.X;
            result.Y = value1.Y - value2.Y;
        }

        /// <summary>
        /// Returns a <see cref="String"/> representation of this <see cref="Vector2"/> in the format:
        /// {X:[<see cref="X"/>] Y:[<see cref="Y"/>]}
        /// </summary>
        /// <returns>A <see cref="String"/> representation of this <see cref="Vector2"/>.</returns>
        public override string ToString()
        {
            return "{X:" + X + " Y:" + Y + "}";
        }


        /// <summary>
        /// Rotates a vector by the specified number of radians
        /// </summary>
        /// <param name="value">The vector to be rotated.</param>
        /// <param name="radians">The amount to rotate the vector.</param>
        /// <returns>A rotated copy of value.</returns>
        /// <remarks>
        /// A positive angle and negative angle
        /// would rotate counterclockwise and clockwise,
        /// respectively
        /// </remarks>
        public static Vector2 Rotate(Vector2 value, float radians)
        {
            float cos = MathF.Cos(radians);
            float sin = MathF.Sin(radians);

            return new Vector2(value.X * cos - value.Y * sin, value.X * sin + value.Y * cos);
        }

        /// <summary>
        /// Rotates a <see cref="Vector2"/> by the specified number of radians
        /// </summary>
        /// <param name="radians">The amount to rotate this <see cref="Vector2"/>.</param>
        /// <remarks>
        /// A positive angle and negative angle
        /// would rotate counterclockwise and clockwise,
        /// respectively
        /// </remarks>
        public void Rotate(float radians)
        {
            float cos = MathF.Cos(radians);
            float sin = MathF.Sin(radians);

            float oldx = X;

            X = X * cos - Y * sin;
            Y = oldx * sin + Y * cos;
        }

        /// <summary>
        /// Rotates a <see cref="Vector2"/> around another <see cref="Vector2"/> representing a location
        /// </summary>
        /// <param name="value">The <see cref="Vector2"/> to be rotated</param>
        /// <param name="origin">The origin location to be rotated around</param>
        /// <param name="radians">The amount to rotate by in radians</param>
        /// <returns>The rotated <see cref="Vector2"/></returns>
        /// <remarks>
        /// A positive angle and negative angle
        /// would rotate counterclockwise and clockwise,
        /// respectively
        /// </remarks>
        public static Vector2 RotateAround(Vector2 value, Vector2 origin, float radians)
        {
            return Rotate(value - origin, radians) + origin;
        }

        /// <summary>
        /// Rotates a <see cref="Vector2"/> around another <see cref="Vector2"/> representing a location
        /// </summary>
        /// <param name="origin">The origin location to be rotated around</param>
        /// <param name="radians">The amount to rotate by in radians</param>
        /// <remarks>
        /// A positive angle and negative angle
        /// would rotate counterclockwise and clockwise,
        /// respectively
        /// </remarks>
        public void RotateAround(Vector2 origin, float radians)
        {
            this -= origin;
            Rotate(radians);
            this += origin;
        }

        /// <summary>
        /// Deconstruction method for <see cref="Vector2"/>.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void Deconstruct(out float x, out float y)
        {
            x = X;
            y = Y;
        }

        /// <summary>
        /// Returns a <see cref="System.Numerics.Vector2"/>.
        /// </summary>
        public System.Numerics.Vector2 ToNumerics()
        {
            return new System.Numerics.Vector2(this.X, this.Y);
        }

        #endregion
    }
}
