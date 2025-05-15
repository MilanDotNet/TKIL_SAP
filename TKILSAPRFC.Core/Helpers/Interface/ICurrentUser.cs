using TKILSAPRFC.Core.Enums;

namespace TKILSAPRFC.Core.Helpers.Interface
{
    public interface ICurrentUser
    {
        int UserId { get; }
        string UserName { get; }
        string UserCode { get; }
        TokenType GrantType { get; }
        string UserDatabase { get; }
    }
}
