namespace fullstack_1.Data
{
    public class _embedded
    {


    }
    public class card
    {

    }
    public class AddressDTOPOST
    {
        public string id { get; set; }
        public string street { get; set; }
        public int number { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string postcode { get; set; }
        public AddressDTOPOST(string id, string street, int number, string city, string country, string postcode) 
        {
            this.id = id;
            this.street = street;
            this.number = number;
            this.city = city;
            this.country = country;
            this.postcode = postcode;
        }
   }

    public class Address
    {
        public string id { get; set; }
        public string street { get; set; }
        public int number { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string postcode { get; set; }
        public Address() { }
        public Address(AddressDTOPOST dto)
        {
            street = dto.street;
            number = dto.number;
            city = dto.city;
            country = dto.country;
            postcode = dto.postcode;
        }
        public Address(string street, int number, string city, string country, string postcode) 
        {
            this.street = street;
            this.number = number;
            this.city = city;
            this.country = country;
            this.postcode = postcode;
        }

        public Address(string street, int number)
        {
            this.street = street;
            this.number = number;
        }
   }
}
