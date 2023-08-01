using System;

namespace Echo.Networking.NameResolution
{
    public readonly struct DnsHeader
    {
        public short Id { get; }
        public bool IsAuthorativeAnswer { get; }
        public bool IsRecursionDesired { get; }
        public bool IsRecursionAvailable { get; }
        public DnsDirection Direction { get; }
        public DnsOperation Operation { get; }
        public DnsResponseResult ResponseResult { get; }
        public byte QuestionCount { get; }
        public byte AnswerRecordCount { get; }
        public byte AuthorityRecordCount { get; }
        public byte AdditionalRecordCount { get; }

        public byte[] ToArray()
        {
            throw new NotImplementedException();
        }
    }
}