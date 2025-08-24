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
        /// <param name="getValue">The update function. Use it to get the interpolation value.</param>
        /// <param name="speed">The interpolation speed.</param>
        /// <returns>An asynchronously lerp operation.</returns>
        public static async Awaitable LerpAsync(float start, float final, float duration, Action<float> getValue, float speed = 1F) =>
            await InterpolateAsync(duration, getValue, value => Mathf.Lerp(start, final, value), speed);

        /// <summary>
        /// Linearly interpolates between <paramref name="start"/> and <paramref name="final"/> colors by <paramref name="duration"/>.
        /// </summary>
        /// <param name="start">The start color.</param>
        /// <param name="final">The final color.</param>
        /// <param name="duration">The entire color interpolation duration.</param>
        /// <param name="getValue">The update function. Use it to get the interpolation color.</param>
        /// <param name="speed">The color interpolation speed.</param>
        /// <returns>An asynchronously color lerp operation.</returns>
        public static async Awaitable LerpAsync(Color start, Color final, float duration, Action<Color> getValue, float speed = 1F) =>
            await InterpolateAsync(duration, getValue, value => Color.Lerp(start, final, value), speed);

        /// <summary>
        /// Linearly interpolates using <paramref name="setValue"/> method by <paramref name="duration"/>.
        /// </summary>
        /// <typeparam name="T">The interpolation type.</typeparam>
        /// <param name="duration">The entire interpolation duration.</param>
        /// <param name="getValue">The get value function. Use it to get the interpolation value.</param>
        /// <param name="setValue">The set value function. Use it to set the interpolation value.</param>
        /// <param name="speed">The interpolation speed.</param>
        /// <returns>An asynchronously interpolation operation.</returns>
        public static async Awaitable InterpolateAsync<T>(float duration, Action<T> getValue, Func<float, T> setValue, float speed)
        {
            var currentTime = 0F;
            while (currentTime < duration)
            {
                var step = currentTime / duration;
                var value = setValue(step);

                getValue?.Invoke(value);
                currentTime += Time.deltaTime * speed;

                await Awaitable.NextFrameAsync();
            }
            getValue?.Invoke(setValue(1F));
        }
    }
}