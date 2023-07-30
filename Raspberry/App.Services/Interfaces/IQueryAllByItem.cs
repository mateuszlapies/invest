namespace Raspberry.App.Services.Interfaces
{
    public interface IQueryAllByItem<T> where T : class
    {
        public IQueryable<T> GetAllByItem(long id);
    }
}
