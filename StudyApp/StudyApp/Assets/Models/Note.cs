namespace StudyApp.Assets.Models
{
    public class Note
    {
        public Note(string owner = null, string title = null, string content = null, string guid = null)
        {
            Owner = owner;
            Title = title;
            Content = content;
            GUID = guid;
        }

        public string Owner { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string GUID { get; set; }
    }
}