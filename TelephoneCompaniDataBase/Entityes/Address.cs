using TelephoneCompaniDataBase.Entityes.Base;

namespace TelephoneCompaniDataBase.Entityes
{
    public class Address : Entity
    {
        public string HouseNumber { get; set; }
        public int StreetId { get; set; }
        public int AbonentId {  get; set; }
    }
}
