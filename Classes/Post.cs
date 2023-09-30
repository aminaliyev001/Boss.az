using CvSpace;
using EmployerSpace;
using extraSpace;
namespace PostSpace;
class Post : Base {
        private string jobtitle;
        public string JobTitle { get => jobtitle; set { if (checkStringLen(value)) jobtitle = value; else throwExcpetion("İş adı boş qala bilməz"); } }
        private string description;
        public string Description { get => description; set {if (checkStringLen(value)) description = value; else throwExcpetion("Təsvir boş qala bilməz"); } }
        private string location;
        public string Location { get => location; set {if (checkStringLen(value)) location = value; else throwExcpetion("Ünvan boş qala bilməz"); } }
        private double salary;
        public double Salary { get => salary; set {if(value > 0)salary=value; else throwExcpetion("Duzgun daxil edin salaryni");} }
        public DateTime DatePosted { get; set; }
        public Category_enum Category {get;set;}
        public Post(Category_enum _Category,string jobTitle, string description, string location, double salary_)
        {
            JobTitle = jobTitle;
            Description = description;
            Location = location;
            Salary = salary_;
            DatePosted = DateTime.Now;
            Category = _Category;
        }
        public Post() {}
        public override string ToString()
        {
            return $"Job Title: {JobTitle}\n" +
                   $"Description: {Description}\n" +
                   $"Location: {Location}\n" +
                   $"Salary: {Salary}\n" +
                   $"Category: {Category.ToString()}\n" +
                   $"Date Posted: {DatePosted.ToString("dd-MM-yyyy")}";
        }
};