using Arcturus.Abstract;
using System.ComponentModel;

namespace Arcturus.Concrete
{
    public class FieldMapper : IFieldMapper
    {
        public void SetField(object field, string name, string value)
        {
            var pi = field.GetType().GetProperty(name);
            
            if (pi == null) throw new PropertyNotFoundException(name);
            
            var pt = pi.PropertyType;
            var tc = TypeDescriptor.GetConverter(pt);
            var targetValue = tc.ConvertFromString(value);
            pi.SetValue(field, targetValue);
        }
    }
}
