using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBClassSaveAndStuff
{
    internal class School
    {
        //school
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string SchoolType { get; set; }

        public School() { }

        public School(string name, string address, string city, string state, string zip, string phone, string email, string website, string schoolType)
        {
            Name = name;
            Address = address;
            City = city;
            State = state;
            Zip = zip;
            Phone = phone;
            Email = email;
            Website = website;
            SchoolType = schoolType;
        }

        public School(int id, string name, string address, string city, string state, string zip, string phone, string email, string website, string schoolType)
        {
            Id = id;
            Name = name;
            Address = address;
            City = city;
            State = state;
            Zip = zip;
            Phone = phone;
            Email = email;
            Website = website;
            SchoolType = schoolType;

        }
        public override string ToString()
        {
            return $"{Id} {Name} {Address} {City} {State} {Zip} {Phone} {Email} {Website} {SchoolType}";
        }
    }

}
