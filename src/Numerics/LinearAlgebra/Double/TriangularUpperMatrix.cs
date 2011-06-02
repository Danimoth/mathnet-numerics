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
    using Distributions;
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
        /// Calculates the L1 norm.
        /// </summary>
        /// <returns>The L1 norm of the matrix.</returns>
        public override double L1Norm()
        {
            var norm = 0.0;
            for (var column = 0; column < ColumnCount; column++)
            {
                var s = 0.0;
                for (var row = 0; row <= column; row++)
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
                for (var column = row; column < ColumnCount; column++)
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
                for (var column = 0; column < row; column++)
                {
                    result.At(row, column, other.At(row, column));
                }

                for (var column = row; column < ColumnCount; column++)
                {
                    result.At(row, column, AtUpper(row, column) + other.At(row, column));
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
                for (var column = row; column < ColumnCount; column++)
                {
                    result.At(row, column, AtUpper(row, column) + other.AtUpper(row, column));
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
                for (var column = 0; column < row; column++)
                {
                    result.At(row, column, other.AtLower(row, column));
                }

                result.At(row, row, AtDiagonal(row) + other.AtDiagonal(row));

                for (var column = row + 1; column < ColumnCount; column++)
                {
                    result.At(row, column, AtUpper(row, column));
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
        protected void DoAdd(TriangularUpperMatrix other, TriangularUpperMatrix result)
        {
            for (var row = 0; row < RowCount; row++)
            {
                for (var column = row; column < ColumnCount; column++)
                {
                    result.AtUpper(row, column, AtUpper(row, column) + other.AtUpper(row, column));
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
            if (triangularUpperOther == null)
            {
                for (var row = 0; row < RowCount; row++)
                {
                    for (var column = 0; column < row; column++)
                    {
                        result.At(row, column, other.At(row, column));
                    }

                    for (var column = row; column < ColumnCount; column++)
                    {
                        result.At(row, column, AtUpper(row, column) - other.At(row, column));
                    }
                }
            }
            else
            {
                for (var row = 0; row < RowCount; row++)
                {
                    for (var column = row; column < ColumnCount; column++)
                    {
                        result.At(row, column, AtUpper(row, column) - triangularUpperOther.AtUpper(row, column));
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
            for (var row = 0; row < RowCount; row++)
            {
                for (var column = row; column < ColumnCount; column++)
                {
                    result.At(row, column, AtUpper(row, column) * scalar);
                }
            }
        }

        /// <summary>
        /// Multiplies this matrix with a vector and places the results into the result vector.
        /// </summary>
        /// <param name="rightSide">The vector to multiply with.</param>
        /// <param name="result">The result of the multiplication.</param>
        protected override void DoMultiply(Vector<double> rightSide, Vector<double> result)
        {
            for (var row = 0; row < RowCount; row++)
            {
                var s = 0.0;
                for (var column = row; column < ColumnCount; column++)
                {
                    s += AtUpper(row, column) * rightSide[column];
                }

                result[row] = s;
            }
        }

        /// <summary>
        /// Left multiply a matrix with a vector ( = vector * matrix ) and place the result in the result vector.
        /// </summary>
        /// <param name="leftSide">The vector to multiply with.</param>
        /// <param name="result">The result of the multiplication.</param>
        protected override void DoLeftMultiply(Vector<double> leftSide, Vector<double> result)
        {
            for (var column = 0; column < ColumnCount; column++)
            {
                var s = 0.0;
                for (var row = 0; row <= column; row++)
                {
                    s += leftSide[row] * AtUpper(row, column);
                }

                result[column] = s;
            }
        }

        /// <summary>
        /// Multiplies this matrix with another matrix and places the results into the result matrix.
        /// </summary>
        /// <param name="other">The matrix to multiply with.</param>
        /// <param name="result">The result of the multiplication.</param>
        /// <remarks> 
        /// Multiplying two upper triangular matrices results in an upper triangular matrix. 
        /// </remarks>
        protected override void DoMultiply(Matrix<double> other, Matrix<double> result)
        {
            var triangularUpperOther = other as TriangularUpperMatrix;

            if (triangularUpperOther == null)
            {
                for (var row = 0; row < RowCount; row++)
                {
                    for (var columnOther = 0; columnOther < other.ColumnCount; columnOther++)
                    {
                        var s = 0.0;
                        for (var column = row; column < ColumnCount; column++)
                        {
                            s += AtUpper(row, column) * other.At(column, columnOther);
                        }

                        result.At(row, columnOther, s);
                    }
                }
            }
            else
            {
                for (var row = 0; row < RowCount; row++)
                {
                    for (var columnOther = row; columnOther < other.ColumnCount; columnOther++)
                    {
                        var s = 0.0;
                        for (var column = row; column < ColumnCount; column++)
                        {
                            s += AtUpper(row, column) * triangularUpperOther.AtUpper(column, columnOther);
                        }

                        result.At(row, columnOther, s);
                    }
                }
            }
        }
        
        /// <summary>
        /// Negate each element of this matrix and place the results into the result matrix.
        /// </summary>
        /// <param name="result">
        /// The result of the negation.
        /// </param>
        protected override void DoNegate(Matrix<double> result)
        {
            var triangularUpperResult = result as TriangularUpperMatrix;

            if (triangularUpperResult == null)
            {
                for (var row = 0; row < RowCount; row++)
                {
                    for (var column = row; column != ColumnCount; column++)
                    {
                        result.At(row, column,-AtUpper(row, column));
                    }
                }
            }
            else
            {
                for (var row = 0; row < RowCount; row++)
                {
                    for (var column = row; column != ColumnCount; column++)
                    {
                        triangularUpperResult.AtUpper(row, column, -AtUpper(row, column));
                    }
                }
            }
        }

        /// <summary>
        /// Pointwise multiplies this matrix with another matrix and stores the result into the result matrix.
        /// </summary>
        /// <param name="other">
        /// The matrix to pointwise multiply with this one.
        /// </param>
        /// <param name="result">
        /// The matrix to store the result of the pointwise multiplication.
        /// </param>
        protected override void DoPointwiseMultiply(Matrix<double> other, Matrix<double> result)
        {
            var triangularUpperOther = other as TriangularUpperMatrix;
            var triangularUpperResult = result as TriangularUpperMatrix;
            if (triangularUpperOther == null || triangularUpperResult == null)
            {
                base.DoPointwiseMultiply(other, result);
            }
            else
            {
                for (var row = 0; row < RowCount; row++)
                {
                    for (var column = row; column < ColumnCount; column++)
                    {
                        triangularUpperResult.AtUpper(row, column, AtUpper(row, column) * triangularUpperOther.AtUpper(row, column));
                    }
                }
            }
        }

        /// <summary>
        /// Pointwise divide this matrix by another matrix and stores the result into the result matrix.
        /// </summary>
        /// <param name="other">
        /// The matrix to pointwise divide this one by.
        /// </param>
        /// <param name="result">
        /// The matrix to store the result of the pointwise division.
        /// </param>
        protected override void DoPointwiseDivide(Matrix<double> other, Matrix<double> result)
        {
            var triangularUpperOther = other as TriangularUpperMatrix;
            var triangularUpperResult = result as TriangularUpperMatrix;
            if (triangularUpperOther == null || triangularUpperResult == null)
            {
                base.DoPointwiseDivide(other, result);
            }
            else
            {
                for (var row = 0; row < RowCount; row++)
                {
                    for (var column = row; column < ColumnCount; column++)
                    {
                        triangularUpperResult.AtUpper(row, column, AtUpper(row, column) / triangularUpperOther.AtUpper(row, column));
                    }
                }
            }
        }

        /// <summary>
        /// Computes the modulus for each element of the matrix.
        /// </summary>
        /// <param name="divisor">
        /// The divisor to use.
        /// </param>
        /// <param name="result">
        /// Matrix to store the results in.
        /// </param>
        protected override void DoModulus(double divisor, Matrix<double> result)
        {
            var triangularUpperResult = result as TriangularUpperMatrix;

            if (triangularUpperResult == null)
            {
                base.DoModulus(divisor, result);
            }
            else
            {
                for (var row = 0; row < RowCount; row++)
                {
                    for (var column = row; column < ColumnCount; column++)
                    {
                        triangularUpperResult.AtUpper(row, column, AtUpper(row, column) % divisor);
                    }
                }
            }
        }

        /// <summary>
        /// Populates a matrix with random elements.
        /// </summary>
        /// <param name="matrix">
        /// The matrix to populate.
        /// </param>
        /// <param name="distribution">
        /// Continuous Random Distribution to generate elements from.
        /// </param>
        protected override void DoRandom(Matrix<double> matrix, IContinuousDistribution distribution)
        {
            var triangularUpperMatrix = matrix as TriangularUpperMatrix;

            if (triangularUpperMatrix == null)
            {
                base.DoRandom(matrix, distribution);
            }
            else
            {
                for (var row = 0; row < matrix.RowCount; row++)
                {
                    for (var column = row; column < matrix.ColumnCount; column++)
                    {
                        triangularUpperMatrix.AtUpper(row, column, distribution.Sample());
                    }
                }
            }
        }

        /// <summary>
        /// Populates a matrix with random elements.
        /// </summary>
        /// <param name="matrix">
        /// The matrix to populate.
        /// </param>
        /// <param name="distribution">
        /// Continuous Random Distribution to generate elements from.
        /// </param>
        protected override void DoRandom(Matrix<double> matrix, IDiscreteDistribution distribution)
        {
            var triangularUpperMatrix = matrix as TriangularUpperMatrix;
            if (triangularUpperMatrix == null)
            {
                base.DoRandom(matrix, distribution);
            }
            else
            {
                for (var row = 0; row < matrix.RowCount; row++)
                {
                    for (var column = row; column < matrix.ColumnCount; column++)
                    {
                        triangularUpperMatrix.AtUpper(row, column, distribution.Sample());
                    }
                }
            }
        }
    }
}
