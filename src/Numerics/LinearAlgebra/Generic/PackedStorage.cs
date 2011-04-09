// <copyright file="PackedStorage.cs" company="Math.NET">
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
    using Properties;

    /// <summary>
    /// Column-Wise packing for Symmetric, Hermitian or Triangular square matrices.  
    /// </summary>
    /// <typeparam name="T">Supported data types are <c>double</c>, <c>single</c>, <see cref="Complex"/>, and <see cref="Complex32"/>.</typeparam>
    /// <remarks> Upper version features faster indexing than the Lower version. </remarks>
    public abstract class PackedStorage<T>
        where T : struct, IEquatable<T>, IFormattable
    {
        /// <summary>
        /// Number of rows or columns.
        /// </summary>
        /// <remarks>Using this instead of a property to speed up calculating 
        /// a matrix index in the data array.</remarks>
        protected readonly int Order;

        /// <summary>
        /// Gets the matrix's data.
        /// </summary>
        /// <value>The matrix's data.</value>
        public T[] Data
        {
            get;
            private set;
        }

        /// <summary>
        /// Initializes a new instance of the PackedStorage class.
        /// </summary>
        /// <param name="order">
        /// The order of the matrix.
        /// </param>
        protected PackedStorage(int order)
        {
            if (order <= 0)
            {
                throw new ArgumentOutOfRangeException(Resources.MatrixRowsOrColumnsMustBePositive);
            }

            Order = order;
            Data = new T[order * (order + 1) / 2];
        }

        /// <summary>
        /// Gets or sets the value at the given row and column.
        /// </summary>
        /// <param name="row">
        /// The row of the element.
        /// </param>
        /// <param name="column">
        /// The column of the element.
        /// </param>
        /// <value>The value to get or set.</value>
        /// <remarks>This method is ranged checked. <see cref="At(int,int)"/> and <see cref="At(int,int,T)"/>
        /// to get and set values without range checking.</remarks>
        public T this[int row, int column]
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

                return Data[IndexOf(row, column)];
            }

            set
            {
                if (row < 0 || row >= Order)
                {
                    throw new ArgumentOutOfRangeException("row");
                }

                if (column < 0 || column >= Order)
                {
                    throw new ArgumentOutOfRangeException("column");
                }

                Data[IndexOf(row, column)] = value;
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
        public abstract int IndexOf(int row, int column);

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
        public abstract int IndexOfLower(int row, int column);

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
        public abstract int IndexOfUpper(int row, int column);

        /// <summary>
        /// Retrieves the index of the requested element without range checking.
        /// </summary>
        /// <param name="row">
        /// The row=column of the diagonal element. 
        /// </param>
        /// <returns>
        /// The requested index. 
        /// </returns>
        public abstract int IndexOfDiagonal(int row);

        /// <summary>
        /// Retrieves the requested element without range checking. 
        /// </summary>
        /// <param name="row">
        /// The row of the element.
        /// </param>
        /// <param name="column">
        /// The column of the element.
        /// </param>
        /// <returns>
        /// The requested element.
        /// </returns>
        public T At(int row, int column)
        {
            return Data[IndexOf(row, column)];
        }

        /// <summary>
        /// Sets the value of the given element.
        /// </summary>
        /// <param name="row">
        /// The row of the element.
        /// </param>
        /// <param name="column">
        /// The column of the element.
        /// </param>
        /// <param name="value">
        /// The value to set the element to.
        /// </param>
        public void At(int row, int column, T value)
        {
            Data[IndexOf(row, column)] = value;
        }

        /// <summary>
        /// Retrieves the requested element without range checking. 
        /// CAUTION:
        /// This method assumes that you request an element from the upper triangle (row less than or equal to column).  
        /// If not, the result is completely wrong.  
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
        public T AtUpper(int row, int column)
        {
            return Data[IndexOfUpper(row, column)];
        }

        /// <summary>
        /// Sets the value of the given element.
        /// CAUTION:
        /// This method assumes that you set an element from the upper triangle (row less than or equal to column).
        /// If not, the result is completely wrong. 
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
        public void AtUpper(int row, int column, T value)
        {
            Data[IndexOfUpper(row, column)] = value;
        }

        /// <summary>
        /// Retrieves the requested element without range checking. 
        /// CAUTION:
        /// This method assumes that you request an element from the lower triangle (row greater than or equal to column).  
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
        public T AtLower(int row, int column)
        {
            return Data[IndexOfLower(row, column)];
        }

        /// <summary>
        /// Sets the value of the given element.
        /// CAUTION:
        /// This method assumes that you set an element from the lower triangle (row greater than or equal to column).
        /// If not, the result is completely wrong. 
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
        public void AtLower(int row, int column, T value)
        {
            Data[IndexOfLower(row, column)] = value;
        }

        /// <summary>
        /// Retrieves the requested element without range checking. 
        /// </summary>
        /// <param name="row">
        /// The row=column of the diagonal element.
        /// </param>
        /// <returns>
        /// The requested element.
        /// </returns>
        public T AtDiagonal(int row)
        {
            return Data[IndexOfDiagonal(row)];
        }

        /// <summary>
        /// Sets the value of the given element.
        /// </summary>
        /// <param name="row">
        /// The row=column of the diagonal element.
        /// </param>
        /// <param name="value">
        /// The value to set the element to.
        /// </param>
        public void AtDiagonal(int row, T value)
        {
            Data[IndexOfDiagonal(row)] = value;
        }
    }
}
