namespace AboutusSpace;
    class Aboutus
    {
        public string Text{get;private set;}
        public string Email { get; private set; }
        public string PhoneNumber { get; private set; }
        public Aboutus(string email, string phoneNumber, string text)
        {
            Email = email;
            PhoneNumber = phoneNumber;
            Text = text;
        }
        public override string ToString()
        {
            return $"{Email}\n" +
                   $"{PhoneNumber}\n" +
                   $"{Text}\n";
        }
    }
