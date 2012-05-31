﻿// <copyright file="StaticStorageScheme.cs" company="Math.NET">
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
namespace MathNet.Numerics.LinearAlgebra.Generic.StorageSchemes.Static
{
    /// <summary>
    ///   Classes that contain indexing information of a static storage scheme.
    /// </summary>
    /// <remarks>
    ///   A static storage scheme always the same and may only depend on the size of the matrix. 
    ///   This category of storage scheme does not need to manage the data array. Therefore, for
    ///   efficiency, it only provides indexes of the matrix elements.
    /// </remarks>
    public abstract class StaticStorageScheme : IStorageScheme
    {
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
        ///   This method is parameter checked. <see cref = "IStorageScheme.IndexOf(int,int)" /> and <see cref = "IStorageScheme.IndexOfDiagonal(int)" /> to get values without parameter checking.
        /// </remarks>
        public abstract int this[int row, int column]
        {
            get;
        }

        /// <summary>
        ///    Gets the length of the stored data.
        /// </summary>
        public abstract int DataLength
        {
            get;
        }

        /// <summary>
        ///  Retrieves the index of the requested element without parameter checking.
        ///  </summary><param name="row">
        ///  The row of the element. 
        ///  </param><param name="column">
        ///  The column of the element. 
        ///  </param><returns>
        ///  The requested index. 
        ///  </returns>
        public abstract int IndexOf(int row, int column);

        /// <summary>
        ///  Retrieves the index of the requested diagonal element without parameter checking.
        ///  </summary><param name="row">
        ///  The row=column of the diagonal element. 
        ///  </param><returns>
        ///  The requested index. 
        ///  </returns>
        public abstract int IndexOfDiagonal(int row);
    }
}