using System;
using System.Collections.Generic;
using System.Text;

namespace KataCheckout.Interfaces
{
    /// <summary>
    /// The clonable interface.
    /// originally called IClonable - turns out non generic interface
    /// already exists in system namespace...
    /// </summary>
    /// <typeparam name="T">The type.</typeparam>
    public interface ICanClone<T>
    {
        /// <summary>
        /// Clones object.
        /// </summary>
        /// <returns>The clone.</returns>
        T Clone();
    }
}
