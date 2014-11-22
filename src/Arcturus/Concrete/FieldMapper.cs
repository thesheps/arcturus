using Arcturus.Abstract;
using AutoMapper;
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

        public void AddMapping<T1, T2>()
        {
            Mapper.CreateMap<T1, T2>();
            Mapper.CreateMap<T2, T1>();
        }

        public T Map<T>(object obj)
        {
            return Mapper.Map<T>(obj);
        }
    }
}