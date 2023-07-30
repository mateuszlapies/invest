namespace Raspberry.App.Services.Interfaces
{
    public interface IQueryByItem<T> where T : class
    {
        public T GetByItem(long id);
    }
}
