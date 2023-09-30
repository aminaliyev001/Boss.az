using extraSpace;
using NotiNameSpace;
using CvSpace;
using PostSpace;
using EmployeeSpace;
namespace EmployerSpace;
class Employer : Human, INotifiable
{
    public List<CV> Vacancies { get; set; }
    public List<Post> posts {get;set;}
    private string? company_name;
    public string CompanyName{get => company_name; set{if (checkStringLen(value)) company_name = value; else throwExcpetion("Şirkət adı boş qala bilməz"); }}
    public Employer(string name, string surname, string company_name,string city, string phone, int age,string email, string password) 
    : base(name, surname, city,phone,age,email,password)
    {
        CompanyName = company_name;
        Vacancies = new List<CV>();
        posts = new List<Post>();
    }
    string INotifiable.GetNotificationSenderInfo() {
        return CompanyName+$" ({Name} {Surname})";
    }
    public override string ToString()
    {
        return
               $"Name: {Name}\n" +
               $"Surname: {Surname}\n" +
               $"Company Name: {CompanyName}\n" +
               $"City: {City}\n" +
               $"Phone: {PhoneNumber}\n" +
               $"Age: {Age}\n" +
               $"Created date: {Created.ToString("dd-MM-yyyy HH:mm:ss")}\n" +
               $"Updated date: {Updated.ToString("dd-MM-yyyy HH:mm:ss")}\n";
    }
    

    public Employer(){}
    
}
