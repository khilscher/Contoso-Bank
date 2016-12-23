

namespace ContosoBankApp.ViewModels
{
    public class AuthenticationInfoViewModel
    {

        public string UserId { get; set; }
        public string UserName { get; set; }
        public string ExpiresOn { get; set; }
        public string TokenType { get; set; }
        public string IdToken { get; set; }

        public AuthenticationInfoViewModel()
        {
            UserId = $"User Id: {App.UserId}";
            UserName = $"UserName: {App.UserName}";
            ExpiresOn = $"Token Expires: {App.TokenExpiresOn}";
            TokenType = $"Token Type: {App.TokenType}";
            IdToken = $"{App.Token}";
        }

    }


}
