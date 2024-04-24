using TelephoneCompaniDataBase.Entityes.Base;

namespace TelephoneCompaniDataBase.Entityes
{
    internal class Address : Entity
    {
        public string HouseNumber { get; set; }
        public int StreetId { get; set; }
        public int AbonentId {  get; set; }
    }
}
