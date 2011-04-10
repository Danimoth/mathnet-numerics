// <copyright file="IExtraAccessors.cs" company="Math.NET">
// Math.NET Numerics, part of the Math.NET Project
// http://numerics.mathdotnet.com
// http://github.com/mathnet/mathnet-numerics
// http://mathnetnumerics.codeplex.com
//
// Copyright (c) 2009-2010 Math.NET
//
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

    /// <summary>
    /// Classes that have extra accessors. 
    /// </summary>
    /// <typeparam name="T">Supported data types are double, single, <see cref="Complex"/>, and <see cref="Complex32"/>.</typeparam>
    public interface IExtraAccessors<T> where T : struct, IEquatable<T>, IFormattable
    {
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
        T At(int row, int column);

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
        void At(int row, int column, T value);

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
        T AtUpper(int row, int column);

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
        void AtUpper(int row, int column, T value);

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
        T AtLower(int row, int column);

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
        void AtLower(int row, int column, T value);

        /// <summary>
        /// Retrieves the requested element without range checking. 
        /// </summary>
        /// <param name="row">
        /// The row=column of the diagonal element.
        /// </param>
        /// <returns>
        /// The requested element.
        /// </returns>
        T AtDiagonal(int row);

        /// <summary>
        /// Sets the value of the given element.
        /// </summary>
        /// <param name="row">
        /// The row=column of the diagonal element.
        /// </param>
        /// <param name="value">
        /// The value to set the element to.
        /// </param>
        void AtDiagonal(int row, T value);
    }
}
