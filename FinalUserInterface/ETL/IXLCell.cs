
namespace ExcelToSQL
{
    internal class PoopCell
    {
        public object Address { get; internal set; }
        public object Value { get; internal set; }

        internal ReadOnlySpan<byte> GetValue<T>()
        {
            throw new NotImplementedException();
        }

        internal bool TryGetValue<T>(out T dateValue)
        {
            throw new NotImplementedException();
        }
    }
}