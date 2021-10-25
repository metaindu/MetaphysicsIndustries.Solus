using System;

namespace MetaphysicsIndustries.Solus.Exceptions
{
    public class InterrogationException : SolusException
    {
        public InterrogationException(
            string message, Exception innerException = null)
            : base(message, innerException)
        {
        }
    }
}