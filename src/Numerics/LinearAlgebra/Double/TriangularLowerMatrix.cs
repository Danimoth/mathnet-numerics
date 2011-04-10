// <copyright file="TriangularLowerMatrix.cs" company="Math.NET">
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
    using Generic;

    /// <summary>
    /// Class for lower triangular swaure matrices. 
    /// A lower triangular matrix has elements on the diagonal and below it. 
    /// </summary>
    public class TriangularLowerMatrix : TriangularMatrix
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TriangularLowerMatrix"/> class.
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
        public TriangularLowerMatrix(int rows, int columns) : base(rows, columns)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TriangularLowerMatrix"/> class.
        /// </summary>
        /// <param name="order">
        /// The order of the matrix.
        /// </param>
        public TriangularLowerMatrix(int order) : base(order)
        {
        }

        /// <summary>
        ///   Gets a value indicating whether this matrix is symmetric.
        /// </summary>
        /// <remarks> A lower triangular matrix will only by symmetric if all values of the stricly lower triangle are zero, 
        /// since by definition all values in the strictly upper triangle are zero. Hence, it will also be a diagonal matrix. 
        /// </remarks>
        public override bool IsSymmetric
        {
            get
            {
                for (var row = 0; row < Order; row++)
                {
                    for (var column = 0; column < row; column++)
                    {
                        if (AtLower(row, column) != 0.0)
                        {
                            return false;
                        }
                    }
                }

                return true;
            }
        }

        #region Overrides of Matrix<double>

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
        public override double At(int row, int column)
        {
            throw new NotImplementedException();
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
        public override void At(int row, int column, double value)
        {
            throw new NotImplementedException();
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
        public override double AtUpper(int row, int column)
        {
            throw new NotImplementedException();
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
        public override void AtUpper(int row, int column, double value)
        {
            throw new NotImplementedException();
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
        public override double AtLower(int row, int column)
        {
            throw new NotImplementedException();
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
        public override void AtLower(int row, int column, double value)
        {
            throw new NotImplementedException();
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
        public override double AtDiagonal(int row)
        {
            throw new NotImplementedException();
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
        public override void AtDiagonal(int row, double value)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates a <strong>Matrix</strong> for the given number of rows and columns.
        /// </summary>
        /// <param name="numberOfRows">
        /// The number of rows.
        /// </param>
        /// <param name="numberOfColumns">
        /// The number of columns.
        /// </param>
        /// <returns>
        /// A <strong>Matrix</strong> with the given dimensions.
        /// </returns>
        /// <remarks>
        /// Creates a matrix of the same matrix type as the current matrix.
        /// </remarks>
        public override Matrix<double> CreateMatrix(int numberOfRows, int numberOfColumns)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates a Vector with a the given dimension.
        /// </summary>
        /// <param name="size">The size of the vector.</param>
        /// <returns>
        /// A Vector with the given dimension.
        /// </returns>
        /// <remarks>
        /// Creates a vector of the same type as the current matrix.
        /// </remarks>
        public override Vector<double> CreateVector(int size)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
