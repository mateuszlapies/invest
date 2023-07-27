namespace Raspberry.App.Services.Interfaces
{
    public interface IDatabaseService<T> where T : class
    {
        public long Add(T entity);
        public T? Get(long id);
        public IList<T> GetAll();
        public long Update(T entity);
    }
}
