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
    ///   Class for lower triangular square matrices. 
    ///   A lower triangular matrix has elements on the diagonal and below it.
    /// </summary>
    public abstract class TriangularLowerMatrix : TriangularMatrix
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
        protected TriangularLowerMatrix(int rows, int columns) : base(rows, columns)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TriangularLowerMatrix"/> class.
        /// </summary>
        /// <param name="order">
        /// The order of the matrix.
        /// </param>
        protected TriangularLowerMatrix(int order) : base(order)
        {
        }

        /// <summary>
        ///   Gets a value indicating whether this matrix is symmetric.
        /// </summary>
        /// <remarks>
        ///   A lower triangular matrix will only be symmetric if all values of the strictly lower triangle are zero, 
        ///   since by definition all values in the strictly upper triangle are zero. Hence, it will also be a diagonal matrix.
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

        /// <summary>
        /// Calculates the L1 norm.
        /// </summary>
        /// <returns>The L1 norm of the matrix.</returns>
        public override double L1Norm()
        {
            var norm = 0.0;
            for (var row = 0; row < RowCount; row++)
            {
                var s = 0.0;
                for (var column = 0; column <= row; column++)
                {
                    s += Math.Abs(AtUpper(row, column));
                }

                norm = Math.Max(norm, s);
            }

            return norm;
        }

        /// <summary>
        /// Calculates the infinity norm of this matrix.
        /// </summary>
        /// <returns>The infinity norm of this matrix.</returns>   
        public override double InfinityNorm()
        {
            var norm = 0.0;
            for (var row = 0; row < RowCount; row++)
            {
                var s = 0.0;
                for (var column = 0; column <= row; column++)
                {
                    s += Math.Abs(AtUpper(row, column));
                }

                norm = Math.Max(norm, s);
            }

            return norm;
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
            for (var row = 0; row < RowCount; row++)
            {
                for (var column = 0; column <= row; column++)
                {
                    result.AtLower(row, column, AtLower(row, column) + other.AtLower(row, column));
                }

                for (var column = row + 1; column < ColumnCount; column++)
                {
                    result.AtUpper(row, column, other.AtUpper(row, column));
                }
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
        protected void DoAdd(TriangularUpperMatrix other, Matrix<double> result)
        {
            for (var row = 0; row < RowCount; row++)
            {
                for (var column = 0; column < row; column++)
                {
                    result.AtLower(row, column, AtLower(row, column));
                }

                result.AtDiagonal(row, AtDiagonal(row) + other.AtDiagonal(row));

                for (var column = row + 1; column < ColumnCount; column++)
                {
                    result.AtUpper(row, column, other.AtUpper(row, column));
                }
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
        protected void DoAdd(TriangularLowerMatrix other, Matrix<double> result)
        {
            for (var row = 0; row < RowCount; row++)
            {
                for (var column = 0; column <= row; column++)
                {
                    result.AtLower(row, column, AtLower(row, column) + other.AtLower(row, column));
                }
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
        protected void DoAdd(DiagonalMatrix other, Matrix<double> result)
        {
            for (var row = 0; row < RowCount; row++)
            {
                for (var column = 0; column < row; column++)
                {
                    result.AtLower(row, column, AtLower(row, column));
                }

                result.AtDiagonal(row, AtDiagonal(row) + other.AtDiagonal(row));
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
            for (var row = 0; row < RowCount; row++)
            {
                for (var column = 0; column <= row; column++)
                {
                    result.AtLower(row, column, AtLower(row, column) - other.AtLower(row, column));
                }

                for (var column = row + 1; column < ColumnCount; column++)
                {
                    result.AtUpper(row, column, other.AtUpper(row, column));
                }
            }
        }
    }
}
