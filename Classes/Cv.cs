using extraSpace;
namespace CvSpace;
public enum langLevel
{
    A1, A2, B1, B2, C1
}
public enum Category_enum
{
    IT,
    Finance,
    Healthcare,
    Marketing,
    Engineering,
    Sales,
    Education,
    Other
}

class Experience : Base
{
    private string name;
    public string Name { get => name; set { if (checkStringLen(value)) name = value; else throwExcpetion("Ad boş qala bilməz"); } }
    public DateTime Started { get; set; }
    public DateTime Ended { get; set; }
    public Experience(string _name, DateTime _start, DateTime _end)
    {
        Name = _name;
        Started = _start;
        Ended = _end;
    }
    public Experience() {}
    public override string ToString()
    {
        return $"{name}\n{Started.ToString("dd-MM-yyy")} - {Ended.ToString("dd-MM-yyy")}";
    }
}
class Language : Base
{
    private string lang;
    public string Lang { get => lang; set { if (checkStringLen(value)) lang = value; else throwExcpetion("Dil boş qala bilməz: məsələn ingilis dili"); } }
    public langLevel Level { get; set; }
    public Language(string _lang, langLevel _level)  {
        Lang = _lang;
        Level = _level;
    }
    public Language()  {
        
    }
    public override string ToString()
    {
        return $"{lang} - {Level.ToString()}";
    }
}
class CV : Base
{
    public bool checkUrl(string url, string shouldContain)
    {
        return url.Contains(shouldContain);
    }
    private string occupation;
    public string Occupation { get => occupation; set { if (checkStringLen(value)) occupation = value; else throwExcpetion("İxtisas boş qala bilməz"); } }
    private string school;
    public string School { get => school; set { if (checkStringLen(value)) school = value; else throwExcpetion("Məktəb boş qala bilməz"); } }
    public List<string> Skills { get; set; }
    public List<Experience> Experiences { get; set; }
    public List<Language> Languages { get; set; }
    public bool HonorsDiplom { get; set; }
    private string? gitlink;
    public string Gitlink { get => gitlink; set { if (checkUrl(value,"github.com")) gitlink = value;else Base.throwExcpetion("Duzgun formada linki qeyd edin"); } }
    private string? linkedin;
    public string Linkedin { get => linkedin; set { if (checkUrl(value,"linkedin.com")) linkedin = value; else Base.throwExcpetion("Duzgun formada linki qeyd edin"); } }

    public Category_enum Category {get;set;}
    public CV(string occupation, string school, List<string> skills, List<Experience> experiences, List<Language> languages, bool honorsDiplom, string gitlink, string linkedin,Category_enum category_)
    {
        Occupation = occupation;
        School = school;
        Skills = skills;
        Experiences = experiences;
        Languages = languages;
        HonorsDiplom = honorsDiplom;
        Gitlink = gitlink;
        Linkedin = linkedin;
        Category = category_;
    }
    public CV() { }
    public override string ToString()
    {
        string skills = Skills != null && Skills.Count > 0
            ? string.Join(", ", Skills)
            : "No skills listed";

        string experiences = Experiences != null && Experiences.Count > 0
            ? string.Join("\n", Experiences)
            : "No experiences listed";

        string languages = Languages != null && Languages.Count > 0
        ? string.Join("\n",Languages)
        : "No languages listed";
        return $"Occupation: {Occupation ?? "Not specified"}\n" +
               $"School: {School ?? "Not specified"}\n" +
               $"Skills: {skills}\n" +
               $"Experiences:\n{experiences}\n" +
               $"Languages: {languages}\n" + 
               $"Honors Diploma: {(HonorsDiplom ? "Yes" : "No")}\n" +
               $"GitHub: {Gitlink ?? "Not specified"}\n" +
               $"LinkedIn: {Linkedin ?? "Not specified"}\n"+
               $"Category: {Category.ToString() ?? "Not specified"}\n";
    }
}