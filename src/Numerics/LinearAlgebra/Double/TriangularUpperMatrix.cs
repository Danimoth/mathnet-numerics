// <copyright file="TriangularUpperMatrix.cs" company="Math.NET">
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
    ///   Class for upper triangular square matrices. 
    ///   An upper triangular matrix has elements on the diagonal and above it.
    /// </summary>
    public abstract class TriangularUpperMatrix : TriangularMatrix
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TriangularUpperMatrix"/> class.
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
        protected TriangularUpperMatrix(int rows, int columns) : base(rows, columns)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TriangularUpperMatrix"/> class.
        /// </summary>
        /// <param name="order">
        /// The order of the matrix.
        /// </param>
        protected TriangularUpperMatrix(int order) : base(order)
        {
        }

        /// <summary>
        ///   Gets a value indicating whether this matrix is symmetric.
        /// </summary>
        /// <remarks>
        ///   An upper triangular matrix will only be symmetric if all values of the strictly upper triangle are zero, 
        ///   since by definition all values in the strictly lower triangle are zero. Hence, it will also be a diagonal matrix.
        /// </remarks>
        public override bool IsSymmetric
        {
            get
            {
                for (var row = 0; row < Order; row++)
                {
                    for (var column = row + 1; column < Order; column++)
                    {
                        if (AtUpper(row, column) != 0.0)
                        {
                            return false;
                        }
                    }
                }

                return true;
            }
        }

        /// <summary>
        /// Adds another matrix to this matrix.
        /// </summary>
        /// <param name="other">
        /// The matrix to add to this matrix.
        /// </param>
        /// <param name="result">
        /// The matrix to store the result of the addition.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// If the other matrix is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// If the two matrices don't have the same dimensions.
        /// </exception>
        protected override void DoAdd(Matrix<double> other, Matrix<double> result)
        {
            var triangularUpperOther = other as TriangularUpperMatrix;
            var triangularUpperResult = result as TriangularUpperMatrix;
            if (triangularUpperOther == null || triangularUpperResult == null)
            {
                base.DoAdd(other, result);
            }
            else
            {
                for (var row = 0; row < RowCount; row++)
                {
                    for (var column = row; column < ColumnCount; column++)
                    {
                        triangularUpperResult.AtUpper(row, column, At(row, column) + triangularUpperOther.AtUpper(row, column));
                    }
                }
            }
        }

        /// <summary>
        /// Subtracts another matrix from this matrix.
        /// </summary>
        /// <param name="other">
        /// The matrix to subtract to this matrix.
        /// </param>
        /// <param name="result">
        /// The matrix to store the result of subtraction.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// If the other matrix is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// If the two matrices don't have the same dimensions.
        /// </exception>
        protected override void DoSubtract(Matrix<double> other, Matrix<double> result)
        {
            var triangularUpperOther = other as TriangularUpperMatrix;
            var triangularUpperResult = result as TriangularUpperMatrix;
            if (triangularUpperOther == null || triangularUpperResult == null)
            {
                base.DoAdd(other, result);
            }
            else
            {
                for (var row = 0; row < RowCount; row++)
                {
                    for (var column = row; column < ColumnCount; column++)
                    {
                        triangularUpperResult.AtUpper(row, column, At(row, column) - triangularUpperOther.AtUpper(row, column));
                    }
                }
            }
        }

        /// <summary>
        /// Multiplies each element of the matrix by a scalar and places results into the result matrix.
        /// </summary>
        /// <param name="scalar">
        /// The scalar to multiply the matrix with.
        /// </param>
        /// <param name="result">
        /// The matrix to store the result of the multiplication.
        /// </param>
        protected override void DoMultiply(double scalar, Matrix<double> result)
        {
            var triangularUpperResult = result as TriangularUpperMatrix;

            if (triangularUpperResult == null)
            {
                base.DoMultiply(scalar, result);
            }
            else
            {
                for (var row = 0; row < RowCount; row++)
                {
                    for (var column = row; column < ColumnCount; column++)
                    {
                        triangularUpperResult.AtUpper(row, column, At(row, column) * scalar);
                    }
                }
            }
        }
    }
}
