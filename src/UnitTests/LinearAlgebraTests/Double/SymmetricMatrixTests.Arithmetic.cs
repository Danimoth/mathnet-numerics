// <copyright file="SymmetricMatrixTests.Arithmetic.cs" company="Math.NET">
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
    using System.Collections.Generic;
    using Distributions;
    using LinearAlgebra.Double;
    using LinearAlgebra.Generic;
    using NUnit.Framework;

    /// <summary>
    /// Abstract class with the common set of matrix tests for symmetric matrices
    /// </summary>
    public abstract partial class SymmetricMatrixTests
    {
        /// <summary>
        /// Setup test matrices.
        /// </summary>
        [SetUp]
        public override void SetupMatrices()
        {
            TestData2D = new Dictionary<string, double[,]>
                         {
                             { "Singular3x3", new[,] { { 1.0, 1.0, 2.0 }, { 1.0, 1.0, 2.0 }, { 1.0, 1.0, 2.0 } } }, 
                             { "Square3x3", new[,] { { -1.1, -2.2, -3.3 }, { 0.0, 1.1, 2.2 }, { -4.4, 5.5, 6.6 } } }, 
                             { "Square4x4", new[,] { { -1.1, -2.2, -3.3, -4.4 }, { 0.0, 1.1, 2.2, 3.3 }, { 1.0, 2.1, 6.2, 4.3 }, { -4.4, 5.5, 6.6, -7.7 } } }, 
                             { "Singular4x4", new[,] { { -1.1, -2.2, -3.3, -4.4 }, { -1.1, -2.2, -3.3, -4.4 }, { -1.1, -2.2, -3.3, -4.4 }, { -1.1, -2.2, -3.3, -4.4 } } }, 
                             { "Tall3x2", new[,] { { -1.1, -2.2 }, { 0.0, 1.1 }, { -4.4, 5.5 } } }, 
                             { "Wide2x3", new[,] { { -1.1, -2.2, -3.3 }, { 0.0, 1.1, 2.2 } } }, 
                             { "Symmetric2x2", new[,] { { -1.1, -2.2 }, { -2.2, 3.3 } } }, 
                             { "Symmetric3x3", new[,] { { 1.0, 2.0, 3.0 }, { 2.0, 2.0, 0.0 }, { 3.0, 0.0, 3.0 } } },
                             { "Symmetric4x4", new[,] { { 1.0, 2.0, -3.0, 4.0 }, { 2.0, 5.0, -6.0, 7.0 }, { -3.0, -6.0, 8.0, 9.0 }, { 4.0, 7.0, 9.0, 10.0 } } }
                         };

            TestMatrices = new Dictionary<string, Matrix>();
            foreach (var name in TestData2D.Keys)
            {
                TestMatrices.Add(name, CreateMatrix(TestData2D[name]));
            }
        }

        /// <summary>
        /// Can multiply with a scalar.
        /// </summary>
        /// <param name="scalar">Scalar value.</param>
        [Test]
        public override void CanMultiplyWithScalar([Values(0, 1, 2.2)] double scalar)
        {
            var matrix = TestMatrices["Symmetric3x3"];
            var clone = matrix.Clone();
            clone = clone.Multiply(scalar);

            for (var i = 0; i < matrix.RowCount; i++)
            {
                for (var j = i; j < matrix.ColumnCount; j++)
                {
                    Assert.AreEqual(matrix[i, j] * scalar, clone[i, j]);
                    Assert.AreEqual(clone[i, j], clone[j, i]);
                }
            }
        }

        /// <summary>
        /// Can add a matrix.
        /// </summary>
        /// <param name="mtxA">Matrix A name.</param>
        /// <param name="mtxB">Matrix B name.</param>
        [Test, Sequential]
        public override void CanAddMatrix([Values("Symmetric4x4", "Symmetric4x4")] string mtxA, [Values("Symmetric4x4", "Square4x4")] string mtxB)
        {
            var matrixA = TestMatrices[mtxA];
            var matrixB = TestMatrices[mtxB];

            var matrix = matrixA.Clone();
            matrix = matrix.Add(matrixB);
            for (var i = 0; i < matrix.RowCount; i++)
            {
                for (var j = 0; j < matrix.ColumnCount; j++)
                {
                    Assert.AreEqual(matrix[i, j], matrixA[i, j] + matrixB[i, j]);
                }
            }
        }

        /// <summary>
        /// Can subtract a matrix.
        /// </summary>
        /// <param name="mtxA">Matrix A name.</param>
        /// <param name="mtxB">Matrix B name.</param>
        [Test, Sequential]
        public override void CanSubtractMatrix([Values("Symmetric4x4", "Symmetric4x4")] string mtxA, [Values("Symmetric4x4", "Square4x4")] string mtxB)
        {
            var matrixA = TestMatrices[mtxA];
            var matrixB = TestMatrices[mtxB];

            var matrix = matrixA.Clone();
            matrix = matrix.Subtract(matrixB);
            for (var i = 0; i < matrix.RowCount; i++)
            {
                for (var j = 0; j < matrix.ColumnCount; j++)
                {
                    Assert.AreEqual(matrix[i, j], matrixA[i, j] - matrixB[i, j]);
                }
            }
        }
    }
}
