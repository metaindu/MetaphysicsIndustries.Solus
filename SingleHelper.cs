using System;

namespace MetaphysicsIndustries.Solus
{
    public static class SingleHelper
    {
        public static float Round(this float f)
        {
            return (float)Math.Round(f);
        }

        public static int RoundInt(this float f)
        {
            return (int)Math.Round(f);
        }
    }
}

