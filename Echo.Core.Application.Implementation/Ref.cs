﻿namespace Echo.Core
{
    public sealed class Ref<T>
    {
        public T Value;

        public Ref(T value)
        {
            Value = value;
        }
    }
}