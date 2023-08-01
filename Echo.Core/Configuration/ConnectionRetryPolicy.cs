using System;

namespace Echo.Core.Configuration
{
    public sealed record ConnectionRetryPolicy(bool IsEnabled, int MaxAttempts, TimeSpan DelayBetweenAttempts)
    {
        public const int InfiniteAttempts = 0;

        public static readonly ConnectionRetryPolicy Default = new ConnectionRetryPolicy(
                IsEnabled: true,
                MaxAttempts: InfiniteAttempts,
                DelayBetweenAttempts: TimeSpan.FromSeconds(5));
    }
}