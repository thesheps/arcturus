using System;

namespace Arcturus.Concrete
{
    public class PropertyNotFoundException : ArgumentException
    {
        public PropertyNotFoundException(string argumentName)
            : base(argumentName)
        {
        }
    }
}