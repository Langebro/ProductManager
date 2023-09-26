using Microsoft.EntityFrameworkCore;
using ProductManager.Data;
using ProductManager.Domain;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Net.Http.Headers;
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
            Console.WriteLine("3. Lägg till kategori");
            Console.WriteLine("4. Lägg till produkt till kategori");
            Console.WriteLine("5. Lista kategorier");
            Console.WriteLine("6. Avsluta");

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

                    AddCategory();

                    break;

                case ConsoleKey.D4:
                case ConsoleKey.NumPad4:

                    AddProductToCat();

                    break;

                case ConsoleKey.D5:
                case ConsoleKey.NumPad5:

                    ListCat();

                    break;

                case ConsoleKey.D6:
                case ConsoleKey.NumPad6:

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
            Console.Clear();
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
            PrintProductInfo(product);
            Console.WriteLine("(R)adera (U)ppdatera");

            var keyPressed = Console.ReadKey(intercept: true);
            if (keyPressed.Key == ConsoleKey.R)
            {
                Console.Clear();
                PrintProductInfo(product);
                Console.WriteLine("(J)a (N)ej");

                var confirmationKey = Console.ReadKey(intercept: true);
                
                if (confirmationKey.Key == ConsoleKey.J)
                {
                    DeleteProdcut(product);
                    Console.Clear();
                    Console.WriteLine("Produkt raderad");
                    Thread.Sleep(2000);
                }

                else if (confirmationKey.Key == ConsoleKey.N)
                {
                    return;
                }

            }

            if (keyPressed.Key == ConsoleKey.U)
            {
                UpdateProduct(product);
            }

            else if (keyPressed.Key == ConsoleKey.Escape)
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

    public static void AddCategory()
    {
        Console.Write("Namn: ");
        string name = Console.ReadLine();

        Console.WriteLine("Är detta korrekt? ");
        Console.WriteLine("(J)a");
        Console.WriteLine("(N)ej");

        var category = new Category
        {
            Name = name,
        };

        var keyPressed = Console.ReadKey(intercept: true);

        if (keyPressed.Key == ConsoleKey.J)
        {
            SaveCategory(category);
            Console.WriteLine("Kategori tillagd");
            Thread.Sleep(2000);
            return;

        }
        else if (keyPressed.Key == ConsoleKey.N)
        {

            Console.Clear();
            AddCategory();
        }


    }

    public static void AddProductToCat()
    {
        Console.Write("SKU: ");
        string SKU = Console.ReadLine();
        Console.Clear();

        var product = GetProduktBySKU(SKU);

        if (product is not null)
        {
            Console.WriteLine($"SKU: {product.SKU}");
            Console.WriteLine($"Namn: {product.Name}");
            Console.WriteLine("Ange kategori: ");
            string name = Console.ReadLine();

            var category = GetCategoryByName(name);

            using var context = new ApplicationDbContext();

            try
            {
                if (category is not null)
                {
                    ProductCategory existingProduct = PreventDoublesInCat(category.Id, product.Id);

                    if(existingProduct is not null)
                    {
                        throw new Exception("Produkt redan tillagd!");
                    }
                        var productCategory = new ProductCategory
                        {
                            CategoryId = category.Id,
                            ProductId = product.Id
                        };
                        context.ProductCategories.Add(productCategory);

                        context.SaveChanges();

                        Console.Clear();
                        Console.WriteLine("produkt tillagd");
                        Thread.Sleep(2000);
                }
            }

            catch (Exception ex) 
            {
                Console.WriteLine("Produkt finns redan tillagd" + ex.Message);
                Thread.Sleep(2000);
            }
        }
        else
        {
            Console.WriteLine("Produkt finns inte");
        }
    }
  
    

    private static void ListCat()
    {

        var categories = GetCategories();

        Console.WriteLine("Namn på kategori:");
        Console.WriteLine("-----------------------------------------------------------");
        foreach (var category in categories)
        {
            var productsInCategory = GetProductsInCategory(category);
            Console.WriteLine($"{category.Name} ({productsInCategory.Count()})");
            foreach (var product in productsInCategory)
            {
                Console.WriteLine($"\t{product.Name}\t\t{product.Price} SEK");
                
            }
        }

        Thread.Sleep(5000);
    }

    private static IEnumerable<Product> GetProductsInCategory(Category category)
    {
        using var context = new ApplicationDbContext();

        var productsInCategory = context.ProductCategories
            .Where(pc => pc.CategoryId == category.Id)
            .Select(pc => pc.Product)
            .ToList();

        return productsInCategory;
    }

    private static IEnumerable<Category> GetCategories()
    {
        using var context = new ApplicationDbContext();

        var categories = context.Category.ToList();

        return categories;
    }

  
    private static void SaveCategory(Category category)
    {
        using var context = new ApplicationDbContext();
        context.Category.Add(category);
        context.SaveChanges();
    }


    private static void PrintProductInfo(Product product)
    {
        Console.WriteLine($"Namn: {product.Name}");
        Console.WriteLine($"description: {product.Description}");
        Console.WriteLine($"SKU: {product.SKU}");
        Console.WriteLine($"Bild: {product.Picture}");
        Console.WriteLine($"Pris: {product.Price}");
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

    private static ProductCategory PreventDoublesInCat(int categoryId, int productId)
    {

        using var context = new ApplicationDbContext();
        var existingProduct = context.ProductCategories
            .FirstOrDefault(pc => pc.CategoryId == categoryId && pc.ProductId == productId);

        return existingProduct;
    }

    private static Category GetCategoryByName(string? name)
    {
        using var context = new ApplicationDbContext();

        var category = context
            .Category
            .FirstOrDefault(category => category.Name == name);

        return category;

    }


    private static void UpdateProduct(Product product)
    {
        Console.Clear();
        using var context = new ApplicationDbContext();
        context.Product.Attach(product);

        Console.Write("Namn: ");
        product.Name = Console.ReadLine();
        Console.WriteLine($"SKU: {product.SKU}");
        Console.Write("Beskrivning: ");
        product.Description = Console.ReadLine();
        Console.Write("Bild: ");
        product.Picture = Console.ReadLine();
        Console.Write("Pris: ");
        product.Price = Console.ReadLine();

        

        Console.WriteLine("Är detta korrekt? (J)a (N)ej");


        var keyPressed = Console.ReadKey(intercept: true);
        if (keyPressed.Key == ConsoleKey.J)
        {
            context.SaveChanges();
            Console.WriteLine("Produkt Sparad");
            Thread.Sleep(2000);
        }
        else if (keyPressed.Key == ConsoleKey.N)
        {
            return;
        }


    }
}