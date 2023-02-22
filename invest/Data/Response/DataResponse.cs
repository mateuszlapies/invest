using Hangfire.Storage.Monitoring;

namespace invest.Data.Response
{
    public class DataResponse<T> : BaseResponse
    {
        public T? Data { get; private set; }
        public DataResponse<T> Processed(T? data)
        {
            if (data == null)
            {
                Failed();
            }
            else
            {
                Succeeded();
                Data = data;
            }
            return this;
        }
    }
}
