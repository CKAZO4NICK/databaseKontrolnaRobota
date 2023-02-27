using EFCoreProject.Services.UserServices;
using KontrolnaRobota.Database;
using KontrolnaRobota.Database.Entities;
using KontrolnaRobota.Database.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace KontrolnaRobota
{

    public class Program
    {
           
        static ApplicationDbContext dbContext = new ApplicationDbContext();
        static IGenericRepository<BuyerEntity> buyerRepository = new GenericRepository<BuyerEntity>(dbContext);
        static IGenericRepository<CheckEntity> checkRepository = new GenericRepository<CheckEntity>(dbContext);
        static IGenericRepository<ProductEntity> productRepository = new GenericRepository<ProductEntity>(dbContext);
        static IBuyerService buyerService = new BuyerService(buyerRepository);
        static ICheckService checkService = new CheckService(checkRepository);
        static IProductService productService = new ProductService(productRepository);
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.InputEncoding = System.Text.Encoding.Unicode;
            Console.ForegroundColor = ConsoleColor.Yellow;
            dbContext = new ApplicationDbContext();


            bool checkExit = false;
          
            do
            {
                Console.WriteLine($"Виберіть, що хочете зробити: \n" +
              "1 - Зробити Покупку\n"
              + "2 - Подивитися товари конкретного покупця\n"
               + "3 - Подивитися усі товари, що купив покупець\n"
                + "4 - Подивитися усі товари, що купив покупець із додатковою інформацією про покупця\n"
                 + "5 - Переглянути усі не куплені товари\n"
                  + "6 - Переглянути окремий чек окремого покупця\n"
                   + "7 - Завершити роботу\n");
                string s = Console.ReadLine();

                switch (s)
                {

                    case "1":
                        BuyerCreate();
                        break;
                    case "2":
                        CheckProducts();
                        break;
                    case "3":
                        CheckAllProducts();
                        break;
                    case "4":
                        CheckAllProductsWithAdditionalDataOfBuyer();
                        break;
                    case "5":
                        AllNotBoughtProducts();
                        break;
                    case "6":
                        GetBuyersCheck();
                        break;
                    case "7":
                        checkExit = true;
                        break;
                    default:
                        Console.WriteLine("Error!");
                        break;
                }
            } while (!checkExit);


            //BuyerCreate();
            //CheckProducts();
            //CheckAllProducts();
            //CheckAllProductsWithAdditionalDataOfBuyer();
            //AllNotBoughtProducts();
            //GetBuyersCheck();
        }

        static void BuyerCreate()
        {

            Console.WriteLine("Name?");
            string buyername = Console.ReadLine();
            Console.WriteLine("Surname?");
            string buyersurname = Console.ReadLine();
            BuyerEntity buyer = buyerService.GetByNameAndSurname(buyername, buyersurname);
            if (buyer == null)
            {
                Console.WriteLine("Email??");
                string buyeremail = Console.ReadLine();
                Console.WriteLine("Birthdate?");
                DateTime buyerdateofbirth = DateTime.Parse(Console.ReadLine());
                buyer = new BuyerEntity()
                {
                    Name = buyername,
                    Surname = buyersurname,
                    Email = buyeremail,
                    BirthDate = buyerdateofbirth,
                };
                buyerService.Create(buyer);
            }
           
            CheckEntity check = new CheckEntity()
            {
                DateOfBuying = DateTime.Now,
                BuyerFK = buyer.Id
            };
            
            checkService.Create(check);


            for (int i = 0; i >= 0; i++)
            {
                Console.WriteLine("Введіть товар, який хочете додати: ");
                string nameProduct = Console.ReadLine();
                if (nameProduct == "n")
                    break;

                Console.WriteLine("Введіть скільки хочете купити товару: ");
                int countProduct = int.Parse(Console.ReadLine());
                for(int j = 0; j < countProduct; j++)
                {
                   
                    if (!productService.BuyProducts(check.Id, nameProduct))
                    {
                        Console.WriteLine("немає даного товару!");
                        break;
                    }

                   
                }
            }
            checkService.Update(check);
            
            buyerService.Update(buyer);           
        }
        static void CheckProducts()
        {
            Console.WriteLine("Ім'я покупця? ");
            string name = Console.ReadLine();
            Console.WriteLine("Фамілія покупця? ");
            string surname = Console.ReadLine();
            Console.WriteLine("номер чеку? ");
            int a = int.Parse(Console.ReadLine());
            List<ProductEntity> products = buyerService.GetProducts(name, surname, a);
            if (products != null)
            {
                
                List<int> count = new List<int>();
                for(int i = 0; i < products.Count; i++)
                {
                    count.Add(1);
                    for (int j = 0; j < products.Count; j++)
                    {
                        if (products[i].Name == products[j].Name && i!=j)
                        {
                            count[i]++;
                        }
                    }
                }
                double ttlprc = 0;
                for(int i = 0; i < products.Count; i += count[i])
                {
                    Console.WriteLine($"{products[i].Id} Продукт: {count[i]}шт - {products[i].Name} | ціна: {products[i].Price} грн");
                    ttlprc += products[i].Price * count[i];
                }
                Console.WriteLine("Загальна ціна: " + ttlprc + "грн");
            }
            else
            {
                Console.WriteLine("Такого покупця не існує!");
            }
        }
        static void CheckAllProducts()
        {
            Console.WriteLine("Ім'я покупця? ");
            string name = Console.ReadLine();
            Console.WriteLine("Фамілія покупця? ");
            string surname = Console.ReadLine();

            int checksCount = buyerService.GetChecksCount(name, surname);
            if (checksCount > 0)
            {
                for (int c = 1; c <= checksCount; c++)
                {
                    List<ProductEntity> products = buyerService.GetProducts(name, surname, c);

                    List<int> count = new List<int>();
                    for (int i = 0; i < products.Count; i++)
                    {
                        count.Add(1);
                        for (int j = 0; j < products.Count; j++)
                        {
                            if (products[i].Name == products[j].Name && i != j)
                            {
                                count[i]++;
                            }
                        }
                    }
                    Console.WriteLine($"Товар з чеку {c}");
                    double ttlprc = 0;
                    for (int i = 0; i < products.Count; i += count[i])
                    {
                        Console.WriteLine($"{products[i].Id} Продукт: {count[i]}шт - {products[i].Name} | ціна: {products[i].Price} грн");
                        ttlprc += products[i].Price * count[i];
                    }
                    Console.WriteLine("Загальна ціна: " + ttlprc + "грн");
                    Console.WriteLine(new String('-', 20));
                }
            }
            else
            {
                Console.WriteLine("Wrong Data!");
            }

        }
        static void CheckAllProductsWithAdditionalDataOfBuyer()
        {
            List<BuyerEntity> buyers = buyerService.GetAllBuyers();
            foreach (BuyerEntity buyer in buyers)
            {
                Console.WriteLine($"Покупець: {buyer.Name} {buyer.Surname}\n" +
                    $"Дата народження: {buyer.BirthDate}\n" +
                    $"Email: {buyer.Email}\n");
                for (int c = 0; c < buyer.Checks.Count; c++)
                {
                    List<ProductEntity> products = checkService.GetBuyedProducts(buyer.Checks.ToList()[c].Id);
                    List<int> count = new List<int>();
                    for (int i = 0; i < products.Count; i++)
                    {
                        count.Add(1);
                        for (int j = 0; j < products.Count; j++)
                        {
                            if (products[i].Name == products[j].Name && i != j)
                            {
                                count[i]++;
                            
                        }
                    }
                    Console.WriteLine($"Товар з чеку {c + 1}");
                    double ttlprc = 0;
                    for (int i = 0; i < products.Count; i += count[i])
                    {
                        Console.WriteLine($"{products[i].Id} Продукт: {count[i]}шт - {products[i].Name} | ціна: {products[i].Price} грн");
                        ttlprc += products[i].Price * count[i];
                    }
                    Console.WriteLine("Загальна ціна: " + ttlprc + "грн");
                    Console.WriteLine(new String('-', 20));
                }
            }
        }
        static void AllNotBoughtProducts()
        {
            List<ProductEntity> butBoughtProducts = productService.GetNotBought();

            List<int> count = new List<int>();
            for (int i = 0; i < butBoughtProducts.Count; i++)
            {
                count.Add(1);
                for (int j = 0; j < butBoughtProducts.Count; j++)
                {
                    if (butBoughtProducts[i].Name == butBoughtProducts[j].Name && i != j)
                    {
                        count[i]++;
                    }
                }
            }
            for (int i = 0; i < butBoughtProducts.Count; i += count[i])
            {
                Console.WriteLine($"{butBoughtProducts[i].Id} Продукт: {count[i]}шт - {butBoughtProducts[i].Name} | ціна: {butBoughtProducts[i].Price} грн");
            }
        }
        static void GetBuyersCheck()
        {
            Console.WriteLine("Ім'я покупця? ");
            string name = Console.ReadLine();
            Console.WriteLine("Фамілія покупця? ");
            string surname = Console.ReadLine();           
            Console.WriteLine("номер чеку? ");
            int a = int.Parse(Console.ReadLine());
            CheckEntity check = buyerService.GetCheck(name, surname, a);
            Console.WriteLine($"Номер чеку: {check.Id}\n"
                +
                $"Дата: {check.DateOfBuying}\n");
            List<ProductEntity> products = checkService.GetBuyedProducts(check.Id);
            List<int> count = new List<int>();
            for (int i = 0; i < products.Count; i++)
            {
                count.Add(1);
                for (int j = 0; j < products.Count; j++)
                {
                    if (products[i].Name == products[j].Name && i != j)
                    {
                        count[i]++;
                    }
                }
            }
            double ttlprc = 0;
            for (int i = 0; i < products.Count; i += count[i])
            {
                Console.WriteLine($"{products[i].Id} Продукт: {count[i]}шт - {products[i].Name} | ціна: {products[i].Price} грн");
                ttlprc += products[i].Price * count[i];
            }
            Console.WriteLine("Загальна ціна: " + ttlprc + "грн");
        }
    }
}