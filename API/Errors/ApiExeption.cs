namespace API.Errors
{
    public class ApiExeption : ApiResponse
    {
        public ApiExeption(int sttatusCode, string details=null,string message = null)
            : base(sttatusCode, message)
        {
            Details = details;
            Details = details;
        }

        public string Details { get; }
    }
}
