namespace Rich.DataContainers
{
    public interface IDataContainer<T> where T : Data
    {
        public T Data { get; set; }
    }
}