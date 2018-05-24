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
    public class FileController
    {
        public void UploadFile(File upload)
        {
            ServerIOController serverIo = new ServerIOController();
            serverIo.UploadFile(upload);
        }

        public File DownloadFile(string guid)
        {
            ServerIOController serverIo = new ServerIOController();
            return serverIo.DownloadFile(guid);
        }

        public void ShareFile(string guid, Dictionary<string, Permission> users)
        {
            ServerIOController serverIo = new ServerIOController();
            serverIo.ShareFile(guid, users);
        }

        public void DeleteFile(string guid)
        {
            ServerIOController serverIo = new ServerIOController();
            serverIo.DeleteFile(guid);
        }
    }
}