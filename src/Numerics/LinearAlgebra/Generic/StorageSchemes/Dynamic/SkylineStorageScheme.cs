// <copyright file="SkylineStorageScheme.cs" company="Math.NET">
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

namespace MathNet.Numerics.LinearAlgebra.Generic.StorageSchemes.Dynamic
{
    using System;
    using Properties;

    /// <summary>
    /// A class for managing indexing when using Skyline Storage scheme. 
    /// </summary>
    /// <remarks>
    /// <a href = "http://en.wikipedia.org/wiki/Skyline_matrix">Wikipedia - Skyline Matrix</a>
    /// Commonly referenced as skyline matrix, or variable band matrix, or envelope storage scheme, or active column. 
    /// </remarks>
    public class SkylineStorageScheme : DynamicStorageScheme<double>
    {
        /// <summary>
        ///   This is a dummy index that is returned when requesting the index of an unknown element. 
        /// </summary>
        private const int DummyIndexOfUnstoredZeroElement = -1;

        /// <summary>
        ///   Number of rows or columns.
        /// </summary>
        /// <remarks>
        ///   Using this instead of a property to speed up calculating a matrix index in the data array.
        /// </remarks>
        protected readonly int Order;
        
        /// <summary>
        ///  Supportive array that stores the index of each column's diagonal element in the data array. 
        /// </summary>
        /// <remarks> 
        /// The difference between two consecutive elements (i), (i + 1) of this array minus 1 shows the number 
        /// of strictly-above-the-diagonal elements of column i. This is the height of the column. 
        /// The length of this array is equal to the order of the matrix plus 1 (the plus 1 is needed so that we can store the height of the last column. 
        /// </remarks>
        private readonly int[] _diagonalIndexes;

        /// <summary>
        ///   Contains the stored elements. 
        /// </summary>
        private double[] _data;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="SkylineStorageScheme"/> class.
        /// </summary>
        /// <param name="dataArray">
        /// The data array.
        /// </param>
        /// <exception cref="ArgumentException">
        ///   If the array is not square. 
        /// </exception>
        /// <remarks> Current implementation saves the whole upper triangle matrix. </remarks>
        public SkylineStorageScheme(double[,] dataArray)
        {
            int order = dataArray.GetLength(0);

            if (dataArray.GetLength(1) != order)
            {
                throw new ArgumentException(Resources.ArgumentMatrixSquare);
            }

            Order = order;
            _diagonalIndexes = new int[order + 1];

            StoreWholeUpperTriangle(dataArray);
        }

        /// <summary>
        /// Stores the whole upper triangle of the array. Builds the corresponding indexes as well. 
        /// </summary>
        /// <param name="dataArray">The data array.</param>
        public void StoreWholeUpperTriangle(double[,] dataArray)
        {
            for (int column = 0; column < Order; column++)
            {
                _diagonalIndexes[column + 1] = _diagonalIndexes[column] + column + 1;
            }

            _data = new double[_diagonalIndexes[_diagonalIndexes.Length - 1]];
            
            int pos = 0;
            for (int column = 0; column < Order; column++)
            {
                for (int row = column; row >= 0; row--)
                {
                    _data[pos] = dataArray[row, column];
                    pos++;
                }
            }
        }

        /// <summary>
        ///   Calculates the diagonal indexes by scanning the matrix and figuring out how high each column is. 
        /// </summary>
        /// <param name="dataArray">The data array.</param>
        public void CalculateDiagonalIndexes(double[,] dataArray)
        {
            // The first index is always 0 and the second is always 1. 
            _diagonalIndexes[1] = 1;

            for (var column = 1; column < Order; column++)
            {
                bool indexIsSet = false;
                for (var row = 0; row < column; row++)
                {
                    if (dataArray[row, column] == 0)
                    {
                        continue;
                    }

                    _diagonalIndexes[column + 1] = _diagonalIndexes[column] + column - row + 1;
                    indexIsSet = true;
                    break;
                }

                if (!indexIsSet)
                {
                    /* If we reach this point, no non-zero values exist above the diagonal of this column. 
                     * The diagonal element is treated separately because it must always be stored and to
                     * take into account the corner case of having a whole column full of zeros. */
                    _diagonalIndexes[column + 1] = _diagonalIndexes[column] + 1;
                }
            }
        }

        /// <summary>
        /// Writes the skyline data array.
        /// </summary>
        /// <param name="dataArray">The data array.</param>
        public void WriteSkylineDataArray(double[,] dataArray)
        {
            _data = new double[_diagonalIndexes[Order + 1] - 1];

            int pos = -1;
            for (int column = 0; column < Order; column++)
            {
                int columnHeight = _diagonalIndexes[column + 1] - _diagonalIndexes[column] - 1;
                int indexOfHighestStoredRow = column - columnHeight;

                for (int row = column; row >= indexOfHighestStoredRow; row--)
                {
                    pos++;
                    _data[pos] = dataArray[row, column];
                }
            }
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
        ///   This method is parameter checked. <see cref = "StorageScheme.IndexOf" /> and <see cref = "StorageScheme.IndexOfDiagonal" /> to get values without parameter checking.
        /// </remarks>
        public override int this[int row, int column]
        {
            get
            {
                throw new NotImplementedException();
            }
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
        public override int IndexOf(int row, int column)
        {
            int columnHeight = _diagonalIndexes[column + 1] - _diagonalIndexes[column] - 1;
            int indexOfHighestStoredRow = column - columnHeight;

            if (row < indexOfHighestStoredRow)
            {
                return DummyIndexOfUnstoredZeroElement;
            }

            return _diagonalIndexes[column] + column - row;
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
            return _diagonalIndexes[row];
        }

        /// <summary>
        ///   Gets the array containing the stored elements.
        /// </summary>
        public override double[] Data
        {
            get
            {
                return _data;
            }
        }
    }
}
