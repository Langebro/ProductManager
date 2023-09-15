using ProductManager.Domain;

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

    private static void SearchProduct()
    {
        throw new NotImplementedException();
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

        Console.Clear();

        var product = new Product()
        {
            Name = name,
            Description = description,
            SKU = sku,
            Picture = picture,
            Price = price

        };

    }


}