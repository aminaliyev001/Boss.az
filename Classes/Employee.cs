using CvSpace;
using extraSpace;
using NotiNameSpace;
namespace EmployeeSpace;
enum Gender_enum {
    Male,Female
}
class Employee : Human, INotifiable
{
    private string? fname;
    public string Fname { get => fname; set { if (checkStringLen(value)) fname = value; else throwExcpetion("Ata adı boş qala bilməz"); } }
    public Gender_enum Gender{get;set;}
    public CV? cv {get; set;}
    public Employee(string _name, string _surname, string _fname,  string _city, Gender_enum _gender, int _age, string _email,string _phonenumber,string _password) : 
    base(_name,_surname,_city,_phonenumber,_age,_email,_password)
    {
        Gender  = _gender;
        Fname = _fname;
        cv = default;
    }
   
    string INotifiable.GetNotificationSenderInfo() {
        return Email+$" ({Name} {Surname})";
    }
    public Employee() {}
    public override string ToString()
    {
        return $"ID: {Id}\n" +
               $"Name: {Name}\n" +
               $"Surname: {Surname}\n" +
               $"Father name: {Fname}\n" +
               $"City: {City}\n" +
               $"Phone: {PhoneNumber}\n" +
               $"Age: {Age}\n" +
               $"Crated date: {Created.ToString("dd-MM-yyyy HH:mm:ss")}\n"+
               $"Updated date: {Updated.ToString("dd-MM-yyyy HH:mm:ss")}\n";
    }
};