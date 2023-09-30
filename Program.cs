using EmployeeSpace;
using EmployerSpace;
using AboutusSpace;
using extraSpace;
using PostSpace;
using CvSpace;
using System.Text.Json;
using System.Net.Mail;
using MailNameSpace;
using LogNameSpace;
using NotiNameSpace;
using System.Globalization;
using System.Text.RegularExpressions;

Aboutus aboutus = new("info@boss.az", "994515111000", "Boss.Az - əmək bazarının bütün iştirakçılarının faydalana biləcəyi, dəqiq və sürətli iş və ya işçi axtarışı üçün nəzərdə tutulmuş onlayn platformadır. Layihə istifadəçilərə geniş iş elanı və CV bazasından rahat istifadə imkanı yaradır.");
List<Employee> employees = new List<Employee>();
List<Employer> employers = new List<Employer>();

Employee? loggedUser = default;
Employer? loggedEmployer = default;

const string employeesFile = "employees.json";
const string employersFile = "employers.json";

string[] menuOptions = {
            "Sign Up",
            "Sign In",
            "About us",
            "Exit"
        };
void ToBack()
{
    Console.WriteLine("Press anything to go back");
    Console.ReadKey();
}
void CheckEmployerExist(string? email, string? company_name)
{
    var employer = employers.FirstOrDefault(e => e.Email.ToLower() == email.ToLower() || e.CompanyName == company_name);
    if (employer != null)
    {
        Base.throwExcpetion($"Email: {email} or company name: {company_name} already exists. Please try another");
    }
    else
    {
        var employee = employees.FirstOrDefault(e => e.Email.ToLower() == email.ToLower());
        if (employee != null)
        {
            Base.throwExcpetion($"Email: {email} already exists. Please try another");
        }
    }
}
void CheckEmployeeExist(string? email)
{
    var employee = employees.FirstOrDefault(e => e.Email.ToLower() == email.ToLower());
    if (employee != null)
    {
        Base.throwExcpetion($"Email {email} already exists. Please try another");
    }
    else
    {
        var employer = employers.FirstOrDefault(e => e.Email.ToLower() == email.ToLower());
        if (employer != null)
        {
            Base.throwExcpetion($"Email: {email} already exists. Please try another");
        }
    }
}
void Page_header(string text)
{
    Console.Clear();
    Console.WriteLine($"===== {text} =====\n");
};
void LoadingAnimation(int seconds)
{
    Console.Write("Loading ");
    for (int i = 0; i < seconds * 3; i++)
    {
        Console.Write(".");
        Thread.Sleep(333);
    }
    Console.WriteLine();
}
void SaveEmployees()
{
    var json = JsonSerializer.Serialize(employees);
    if (File.Exists(employeesFile))
    {
        File.Delete(employeesFile);
    }
    File.WriteAllText(employeesFile, json);
}
void SaveEmployers()
{
    var json = JsonSerializer.Serialize(employers);
    if (File.Exists(employersFile))
    {
        File.Delete(employersFile);
    }
    File.WriteAllText(employersFile, json);
}
void LoadAll()
{
    if (File.Exists(employeesFile))
    {
        var json = File.ReadAllText(employeesFile);
        if (!string.IsNullOrEmpty(json))
            employees = JsonSerializer.Deserialize<List<Employee>>(json);
    }
    if (File.Exists(employersFile))
    {
        var json = File.ReadAllText(employersFile);
        if (!string.IsNullOrEmpty(json))
            employers = JsonSerializer.Deserialize<List<Employer>>(json);
    }
}
void SelectionMenu(int index)
{
    switch (index)
    {
        case 0:
            SignUpMenu();
            break;
        case 1:
            SignInMenu();
            break;
        case 2:
            AboutUs();
            break;
        case 3:
            Environment.Exit(0);
            break;
    }
}
void Main()
{
    int cselection = 0;
    LoadAll();
    while (true)
    {
        DisplayMenu(menuOptions, cselection);
        var key = Console.ReadKey();
        switch (key.Key)
        {
            case ConsoleKey.UpArrow:
                if (cselection > 0) cselection--;
                break;
            case ConsoleKey.DownArrow:
                if (cselection < menuOptions.Length - 1) cselection++;
                break;
            case ConsoleKey.Enter:
                SelectionMenu(cselection);
                break;
        }
    }

}
void DisplayMenu(string[] options, int _selection)
{
    Page_header("Boss.az");
    for (int i = 0; i < options.Length; i++)
    {
        if (i == _selection)
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
        }
        Console.WriteLine(options[i]);
        Console.ResetColor();
    }
}
void SignUpMenu()
{
    string[] options = {
                "Employee Sign Up",
                "Employer Sign Up",
                "Back"
            };
    int cselection = 0;
    while (true)
    {
        DisplayMenu(options, cselection);
        var key = Console.ReadKey();
        switch (key.Key)
        {
            case ConsoleKey.UpArrow:
                if (cselection > 0) cselection--;
                break;
            case ConsoleKey.DownArrow:
                if (cselection < options.Length - 1) cselection++;
                break;
            case ConsoleKey.Enter:
                if (cselection == 0)
                {
                    SignUp();
                }
                else if (cselection == 1)
                {
                    SignUp(false);
                }
                else if (cselection == 2)
                {
                    return;
                }
                break;
        }
    }
}
void SignUp(bool isEmployee = true)
{
    string text = isEmployee ? "Employee" : "Employer";
sign_up_line:
    try
    {
        Page_header($"{text} Sign Up");
        string? company_name = "";
        string? fname = "";
        Gender_enum gender = default;

        Console.WriteLine("Name:");
        string? name;
        while (string.IsNullOrWhiteSpace(name = Console.ReadLine()))
        {
            Console.WriteLine("Name cannot be empty. Please enter a valid name: ");
        }

        Console.WriteLine("Surname:");
        string? surname;
        while (string.IsNullOrWhiteSpace(surname = Console.ReadLine()))
        {
            Console.WriteLine("Surname cannot be empty. Please enter a valid surname: ");
        }
        if (!isEmployee)
        {
            Console.WriteLine("Company name:");
            while (string.IsNullOrWhiteSpace(company_name = Console.ReadLine()))
            {
                Console.WriteLine("Company name cannot be empty. Please enter a valid company name: ");
            }
        }
        else
        {
            Console.WriteLine("Father Name:");
            while (string.IsNullOrWhiteSpace(fname = Console.ReadLine()))
            {
                Console.WriteLine("Father's name cannot be empty. Please enter a valid father name: ");
            }
            Console.WriteLine("Gender (Male/Female):");
            ConsoleKeyInfo gender_text = Console.ReadKey();
            while (gender_text.Key != ConsoleKey.M && gender_text.Key != ConsoleKey.F)
            {
                gender_text = Console.ReadKey();
                Console.WriteLine("Invalid gender. Please enter M or F: ");
            }
            if (gender_text.Key == ConsoleKey.M)
                gender = Gender_enum.Male;
            else gender = Gender_enum.Female;

        }
        Console.WriteLine("\nCity:");
        string? city;
        while (string.IsNullOrWhiteSpace(city = Console.ReadLine()))
        {
            Console.WriteLine("City cannot be empty. Please enter a valid city: ");
        }
        Console.WriteLine("Enter Age:");
        int age;
        while (!int.TryParse(Console.ReadLine(), out age) || age < 16 || age > 100)
        {
            Console.WriteLine("Invalid input. Please enter a valid age between 16 and 100: ");
        }
    emailSignUp:
        Console.WriteLine("Enter Email:");
        string? email;
        while (string.IsNullOrWhiteSpace(email = Console.ReadLine()) || !Employee.emailValidity(email))
        {
            Console.WriteLine("Invalid email. Please enter a valid email address: ");
        }
        Random random = new Random();
        string code_6 = random.Next(100000, 999999).ToString();
        MailSender.SendMail(email, "Tesdiqle", $"Kod: {code_6} . Kenar sexsler ile paylasmayin");
        Console.WriteLine("A verification code has been sent to your email. Please enter the code: ");
        string? enteredCode = Console.ReadLine();
        LoadingAnimation(2);
        if (enteredCode == code_6)
        {
            Console.WriteLine("Verification successful");
        }
        else
        {
            Console.WriteLine("Incorrect verification code");
            goto emailSignUp;
        }
        Console.WriteLine("Enter Phone Number:");
        string? phoneNumber;
        while (string.IsNullOrWhiteSpace(phoneNumber = Console.ReadLine()) || !Human.checkPhoneNumber(phoneNumber))
        {
            Console.WriteLine("Invalid phone number.(ex:994511234567) : ");
        }
        Console.WriteLine("Password (Minimum 8 characters):");
        string? password;
        while (string.IsNullOrWhiteSpace(password = Human.ReadPassword()) || password.Length < 8)
        {
            Console.WriteLine("Invalid password. Please enter a password with at least 8 characters: ");
        }
        if (isEmployee)
        {
            CheckEmployeeExist(email);
            var employee = new Employee(name, surname, fname, city, gender, age, email, phoneNumber, password);
            employees.Add(employee);
        }
        else
        {
            CheckEmployerExist(email, company_name);
            var employer = new Employer(name, surname, company_name, city, phoneNumber, age, email, password);
            employers.Add(employer);
        }
        if (isEmployee)
            SaveEmployees();
        else SaveEmployers();
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
        LoadingAnimation(2);
        goto sign_up_line;
    }
    Console.WriteLine($"{text} registered successfully ");
    LoadingAnimation(2);
}
void SignInMenu()
{
sign_in:
    try
    {
        Page_header("Sign in");
        Console.WriteLine("Email:");
        string? email;
        while (string.IsNullOrWhiteSpace(email = Console.ReadLine()) || !Employee.emailValidity(email))
        {
            Console.WriteLine("Invalid email. Please enter a valid email address: ");
        }
        Console.WriteLine("Password:");
        string? password;
        while (string.IsNullOrWhiteSpace(password = Human.ReadPassword()))
        {
            Console.WriteLine("Invalid password. Try again: ");
        }
        var employee = employees.FirstOrDefault(e => e.Email.ToLower() == email.ToLower() && e.Password == Employee.hashPass(password));
        if (employee != null)
        {
            Console.WriteLine("Successfully signed in as an Employee");
            LoadingAnimation(2);
            loggedUser = employee;
            Log.Write($"{loggedUser.Email}(id:{loggedUser.Id}) proqrama giris etdi");
            EmployeeMenu();
        }
        else
        {
            var employer = employers.FirstOrDefault(e => e.Email.ToLower() == email.ToLower() && e.Password == Employee.hashPass(password));
            if (employer != null)
            {
                Console.WriteLine("Successfully signed in as an Employer");
                LoadingAnimation(2);
                loggedEmployer = employer;
                Log.Write($"{loggedEmployer.Email}(id:{loggedEmployer.Id}) proqrama giris etdi");
                EmployerMenu();
            }
            else
            {
                Console.WriteLine("Invalid email or password");
                LoadingAnimation(2);
                goto sign_in;
            }
        }
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
        LoadingAnimation(2);
        goto sign_in;
    }
}

void AboutUs()
{
    Page_header("About US");
    Console.WriteLine($"{aboutus.ToString()}");

    ToBack();
}
// List<Post> FetchJobs() {
//     return default;
// }
void SavedBack(bool employee = true)
{
    if(employee) {
    Log.Write($"{loggedUser.Email}(id:{loggedUser.Id}) melumatlarini yeniledi");
    loggedUser.Updated = DateTime.Now;
    Console.WriteLine("\nSucessfully saved your info");
    SaveEmployees();
    } else {
    Log.Write($"{loggedEmployer.Email}(id:{loggedEmployer.Id}) melumatlarini yeniledi");
    loggedEmployer.Updated = DateTime.Now;
    Console.WriteLine("\nSucessfully saved your info");
    SaveEmployers();
    }
    LoadingAnimation(2);
}
void DisplayMyProfile()
{
    Log.Write($"{loggedUser.Email}(id:{loggedUser.Id}) Profile menyusuna giris etdi");

    Console.Clear();
    string[] editOptions = { $"Name", $"Surname", $"Father name", $"City", $"Phone", $"Age", $"Email", $"Gender", $"Password", "Back" };
    int SOption = 0;
    while (true)
    {
        Console.Clear();
        Page_header("Edit Profile");
        DisplayMenu(editOptions, SOption);
        Console.WriteLine($"\n{loggedUser.ToString()}");
        ConsoleKeyInfo keyInfo = Console.ReadKey();
        switch (keyInfo.Key)
        {
            case ConsoleKey.UpArrow:
                SOption = SOption > 0 ? SOption -= 1 : SOption;
                break;
            case ConsoleKey.DownArrow:
                SOption = SOption < editOptions.Length - 1 ? SOption += 1 : SOption;
                break;
            case ConsoleKey.Enter:
                Console.Clear();
                switch (SOption)
                {
                    case 0:
                        string? name;
                        Console.Write("Enter a new name: ");
                        while (string.IsNullOrWhiteSpace(name = Console.ReadLine()))
                            Console.WriteLine("Name cannot be empty. Please enter a valid name: ");
                        loggedUser.Name = name;
                        SavedBack();
                        break;
                    case 1:
                        string? surname;
                        Console.Write("Enter a new surname: ");
                        while (string.IsNullOrWhiteSpace(surname = Console.ReadLine()))
                            Console.WriteLine("Surname cannot be empty. Please enter a valid surname: ");
                        loggedUser.Surname = surname;
                        SavedBack();
                        break;
                    case 2:
                        string? fname;
                        Console.WriteLine("Father name:");
                        while (string.IsNullOrWhiteSpace(fname = Console.ReadLine()))
                        {
                            Console.WriteLine("Father's name cannot be empty. Please enter a valid father name: ");
                        }
                        loggedUser.Fname = fname;
                        SavedBack();
                        break;
                    case 3:
                        string? city;
                        Console.Write("Enter a new city: ");
                        while (string.IsNullOrWhiteSpace(city = Console.ReadLine()))
                            Console.WriteLine("City cannot be empty. Please enter a valid city: ");
                        loggedUser.City = city;
                        SavedBack();
                        break;
                    case 4:
                        Console.WriteLine("Enter Phone Number:");
                        string? phoneNumber;
                        while (string.IsNullOrWhiteSpace(phoneNumber = Console.ReadLine()) || !Human.checkPhoneNumber(phoneNumber))
                        {
                            Console.WriteLine("Invalid phone number.(ex:994511234567) : ");
                        }
                        loggedUser.PhoneNumber = phoneNumber;
                        SavedBack();
                        break;
                    case 5:
                        Console.WriteLine("Enter a new age:");
                        int age;
                        while (!int.TryParse(Console.ReadLine(), out age) || age < 16 || age > 100)
                        {
                            Console.WriteLine("Invalid input. Please enter a valid age between 16 and 100: ");
                        }
                        loggedUser.Age = age;
                        SavedBack();
                        break;
                    case 6:
                    editEmailSignUp:
                        Console.WriteLine("Enter a new email:");
                        string? email;
                        while (string.IsNullOrWhiteSpace(email = Console.ReadLine()) || !Employee.emailValidity(email))
                        {
                            Console.WriteLine("Invalid email. Please enter a valid email address: ");
                        }
                        Random random = new Random();
                        string code_6 = random.Next(100000, 999999).ToString();
                        MailSender.SendMail(email, "Tesdiqle", $"Kod: {code_6} . Kenar sexsler ile paylasmayin");
                        Console.WriteLine("A verification code has been sent to your email. Please enter the code: ");
                        string? enteredCode = Console.ReadLine();
                        LoadingAnimation(2);
                        if (enteredCode == code_6)
                        {
                            Console.WriteLine("Verification successful");
                            loggedUser.Email = email;
                            SavedBack();
                        }
                        else
                        {
                            Console.WriteLine("Incorrect verification code");
                            goto editEmailSignUp;
                        }

                        break;
                    case 7:
                        Console.WriteLine("Enter a new gender (Male/Female):");
                        ConsoleKeyInfo gender_text = Console.ReadKey();
                        while (gender_text.Key != ConsoleKey.M && gender_text.Key != ConsoleKey.F)
                        {
                            gender_text = Console.ReadKey();
                            Console.WriteLine("Invalid gender. Please enter M or F: ");
                        }
                        if (gender_text.Key == ConsoleKey.M)
                            loggedUser.Gender = Gender_enum.Male;
                        else loggedUser.Gender = Gender_enum.Female;

                        SavedBack();
                        break;
                    case 8:
                        Console.WriteLine("Enter a new password (Minimum 8 characters):");
                        string? password;
                        while (string.IsNullOrWhiteSpace(password = Human.ReadPassword()) || password.Length < 8)
                        {
                            Console.WriteLine("Invalid password. Please enter a password with at least 8 characters: ");
                        }
                        loggedUser.Password = Human.hashPass(password);
                        SavedBack();
                        break;
                    case 9:
                        return;
                }
                break;
        }
    }
}
void DisplayMyProfileEmployer()
{
    Log.Write($"{loggedEmployer.Email}(id:{loggedEmployer.Id}) Profile menyusuna giris etdi");
    Console.Clear();
    string[] editOptions = { $"Name", $"Surname", $"City", $"Phone", $"Age", $"Email", $"Password", "Back" };
    int SOption = 0;
    while (true)
    {
        Console.Clear();
        Page_header("Edit Profile");
        DisplayMenu(editOptions, SOption);
        Console.WriteLine($"\n{loggedEmployer.ToString()}");
        ConsoleKeyInfo keyInfo = Console.ReadKey();
        switch (keyInfo.Key)
        {
            case ConsoleKey.UpArrow:
                SOption = SOption > 0 ? SOption -= 1 : SOption;
                break;
            case ConsoleKey.DownArrow:
                SOption = SOption < editOptions.Length - 1 ? SOption += 1 : SOption;
                break;
            case ConsoleKey.Enter:
                Console.Clear();
                switch (SOption)
                {
                    case 0:
                        string? name;
                        Console.Write("Enter a new name: ");
                        while (string.IsNullOrWhiteSpace(name = Console.ReadLine()))
                            Console.WriteLine("Name cannot be empty. Please enter a valid name: ");
                        loggedEmployer.Name = name;
                        SavedBack(false);
                        break;
                    case 1:
                        string? surname;
                        Console.Write("Enter a new surname: ");
                        while (string.IsNullOrWhiteSpace(surname = Console.ReadLine()))
                            Console.WriteLine("Surname cannot be empty. Please enter a valid surname: ");
                        loggedEmployer.Surname = surname;
                        SavedBack(false);
                        break;
                    case 2:
                        string? city;
                        Console.Write("Enter a new city: ");
                        while (string.IsNullOrWhiteSpace(city = Console.ReadLine()))
                            Console.WriteLine("City cannot be empty. Please enter a valid city: ");
                        loggedEmployer.City = city;
                        SavedBack(false);
                        break;
                    case 3:
                        Console.WriteLine("Enter Phone Number:");
                        string? phoneNumber;
                        while (string.IsNullOrWhiteSpace(phoneNumber = Console.ReadLine()) || !Human.checkPhoneNumber(phoneNumber))
                        {
                            Console.WriteLine("Invalid phone number.(ex:994511234567) : ");
                        }
                        loggedEmployer.PhoneNumber = phoneNumber;
                        SavedBack(false);
                        break;
                    case 4:
                        Console.WriteLine("Enter a new age:");
                        int age;
                        while (!int.TryParse(Console.ReadLine(), out age) || age < 16 || age > 100)
                        {
                            Console.WriteLine("Invalid input. Please enter a valid age between 16 and 100: ");
                        }
                        loggedEmployer.Age = age;
                        SavedBack(false);
                        break;
                    case 5:
                    editEmailSignUp:
                        Console.WriteLine("Enter a new email:");
                        string? email;
                        while (string.IsNullOrWhiteSpace(email = Console.ReadLine()) || !Employee.emailValidity(email))
                        {
                            Console.WriteLine("Invalid email. Please enter a valid email address: ");
                        }
                        Random random = new Random();
                        string code_6 = random.Next(100000, 999999).ToString();
                        MailSender.SendMail(email, "Tesdiqle", $"Kod: {code_6} . Kenar sexsler ile paylasmayin");
                        Console.WriteLine("A verification code has been sent to your email. Please enter the code: ");
                        string? enteredCode = Console.ReadLine();
                        LoadingAnimation(2);
                        if (enteredCode == code_6)
                        {
                            Console.WriteLine("Verification successful");
                            loggedEmployer.Email = email;
                            SavedBack(false);
                        }
                        else
                        {
                            Console.WriteLine("Incorrect verification code");
                            goto editEmailSignUp;
                        }

                        break;
                    case 6:
                        Console.WriteLine("Enter a new password (Minimum 8 characters):");
                        string? password;
                        while (string.IsNullOrWhiteSpace(password = Human.ReadPassword()) || password.Length < 8)
                        {
                            Console.WriteLine("Invalid password. Please enter a password with at least 8 characters: ");
                        }
                        loggedEmployer.Password = Human.hashPass(password);
                        SavedBack(false);
                        break;
                    case 7:
                        return;
                }
                break;
        }
    }
}

void DisplayNotifications(bool isEmployee = true)
{
    Human? Loggedone = default;
    if(isEmployee) {
        Loggedone = loggedUser;
    } else {Loggedone = loggedEmployer;}

    Log.Write($"{Loggedone.Email}(id:{Loggedone.Id}) Notifications menyusuna daxil oldu");
    int SelectedN = 0;
    bool showReadMessages = false;
    while (true)
    {
        Page_header("Notifications");
        List<Notification> notificationsToDisplay;
        if (Loggedone.notifications == null || Loggedone.notifications.Count == 0)
        {
            Console.WriteLine("No notifications to display\n ESC => BACK");
            Console.WriteLine("Press 'R' to toggle showing read messages");
            notificationsToDisplay = new List<Notification>();
        }
        else
        {
            notificationsToDisplay = showReadMessages
                ? Loggedone.notifications
                : Loggedone.notifications.Where(n => n.Status == NotificationStatus.Unread).ToList();
            if (notificationsToDisplay.Count == 0)
            {
                Console.WriteLine("No unread notifications \n press ESC to go back");
            }
            else
            {
                for (int i = 0; i < notificationsToDisplay.Count; i++)
                {
                    if (i == SelectedN)
                    {
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                    string statusText = notificationsToDisplay[i].Status == NotificationStatus.Unread ? "Unread" : "Read";
                    Console.WriteLine($"{notificationsToDisplay[i].Title} ({notificationsToDisplay[i].Status.ToString()}) => {notificationsToDisplay[i].Text}");
                    Console.ResetColor();
                }
                Console.WriteLine("Click'R' to see read messages");
            }
        }
        ConsoleKeyInfo keyInfo = Console.ReadKey();
        switch (keyInfo.Key)
        {
            case ConsoleKey.DownArrow:
                if (notificationsToDisplay.Count - 1 > SelectedN) SelectedN += 1;
                break;
            case ConsoleKey.UpArrow:
                if (SelectedN > 0) SelectedN -= 1;
                break;
            case ConsoleKey.Enter:
                if (notificationsToDisplay.Count > 0)
                {
                    notificationsToDisplay[SelectedN].Status = NotificationStatus.Read;
                }
                break;
            case ConsoleKey.R:
                showReadMessages = !showReadMessages;
                break;
        }
        if (keyInfo.Key == ConsoleKey.Escape)
            break;
    }
    if(isEmployee)
    SaveEmployees();
    else SaveEmployers();
}
void DisplayMyCV()
{
    Log.Write($"{loggedUser.Email}(id:{loggedUser.Id}) CV menyusuna daxil oldu");
    int selectedItem = 0;
    string[] cvOptions;
    if (loggedUser.cv == null)
    {
        cvOptions = new string[] { "Add CV", "Back" };
    }
    else
    {
        cvOptions = new string[] { "Edit CV", "Back" };
    }
    while (true)
    {
        Page_header("My CV");
        DisplayMenu(cvOptions, selectedItem);
        if (loggedUser.cv == null)
        {
            Console.WriteLine("\nYou do not have a CV");
        }
        else Console.WriteLine("\n" + loggedUser.cv.ToString());
        ConsoleKeyInfo keyInfo = Console.ReadKey();
        switch (keyInfo.Key)
        {
            case ConsoleKey.DownArrow:
                if (selectedItem < cvOptions.Length - 1) selectedItem += 1;
                break;
            case ConsoleKey.UpArrow:
                if (selectedItem > 0) selectedItem -= 1;
                break;
            case ConsoleKey.Enter:
                Console.Clear();
                switch (selectedItem)
                {
                    case 0:
                        EditOrAddCV();
                        break;
                    case 1:
                        return;
                }
                break;
        }
    }
}
void EditOrAddCV(CV? existingCV = null)
{
    string occupation = existingCV != null ? existingCV.Occupation : null;
    string school = existingCV != null ? existingCV.School : null;
    List<string> skills = existingCV != null ? existingCV.Skills : new List<string>();
    List<Experience> experiences = existingCV != null ? existingCV.Experiences : new List<Experience>();
    List<Language> languages = existingCV != null ? existingCV.Languages : new List<Language>();
    bool honorsDiplom = existingCV != null ? existingCV.HonorsDiplom : false;
    string gitlink = existingCV != null ? existingCV.Gitlink : null;
    string linkedin = existingCV != null ? existingCV.Linkedin : null;
    if (existingCV == null)
    {
        existingCV = new CV();
        loggedUser.cv = existingCV;
        Page_header("Add a CV");
    }
    else
    {
        Page_header("Edit a CV");
    }
occupation_point:
    try
    {
        Console.Write("Enter an Occupation: ");
        string? oc_input = Console.ReadLine();
        existingCV.Occupation = oc_input == null ? occupation : oc_input;
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
        LoadingAnimation(2);
        goto occupation_point;
    }
school_point:
    try
    {
        Console.Write("Enter a School: ");
        string? school_input = Console.ReadLine();
        existingCV.School = school_input == null ? school : school_input;
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
        LoadingAnimation(2);
        goto school_point;
    }
    Console.Write("Enter Skills (press Enter when done): ");
    List<string> skills_input = new List<string>();
    while (true)
    {
    skills_point:
        try
        {
            string? skillsInput = Human.ReadString();
            if (!string.IsNullOrEmpty(skillsInput))
                skills_input.Add(skillsInput);
            else break;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            LoadingAnimation(2);
            goto skills_point;
        }
    }
    if (skills_input == null || skills_input.Count == 0)
        loggedUser.cv.Skills = skills;
    else loggedUser.cv.Skills = skills_input;

    Console.WriteLine("Enter Experiences (press Enter when done):");
    while (true)
    {
    experiences_point:
        try
        {
            Console.Write("Experience Name: ");
            string experienceName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(experienceName))
                break;

            Console.Write("Start Date (dd-MM-yyyy): ");
            DateTime start_date;
            string? startDate = Console.ReadLine();
            if (startDate != null && DateTime.TryParseExact(startDate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out start_date))
            {
                Console.Write("End Date (dd-MM-yyyy): ");
                DateTime end_date;
                string? endDate = Console.ReadLine();
                if (endDate != null && DateTime.TryParseExact(startDate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out end_date))
                {
                    experiences.Add(new Experience(experienceName, start_date, end_date));
                }
                else Base.throwExcpetion("Düzgün formatda tarix daxil edin");
            }
            else Base.throwExcpetion("Düzgün formatda tarix daxil edin");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            LoadingAnimation(2);
            goto experiences_point;
        }
    }
    existingCV.Experiences = experiences;
    Console.WriteLine("Enter Languages (press Enter when done):");
    while (true)
    {
    languages_point:
        try
        {
            Console.Write("Language: ");
            string languageName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(languageName))
                break;
            int lan_selec = 0;
            string[] lang_levels = { "A1", "A2", "B1", "B2", "C1" };
            while (true)
            {
                DisplayMenu(lang_levels, lan_selec);
                ConsoleKeyInfo key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (0 < lan_selec) lan_selec -= 1;
                        break;
                    case ConsoleKey.DownArrow:
                        if (lang_levels.Length - 1 > lan_selec) lan_selec += 1;
                        break;
                    case ConsoleKey.Enter:
                        langLevel sl = (langLevel)lan_selec;
                        languages.Add(new Language(languageName, sl));
                        break;
                }
                if (key.Key == ConsoleKey.Enter) break;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            LoadingAnimation(2);
            goto languages_point;
        }
    }

    existingCV.Languages = languages;


honorsdiplom_point:
    try
    {
        Console.Write("Do you have a Honors Diploma (Yes/No): ");
        ConsoleKeyInfo diplom = Console.ReadKey();
        if (diplom.Key == ConsoleKey.Y)
            existingCV.HonorsDiplom = true;
        else if (diplom.Key == ConsoleKey.N)
            existingCV.HonorsDiplom = false;
        else Base.throwExcpetion("Enter Y or N");
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
        LoadingAnimation(2);
        goto honorsdiplom_point;
    }
github_point:
    try
    {
        Console.Write("GitHub Link: ");
        string? git_input = Console.ReadLine();
        existingCV.Gitlink = git_input == null ? gitlink : git_input;
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
        LoadingAnimation(2);
        goto github_point;
    }
linkedin_point:
    try
    {
        Console.Write("LinkedIn Link: ");
        existingCV.Linkedin = Console.ReadLine();
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
        LoadingAnimation(2);
        goto linkedin_point;
    }
    Log.Write($"{loggedUser.Email}(id:{loggedUser.Id}) CV-de deyisiklikler etdi");
    SavedBack();
}
void EmployeeMenu()
{
    int selectedItem = 0;
    while (true)
    {
        int noti_count = loggedUser.notifications != null ? loggedUser.notifications.Count : 0;
        string notification_text = $"Notifications({noti_count})";
        string[] menuItems = { "Profile", notification_text, "My CV", "Jobs", "Log out" };
        DisplayMenu(menuItems, selectedItem);
        ConsoleKeyInfo keyInfo = Console.ReadKey();
        switch (keyInfo.Key)
        {
            case ConsoleKey.DownArrow:
                if (selectedItem < menuItems.Length - 1) selectedItem += 1;
                break;
            case ConsoleKey.UpArrow:
                if (selectedItem > 0) selectedItem -= 1;
                break;
            case ConsoleKey.Enter:
                Console.Clear();
                switch (selectedItem)
                {
                    case 0:
                        DisplayMyProfile();
                        break;
                    case 1:
                        DisplayNotifications();
                        break;
                    case 2:
                        DisplayMyCV();
                        break;
                    case 3:
                        DisplayJobs();
                        break;
                    case 4:
                        Log.Write($"{loggedUser.Email}(id:{loggedUser.Id}) hesabindan cixis etdi");
                        loggedUser = default;
                        SignInMenu();
                        break;
                }
                break;
        }
    }
}
/* */

void DisplayPosts(List<Post> posts)
{
    string[] menuItems = {"Add a post","Edit a post","Delete a post","Back"};
    int selectedItem = 0;
    while (true)
    {
        Page_header("Employer Posts");
        DisplayMenu(menuItems,selectedItem);
        Console.WriteLine($"\n");
        foreach(var az in posts){
            Console.WriteLine(az.ToString());
        }
        ConsoleKeyInfo choice = Console.ReadKey();
        switch (choice.Key)
        {
            case ConsoleKey.DownArrow:
                if (selectedItem < menuItems.Length - 1) selectedItem += 1;
                break;
            case ConsoleKey.UpArrow:
                if (selectedItem > 0) selectedItem -= 1;
                break;
            case ConsoleKey.Enter:
                if(selectedItem == 0)
                    AddPost(posts);
                else if(selectedItem == 1)
                    EditPost(posts);
                else if(selectedItem == 2)
                    DeletePost(posts);
                else if(selectedItem == 3)
                    return;
                break;
        }
        
    }
}

void AddPost(List<Post> posts)
{
    Page_header("Add a new Post");
    Post newPost = new Post(); 
    newPost.DatePosted = DateTime.Now;
    addpost_point:
    try
    {
        Console.WriteLine("Select a Category:");
        foreach (Category_enum category in Enum.GetValues(typeof(Category_enum)))
        {
            Console.WriteLine($"[{(int)category}] {category}");
        }

        int selectedCategory = -1;
        while (selectedCategory < 0 || selectedCategory >= Enum.GetValues(typeof(Category_enum)).Length)
        {
            Console.Write("Enter the category number: ");
            if (int.TryParse(Console.ReadLine(), out selectedCategory) && Enum.IsDefined(typeof(Category_enum), selectedCategory))
            {
                newPost.Category = (Category_enum)selectedCategory;
            }
            else
            {
                Console.WriteLine("Invalid category number. Please try again.");
            }
        }
        Console.Write("Job Title: ");
        newPost.JobTitle = Console.ReadLine();

        Console.Write("Description: ");
        newPost.Description = Console.ReadLine();

        Console.Write("Location: ");
        newPost.Location = Console.ReadLine();

        Console.Write("Salary: ");
        string salaryInput = Console.ReadLine();

        if (!string.IsNullOrWhiteSpace(salaryInput) && double.TryParse(salaryInput, out double parsedSalary))
        {
            newPost.Salary = parsedSalary;
        }
        posts.Add(newPost);
        SavedBack(false);
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
        LoadingAnimation(2);
        goto addpost_point;
    }
}

void EditPost(List<Post> posts)
{
    int selectedPostIndex = 0;
    while (true)
    {
        Page_header("Edit Post");
        string[] postTitles = posts.Select(post => post.JobTitle).ToArray();
        string[] menuItems = postTitles.Append("Quit").ToArray();
        DisplayMenu(menuItems, selectedPostIndex);
        ConsoleKeyInfo keyInfo = Console.ReadKey();
        switch (keyInfo.Key)
        {
            case ConsoleKey.UpArrow:
                if (selectedPostIndex > 0)
                {
                    selectedPostIndex--;
                }
                break;

            case ConsoleKey.DownArrow:
                if (selectedPostIndex < posts.Count)
                {
                    selectedPostIndex++;
                }
                break;

            case ConsoleKey.Enter:
                if (selectedPostIndex == posts.Count)
                {
                    return;
                }
                EditSinglePost(posts[selectedPostIndex]);
                break;
        }
    }
}
void EditSinglePost(Post post)
{
    Page_header($"Edit Post - {post.JobTitle}");
    edit_point:
    try
    {
        Console.Write("Job Title: ");
        post.JobTitle = Console.ReadLine();

        Console.Write("Description: ");
        post.Description = Console.ReadLine();

        Console.Write("Location: ");
        post.Location = Console.ReadLine();

        Console.Write("Salary ");
        string salaryInput = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(salaryInput) && double.TryParse(salaryInput, out double parsedSalary))
        {
            post.Salary = parsedSalary;
        }
        Console.WriteLine("Categories:");
        foreach (Category_enum category in Enum.GetValues(typeof(Category_enum)))
        {
            Console.WriteLine($"{(int)category} - {category}");
        }
        string categoryInput = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(categoryInput))
        {
            if (Enum.TryParse(categoryInput, out Category_enum category))
            {
                post.Category = category;
            }
            else
            {
                throw new Exception("Sehv cateogry. Please enter a valid category number");
            }
        }
        SavedBack(false);
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
        LoadingAnimation(2);
        goto edit_point;
    }
}

void DeletePost(List<Post> posts)
{
    Page_header("Delete Post");
    while (true)
    {
        deletepost_point:
        Console.WriteLine("Select a post to delete:");
        for (int i = 0; i < posts.Count; i++)
        {
            Console.WriteLine($"[{i + 1}] {posts[i].JobTitle}");
        }
        Console.WriteLine("[Q] Quit");
        Console.Write("Enter: ");
        string choice = Console.ReadLine();
        if (choice.ToUpper() == "Q")
        {
            return;
        }
        if (int.TryParse(choice, out int postIndex) && postIndex >= 1 && postIndex <= posts.Count)
        {
            Post postToDelete = posts[postIndex - 1];
            try
            {
                posts.Remove(postToDelete);
                SavedBack(false);
                return;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                LoadingAnimation(2);
                goto deletepost_point;
            }
        }
        else
        {
            ToBack();
        }
    }
}
void SendCVRequest(Post selectedJob, Employee employee, List<Employer> employersList)
{
    Employer employer = FindEmployer(selectedJob, employersList);
    if (employer != null)
    {
        employer.Vacancies.Add(employee.cv);
        employer.notifications.Add(new Notification($"Job request for {selectedJob.JobTitle}", $"{selectedJob.JobTitle} => {employee.ToString()}"));
        Console.WriteLine("c");
        Console.WriteLine("CV request sent successfully");
    }
    else
    {
        Base.throwExcpetion("Job not found");
    }
}

Employer FindEmployer(Post job, List<Employer> employersList)
{
    foreach (Employer employer in employersList)
    {
        if (employer.posts.Contains(job))
        {
            return employer;
        }
    }
    return null;
}
void DisplayJobs()
{
    Log.Write($"{loggedUser.Email}(id:{loggedUser.Id}) Jobs menyusunda daxil oldu");
    int selectedJobIndex;
    while (true)
    {
        int count = 1;
        Page_header("Available Jobs");
        for (int i = 0; i < employers.Count; i++)
        {
            List<Post> jobs = employers[i].posts;
            Console.WriteLine($"Company: {employers[i].CompanyName}");
            for (int j = 0; j < jobs.Count; j++)
            {
                Console.WriteLine($"[{count}] => {jobs[j].ToString()}");
                count+=1;
            }
        }

        Console.WriteLine("[0] Go Back");
        Console.Write("Select a job : ");
        string choice = Console.ReadLine();
        if (choice == "0")
        {
            return; 
        }
        else if (int.TryParse(choice, out selectedJobIndex) && Convert.ToInt32(choice) > 0)
        {
            Post selectedJob = default;
            int count2 = 0;
            for (int i = 0; i < employers.Count; i++)
            {
                List<Post> jobs = employers[i].posts;
                for (int j = 0; j < jobs.Count; j++)
                {
                    if (count2 == selectedJobIndex - 1)
                    {
                        selectedJob = jobs[j];
                        break;
                    }
                    count2 += 1;
                }
                if (selectedJob != null)
                {
                    break;
                }
            }
            if (selectedJob == null) continue;
            SendCVRequest(selectedJob, loggedUser, employers);
            Log.Write($"{loggedUser.Email}(id:{loggedUser.Id}) CV-ni gonderdi");
            SaveEmployers();
            Console.WriteLine("Succesfully sent the cv");
            LoadingAnimation(2);
            break;
        }
    }
}
/* */
void EmployerMenu()
{
    int selectedItem = 0;
    while (true)
    {
        int noti_count = loggedEmployer.notifications != null ? loggedEmployer.notifications.Count : 0;
        string notification_text = $"Notifications({noti_count})";
        string[] menuItems = { "Profile", notification_text, "My Posts", "Log out" };
        DisplayMenu(menuItems, selectedItem);
        ConsoleKeyInfo keyInfo = Console.ReadKey();
        switch (keyInfo.Key)
        {
            case ConsoleKey.DownArrow:
                if (selectedItem < menuItems.Length - 1) selectedItem += 1;
                break;
            case ConsoleKey.UpArrow:
                if (selectedItem > 0) selectedItem -= 1;
                break;
            case ConsoleKey.Enter:
                Console.Clear();
                switch (selectedItem)
                {
                    case 0:
                        DisplayMyProfileEmployer();
                        break;
                    case 1:
                        DisplayNotifications(false);
                        break;
                    case 2:
                        DisplayPosts(loggedEmployer.posts);
                        break;
                    case 3:
                        Log.Write($"{loggedEmployer.Email}(id:{loggedEmployer.Id}) hesabindan cixis etdi");
                        loggedEmployer = default;
                        SignInMenu();
                        break;
                }
                break;
        }
    }

}
Main();
