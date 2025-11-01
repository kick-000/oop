using System;
using System.ComponentModel;
using System.Collections.Generic;
public class Program
{
    public static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Machine machine = new Machine();
        Screen screen = new Screen(machine);
        screen.Start();
    }
}
class Screen
{
    Machine machine;
    public Screen(Machine machine)
    {
        this.machine = machine;
    }
    public void Start()
    {
        
        while (true)
        {
            Console.WriteLine("______________________________________");
            Console.WriteLine("| Введите номер операции:            |");
            Console.WriteLine("|    1.Просмотреть список товаров.   |");
            Console.WriteLine("|    2.Внести деньги.                |");
            Console.WriteLine("|    3.Выбрать товары.               |");
            Console.WriteLine("|    4.Получить сдачу.               |");
            Console.WriteLine("|    5.Отменить операцию.            |");
            Console.WriteLine("|    6.Режим администратора.         |");
            Console.WriteLine("|    0.Выход.                        |");
            Console.WriteLine("|____________________________________|");
            Console.WriteLine();
            if (!int.TryParse(Console.ReadLine(), out int number))
            {
                Console.WriteLine("Ошибка ввода!");
                return;
            }
            switch (number)
            {
                case 1:
                    machine.Viewproducts();
                    break;
                case 2:
                    machine.AddMoney();
                    break;
                case 3:
                    machine.Viewproducts();
                    machine.BuyProducts();
                    break;
                case 4:
                    machine.GetChange();
                    break;
                case 5:
                    machine.ReturnMoney();
                    break;
                case 6:
                    Adm();
                    break;
                case 0:
                    return;
                default:
                    Console.WriteLine("Неверное значение, попробуйте снова.");
                    continue;
            }
        }
        
    }
    public void Adm()
    {
        Console.WriteLine("ВВедите пароль");
        string pas = Console.ReadLine();
        if (!Admins.Examination(pas))
        {
            Console.WriteLine("Вы ввели не верный пароль!!!");
            return;
        }

        bool admMeny = true;
        while (admMeny)
        {
            Console.WriteLine("______________________________________");
            Console.WriteLine("| Введите номер операции:            |");
            Console.WriteLine("|   1.Пополнить товары.              |");
            Console.WriteLine("|   2.Сбор выручки                   |");
            Console.WriteLine("|   3.Выйти из режима администратора |");
            Console.WriteLine("|____________________________________|");
            Console.WriteLine();
            if (!int.TryParse(Console.ReadLine(), out int num))
            {
                Console.WriteLine("Ошибка ввода!");
            }
            switch (num)
            {
                case 1:
                    machine.AddProducts();
                    break;
                case 2:
                    machine.RevenueReset();
                    break;
                case 3:
                    admMeny = false;
                    break;
                default: 
                    Console.WriteLine("Неверное значение, попробуйте снова.");
                    break;
            }
        }
    }
}

class Machine
{
    private decimal revenue = 0; // наши деньги 
    private Assortment assortment = new Assortment();
    private Walet walet = new Walet();
    public void BuyProducts()
    {
        Console.WriteLine("Выберете номер товара.");
        if (!int.TryParse(Console.ReadLine(), out int numberProducts))
        {
            Console.WriteLine("Ошибка ввода!");
            return;
        }

        if (numberProducts > assortment.ProductsCount || numberProducts < 1)
        {
            Console.WriteLine("Неверный номер товара, Попробуйте снова");
            return;
        }
        Products temporarily = assortment.OpenProduct(numberProducts);
        if (temporarily == null)
        {
            Console.WriteLine("Такого товара нет(");
            return;
        }
        if (temporarily.Price > walet.Balance)
        {
            Console.WriteLine("Недостаточно средств.");
            return;
        }
        
        if (temporarily.Quantity == 0)
        {
            Console.WriteLine("Товар закончился(");
            return;
        }
        
        Console.WriteLine("Идет оплата...");
        walet.Balance -= temporarily.Price;
        revenue += temporarily.Price;
        temporarily.Quantity -= 1;
        Console.WriteLine("Вы купили " + temporarily.Name + " за " + temporarily.Price);
    }
    public void RevenueReset()
    {
        Console.WriteLine($"Вы забрали {revenue}₽");
        revenue = 0;
    }
    public void GetChange()
    {
        walet.Change();
    }
    public void AddMoney()
    {
        walet.DepositMoney();
    }
    public void Viewproducts()
    {
        assortment.Viewproducts();
    }
    public void AddProducts()
    {
        assortment.AddProducts();
    }
    public void ReturnMoney()
    {
        walet.ReturnMoney();
    }
}

class Admins
{
    private const string password = "he$3DWl";

    public static bool Examination(string pas)
    {
        return pas == password;
    }
}

class Products
{
    private string name;
    private decimal price;
    private int quantity;
    
    public string Name
    {
        get => name;
    }
    public decimal Price
    {
        get => price;
    }

    public int Quantity
    {
        get => quantity;
        set => quantity = value;
    }
    public Products(string name, decimal price, int quantity)
    {
        this.name = name;
        this.price = price;
        this.quantity = quantity;
    }
}

class Assortment
{
    private List<Products> goods = new List<Products>();
    public Assortment()
    {
        goods.Add(new Products("Вода", 50, 10));
        goods.Add(new Products("Шоколад", 75, 5));
        goods.Add(new Products("Чипсы", 60, 1));
    }
    public int ProductsCount => goods.Count;
    public void Viewproducts()
    {
        Console.WriteLine("Список товаров:");
        if (goods.Count == 0)
        {
            Console.WriteLine(" Товары закончились, вернитесь позже) ");
            return;
        }

        for (int i = 0; i < goods.Count; i++)
        {
            if (goods[i].Quantity > 0)
            {
                Console.WriteLine($"  {i + 1}. {goods[i].Name} {goods[i].Price} рублей");
            }
        }
    }
    public void AddProducts()
    {
        Console.WriteLine("Введите название товара который хотите внести.");
        string product = Console.ReadLine();

        for (int i = 0; i < goods.Count; i++)
        {
            if (goods[i].Name.ToLower() == product.ToLower())
            {
                Console.WriteLine("Введите количество товаров.");
                if (!int.TryParse(Console.ReadLine(), out int count))
                {
                    Console.WriteLine("Ошибка ввода!");
                    return;
                }
                goods[i].Quantity += count;
                Console.WriteLine(product + " теперь стало " + goods[i].Quantity);
                return;

            }
        }

        Console.WriteLine("Введите количество.");
        if (!int.TryParse(Console.ReadLine(), out int countt))
        {
            Console.WriteLine("Ошибка ввода!");
            return;
        }

        Console.WriteLine("Введите цену.");
        if (!decimal.TryParse(Console.ReadLine(), out decimal pricee))
        {
            Console.WriteLine("Ошибка ввода!");
        }
        goods.Add(new Products(product, pricee, countt));
        Console.WriteLine($"Вы добавили {product} в количестве {countt} по цене {pricee}.");
    }
    public  Products OpenProduct(int n)
    {
        if ((n - 1) >= 0 && (n - 1) < goods.Count)
        {
            return goods[n - 1];
        }
        return null;
    }
}

class Walet
{
    private decimal balance = 0;

    private List<Coins> listCoins = new List<Coins>
    {
        new Coins(1000, 0),
        new Coins(500, 0),
        new Coins(200, 1),
        new Coins(100, 2),
        new Coins(50, 3),
        new Coins(10, 5),
        new Coins(5, 2),
        new Coins(2, 3),
        new Coins(1, 10),
        new Coins(0.5m, 15),
        new Coins(0.2m, 3),
        new Coins(0.1m, 20)
    };
    
    public decimal Balance
    {
        get => balance;
        set => balance = value;
    }

    public void DepositMoney()
    {
        Console.WriteLine("Внесите деньги (напишите размеры купюр или монет через пробел)");
        Console.WriteLine("Доступные номиналы: 0,1 ; 0,2 ;  0,5 ; 1 ; 2 ; 5 ; 10 ; 50 ; 100 ; 200 ; 500 ; 1000");
        string stringMoneys = Console.ReadLine();
        List<decimal> denomination = new List<decimal> {0.1m, 0.2m,0.5m, 1, 2, 5, 10, 50, 100, 200, 500, 1000 };
        if (string.IsNullOrWhiteSpace(stringMoneys))
        {
            Console.WriteLine("Вы не внесли деньги!");
            return;
        }

        string[] arrayMoneys = stringMoneys.Split(' ');
        List<decimal> intMoneys = new List<decimal>();
       
        decimal ad = 0;
        foreach (string i in arrayMoneys)
        {
            if (!decimal.TryParse(i, out decimal money))
            {
                Console.WriteLine("Автомат не может принимать такие деньги - " + i);
                continue;
            }
            if (!denomination.Contains(money))
            {
                Console.WriteLine("Вы внесли не доступный номинал - " + money);
                continue;
            }
            
            foreach (Coins j in listCoins)
            {
                if (j.Value == money)
                {
                    j.Quantity += 1;
                    break;
                }
            }
            intMoneys.Add(money);
        }
        foreach (decimal i in intMoneys)
        {
            balance += i;
            ad += i;
        }

        Console.WriteLine($"Вы внесли {ad} ₽ Текущий баланс {balance} ₽. ");
}
    
    public void ReturnMoney()
    {
        Console.WriteLine("Отмена операции.");
        if (balance != 0)
        {
            Console.WriteLine("Депозит: " + balance + " рублей");
            Console.WriteLine("Возврат депозита....");
            Change();
        }
        else
        {
            Console.WriteLine("Операция отменена.");
        }

    }

    public void Change()
    {
        if (balance == 0)
        {
            Console.WriteLine("Вы не вносили деньги, баланс = 0");
            return;
        }

        decimal change = balance;
        Console.WriteLine($"Выдача сдачи: {change} ₽");
        
        foreach (Coins coin in listCoins )
        {
            int count = 0;
            while (coin.Value <= change && coin.Quantity > 0)
            {
                coin.Quantity--;
                change -= coin.Value;
                change = Math.Round(change, 2);
                count++;
            }
            if (count > 0)
            {
                Console.WriteLine("     " + coin.Value + " ₽ " +  count);
            }
        }
        if (change > 0)
        {
            Console.WriteLine($"Невозможно выдать сдачу полностью. Остаток {change} ₽");
            balance = change;
        }
        else
        {
            balance = 0;
        }
        

    }
}

class Coins
{
    private decimal mon;
    private int quantity;
    public decimal Value
    {
        get => mon;
    }

    public int Quantity
    {
        get => quantity;
        set => quantity = value;
    }

    public Coins(decimal mon, int quantity)
    {
        this.mon = mon;
        this.quantity = quantity;
    }
}
