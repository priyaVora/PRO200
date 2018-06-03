using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using StudyApp.Assets.Models;

namespace StudyApp.RecyclerView_Resources
{
    public class ShareAlbum
    {
        static UserAccount[] mBuiltInUserAccount =
        {
            new UserAccount{ UserName = "prvora89", PhoneNumber="415-415-415",Email="test89@gmail.com",
            ListOfFiles = null, ListOfGoals = null, ListOfNotes = null},
            new UserAccount{ UserName = "User2", PhoneNumber="415-415-415",Email="test89@gmail.com",
                            ListOfFiles = null, ListOfGoals = null, ListOfNotes = null},
            new UserAccount{ UserName = "User3", PhoneNumber="415-415-415",Email="test89@gmail.com",
                            ListOfFiles = null, ListOfGoals = null, ListOfNotes = null},
            new UserAccount{ UserName = "User4", PhoneNumber="415-415-415",Email="test89@gmail.com",
                            ListOfFiles = null, ListOfGoals = null, ListOfNotes = null},
            new UserAccount{ UserName = "User5", PhoneNumber="415-415-415",Email="test89@gmail.com",
                            ListOfFiles = null, ListOfGoals = null, ListOfNotes = null},
            new UserAccount{ UserName = "User6", PhoneNumber="415-415-415",Email="test89@gmail.com",
                            ListOfFiles = null, ListOfGoals = null, ListOfNotes = null},
            new UserAccount{ UserName = "User7", PhoneNumber="415-415-415",Email="test89@gmail.com",
                            ListOfFiles = null, ListOfGoals = null, ListOfNotes = null},
        };

        private UserAccount[] mUserAccounts;

        public ShareAlbum()
        {
            mUserAccounts = mBuiltInUserAccount;
        }

        public int NumUsers
        {
            get { return mUserAccounts.Length; }
        }

        public UserAccount this[int i]
        {
            get { return mUserAccounts[i]; }
        }
    }
}