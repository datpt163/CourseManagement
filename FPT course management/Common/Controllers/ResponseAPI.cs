namespace FPT_course_management.Common.Controllers
{
    public class ResponseAPI
    {
        public int StatusCode {  get; set; }
        public string? Message { get; set; }
        public object? Data { get; set; }
    }
}
