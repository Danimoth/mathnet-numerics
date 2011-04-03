// <copyright file="SymmetricMatrixTests.cs" company="Math.NET">
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

namespace MathNet.Numerics.UnitTests.LinearAlgebraTests.Double
{
    using System;
    using Distributions;
    using LinearAlgebra.Double;
    using LinearAlgebra.Generic;
    using NUnit.Framework;

    /// <summary>
    /// Abstract class with the common set of matrix tests for symmetric matrices
    /// </summary>
    [TestFixture]
    public abstract partial class SymmetricMatrixTests : MatrixTests
    {
        /// <summary>
        /// Can transpose a matrix.
        /// </summary>
        /// <param name="name">Matrix name.</param>
        [Test, Sequential]
        public override sealed void CanTransposeMatrix([Values("Symmetric3x3")] string name)
        {
            var matrix = CreateMatrix(TestData2D[name]);
            var transpose = matrix.Transpose();

            Assert.AreSame(matrix, transpose);
        }

        /// <summary>
        /// Can check if a [,] array is symmetric. 
        /// </summary>
        /// <param name="name">Matrix name.</param>
        [Test, Sequential]
        public void CanCheckIfSymmetric([Values("Singular3x3", "Square3x3", "Square4x4", "Tall3x2", "Wide2x3", "Symmetric3x3")] string name)
        {
            var matrix = CreateMatrix(TestData2D[name]);
            var transpose = matrix.Trace();

            if (matrix.Equals(transpose))
            {
                Assert.IsTrue(SymmetricMatrix.CheckIfSymmetric(TestData2D[name]));
            }
            else
            {
                Assert.IsFalse(SymmetricMatrix.CheckIfSymmetric(TestData2D[name]));
            }
        }
    }
}
