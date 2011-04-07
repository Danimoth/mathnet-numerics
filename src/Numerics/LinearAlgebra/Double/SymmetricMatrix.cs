// <copyright file="SymmetricMatrix.cs" company="Math.NET">
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
    using Properties;

    /// <summary>
    /// Symmetric <c>double</c> version of the <see cref="Matrix"/> class.
    /// </summary>
    [Serializable]
    public abstract class SymmetricMatrix : Matrix
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SymmetricMatrix"/> class.
        /// </summary>
        /// <param name="order">
        /// The order of the matrix.
        /// </param>
        protected SymmetricMatrix(int order)
            : base(order)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SymmetricMatrix"/> class.
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
        protected SymmetricMatrix(int rows, int columns)
            : this(rows)
        {
            if (rows != columns)
            {
                throw new ArgumentException(Resources.ArgumentMatrixSquare);
            }
        }

        #region Extra accessors for Symmetric Matrices. Used for efficiency. 
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
        public abstract double AtUpper(int row, int column);

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
        public abstract void AtUpper(int row, int column, double value);

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
        public abstract double AtLower(int row, int column);

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
        public abstract void AtLower(int row, int column, double value);

        /// <summary>
        /// Retrieves the requested element without range checking. 
        /// </summary>
        /// <param name="row">
        /// The row=column of the diagonal element.
        /// </param>
        /// <returns>
        /// The requested element.
        /// </returns>
        public abstract double AtDiagonal(int row);

        /// <summary>
        /// Sets the value of the given element.
        /// </summary>
        /// <param name="row">
        /// The row=column of the diagonal element.
        /// </param>
        /// <param name="value">
        /// The value to set the element to.
        /// </param>
        public abstract void AtDiagonal(int row, double value);
        #endregion

        /// <summary>
        /// Returns the transpose of this matrix. The transpose is equal and this method returns a reference to this matrix. 
        /// </summary>        
        /// <returns>The transpose of this matrix.</returns>
        public override sealed Matrix<double> Transpose()
        {
            return this;
        }

        /// <summary>
        /// Returns a value indicating whether the array is symmetric.  
        /// </summary>
        /// <param name="array">The array to check for symmetry. </param>
        /// <returns>True is array is symmetric, false if not symmetric. </returns>
        public static bool CheckIfSymmetric(double[,] array)
        {
            var rows = array.GetLength(0);
            var columns = array.GetLength(1);

            if (rows != columns)
            {
                return false;
            }

            for (var row = 0; row < rows; row++)
            {
                for (var column = 0; column < columns; column++)
                {
                    if (column >= row)
                    {
                        continue;
                    }

                    if (!array[row, column].Equals(array[column, row]))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Adds another matrix to this matrix.
        /// </summary>
        /// <param name="other">The matrix to add to this matrix.</param>
        /// <param name="result">The matrix to store the result of the addition.</param>
        /// <exception cref="ArgumentNullException">If the other matrix is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">If the two matrices don't have the same dimensions.</exception>
        protected override void DoAdd(Matrix<double> other, Matrix<double> result)
        {
            var symmetricOther = other as SymmetricMatrix;
            var symmetricResult = result as SymmetricMatrix;
            if (symmetricOther == null || symmetricResult == null)
            {
                base.DoAdd(other, result);
            }
            else
            {
                for (var row = 0; row < RowCount; row++)
                {
                    for (var column = row; column < ColumnCount; column++)
                    {
                        symmetricResult.AtUpper(row, column, At(row, column) + symmetricOther.AtUpper(row, column));
                    }
                }
            }
        }

        /// <summary>
        /// Subtracts another matrix from this matrix.
        /// </summary>
        /// <param name="other">The matrix to subtract to this matrix.</param>
        /// <param name="result">The matrix to store the result of subtraction.</param>
        /// <exception cref="ArgumentNullException">If the other matrix is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">If the two matrices don't have the same dimensions.</exception>
        protected override void DoSubtract(Matrix<double> other, Matrix<double> result)
        {
            var symmetricOther = other as SymmetricMatrix;
            var symmetricResult = result as SymmetricMatrix;
            if (symmetricOther == null || symmetricResult == null)
            {
                base.DoSubtract(other, result);
            }
            else
            {
                for (var row = 0; row < RowCount; row++)
                {
                    for (var column = row; column < ColumnCount; column++)
                    {
                        symmetricResult.AtUpper(row, column, At(row, column) - symmetricOther.AtUpper(row, column));
                    }
                }
            }
        }

        /// <summary>
        /// Multiplies each element of the matrix by a scalar and places results into the result matrix.
        /// </summary>
        /// <param name="scalar">The scalar to multiply the matrix with.</param>
        /// <param name="result">The matrix to store the result of the multiplication.</param>
        protected override void DoMultiply(double scalar, Matrix<double> result)
        {
            var symmetricResult = result as SymmetricMatrix;

            if (symmetricResult == null)
            {
                base.DoMultiply(scalar, result);
            }
            else
            {
                for (var row = 0; row < RowCount; row++)
                {
                    for (var column = row; column < ColumnCount; column++)
                    {
                        symmetricResult.AtUpper(row, column, At(row, column) * scalar);
                    }
                }
            }
        }

        /// <summary>
        /// Multiplies the transpose of this matrix with another matrix and places the results into the result matrix.
        /// </summary>
        /// <param name="other">The matrix to multiply with.</param>
        /// <param name="result">The result of the multiplication.</param>
        protected override sealed void DoTransposeThisAndMultiply(Matrix<double> other, Matrix<double> result)
        {
            DoMultiply(other, result);
        }

        /// <summary>
        /// Multiplies the transpose of this matrix with a vector and places the results into the result vector.
        /// </summary>
        /// <param name="rightSide">The vector to multiply with.</param>
        /// <param name="result">The result of the multiplication.</param>
        protected override sealed void DoTransposeThisAndMultiply(Vector<double> rightSide, Vector<double> result)
        {
            DoMultiply(rightSide, result);
        }

        /// <summary>
        /// Negate each element of this matrix and place the results into the result matrix.
        /// </summary>
        /// <param name="result">The result of the negation.</param>
        protected override void DoNegate(Matrix<double> result)
        {
            var symmetricResult = result as SymmetricMatrix;

            if (symmetricResult == null)
            {
                base.DoNegate(result);
            }
            else
            {
                for (var row = 0; row < RowCount; row++)
                {
                    for (var column = row; column != ColumnCount; column++)
                    {
                        symmetricResult[row, column] = -AtUpper(row, column);
                    }
                }
            }
        }

        /// <summary>
        /// Pointwise multiplies this matrix with another matrix and stores the result into the result matrix.
        /// </summary>
        /// <param name="other">The matrix to pointwise multiply with this one.</param>
        /// <param name="result">The matrix to store the result of the pointwise multiplication.</param>
        protected override void DoPointwiseMultiply(Matrix<double> other, Matrix<double> result)
        {
            var symmetricOther = other as SymmetricMatrix;
            var symmetricResult = result as SymmetricMatrix;
            if (symmetricOther == null || symmetricResult == null)
            {
                base.DoPointwiseMultiply(other, result);
            }
            else
            {
                for (var row = 0; row < RowCount; row++)
                {
                    for (var column = row; column < ColumnCount; column++)
                    {
                        symmetricResult.AtUpper(row, column, At(row, column) * symmetricOther.AtUpper(row, column));
                    }
                }
            }
        }

        /// <summary>
        /// Pointwise divide this matrix by another matrix and stores the result into the result matrix.
        /// </summary>
        /// <param name="other">The matrix to pointwise divide this one by.</param>
        /// <param name="result">The matrix to store the result of the pointwise division.</param>
        protected override void DoPointwiseDivide(Matrix<double> other, Matrix<double> result)
        {
            var symmetricOther = other as SymmetricMatrix;
            var symmetricResult = result as SymmetricMatrix;
            if (symmetricOther == null || symmetricResult == null)
            {
                base.DoPointwiseDivide(other, result);
            }
            else
            {
                for (var row = 0; row < RowCount; row++)
                {
                    for (var column = row; column < ColumnCount; column++)
                    {
                        symmetricResult.AtUpper(row, column, At(row, column) / symmetricOther.AtUpper(row, column));
                    }
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether this matrix is symmetric.
        /// </summary>
        public override bool IsSymmetric
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Computes the modulus for each element of the matrix.
        /// </summary>
        /// <param name="divisor">The divisor to use.</param>
        /// <param name="result">Matrix to store the results in.</param>
        protected override void DoModulus(double divisor, Matrix<double> result)
        {
            var symmetricResult = result as SymmetricMatrix;

            if (symmetricResult == null)
            {
                base.DoModulus(divisor, result);
            }
            else
            {
                for (var row = 0; row < RowCount; row++)
                {
                    for (var column = row; column < ColumnCount; column++)
                    {
                        symmetricResult.AtUpper(row, column, At(row, column) % divisor);
                    }
                }
            }
        }

        /// <summary>
        /// Populates a matrix with random elements.
        /// </summary>
        /// <param name="matrix">The matrix to populate.</param>
        /// <param name="distribution">Continuous Random Distribution to generate elements from.</param>
        protected override void DoRandom(Matrix<double> matrix, IContinuousDistribution distribution)
        {
            var symmetricMatrix = matrix as SymmetricMatrix;

            if (symmetricMatrix == null)
            {
                base.DoRandom(matrix, distribution);
            }
            else
            {
                for (var row = 0; row < matrix.RowCount; row++)
                {
                    for (var column = row; column < matrix.ColumnCount; column++)
                    {
                        symmetricMatrix.AtUpper(row, column, distribution.Sample());
                    }
                }
            }
        }

        /// <summary>
        /// Populates a matrix with random elements.
        /// </summary>
        /// <param name="matrix">The matrix to populate.</param>
        /// <param name="distribution">Continuous Random Distribution to generate elements from.</param>
        protected override void DoRandom(Matrix<double> matrix, IDiscreteDistribution distribution)
        {
            var symmetricMatrix = matrix as SymmetricMatrix;

            if (symmetricMatrix == null)
            {
                base.DoRandom(matrix, distribution);
            }
            else
            {
                for (var row = 0; row < matrix.RowCount; row++)
                {
                    for (var column = row; column < matrix.ColumnCount; column++)
                    {
                        symmetricMatrix.AtUpper(row, column, distribution.Sample());
                    }
                }
            }
        }
    }
}
