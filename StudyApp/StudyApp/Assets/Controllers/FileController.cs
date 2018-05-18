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

namespace StudyApp.Assets.Controllers
{
    class FileController
    {
        public void UploadFile(File upload)
        {
            throw new NotImplementedException();
        }

        public File DownloadFile(string guid)
        {
            throw new NotImplementedException();
        }

        public void ShareFile(Dictionary<UserAccount, Permission> users)
        {
            throw new NotImplementedException();
        }

        public void DeleteFile(string guid)
        {
            throw new NotImplementedException();
        }
    }
}