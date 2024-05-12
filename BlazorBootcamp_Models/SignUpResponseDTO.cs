namespace BlazorBootcamp_Models
{
    public class SignUpResponseDTO
    {
        public bool IsRegistrationSuccesful { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
