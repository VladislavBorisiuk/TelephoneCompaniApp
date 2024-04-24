using TelephoneCompaniDataBase.Entityes.Base;

namespace TelephoneCompaniDataBase.Entityes
{
    internal class PhoneNumber : Entity
    {
        public string PhoneNumberString { get; set; }
        
        public string Type { get; set; }

        public int AbonentId { get; set; }
    }
}
