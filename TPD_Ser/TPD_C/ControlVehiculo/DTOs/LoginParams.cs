namespace TPD_C.ControlVehiculo.DTOs
{
    public class LoginParams
    {
        public string Grant_type { get; } = "password";

        public string Client_id { get; } = "mz-a3tek";

        public string Client_secret { get; } = "WJ4wUJo79qFsMm4T9Rj7dKw4";

        public string Scope { get; } = "openid mz6-api.all mz_username";

        public string Username { get; } = "API_DIAMANTE";

        public string Password { get; } = "D14M4NT32020";
    }
}
