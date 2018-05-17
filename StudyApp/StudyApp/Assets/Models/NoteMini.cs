namespace StudyApp.Assets.Models
{
    public class NoteMini
    {
        public NoteMini(string title = null, string guid = null)
        {
            Title = title;
            GUID = guid;
        }

        public string Title { get; set; }
        public string GUID { get; set; }
    }
}