namespace ZeroLine.API.Helper
{
    public class ApiExceptions : ResponseAPI
    {
        public ApiExceptions(int statusCode, string message = null, string details = null) : base(statusCode, message)
        {
            this.details = details;
        }
        public string details { get; set; } 
    }
}
