using System;
using UnityEngine;

namespace ActionCode.AwaitableSystem
{
    /// <summary>
    /// Utility class for <see cref="Awaitable"/>.
    /// </summary>
    public static class AwaitableUtility
    {
        /// <summary>
        /// Waits asynchronously until the given condition is true.
        /// </summary>
        /// <param name="condition">A boolean condition.</param>
        /// <returns>An asynchronously operation.</returns>
        public static async Awaitable WaitUntilAsync(Func<bool> condition)
        {
            while (!condition()) await Awaitable.NextFrameAsync();
        }

        /// <summary>
        /// Waits asynchronously until the given condition is false.
        /// </summary>
        /// <param name="condition">A boolean condition.</param>
        /// <returns>An asynchronously operation.</returns>
        public static async Awaitable WaitWhileAsync(Func<bool> condition)
        {
            while (condition()) await Awaitable.NextFrameAsync();
        }
    }
}