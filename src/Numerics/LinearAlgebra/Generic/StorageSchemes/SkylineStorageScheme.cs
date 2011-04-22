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

namespace MathNet.Numerics.LinearAlgebra.Generic.StorageSchemes
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
    public class SkylineStorageScheme : StorageScheme, IDynamicStorageScheme<double>
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
        ///  Supportive array that stores the index on which the corresponding row starts in the data array.  
        /// </summary>
        /// <remarks> 
        /// The difference between two consecutive elements i, i + 1 of this array shows the number of strictly-above-the-diagonal elements of column i. 
        /// The length of this array is equal to the order of the matrix plus 1 (the plus 1 is needed so that we can store the height of the last column. 
        /// </remarks>
        private readonly int[] _rowIndexes;

        /// <summary>
        ///   Contains the stored elements. 
        /// </summary>
        private readonly double[] _data;
        
        /// <summary>
        ///   Length of the stored data. 
        /// </summary>
        private readonly int _dataLength;

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
            _rowIndexes = new int[order + 1];

            for (int i = 0; i < order; i++)
            {
                _rowIndexes[i + 1] = _rowIndexes[i] + i + 1;
            }

            _dataLength = _rowIndexes[_rowIndexes.Length - 1];
            _data = new double[_dataLength];
            
            int pos = 0;
            for (int j = 0; j < order; j++)
            {
                for (int i = j; i >= 0; i--)
                {
                    _data[pos] = dataArray[i, j];
                    pos++;
                }
            }
        }

        public void CalculateRowIndexes(double[,] dataArray)
        {
            // The first index is always 0 and the second is always 1. 
            _rowIndexes[1] = 1;

            for (var column = 1; column < Order; column++)
            {
                bool indexIsSet = false;
                for (var row = 0; row < column; row++)
                {
                    if (dataArray[row, column] == 0)
                    {
                        continue;
                    }

                    _rowIndexes[column + 1] = _rowIndexes[column] + column - row + 1;
                    indexIsSet = true;
                    break;
                }

                if (!indexIsSet)
                {
                    /* If we reach this point, no non-zero values exist above the diagonal of this column. 
                     * The diagonal element is treated separately because it must always be stored and to
                     * take into account the corner case of having a whole column full of zeros. */
                    _rowIndexes[column + 1] = _rowIndexes[column] + 1;
                }
            }
        }

        /// <summary>
        ///   Gets the length of the stored data.
        /// </summary>
        public override int DataLength
        {
            get
            {
                return _dataLength;
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        /// <summary>
        ///   Gets the array containing the stored elements.
        /// </summary>
        public double[] Data
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}
