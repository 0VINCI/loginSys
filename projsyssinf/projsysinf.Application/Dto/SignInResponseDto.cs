using profsysinf.Core.Aggregates;

namespace projsysinf.Application.Dto
{
    public class SignInResponseDto
    {
        public int UserId { get; set; }
        public string Token { get; set; }
        public IEnumerable<LogEntryDto> History { get; set; }
    }
}
