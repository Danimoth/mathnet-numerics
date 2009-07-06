﻿// <copyright file="Combinatorics.cs" company="Math.NET">
// Math.NET Numerics, part of the Math.NET Project
// http://mathnet.opensourcedotnet.info
//
// Copyright (c) 2009 Math.NET
//
// Permission is hereby granted, free of charge, to any person
// obtaining a copy of this software and associated documentation
// files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use,
// copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following
// conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
// </copyright>

namespace MathNet.Numerics.Distributions
{
    using System.Collections.Generic;

    /// <summary>
    /// The interface for discrete univariate distributions.
    /// </summary>
    public interface IDiscreteDistribution : IDistribution
    {
        /// <summary>
        /// The mode of the distribution.
        /// </summary>
        int Mode { get; }

        /// <summary>
        /// The median of the distribution.
        /// </summary>
        int Median { get; }

        /// <summary>
        /// The smallest element in the domain of the distributions which can be represented by an integer.
        /// </summary>
        int Minimum { get; }

        /// <summary>
        /// The largest element in the domain of the distributions which can be represented by an integer.
        /// </summary>
        int Maximum { get; }

        /// <summary>
        /// Computes values of the probability mass function.
        /// </summary>
        /// <param name="k">The location in the domain where we want to evaluate the probability mass function.</param>
        double Probability(int k);

        /// <summary>
        /// Computes values of the log probability mass function.
        /// </summary>
        /// <param name="k">The location in the domain where we want to evaluate the log probability mass function.</param>
        double ProbabilityLn(int k);

        /// <summary>
        /// Draws a random sample from the distribution.
        /// </summary>
        int Sample();

        /// <summary>
        /// Draws a sequence of random samples from the distribution.
        /// </summary>
        IEnumerable<int> Samples();
    }
}
