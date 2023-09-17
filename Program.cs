using ProductManager.Domain;
using System.Reflection;

namespace ProductManager;


internal class Program
{
    static void Main()
    {

        var applicationRunning = true;

        Console.CursorVisible = false;
        Console.Title = "ProductManger";

        do
        {
            Console.WriteLine("1. Ny produkt");
            Console.WriteLine("3. Sök produkt");
            Console.WriteLine("3. Avsluta");

            var keyPressed = Console.ReadKey(intercept: true);

            Console.Clear();

            switch (keyPressed.Key)
            {
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:

                    AddProduct();

                    break;

                case ConsoleKey.D2:
                case ConsoleKey.NumPad2:

                    SearchProduct();

                    break;

                case ConsoleKey.D3:
                case ConsoleKey.NumPad3:

                    applicationRunning = false;

                    return;
            }

            Console.Clear();

        } while (applicationRunning);


    }

    private static void AddProduct()
    {
        Console.Write("Namn: ");

        string name = Console.ReadLine();

        Console.Write("SKU: ");

        string sku = Console.ReadLine();

        Console.Write("Beskrivning: ");

        string description = Console.ReadLine();

        Console.Write("Bild: ");

        string picture = Console.ReadLine();

        Console.Write("Pris: ");

        string price = Console.ReadLine();

        Console.WriteLine("Är detta korrekt? ");
        Console.WriteLine("Ja");
        Console.WriteLine("Nej");
        Console.ReadLine();

        var product = new Product
        {
            Name = name,
            SKU = sku,
            Description = description,
            Picture = picture,
            Price = price,

        };


        var keyPressed = Console.ReadKey(intercept: true);

        if (keyPressed.Key == ConsoleKey.J)
        {
            SaveProduct(product);
        }
        else if (keyPressed.Key == ConsoleKey.N)
        {

            Console.Clear();
            AddProduct();
        }

    }

    private static void SaveProduct(Product product)
    {
        Console.WriteLine("Produkt Sparad");
        Console.ReadLine();
    }

    private static void SearchProduct()
    {
        Console.Write("SKU: ");

        string SKU = Console.ReadLine();

        Console.Clear();

        var product = GetProduktBySKU(SKU);

        if (product is not null)
        {
            Console.WriteLine($"Namn: {product.Name}");
            Console.WriteLine($"description: {product.Description}");
            Console.WriteLine($"SKU: {product.SKU}");
            Console.WriteLine($"Bild: {product.Picture}");
            Console.WriteLine($"Pris: {product.Price}");

            WaitUntil(ConsoleKey.Escape);
        }
        else
        {
            Console.WriteLine("Produkt finns ej");

            Thread.Sleep(2000);
        }
    }


    private static Product GetProduktBySKU(string? sku)
    {
        throw new NotImplementedException();

    }

    private static void WaitUntil(ConsoleKey key)
    {
        while (Console.ReadKey(true).Key != key) ;

    }

}