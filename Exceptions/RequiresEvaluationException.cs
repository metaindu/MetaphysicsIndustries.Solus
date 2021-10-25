using System;

namespace MetaphysicsIndustries.Solus.Exceptions
{
    public class RequiresEvaluationException : InterrogationException
    {
        public RequiresEvaluationException(
            string message =
                "Interrogation would require the expression be evaluated",
            Exception innerException = null)
            : base(message, innerException)
        {
        }
    }
}