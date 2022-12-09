namespace SkillProfi.Contacts
{
    public class ContactsTransfer
    {
        public ContactsTransfer(string adress, string phoneNumber, string email, string linkToMapContructor)
        {
            Adress = adress;
            PhoneNumber = phoneNumber;
            Email = email;
            LinkToMapContructor = linkToMapContructor;
        }

        public string Adress { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public string LinkToMapContructor { get; set; }
    }
}
