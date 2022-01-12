using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AddressBookFileIO
{

    [Serializable]
    class Address
    {
        // Constants
        private const int UPDATE_FIRST_NAME = 1;
        private const int UPDATE_LAST_NAME = 2;
        private const int UPDATE_ADDRESS = 3;
        private const int UPDATE_CITY = 4;
        private const int UPDATE_STATE = 5;
        private const int UPDATE_ZIP = 6;
        private const int UPDATE_PHONE_NUMBER = 7;
        private const int UPDATE_EMAIL = 8;
        private const string PERSON_NAME = "name";

        // Variables
        private string firstName;
        private string lastName;
        private string address;
        private string city;
        private string state;
        private string zip;
        private string phoneNumber;
        private string email;
        private int contactSerialNum = 0;
        public string nameOfAddressBook = " ";

        // Object initialisation
        [NonSerialized]

        public List<Person> contactList = new List<Person>();

        //for storing name of address book
        public Address(string name)
        {
            nameOfAddressBook = name;
        }

        //to add contacts
        public void AddContact()
        {

            Console.WriteLine("\nEnter The First Name of Contact");
            firstName = Console.ReadLine();


            Console.WriteLine("\nEnter The Last Name of Contact");
            lastName = Console.ReadLine();


            Console.WriteLine("\nEnter The Address of Contact");
            address = Console.ReadLine();


            Console.WriteLine("\nEnter The City Name of Contact");
            city = Console.ReadLine().ToLower();


            Console.WriteLine("\nEnter The State Name of Contact");
            state = Console.ReadLine().ToLower();


            Console.WriteLine("\nEnter the Zip of Locality of Contact");
            zip = Console.ReadLine();


            Console.WriteLine("\nEnter The Phone Number of Contact");
            phoneNumber = Console.ReadLine();


            Console.WriteLine("\nEnter The Email of Contact");
            email = Console.ReadLine();

            // Creating an instance of contact with given details
            Person addNewContact = new Person(firstName, lastName, address, city, state, zip, phoneNumber, email, nameOfAddressBook);

            // Checking for duplicates with the equals method
            // Loop continues till the given contact not equal to any available contact
            while (addNewContact.Equals(contactList))
            {
                Console.WriteLine("contact already exists");


                // Giving option to user to re enter or to exit
                Console.WriteLine("Type Y to enter new name or any other key to exit");

                // If user wants to re-enter then taking input from user
                // Else return 
                if ((Console.ReadLine().ToLower() == "y"))
                {
                    Console.WriteLine("Enter new first name");
                    firstName = Console.ReadLine();
                    Console.WriteLine("Enter new last name");
                    lastName = Console.ReadLine();
                    addNewContact = new Person(firstName, lastName, address, city, state, zip, phoneNumber, email, nameOfAddressBook);
                }
                else
                    return;
            }


            contactList.Add(addNewContact);


            AddressDetails.AddToCityDictionary(city, addNewContact);


            AddressDetails.AddToStateDictionary(state, addNewContact);
            Console.WriteLine("\nContact added successfully");
        }

        //to search the contact details
        public void SearchContactDetails()
        {
            // If the List doesnt have any contacts
            // Else get the name to search for details
            if (contactList.Count() == 0)
            {
                Console.WriteLine("No saved contacts");
                return;
            }
            Console.WriteLine("\nEnter the name of candidate to get Details");
            string name = Console.ReadLine().ToLower();

            // Search the contact by name
            Person contact = SearchByName(name);

            // If contact doesnt exist
            if (contact == null)
            {
                Console.WriteLine("No record found");
                return;
            }

            // Print the details of the contact after search
            contact.toString();
        }

        /// <summary>
        /// Updates the contact.
        /// </summary>
        public void UpdateContact()
        {
            // If the List have no contacts
            if (contactList.Count() == 0)
            {
                Console.WriteLine("No saved contacts");
                return;
            }

            // Input the name to be updated
            Console.WriteLine("\nEnter the name of candidate to be updated");
            string name = Console.ReadLine();


            // Search the name
            Person contact = SearchByName(name);

            // If contact doesnt exist
            if (contact == null)
            {
                Console.WriteLine("No record found");
                return;
            }

            // To print details of searched contact
            contact.toString();

            int updateAttributeNum = 0;

            // Getting the attribute to be updated
            Console.WriteLine("\nEnter the row number attribute to be updated or 0 to exit");
            try
            {
                updateAttributeNum = Convert.ToInt32(Console.ReadLine());
                if (updateAttributeNum == 0)
                    return;
            }
            catch
            {
                Console.WriteLine("Invalid entry");
                return;
            }

            // Getting the new value of attribute
            Console.WriteLine("\nEnter the new value to be entered");
            string newValue = Console.ReadLine();

            // Updating selected attribute with selected value
            switch (updateAttributeNum)
            {
                case UPDATE_FIRST_NAME:


                    firstName = contact.firstName;

                    contact.firstName = newValue;

                    // If duplicate contact exists with that name then revert the operation
                    if (contact.Equals(contactList))
                    {
                        contact.firstName = firstName;
                        Console.WriteLine("Contact already exists with that name");
                        return;
                    }
                    break;
                case UPDATE_LAST_NAME:

                    // Store the LastName of given contact in variable
                    lastName = contact.lastName;

                    // Update the contact with given name
                    contact.lastName = newValue;

                    // If duplicate contact exists with that name then revert the operation
                    if (contact.Equals(contactList))
                    {
                        contact.lastName = lastName;
                        Console.WriteLine("Contact already exists with that name");
                        return;
                    }
                    break;
                case UPDATE_ADDRESS:
                    contact.address = newValue;
                    break;
                case UPDATE_CITY:

                    // Remove the contact from city dictionary
                    AddressDetails.cityToContactMap[contact.city].Remove(contact);

                    // Update the contact city
                    contact.city = newValue;

                    // Add to city dictionary
                    AddressDetails.AddToCityDictionary(contact.city, contact);
                    break;
                case UPDATE_STATE:

                    // Remove the contact from state dictionary
                    AddressDetails.stateToContactMap[contact.state].Remove(contact);

                    // Update the contact state
                    contact.state = newValue;

                    // Add to state dictionary
                    AddressDetails.AddToStateDictionary(contact.state, contact);
                    break;
                case UPDATE_ZIP:
                    contact.zip = newValue;
                    break;
                case UPDATE_PHONE_NUMBER:
                    contact.phoneNumber = newValue;
                    break;
                case UPDATE_EMAIL:
                    contact.email = newValue;
                    break;
                default:
                    Console.WriteLine("Invalid Entry");
                    return;
            }

            Console.WriteLine("\nUpdate Successful");
        }

        //to remove a particular contact
        public void RemoveContact()
        {
            // If the List does not have any contacts
            if (contactList.Count() == 0)
            {
                Console.WriteLine("No saved contacts");
                return;
            }

            // Input the name of the contact to be removed
            Console.WriteLine("\nEnter the name of contact to be removed");
            string name = Console.ReadLine().ToLower();


            // Search for the contact
            Person contact = SearchByName(name);


            if (contact == null)
            {
                Console.WriteLine("No record found");
                return;
            }

            // Print the details of contact for confirmation
            contact.toString();

            // Asking for confirmation to delete contact
            // If user says yes(y) then delete the contact
            Console.WriteLine("Press y to confirm delete or any other key to abort");
            switch (Console.ReadLine().ToLower())
            {
                case "y":
                    contactList.Remove(contact);
                    AddressDetails.cityToContactMap[contact.city].Remove(contact);
                    AddressDetails.stateToContactMap[contact.state].Remove(contact);
                    Console.WriteLine("Contact deleted");

                    break;
                default:
                    Console.WriteLine("Deletion aborted");
                    break;
            }
        }

        //to get all the contacts
        public void GetAllContacts()
        {
            // If the List does not have any contacts
            if (contactList.Count() == 0)
            {
                Console.WriteLine("\nNo saved contacts");
                return;
            }

            List<Person> listForSorting = null;

            // Get the order of contacts and sort accordingly
            Console.WriteLine("\n\nselect the sorting attribute of contacts :\n\nname\nAny other key for default order");
            switch (Console.ReadLine().ToLower())
            {
                case PERSON_NAME:
                    listForSorting = contactList.OrderBy(contact => (contact.firstName + contact.lastName)).ToList();
                    break;
                default:
                    listForSorting = contactList;
                    break;
            }
            // Display all contact details in list
            listForSorting.ForEach(contact => contact.toString());
        }

        //search by name of the person
        private Person SearchByName(string name)
        {

            if (contactList.Count == 0)
                return null;
            int numOfContactsSearched = 0;

            // storing the count of contacts with searched name string
            int numOfConatctsWithNameSearched = 0;


            foreach (Person contact in contactList)
            {
                // Incrementing the no of contacts searched
                numOfContactsSearched++;

                // If contact name matches exactly then it returns the index of that contact
                if ((contact.firstName + " " + contact.lastName).Equals(name))
                    return contact;

                // If a part of contact name matches then we would ask them to enter accurately
                if ((contact.firstName + " " + contact.lastName).Contains(name))
                {

                    // num of contacts having search string
                    numOfConatctsWithNameSearched++;
                    Console.WriteLine("\nname of contact is {0}", contact.firstName + " " + contact.lastName);
                }

                // If string is not part of any name then exit
                if (numOfContactsSearched == contactList.Count() && numOfConatctsWithNameSearched == 0)
                    return null;
            }

            // Ask to enter name accurately
            Console.WriteLine("\nInput the contact name as firstName lastName\n or E to exit");
            name = Console.ReadLine();

            // To exit
            if (name.ToLower() == "e")
                return null;

            // To continue search with new name
            return SearchByName(name);
        }
    }
}
