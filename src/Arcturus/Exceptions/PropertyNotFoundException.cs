using System;

namespace Arcturus.Exceptions
{
    public class PropertyNotFoundException : ArgumentException
    {
        public PropertyNotFoundException(string argumentName)
            : base(argumentName)
        {
        }
    }
}