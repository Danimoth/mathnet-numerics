﻿// <copyright file="PackedStorageUpper.cs" company="Math.NET">
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
        /// Initializes a new instance of the PackedStorageUpper class.
        /// </summary>
        /// <param name="order">
        /// The order of the matrix.
        /// </param>
        public PackedStorageUpper(int order) 
            : base(order)
        {
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
        public override int IndexOf(int row, int column)
        {
            var r = Math.Min(row, column);
            var c = Math.Max(row, column);
            return IndexOfUpper(r, c);
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
        public override int IndexOfLower(int row, int column)
        {
            return IndexOfUpper(column, row);
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
        public override int IndexOfUpper(int row, int column)
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
        public override int IndexOfDiagonal(int row)
        {
            return (row * (row + 3)) / 2;
        }
    }
}