// <copyright file="SquareMatrix.cs" company="Math.NET">
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

namespace MathNet.Numerics.LinearAlgebra.Double
{
    using System;
    using Properties;

    /// <summary>
    /// Abstract class for square matrices. 
    /// </summary>
    [Serializable]
    public abstract class SquareMatrix : Matrix
    {
        /// <summary>
        ///   Number of rows or columns.
        /// </summary>
        /// <remarks>
        ///   Using this instead of a property to speed up calculating a matrix index in the data array.
        /// </remarks>
        protected readonly int Order;

        /// <summary>
        /// Initializes a new instance of the <see cref="SquareMatrix"/> class.
        /// </summary>
        /// <param name="rows">
        /// The number of rows.
        /// </param>
        /// <param name="columns">
        /// The number of columns.
        /// </param>
        /// <exception cref="ArgumentException">
        /// If <paramref name="rows"/> not equal to <paramref name="columns"/>.
        /// </exception>
        protected SquareMatrix(int rows, int columns) 
            : base(rows, columns)
        {
            if (rows != columns)
            {
                throw new ArgumentException(Resources.ArgumentMatrixSquare);
            }

            Order = rows;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SquareMatrix"/> class.
        /// </summary>
        /// <param name="order">
        /// The order of the matrix.
        /// </param>
        protected SquareMatrix(int order) 
            : base(order)
        {
            Order = order;
        }
    }
}
