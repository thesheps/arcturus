namespace Arcturus.Abstract
{
    public interface IFieldMapper
    {
        void SetField(object field, string name, string value);
        void AddMapping<T1, T2>();
        T Map<T>(object obj);
    }
}