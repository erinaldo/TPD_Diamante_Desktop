﻿namespace TPD_C.ControlVehiculo.DTOs
{
    public class Auth_token
    {
        public string Access_token { get; set; }

        public int Expires_in { get; set; }

        public string Token_type { get; set; }
    }
}
