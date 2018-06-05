using System.Collections.Generic;

namespace StudyApp.Assets.Models
{
    public class UserAccount
    {
        public UserAccount(List<Goal> listOfGoals = null, List<Note> listOfNotes = null, List<File> listOfFiles = null, string userName = null, string phoneNumber = null, string email = null)
        {
            ListOfGoals = listOfGoals;
            ListOfNotes = listOfNotes;
            ListOfFiles = listOfFiles;
            UserName = userName;
            PhoneNumber = phoneNumber;
            Email = email;
        }

        public List<Goal> ListOfGoals { get; set; }
        public List<Note> ListOfNotes { get; set; }
        public List<File> ListOfFiles { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Points { get; set; }
    }
}