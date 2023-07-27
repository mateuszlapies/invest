namespace Raspberry.App.Services.Interfaces
{
    public interface IQueryByItem<T> where T : class
    {
        public IList<T> GetAllByItem(long id);
    }
}
