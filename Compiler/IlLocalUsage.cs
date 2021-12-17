namespace MetaphysicsIndustries.Solus.Compiler
{
    public enum IlLocalUsage
    {
        /// <summary>
        /// The value of the variable will be taken from the baked
        /// environment when the compiled method starts executing, and stored
        /// in the localvar.
        /// </summary>
        BakedVariable,
    }
}