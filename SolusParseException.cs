using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class SolusParseException : ApplicationException
    {
        public SolusParseException(SolusParser.Ex ex, string error)
        {
            _ex = ex;
            _error = error;
        }

        private SolusParser.Ex _ex;
        public SolusParser.Ex Ex
        {
            get { return _ex; }
        }

        private string _error;
        public string Error
        {
            get { return _error; }
        }

        public override string Message
        {
            get
            {
                return _error;
            }
        }
    }
}
