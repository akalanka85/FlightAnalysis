namespace FlightAnalysis
{
    public static class AsyncExtensions
    {
        /// <summary>
        /// Converts an <see cref="IAsyncEnumerable{T}"/> into a <see cref="List{T}"/> asynchronously.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the async enumerable.</typeparam>
        /// <param name="source">The asynchronous enumerable source to be converted.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains a <see cref="List{T}"/> with all elements from the source.
        /// </returns>
        public static async Task<List<T>> ToListAsync<T>(this IAsyncEnumerable<T> source)
        {
            var result = new List<T>();
            await foreach (var item in source)
            {
                result.Add(item);
            }
            return result;
        }
    }
}
