
using Lab2Butik;


class Program
{

    static List<Customer> customers = new List<Customer>();
    static List<Product> products = new List<Product>();
    static Customer currentCustomer = null;

    public static void Main(string[] args)
    {
        // create products for customer to buy
        products.Add(new Product { ProductName = "Hotdog", Price = 15 });
        products.Add(new Product { ProductName = "Drink", Price = 22 });
        products.Add(new Product { ProductName = "Apple", Price = 5 });
        products.Add(new Product { ProductName = "Pizza", Price = 45 });

        // create 3 predefined customers
        customers.Add(new Customer("Knatte", "123"));
        customers.Add(new Customer("Fnatte", "321"));
        customers.Add(new Customer("Tjatte", "213"));
        customers.Add(new Customer("Mustafa", "Abadir"));

        while (true)
        {
            Console.Clear();
            Console.WriteLine("********** Main Menu **********\n\n");

            // if no customer has logged in yet then show the login/registration menu else show the shopping menu
            if (currentCustomer == null)
            {
                ShowLoginMenu();
            }
            else
            {
                ShowShoppingMenu();
            }
        }

    }

    // Shows login/register menu
    static void ShowLoginMenu()
    {

        Console.WriteLine("Welcome to our shop! Please choose an option:");
        Console.WriteLine("1. Login");
        Console.WriteLine("2. Register");
        Console.Write("Select an option: ");

        string customerChoice = Console.ReadLine();

        if (customerChoice == "1")
        {
            currentCustomer = Customer.Login(customers);

        }
        else if (customerChoice == "2")
        {
            currentCustomer = Customer.Register(customers);
        }
        else
        {
            currentCustomer = null;
        }
    }

    // Shows shopping menu
    static void ShowShoppingMenu()
    {
        Console.Clear();
        Console.WriteLine("********** Shooping Menu **********\n\n");
        Console.WriteLine($"Welcome, {currentCustomer.CustomerName}. Please choose an option:");
        Console.WriteLine("1. View your shopping cart");
        Console.WriteLine("2. Shop products");
        Console.WriteLine("3. View your account details");
        Console.WriteLine("4. Checkout");
        Console.WriteLine("5. Logout");
        Console.Write("Select an option: ");

        string customerChoice = Console.ReadLine();


        if (customerChoice == "1")
        {
            currentCustomer.ViewCart();
        }
        else if (customerChoice == "2")
        {
            ShopProducts();
        }
        else if (customerChoice == "3")
        {
            ViewCustomerDetails();
        }
        else if (customerChoice == "4")
        {
            Checkout();
        }
        else if (customerChoice == "5")
        {
            currentCustomer = null;
        }
    }

    // Shows customer details such as name, password and content of cart by using ToString() metod
    static void ViewCustomerDetails()
    {
        Console.Clear();
        Console.WriteLine(currentCustomer.ToString());
        Console.Write("\nPlease click Enter to return to the Shopping Menu. ");
        Console.ReadKey();
    }
    // Shows customer products to shop 
    static void ShopProducts()
    {
        Console.Clear();
        Console.WriteLine("Available products:");

        // display products

        for (int i = 0; i < products.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {products[i].ProductName} - Price: {products[i].Price} kr");
        }

        Console.WriteLine("\nPlease select the number of the product you want to add your cart: ");
        int customerChoice = -1;
        // keep asking for a number until the customer enters a valid number
        while (true)
        {
            // the customer wrote a valid number and choose a product
            if (int.TryParse(Console.ReadLine(), out customerChoice) && customerChoice > 0 && customerChoice <= products.Count)
            {
                if (customerChoice == 0)
                {
                    Console.ReadKey();
                    return;
                }
                break;
            }
            Console.WriteLine("Invalid number. Please enter again a valid product number.");
        }

        // ask the customer for how many of this product should be added to the cart
        Console.WriteLine($"How many {products[customerChoice - 1].ProductName}(s) would you like to add to the cart? ");
        int selectedQuantity = -1;
        // if the customer has entered a valid item number, ask for the quantity
        while (true)
        {
            if (int.TryParse(Console.ReadLine(), out selectedQuantity) && selectedQuantity > 0)
            {
                break;
            }
            Console.WriteLine("Invalid quantity. Please enter again a valid quantity number.");
        }

        // add the selected item and quantity to the cart
        Product selectedProduct = products[customerChoice - 1];
        currentCustomer.AddToCart(selectedProduct, selectedQuantity);
        Console.Write($"{selectedQuantity} {selectedProduct.ProductName}(s) have been added to your cart. \n\nPlease click Enter to return to the Shopping Menu ");
        Console.ReadKey();
    }

    // Shows checkout
    static void Checkout()
    {
        Console.Clear();
        if (currentCustomer.Cart.Count == 0)
        {
            Console.WriteLine("Your cart is empty ....");
            Console.Write("Please click Enter to return to the Shopping Menu and add products to the cart before checking out. ");
        }
        else
        {
            double grandTotal = 0;
            Console.WriteLine("Your shopping cart contains the following products: \n");
            foreach (var product in currentCustomer.Cart)
            {
                double productTotalPrice = product.CalcultateTotalPrice();
                Console.WriteLine($"{product.ProductName} - {product.Price} kr x {product.Quantity} = {productTotalPrice} kr");
                grandTotal += productTotalPrice;
            }
            Console.WriteLine("-----------------------------------");
            Console.WriteLine($"Grand Total: {grandTotal} kr \n");
            Console.Write("Do you wish to proceed to payement? \n1.{0}\n2.{1}", " Yes ", " No \nSelect an option: ");

            string customerChoice = Console.ReadLine();
            if (customerChoice == "1")
            {
                Console.WriteLine("\nPayment successfull! Thank you for shopping at our shop.");
  
                Console.Write("\nPlease click Enter to return to the Main Menu ");
                // empty cart
                currentCustomer.Cart.Clear();
                currentCustomer = null;
            }

            // if the customer enter an enter number than 1
            else
            {
                Console.Write("\nCheckout canceled. Please click Enter to return to the Shopping Menu. ");
            }
        }
        Console.ReadKey();
    }
}