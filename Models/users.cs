using System;
    public class users
    {
        public long id { get; set; }
        public string email { get; set; }
        public string encrypted_password { get; set; }
        public string reset_password_token { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }

}
