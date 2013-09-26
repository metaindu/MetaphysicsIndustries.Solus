using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class SolusParseException : ApplicationException
    {
        public SolusParseException(int location, string error)
        {
            Location = location;
            _error = error;
        }

        public int Location;

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
