using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Text;

namespace AddressBookFileIO
{
    [Serializable]
    class AddressDetails
    {
        [NonSerialized]

        string nameOfAddressBook;

        // Constants
        const string ADD_CONTACT = "add";
        const string UPDATE_CONTACT = "update";
        const string SEARCH_CONTACT = "search";
        const string REMOVE_CONTACT = "remove";
        const string GET_ALL_CONTACTS = "view";

        // Collection Decleration
        public Dictionary<string, Address> addressBookList = new Dictionary<string, Address>();
        public static Dictionary<string, List<Person>> cityToContactMap = new Dictionary<string, List<Person>>();
        public static Dictionary<string, List<Person>> stateToContactMap = new Dictionary<string, List<Person>>();
        private Dictionary<string, List<Person>> cityToCOntactMapInstance;
        private Dictionary<string, List<Person>> stateToContactMapInstance;


        //get address book
        private Address GetAddressBook()
        {
            Console.WriteLine("\nEnter name of Address Book to be accessed or to be added");
            nameOfAddressBook = Console.ReadLine();

            // search for address book in dictionary
            if (addressBookList.ContainsKey(nameOfAddressBook))
            {
                Console.WriteLine("\nAddressBook Identified");
                return addressBookList[nameOfAddressBook];
            }

            // Offer to create a address book if not found in dictionary

            Console.WriteLine("\nAddress book not found. Type y to create a new address book or E to abort");

            // If user want to create a new address book
            if ((Console.ReadLine().ToLower()) == "y")
            {
                Address addressBook = new Address(nameOfAddressBook);
                addressBookList.Add(nameOfAddressBook, addressBook);
                Console.WriteLine("\nNew AddressBook Created");
                return addressBookList[nameOfAddressBook];
            }

            // If User want to abort the operation 
            else
            {
                Console.WriteLine("\nAction Aborted");
                return null;
            }
        }

        //search for city
        public void SearchInCity()
        {
            // Returns no record found if address book is empty
            if (addressBookList.Count == 0)
            {
                Console.WriteLine("\nNo record found");
                return;
            }

            // Get the name of city from user
            Console.WriteLine("\nEnter the city name to search for contact");
            string cityName = Console.ReadLine().ToLower();

            // If the city doesnt have any contacts
            if (!cityToContactMap.ContainsKey(cityName) || cityToContactMap[cityName].Count == 0)
            {
                Console.WriteLine("\nNo record found");
                return;
            }

            // Get the person name to be searched
            Console.WriteLine("\nEnter the person firstname to be searched");
            string firstName = Console.ReadLine().ToLower();
            Console.WriteLine("\nEnter the person lastname to be searched");
            string lastName = Console.ReadLine().ToLower();

            // Get the list of contacts whose city and name matches with search
            var searchResult = cityToContactMap[cityName].FindAll(contact => contact.firstName.ToLower() == firstName
                                                && contact.lastName.ToLower() == lastName);
            if (searchResult.Count == 0)
            {
                Console.WriteLine("\nNo record found");
                return;
            }
            Console.Write("\nThe contacts found in of given search are :");

            // print the list of contacts whose city and name matches with search
            searchResult.ForEach(contact => contact.toString());

        }


        internal void CountAllByCity()
        {
            // Returns no record found if address book is empty
            if (addressBookList.Count == 0)
            {
                Console.WriteLine("\nNo record found");
                return;
            }

            // To get count of contacts in all cities
            foreach (KeyValuePair<string, List<Person>> keyValuePair in cityToContactMap)
                Console.WriteLine("No of contacts in city {0} is {1}", keyValuePair.Key, keyValuePair.Value.Count());
        }


        public void ViewAllByCity()
        {
            if (addressBookList.Count == 0)
            {
                Console.WriteLine("\nNo record found");
                return;
            }

            // Get the name of city from user
            Console.WriteLine("\nEnter the city name to search for contact");
            string cityName = Console.ReadLine().ToLower();

            // If the given city doesnt exist
            if (!(cityToContactMap.ContainsKey(cityName)))
            {
                Console.WriteLine("\nNo contacts exist in the city");
                return;
            }

            // Print all contact details in city
            cityToContactMap[cityName].ForEach(contact => contact.toString());
        }

        //view all state
        public void ViewAllByState()
        {
            if (addressBookList.Count == 0)
            {
                Console.WriteLine("\nNo record found");
                return;
            }

            // Get the name of city from user
            Console.WriteLine("\nEnter the state name to search for contact");
            string stateName = Console.ReadLine().ToLower();

            // If the given city doesnt exist
            if (!(stateToContactMap.ContainsKey(stateName)))
            {
                Console.WriteLine("\nNo contacts exist in the state");
                return;
            }

            // To print details of all the contacts
            stateToContactMap[stateName].ForEach(contact => contact.toString());
        }

        //search all state
        internal void CountAllByState()
        {
            // Returns no record found if address book is empty
            if (addressBookList.Count == 0)
            {
                Console.WriteLine("\nNo record found");
                return;
            }

            // To get count of contacts in all cities
            foreach (KeyValuePair<string, List<Person>> keyValuePair in stateToContactMap)
                Console.WriteLine("Nunber of contacts in state {0} is {1}", keyValuePair.Key, keyValuePair.Value.Count());
        }

        //Search for state
        public void SearchInState()
        {
            // Returns no record found if address book is empty
            if (addressBookList.Count == 0)
            {
                Console.WriteLine("\nNo record found");
                return;
            }

            // Get the name of city from user
            Console.WriteLine("\nEnter the state name to search for contact");
            string stateName = Console.ReadLine().ToLower();

            // If the city doesnt have any contacts
            if (!stateToContactMap.ContainsKey(stateName) || stateToContactMap[stateName].Count == 0)
            {
                Console.WriteLine("\nNo record found");
                return;
            }

            // Get the person name to be searched
            Console.WriteLine("\nEnter the person firstname to be searched");
            string firstName = Console.ReadLine().ToLower();
            Console.WriteLine("\nEnter the person lastname to be searched");
            string lastName = Console.ReadLine().ToLower();

            // Get the list of contacts whose city and name matches with search
            var searchResult = stateToContactMap[stateName].FindAll(contact => contact.firstName.ToLower() == firstName
                                                                    && contact.lastName.ToLower() == lastName);

            // If no contacts exist
            if (searchResult.Count() == 0)
            {
                Console.WriteLine("\nNo contacts found of given search");
                return;
            }
            Console.Write("\nThe contacts found in of given search are :");

            // Display the search results
            searchResult.ForEach(contact => contact.toString());
        }

        //Delete address book
        public void DeleteAddressBook()
        {
            // Returns no record found if address book is empty
            if (addressBookList.Count == 0)
            {
                Console.WriteLine("No record found");
                return;
            }
            Console.WriteLine("\nEnter the name of address book to be deleted :");

            //search for address book with given name
            try
            {
                string addressBookName = Console.ReadLine();

                // Remove AddressBook with given name
                addressBookList.Remove(addressBookName);

                // Remove contacts from city dictionary
                foreach (KeyValuePair<string, List<Person>> keyValuePair in cityToContactMap)
                    cityToContactMap[keyValuePair.Key].RemoveAll(contact => contact.nameOfAddressBook == addressBookName);

                // Remove contacts from state dictionary
                foreach (KeyValuePair<string, List<Person>> keyValuePair in stateToContactMap)
                    stateToContactMap[keyValuePair.Key].RemoveAll(contact => contact.nameOfAddressBook == addressBookName);
                Console.WriteLine("Address book deleted successfully");

            }
            catch
            {
                Console.WriteLine("Address book not found");
            }
        }

        //view all address book
        public void ViewAllAddressBooks()
        {
            // Returns no record found if address book is empty
            if (addressBookList.Count() == 0)
            {
                Console.WriteLine("No record found");
                return;
            }

            // Print the address book names available
            Console.WriteLine("\nThe namesof address books available are :");
            foreach (KeyValuePair<string, Address> keyValuePair in addressBookList)
                Console.WriteLine(keyValuePair.Key);

        }

        //add or access addressbook
        public void AddOrAccessAddressBook()
        {
            // To get the name of the addressbook
            Address addressBook = GetAddressBook();

            // Returns no record found if address book is empty
            if (addressBook == null)
            {
                Console.WriteLine("Action aborted");
                return;
            }

            // select the action to be performed in address book   
            while (true)
            {
                Console.WriteLine("\nSelect from below to work on Address book {0}", addressBook.nameOfAddressBook);
                Console.WriteLine("\nType\n\nAdd - To add a contact" +
                                  "\nUpdate- To update a contact" +
                                  "\nView - To view all contacts" +
                                  "\nRemove - To remove a contact and " +
                                  "\nSearch- To search to get contact deatails\nE - To exit\n ");
                switch (Console.ReadLine().ToLower())
                {
                    case ADD_CONTACT:
                        addressBook.AddContact();
                        break;

                    case UPDATE_CONTACT:
                        addressBook.UpdateContact();
                        break;

                    case SEARCH_CONTACT:
                        addressBook.SearchContactDetails();
                        break;

                    case REMOVE_CONTACT:
                        addressBook.RemoveContact();
                        break;

                    case GET_ALL_CONTACTS:
                        addressBook.GetAllContacts();
                        break;

                    default:
                        Console.WriteLine("\nInvalid option. Exiting address book");
                        return;
                }

                // Ask the user to continue in same address book or to exit
                Console.WriteLine("\nType y to continue in same address Book or any other key to exit");

                // If not equal to y  then exit
                if (!(Console.ReadLine().ToLower() == "y"))
                {
                    Console.WriteLine("User exited the address book " + nameOfAddressBook);
                    return;
                }
            }
        }

        //add city
        public static void AddToCityDictionary(string cityName, Person contact)
        {
            // Check if the map already has city key
            if (!(cityToContactMap.ContainsKey(cityName)))
                cityToContactMap.Add(cityName, new List<Person>());

            // Add the contact to list of respective city map
            cityToContactMap[cityName].Add(contact);
        }

        //add contact to dictionary
        public static void AddToStateDictionary(string stateName, Person contact)
        {
            // Check if the map already has state key
            if (!stateToContactMap.ContainsKey(stateName))
                stateToContactMap.Add(stateName, new List<Person>());

            // Add the contact to list of respective city map
            stateToContactMap[stateName].Add(contact);
        }


        //get from file
        public static AddressDetails GetFromFile()
        {
            FileStream stream;
            string path = @"C:\Users\Kranthi\Desktop\Bridgelabz\Day27_File_IO\Day27_File_IO\TextFile1.txt";
            try
            {
                // Open the specified path
                // If path is not found then it throws file not found exception
                using (stream = new FileStream(path, FileMode.Open))
                {
                    BinaryFormatter formatter = new BinaryFormatter();

                    // Deserialize the data from file
                    // If stream is null then it throws Serialization exception
                    AddressDetails addressBookDetails = (AddressDetails)formatter.Deserialize(stream);

                    // Copy the details of instance variables to static
                    cityToContactMap = addressBookDetails.cityToCOntactMapInstance;
                    stateToContactMap = addressBookDetails.stateToContactMapInstance;
                    return addressBookDetails;
                };
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("file not found");
                return new AddressDetails();
            }
            catch (SerializationException)
            {
                Console.WriteLine("No previous records");
                return new AddressDetails();
            }
        }

        //write to file
        public void WriteToFile()
        {
            string path = @"C:\Users\Kranthi\Desktop\Bridgelabz\Day27_File_IO\Day27_File_IO\TextFile1.txt";
            FileStream stream = new FileStream(path, FileMode.OpenOrCreate);
            BinaryFormatter formatter = new BinaryFormatter();

            // Copy the details of static variables to instance to serialize them
            cityToCOntactMapInstance = cityToContactMap;
            stateToContactMapInstance = stateToContactMap;
            formatter.Serialize(stream, this.MemberwiseClone());
        }
    }
}
