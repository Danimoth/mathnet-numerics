// <copyright file="PackedStorageUpper.cs" company="Math.NET">
// Math.NET Numerics, part of the Math.NET Project
// http://numerics.mathdotnet.com
// http://github.com/mathnet/mathnet-numerics
// http://mathnetnumerics.codeplex.com
// Copyright (c) 2009-2010 Math.NET
// Permission is hereby granted, free of charge, to any person
// obtaining a copy of this software and associated documentation
// files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use,
// copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following
// conditions:
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
// </copyright>

namespace MathNet.Numerics.LinearAlgebra.Generic
{
    using System;

    /// <summary>
    /// Column-Wise packing for Symmetric, Hermitian or Triangular square matrices. 
    /// This variation stores the upper triangle of a matrix (row less than or equal to column). 
    /// </summary>
    /// <typeparam name="T">Supported data types are <c>double</c>, <c>single</c>, <see cref="Complex"/>, and <see cref="Complex32"/>.</typeparam>
    /// <remarks> Upper version features faster indexing than the Lower version. </remarks>
    public class PackedStorageUpper<T> : PackedStorage<T>
        where T : struct, IEquatable<T>, IFormattable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PackedStorageUpper{T}"/> class. 
        /// </summary>
        /// <param name="order">
        /// The order of the matrix.
        /// </param>
        public PackedStorageUpper(int order) 
            : base(order)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PackedStorageUpper{T}"/> class with all entries set to a particular value.
        /// </summary>
        /// <param name="order">
        /// The number of rows or columns. 
        /// </param>
        /// <param name="value">
        /// The value which we assign to each element of the matrix.
        /// </param>
        public PackedStorageUpper(int order, T value)
            : base(order, value)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PackedStorageUpper{T}"/> class from a proper one-dimensional array. This constructor
        /// will reference the one dimensional array and not copy it.
        /// </summary>
        /// <param name="order">The size of the square matrix.</param>
        /// <param name="array"> The one dimensional array to create this matrix from.  </param>
        /// <exception cref="ArgumentException">
        /// If <paramref name="array"/> does not represent a packed array.
        /// </exception>
        public PackedStorageUpper(int order, T[] array)
            : base(order, array)
        {
        }

        /// <summary>
        /// Retrieves the requested element without range checking. 
        ///   CAUTION:
        ///   This method assumes that you request an element from the upper triangle (row less than or equal to column).  
        ///   If not, the result is completely wrong.
        /// </summary>
        /// <param name="row">
        /// The row of the element. Must be less than or equal to column. 
        /// </param>
        /// <param name="column">
        /// The column of the element. Must be more than or equal to row. 
        /// </param>
        /// <returns>
        /// The requested element from the upper triangle.
        /// </returns>
        public override T AtUpper(int row, int column)
        {
            return Data[IndexOfUpper(row, column)];
        }

        /// <summary>
        /// Sets the value of the given element.
        ///   CAUTION:
        ///   This method assumes that you set an element from the upper triangle (row less than or equal to column).
        ///   If not, the result is completely wrong.
        /// </summary>
        /// <param name="row">
        /// The row of the element. Must be less than or equal to column.
        /// </param>
        /// <param name="column">
        /// The column of the element. Must be more than or equal to row. 
        /// </param>
        /// <param name="value">
        /// The value on the upper triangle to set the element to.
        /// </param>
        public override void AtUpper(int row, int column, T value)
        {
            Data[IndexOfUpper(row, column)] = value;
        }

        /// <summary>
        /// Retrieves the requested element without range checking. 
        ///   CAUTION:
        ///   This method assumes that you request an element from the lower triangle (row greater than or equal to column).
        /// </summary>
        /// <param name="row">
        /// The row of the element. Must be more than or equal to column. 
        /// </param>
        /// <param name="column">
        /// The column of the element. Must be less than or equal to row. 
        /// </param>
        /// <returns>
        /// The requested element from the lower triangle.
        /// </returns>
        public override T AtLower(int row, int column)
        {
            return row == column ? Data[IndexOfDiagonal(row)] : default(T);
        }

        /// <summary>
        /// Sets the value of the given element.
        ///   CAUTION:
        ///   This method assumes that you set an element from the lower triangle (row greater than or equal to column).
        ///   If not, the result is completely wrong.
        /// </summary>
        /// <param name="row">
        /// The row of the element. Must be more than or equal to column
        /// </param>
        /// <param name="column">
        /// The column of the element. Must be less than or equal to row. 
        /// </param>
        /// <param name="value">
        /// The value on the lower triangle to set the element to.
        /// </param>
        public override void AtLower(int row, int column, T value)
        {
            if (row == column)
            {
                Data[IndexOfDiagonal(row)] = value;
            }
            else
            {
                throw new InvalidOperationException("Cannot write in the lower triangle of an upper triangle matrix");
            }
        }

        /// <summary>
        /// Retrieves the index of the requested element without range checking.
        /// </summary>
        /// <param name="row">
        /// The row of the element. 
        /// </param>
        /// <param name="column">
        /// The column of the element. 
        /// </param>
        /// <returns>
        /// The requested index. 
        /// </returns>
        protected override int IndexOf(int row, int column)
        {
            return IndexOfUpper(row, column);
        }

        /// <summary>
        /// Retrieves the index of the requested element without range checking. 
        /// CAUTION:
        /// This method assumes (for performance) that you request an index from the upper triangle (row less than or equal column). 
        /// If not, the index is completely wrong.  
        /// </summary>
        /// <param name="row">
        /// The row of the element. Must be less than or equal to column.
        /// </param>
        /// <param name="column">
        /// The column of the element. Must be more than or equal to row.
        /// </param>
        /// <returns>
        /// The requested index. 
        /// </returns>
        private int IndexOfUpper(int row, int column)
        {
            return row + ((column * (column + 1)) / 2);
        }

        /// <summary>
        /// Retrieves the index of the requested element without range checking.
        /// </summary>
        /// <param name="row">
        /// The row=column of the diagonal element. 
        /// </param>
        /// <returns>
        /// The requested index. 
        /// </returns>
        protected override int IndexOfDiagonal(int row)
        {
            return (row * (row + 3)) / 2;
        }
    }
}
