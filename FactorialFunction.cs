using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class FactorialFunction : SingleArgumentFunction
    {
        private static Dictionary<int, float> _presets = new Dictionary<int, float>();

        static FactorialFunction()
        {
            _presets.Add(0,1);
            _presets.Add(1,1);
            _presets.Add(2,2);
            _presets.Add(3,6);
            _presets.Add(4,24);
            _presets.Add(5,120);
            _presets.Add(10, 3628800);

        }

        protected override Literal InternalCall(VariableTable varTable, Literal[] args)
        {
            float p = args[0].Value;

            if (p != (int)p) throw new ArgumentException("Argument must be an integer");

            return new Literal(GetValue((int)p));
        }

        private float GetValue(int p)
        {
            if (!_presets.ContainsKey(p))
            {
                _presets[p] = GetValue(p - 1);
            }

            return _presets[p];
        }

        public FactorialFunction()
        {
            Name = "Factorial";

            throw new NotImplementedException();
        }

        public override string DisplayName
        {
            get { return "!"; }
        }

        public override string ToString(ExpressionCollection arguments)
        {
            //treat as post-fix unary operator?

            return base.ToString(arguments);
        }
    }
}
