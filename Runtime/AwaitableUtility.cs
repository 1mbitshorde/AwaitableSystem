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

        /// <summary>
        /// Linearly interpolates between <paramref name="start"/> and <paramref name="final"/> by <paramref name="duration"/>.
        /// </summary>
        /// <param name="start">The start value.</param>
        /// <param name="final">The final value.</param>
        /// <param name="duration">The entire interpolation duration.</param>
        /// <param name="onUpdate">The update function. Use it to get the interpolation value.</param>
        /// <returns></returns>
        public static async Awaitable LerpAsync(float start, float final, float duration, Action<float> onUpdate)
        {
            var currentTime = 0F;
            while (currentTime < duration)
            {
                var step = currentTime / duration;
                var value = Mathf.Lerp(start, final, step);

                onUpdate?.Invoke(value);
                currentTime += Time.deltaTime;

                await Awaitable.NextFrameAsync();
            }
            onUpdate?.Invoke(final);
        }
    }
}