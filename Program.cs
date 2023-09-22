using ProductManager.Data;
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
            Console.WriteLine("2. Sök produkt");
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

                    break;
            }

            Console.Clear();

        } while (applicationRunning);


    }

    private static void AddProduct()
    {
        Console.Write("Namn: ");
        string? name = Console.ReadLine();
        Console.Write("SKU: ");
        string? sku = Console.ReadLine();
        Console.Write("Beskrivning: ");
        string? description = Console.ReadLine();
        Console.Write("Bild: ");
        string picture = Console.ReadLine();
        Console.Write("Pris: ");
        string price = Console.ReadLine();

        Console.WriteLine("Är detta korrekt? ");
        Console.WriteLine("(J)a");
        Console.WriteLine("(N)ej");

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
            Console.WriteLine("Produkt Sparad");
            Thread.Sleep(2000);
            return;

        }
        else if (keyPressed.Key == ConsoleKey.N)
        {

            Console.Clear();
            AddProduct();
        }

    }

    private static void SaveProduct(Product product)
    {
        using var context = new ApplicationDbContext();
        context.Product.Add(product);
        context.SaveChanges();

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
            Console.WriteLine("(R)adera produkt?");

            var keyPressed = Console.ReadKey(intercept: true);
            if (keyPressed.Key == ConsoleKey.R)
            {
                Console.Clear();
                Console.WriteLine($"Namn: {product.Name}");
                Console.WriteLine($"description: {product.Description}");
                Console.WriteLine($"SKU: {product.SKU}");
                Console.WriteLine($"Bild: {product.Picture}");
                Console.WriteLine($"Pris: {product.Price}");
                Console.WriteLine("(J)a (N)ej");

            }

            else if (keyPressed.Key == ConsoleKey.Escape)
            {
                return;
            }

            var confirmationKey = Console.ReadKey(intercept: true);
            if (confirmationKey.Key == ConsoleKey.J)
            {
                DeleteProdcut(product);
                Console.WriteLine("Produkt raderad");
                Thread.Sleep(2000);
            }
            else if (keyPressed.Key == ConsoleKey.N)
            {
                return;
            }

        }
        else
        {
            Console.WriteLine("Produkt finns ej");

            Thread.Sleep(2000);
        }
    }

    private static void DeleteProdcut(Product product)
    {
        using var context = new ApplicationDbContext();
        context.Product.Remove(product);
        context.SaveChanges();
    }

    private static Product GetProduktBySKU(string? sku)
    {
        using var context = new ApplicationDbContext();

        var product = context
            .Product
            .FirstOrDefault(product => product.SKU == sku);

        return product;

    }
}