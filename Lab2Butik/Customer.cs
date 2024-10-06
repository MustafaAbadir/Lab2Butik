using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2Butik
{
    public class Customer
    {
        public string CustomerName { get; private set; }
        public string Password { get; private set; }

        public List<Product> Cart;

        // Initializes user with a username, password and shopping cart as an empty list
        public Customer(string name, string password)
        {
            CustomerName = name;
            Password = password;
            Cart = new List<Product>();
        }

        public static string GetValidCustomerInput(string promptMessage)
        {
            string input;
            while (true)
            {
                Console.WriteLine(promptMessage);
                input = Console.ReadLine();

                // check if the input is null or empty
                if (!string.IsNullOrEmpty(input))
                {
                    // customer put in valid password and name.
                    break;
                }
                else
                {
                    Console.WriteLine("Password or name cannot be empty. Please try again.");
                }
            }
            return input;
        }
        // Verifies user name and password before login in
        public static Customer Login(List<Customer> customer)
        {

            Console.Clear();

            // get a valid name
            string name = GetValidCustomerInput("Enter your name: ");
            // get a valid password
            string password = GetValidCustomerInput("Enter your password: ");

            // check if a customer with entered name and password that already exists
            Customer existingCustomer = customer.Find(c => c.CustomerName == name);

            if (existingCustomer != null)
            {

                // loop until correct password is given 
                while (true)
                {
                    //customer wrote an existing name and password
                    if (existingCustomer.Password == password)
                    {
                        return existingCustomer;
                    }
                    else
                    {
                        Console.WriteLine("\nIncorrect name or password. Please try again\n");
                        password = GetValidCustomerInput("Enter your password: ");
                    }
                }
            }
            // customer wrote a name that does not exist
            else
            {
                Console.WriteLine("\nThis customer does not exist.\n");
                Console.Write("Do you wish to register a new customer? \n1.{0}\n2.{1}", " Yes ", " No \nSelect an option: ");

                string customerChoice = Console.ReadLine();

                if (customerChoice == "1")
                {
                    // allow customer to register
                    return Register(customer);
                }
                else
                {
                    return null;
                }
            }
        }

        // Creates a new customer with a unique name and password and add to the list. 
        public static Customer Register(List<Customer> customer)
        {
            Console.Clear();

            // get a valid name
            string name = GetValidCustomerInput("Create name: ");

            // get a valid password
            string password = GetValidCustomerInput("Create password: ");
            while (true)
            {

                // check if a customer with the same username and password as an existing customer tries to register
                bool isOldCustomer = customer.Exists(c => c.CustomerName == name && c.Password == password);

                if (isOldCustomer)
                {
                    Console.Write("\nAn account with this name and password already exists. Please click Enter to return to Main Menu and log in instead. ");
                    Console.ReadKey();
                    return null;

                }
                else
                {
                    // create new user
                    var newCustomer = new Customer(name, password);
                    customer.Add(newCustomer);
                    // return the newly registered user
                    return newCustomer;
                }

            }
        }

        // Overrides ToString() method to print customer name, password and shopping cart
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Name: {CustomerName}");
            sb.AppendLine($"Password: {Password}");
            sb.Append("Shopping caart: ");
            if (Cart.Count == 0)
            {
                sb.Append("Your cart is empty");
            }
            else
            {
                double grandTotal = 0;
                sb.AppendLine("\n");
                foreach (var product in Cart)
                {
                    double productTotalPrice = product.CalcultateTotalPrice();
                    sb.AppendLine($"{product.ProductName} - {product.Price} x {product.Quantity} = {productTotalPrice} kr");
                    grandTotal += productTotalPrice;
                }
                sb.AppendLine("-----------------------------------");
                sb.AppendLine($"Grand Total: {grandTotal} kr");
            }
            return sb.ToString();
        }

        // Adds an item to the cart or increase the quantity if the product already exists
        public void AddToCart(Product product, int quantity)
        {
            Product productItem = Cart.Find(p => p.ProductName == product.ProductName);

            if (productItem != null)
            {
                // if product already exists in the cart then increase the quantity
                productItem.Quantity++;
            }
            else
            {
                // set the quantity for the item from user input
                product.Quantity = quantity;
                // add new product to the cart
                Cart.Add(product);
            }
        }

        // Shows products in the cart
        public void ViewCart()
        {
            Console.Clear();
            if (Cart.Count == 0)
            {
                Console.Write("Your shopping cart is empty ... \n\nPlease click Enter to return to the Shopping Menu. ");
            }
            else
            {
                double grandTotal = 0;
                Console.WriteLine("Your shopping cart contains the following products: \n");
                foreach (var product in Cart)
                {
                    double productTotalPrice = product.CalcultateTotalPrice();
                    Console.WriteLine($"{product.ProductName} - {product.Price} kr x {product.Quantity} = {productTotalPrice} kr");
                    grandTotal += productTotalPrice;
                }
                Console.WriteLine("-----------------------------------");
                Console.WriteLine($"Grand Total: {grandTotal} kr");
                Console.Write("\nPlease click Enter to return to the Shopping Menu. ");
            }
            Console.ReadKey();
        }
    }
}
