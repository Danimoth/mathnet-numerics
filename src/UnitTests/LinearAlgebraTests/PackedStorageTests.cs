// <copyright file="PackedStorageTests.cs" company="Math.NET">
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

namespace MathNet.Numerics.UnitTests.LinearAlgebraTests
{
    using LinearAlgebra.Double;
    using NUnit.Framework;

    /// <summary>
    /// Packed Storage tests.
    /// </summary>
    public class PackedStorageTests
    {
        /*
        /// <summary>
        /// Can find the Index of an element. 
        /// </summary>
        [Test]
        public void CanIndexOf()
        {
            var matrix = TestData2D["IndexTester4x4"];
            Assert.IsTrue(SymmetricMatrix.CheckIfSymmetric(matrix));

            for (var row = 0; row < matrix.GetLength(0); row++)
            {
                for (var column = 0; column < matrix.GetLength(1); column++)
                {
                    Assert.AreEqual(matrix[row, column], SymmetricDenseMatrix.IndexOf(row, column));
                }
            }
        }

        /// <summary>
        /// Can find the Index of an element in the upper triangle. 
        /// </summary>
        [Test]
        public void CanIndexOfUpper()
        {
            var matrix = TestData2D["IndexTester4x4"];

            for (var row = 0; row < matrix.GetLength(0); row++)
            {
                for (var column = row; column < matrix.GetLength(1); column++)
                {
                    Assert.AreEqual(matrix[row, column], SymmetricDenseMatrix.IndexOfUpper(row, column));
                }
            }
        }

        /// <summary>
        /// Can find the Index of an element in the lower triangle. 
        /// </summary>
        [Test]
        public void CanIndexOfLower()
        {
            var matrix = TestData2D["IndexTester4x4"];

            for (var row = 0; row < matrix.GetLength(0); row++)
            {
                for (var column = 0; column <= row; column++)
                {
                    Assert.AreEqual(matrix[row, column], SymmetricDenseMatrix.IndexOfLower(row, column));
                }
            }
        }

        /// <summary>
        /// Can find the Index of an element in the diagonal. 
        /// </summary>
        [Test]
        public void CanIndexOfDiagonal()
        {
            var matrix = TestData2D["IndexTester4x4"];

            for (var i = 0; i < matrix.GetLength(0); i++)
            {
                Assert.AreEqual(matrix[i, i], SymmetricDenseMatrix.IndexOfDiagonal(i));
            }
        }
         */
    }
}
