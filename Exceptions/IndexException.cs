using System;

namespace MetaphysicsIndustries.Solus.Exceptions
{
    public class IndexException : SolusException
    {
        public IndexException(string message, Exception innerException=null)
            : base(message, innerException)
        {
        }
    }
}