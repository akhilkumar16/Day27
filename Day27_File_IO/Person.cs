using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace AddressBookFileIO
{
    [Serializable]
    public class Person
    {
        //declaration
        public string firstName;
        public string lastName;
        public string address;
        public string city;
        public string state;
        public string zip;
        public string phoneNumber;
        public string email;
        public string nameOfAddressBook;

        //new instance for reference
        public Person(string firstName, string lastName, string address, string city, string state, string zip,
                               string phoneNumber, string email, string nameOfAddressBook)
        {
            this.firstName = firstName.ToLower();
            this.lastName = lastName.ToLower();
            this.address = address;
            this.city = city;
            this.state = state;
            this.zip = zip;
            this.phoneNumber = phoneNumber;
            this.email = email;
            this.nameOfAddressBook = nameOfAddressBook;
        }

        //to specify whether the new object is equal to current obj or not
        public override bool Equals(Object obj)
        {
            // if the list is null
            if (obj == null)
                return false;
            try
            {
                // Get the contacts from list with same name
                var duplicates = ((List<Person>)obj).Find(contact => ((contact.firstName).ToLower() == (this.firstName).ToLower()
                                                                        && (contact.lastName).ToLower() == (this.lastName).ToLower()
                                                                        && contact.nameOfAddressBook == this.nameOfAddressBook));

                // Return true if duplicate is found else false
                if (duplicates != null)
                    return true;
                else
                    return false;
            }
            catch
            {
                // Get the contacts from list with same name
                var contact = ((Person)obj);
                return ((contact.firstName).ToLower() == (this.firstName).ToLower()
                        && (contact.lastName).ToLower() == (this.lastName).ToLower()
                        && contact.nameOfAddressBook == this.nameOfAddressBook);
            }
        }

        public void toString()
        {
            // For null contact
            if (this.nameOfAddressBook == null)
            {
                Console.WriteLine("No record found");
                return;
            }

            // Display all the atributes of contact
            int rowNum = 1;
            Console.WriteLine("\nname of contact is {0}", this.firstName + " " + lastName);
            Console.WriteLine("{0}-firstname is {1}", rowNum++, firstName);
            Console.WriteLine("{0}-lastname is {1}", rowNum++, lastName);
            Console.WriteLine("{0}-address is {1}", rowNum++, address);
            Console.WriteLine("{0}-city is {1}", rowNum++, city);
            Console.WriteLine("{0}-state is {1}", rowNum++, state);
            Console.WriteLine("{0}-zip is {1}", rowNum++, zip);
            Console.WriteLine("{0}-phoneNumber is {1}", rowNum++, phoneNumber);
            Console.WriteLine("{0}-email is {1}", rowNum++, email);
        }
    }
}