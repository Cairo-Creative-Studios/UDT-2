namespace UDT.DataContainers
{
    public interface IStaticDataContainer<T> where T : Data
    {
        public static T Data { get; set; }
    }
}