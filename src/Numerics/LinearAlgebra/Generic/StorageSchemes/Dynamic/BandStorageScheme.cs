// <copyright file="BandStorageScheme.cs" company="Math.NET">
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

    /// <summary>
    /// A class for managing the Band Storage scheme. 
    /// </summary>
    /// <remarks>
    /// <a href = "http://en.wikipedia.org/wiki/Band_matrix">Wikipedia - Band Matrix</a>
    /// A skyline matrix, also called "variable band matrix" is a generalization of band matrix 
    /// </remarks>
    public class BandStorageScheme : DynamicStorageScheme<double>
    {
        /// <summary>
        ///   This is a dummy index that is returned when requesting the index of an element that resides outside the stored region. 
        /// </summary>
        private const int DummyIndexOfUnstoredZeroElement = -1;

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
        public virtual int IndexOf(int row, int column)
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
        public virtual int IndexOfDiagonal(int row)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///   Gets the requested element.
        /// </summary>
        /// <param name = "row">
        ///   The row of the element.
        /// </param>
        /// <param name = "column">
        ///   The column of the element.
        /// </param>
        /// <remarks>
        ///   This method is parameter checked. <see cref = "DynamicStorageScheme{T}.At(int,int)" /> and <see cref = "DynamicStorageScheme{T}.At(int,int,T)" /> to get values without parameter checking.
        /// </remarks>
        public override double this[int row, int column]
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Retrieves the requested element without parameter checking.
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
        /// Gets the array containing the stored elements.
        /// </summary>
        public override double[] Data
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}
