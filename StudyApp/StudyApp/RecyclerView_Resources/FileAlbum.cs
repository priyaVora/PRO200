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
    public class FileAlbum
    {
        // Built-in photo collection - this could be replaced with
        // a photo database:

        static FileMini[] mBuiltInFiles = {
             new FileMini {Name = "File 1",
                        Extension = ".txt", GUID = "1"},
             new FileMini {Name = "File 2",
                        Extension = ".txt", GUID = "2"},
             new FileMini {Name = "File 3",
                        Extension = ".txt", GUID = "3"},
             new FileMini {Name = "File 4",
                        Extension = ".txt", GUID = "4" },
             new FileMini {Name = "File 5",
                        Extension = ".txt", GUID = "5" },
             new FileMini {Name = "File 6",
                        Extension = ".txt", GUID = "6"},
             new FileMini {Name = "File 7",
                        Extension = ".txt", GUID = "7"},
             new FileMini {Name = "File 8",
                        Extension = ".txt", GUID = "8"},
             new FileMini {Name = "File 9",
                        Extension = ".txt", GUID = "9" },
             new FileMini {Name = "File 10",
                        Extension = ".txt", GUID = "10" },
             new FileMini {Name = "File 11",
                        Extension = ".txt", GUID = "11"},
             new FileMini {Name = "File 12",
                        Extension = ".txt", GUID = "12"},
             new FileMini {Name = "File 13",
                        Extension = ".txt", GUID = "13"},
             new FileMini {Name = "File 14",
                        Extension = ".txt", GUID = "14" },
             new FileMini {Name = "File 15",
                        Extension = ".txt", GUID = "15" },
             new FileMini {Name = "File 16",
                        Extension = ".txt", GUID = "16"},
             new FileMini {Name = "File 17",
                        Extension = ".txt", GUID = "17"},
             new FileMini {Name = "File 18",
                        Extension = ".txt", GUID = "18"},
             new FileMini {Name = "File 19",
                        Extension = ".txt", GUID = "19" },
             new FileMini {Name = "File 20",
                        Extension = ".txt", GUID = "20" },

        };

        // Array of files that make up the album:
        private FileMini[] mFiles;

   

        // Create an instance copy of the built-in files list, later will grab the user's list of files
     
        public FileAlbum()
        {
            mFiles = mBuiltInFiles;
    
        }

        public FileAlbum(List<FileMini> fileMinis)
        {
            mFiles = fileMinis.ToArray();

        }

        // Return the number of file in the file collection:
        public int NumFiles
        {
            get { return mFiles.Length; }
        }

        
        public FileMini this[int i]
        {
            get { return mFiles[i]; }
        }
    }
}