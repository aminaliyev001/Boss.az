namespace NotiNameSpace;
public enum NotificationStatus
{
    Unread, Read
}
public interface INotifiable
{
    string GetNotificationSenderInfo();
}
public class Notification
{
    public string Title { get; set; }
    public string Text { get; set; }
    public NotificationStatus Status { get; set; }
    public Notification(string title, string text)
    {
        Title = title;
        Text = text;
        Status = NotificationStatus.Unread;
    }
    public Notification() {}
    public override string ToString()
    {
        return $"Title: {Title}\nText: {Text}\nStatus: {Status.ToString()}";
    }
}