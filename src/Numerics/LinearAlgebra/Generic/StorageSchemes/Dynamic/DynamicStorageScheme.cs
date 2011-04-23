// <copyright file="DynamicStorageScheme.cs" company="Math.NET">
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
    ///   Classes that completely manage dynamic storage scheme.
    /// </summary>
    /// <remarks>
    ///   A dynamic storage scheme is dependent on the actual matrix and therefore different for
    ///   every matrix. This category of storage scheme needs to manage the data array.
    /// </remarks>
    /// <typeparam name = "T">Supported data types are double, single, <see cref = "Complex" />, and <see cref = "Complex32" />.</typeparam>
    public abstract class DynamicStorageScheme<T> : StorageScheme
        where T : struct, IEquatable<T>, IFormattable
    {
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
        ///   This method is parameter checked. <see cref = "At(int,int)" /> and <see cref = "At(int,int,T)" /> to get values without parameter checking.
        /// </remarks>
        public abstract T this[int row, int column]
        {
            get;
            set;
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
        public abstract T At(int row, int column);

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
        public abstract void At(int row, int column, T value);


        /// <summary>
        ///    Gets the array containing the stored elements.
        ///  </summary>
        public abstract T[] Data
        {
            get;
        }
    }
}
