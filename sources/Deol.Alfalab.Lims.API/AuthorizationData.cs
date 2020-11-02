namespace Deol.Alfalab.Lims.API
{
    public class AuthorizationData
    {
        public string Sender { get; }
        public string Receiver { get; }
        public string Password { get; }

        public AuthorizationData (string sender, string receiver, string password)
        {
            this.Sender = sender;
            this.Receiver = receiver;
            this.Password = password;
        }
    }
}
