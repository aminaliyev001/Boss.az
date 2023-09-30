using System.Text.RegularExpressions;
using NotiNameSpace;
namespace extraSpace;
abstract class Base
{
    public static bool checkStringLen(string text, int len = 0)
    {
        if (text.Length > len)
            return true;
        else return false;
    }

    public static void throwExcpetion(string text)
    {
        throw new Exception(text);
    }
}
abstract class Human : Base
{
    protected string id;
    public string Id { get => id; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
    public List<Notification> notifications {get;set;}
    private string name;
    public string Name { get => name; set { if (checkStringLen(value)) name = value; else throwExcpetion("Ad boş qala bilməz"); } }
    private string surname;
    public string Surname { get => surname; set { if (checkStringLen(value)) surname = value; else throwExcpetion("Soyad boş qala bilməz"); } }
    private string city;
    public string City { get => city; set { if (checkStringLen(value)) city = value; else throwExcpetion("Şəhər boş qala bilməz"); } }
    private string phoneNumber { get; set; }
    public string PhoneNumber { get => phoneNumber; set { if (checkPhoneNumber(value)) phoneNumber = value; else throwExcpetion("Telefon nömrəsi 12 simvoldan ibarət olmalıdır, ex:994511234567"); } }
    private int age;
    public int Age { get => age; set { if (checkAge(value)) age = value; else throwExcpetion("Yaş 16-100 aralığında olmalıdır"); } }
    private string email;
    public string Email { get => email; set { if (emailValidity(value)) email = value; else throwExcpetion("E-mail düzgün daxil edilməyib"); } }
    private string password;
    public string Password { get => password; set { if (checkStringLen(value, 8 - 1)) password = value; else throwExcpetion("Şifrə minimum 8 simvoldan ibarət olmalıdır"); } }
    public Human(string _name, string _surname, string _city, string _phonenumber, int _age, string _email, string _password)
    {
        id = Guid.NewGuid().ToString();
        Name = _name;
        Surname = _surname;
        City = _city;
        PhoneNumber = _phonenumber;
        Age = _age;
        Email = _email;
        Password = hashPass(_password);
        notifications = new List<Notification>();
        Created = DateTime.Now;
        Updated = DateTime.Now;
    }
    public static string? ReadPassword()
    {
        string password = "";
        ConsoleKeyInfo key;
        while (true)
        {
            key = Console.ReadKey(intercept: true);
            if (key.Key == ConsoleKey.Enter)
            {
                Console.WriteLine();
                break;
            }
            else if (key.Key == ConsoleKey.Backspace && password.Length > 0)
            {
                password = password.Substring(0, password.Length - 1);
                Console.Write("\b \b");
            }
            else if (!char.IsControl(key.KeyChar))
            {
                password += key.KeyChar;
                Console.Write("*");
            }
        }
        return password;
    }
    public static string? ReadString()
    {
        string password = "";
        ConsoleKeyInfo key;
        while (true)
        {
            key = Console.ReadKey(intercept: true);
            if (key.Key == ConsoleKey.Enter)
            {
                Console.WriteLine();
                break;
            }
            else if (key.Key == ConsoleKey.Backspace && password.Length > 0)
            {
                password = password.Substring(0, password.Length - 1);
                Console.Write("\b \b");
            }
            else if (!char.IsControl(key.KeyChar))
            {
                password += key.KeyChar;
                Console.Write(key.KeyChar);
            }
        }
        return password;
    }
    public static string hashPass(string pass)
    {
        string hashed = "";
        foreach (char p in pass)
        {
            hashed += Convert.ToInt32(p).ToString() + ",";
        }
        return hashed.TrimEnd(',');
    }

    public static string unHashPass(string hashed)
    {
        string[] numbers = hashed.Split(',');
        string unhashed = "";
        foreach (string numberStr in numbers)
        {
            int number = Convert.ToInt32(numberStr);
            unhashed += Convert.ToChar(number);
        }
        return unhashed;
    }
    public static bool emailValidity(string email)
    {
        string pattern = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";
        return Regex.IsMatch(email, pattern);
    }
    public static bool checkAge(int age)
    {
        if (age >= 16 && 100 >= age)
        {
            return true;
        }
        return false;
    }
    public static bool checkPhoneNumber(string phone)
    {
        string pattern = @"^994(50|51|10|99|77|70)\d{7}$";
        return Regex.IsMatch(phone, pattern);
    }

    public Human() {
        id = Guid.NewGuid().ToString();
        Created = DateTime.Now;
        Updated = DateTime.Now;
        notifications = new List<Notification>();
     }
}