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
    /// A class for managing indexing when using Packed Storage scheme, which is a column-Wise packing scheme for Symmetric, Hermitian or Triangular square matrices. 
    ///   This variation provides indexes for storing the upper triangle of a matrix (row less than or equal to column).
    /// </summary>
    /// <remarks>
    /// Upper version features faster indexing than the Lower version.
    /// </remarks>
    public class PackedStorageSchemeUpper : PackedStorageScheme
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PackedStorageSchemeUpper"/> class.
        /// </summary>
        /// <param name="order">
        /// The order of the matrix.
        /// </param>
        public PackedStorageSchemeUpper(int order)
            : base(order)
        {
        }

        /// <summary>
        ///   Gets the index of the given element.
        /// </summary>
        /// <param name = "row">
        ///   The row of the element.
        /// </param>
        /// <param name = "column">
        ///   The column of the element.
        /// </param>
        /// <remarks>
        ///   This method is parameter checked. <see cref = "IndexOf(int,int)" /> and <see cref = "IndexOfDiagonal(int)" /> to get values without parameter checking.
        /// </remarks>
        public override int this[int row, int column]
        {
            get
            {
                if (row < 0 || row >= Order)
                {
                    throw new ArgumentOutOfRangeException("row");
                }

                if (column < 0 || column >= Order)
                {
                    throw new ArgumentOutOfRangeException("column");
                }

                if (row > column)
                {
                    throw new ArgumentException("Row must be less than or equal to column");
                }

                return IndexOf(row, column);
            }
        }

        /// <summary>
        /// Retrieves the index of the requested element without parameter checking. Row must be less than or equal to column.
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
            return row + ((column * (column + 1)) / 2);
        }

        /// <summary>
        /// Retrieves the index of the requested diagonal element without parameter checking.
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
