namespace invest.Data.Response
{
    public class BaseResponse
    {
        public bool Success { get; private set; }

        public BaseResponse Failed()
        {
            Success = false;
            return this;
        }

        public BaseResponse Succeeded()
        {
            Success = true;
            return this;
        }
    }
}
