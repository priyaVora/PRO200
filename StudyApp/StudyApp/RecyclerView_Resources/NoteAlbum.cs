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
    public class NoteAlbum
    {
        static NoteMini[] mBuiltInNotes = {
            new NoteMini {Title = "Note 1", GUID="1"},
            new NoteMini {Title = "Note 2", GUID="2"},
            new NoteMini {Title = "Note 3",  GUID="3"},
            new NoteMini {Title = "Note 4",  GUID="4"},
            new NoteMini {Title = "Note 5", GUID="5"},
            new NoteMini {Title = "Note 6", GUID="6"},
            new NoteMini {Title = "Note 7", GUID="7"},
            new NoteMini {Title = "Note 8", GUID="8"},
            new NoteMini {Title = "Note 9", GUID="9"},
            new NoteMini {Title = "Note 10",GUID="10"},
            new NoteMini {Title = "Note 12", GUID="11"},
            new NoteMini {Title = "Note 13", GUID="12"},
            new NoteMini {Title = "Note 14", GUID="13"},
            new NoteMini {Title = "Note 15", GUID="14"},
            new NoteMini {Title = "Note 16", GUID="15"},
            new NoteMini {Title = "Note 17", GUID="16"},
            new NoteMini {Title = "Note 18", GUID="17"},
            new NoteMini {Title = "Note 19", GUID="18"},

        };

        // Array of Notes that make up the album:
        private NoteMini[] mNotes;
        public NoteAlbum()
        {
            mNotes = mBuiltInNotes;
        }
        public NoteAlbum(List<NoteMini> list)
        {
            mNotes = list.ToArray();
        }

        // Return the number of notes in the notes collection:
        public int NumNotes
        {
            get { return mNotes.Length; }
        }

        public NoteMini this[int i]
        {
            get { return mNotes[i]; }
        }
    }
}