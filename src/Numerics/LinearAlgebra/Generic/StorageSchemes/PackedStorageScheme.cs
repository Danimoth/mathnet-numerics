// <copyright file="PackedStorageScheme.cs" company="Math.NET">
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

namespace MathNet.Numerics.LinearAlgebra.Generic.StorageSchemes
{
    using System;
    using Properties;

    /// <summary>
    /// A class for managing indexing when using Packed Storage scheme, which is a column-Wise packing scheme for Symmetric, Hermitian or Triangular square matrices.
    /// </summary>
    /// <remarks>
    /// Upper version features faster indexing than the Lower version.
    /// </remarks>
    public abstract class PackedStorageScheme
    {
        /// <summary>
        ///   Number of rows or columns.
        /// </summary>
        /// <remarks>
        ///   Using this instead of a property to speed up calculating 
        ///   a matrix index in the data array.
        /// </remarks>
        protected readonly int Order;

        /// <summary>
        /// Initializes a new instance of the <see cref="PackedStorageScheme"/> class.
        /// </summary>
        /// <param name="order">
        /// The order of the matrix.
        /// </param>
        protected PackedStorageScheme(int order)
        {
            if (order <= 0)
            {
                throw new ArgumentOutOfRangeException(Resources.MatrixRowsOrColumnsMustBePositive);
            }

            Order = order;

            PackedDataSize = order * (order + 1) / 2;
        }

        /// <summary>
        ///   Gets the size of the Packed Data.
        /// </summary>
        public int PackedDataSize
        {
            get;
            private set;
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
        public abstract int this[int row, int column]
        {
            get;
        }

        /// <summary>
        /// Retrieves the index of the requested element without parameter checking.
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
        public abstract int IndexOf(int row, int column);

        /// <summary>
        /// Retrieves the index of the requested diagonal element without parameter checking.
        /// </summary>
        /// <param name="row">
        /// The row=column of the diagonal element. 
        /// </param>
        /// <returns>
        /// The requested index. 
        /// </returns>
        public abstract int IndexOfDiagonal(int row);
    }
}
