using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biby
{
    /// <summary>
    /// Interface for computing universal hash codes on integeral numbers.
    /// </summary>
    /// <typeparam name="T">The numeric type.</typeparam>
    public interface IUniversalHash<T>
    {
        /// <summary>
        /// Computes a universal hash code.
        /// </summary>
        /// <param name="scalingFactor">The multiplicative constant.</param>
        /// <param name="additiveConstant">The additive constant.</param>
        /// <param name="bitCount">The number of bits in the resulting hash.</param>
        /// <returns>An integral universal hash code.</returns>
        [System.Diagnostics.Contracts.Pure]
        T GetHashCode(T scalingFactor, T additiveConstant, int bitCount);
    }
}
