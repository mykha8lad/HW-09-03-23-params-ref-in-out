using System;

namespace HW_09_03_23_params_ref_in_out
{
    public class Address
    {
        private string city;
        private string street;
        private string homeNumber;

        public Address(string city, string street, string homeNumber)
        {
            setCity(city);
            setStreet(street);
            setHomeNumber(homeNumber);
        }

        public void setCity(string city) { this.city = city; }
        public void setStreet(string street) { this.street = street; }
        public void setHomeNumber(string homeNumber) { this.homeNumber = homeNumber; }

        public string getCity() { return this.city; }
        public string getStreet() { return this.street; }
        public string getHomeNumber() { return this.homeNumber; }

        public override string ToString()
        {
            return ($"{getCity()}, {getStreet()} {getHomeNumber()}");
        }
    }
}
