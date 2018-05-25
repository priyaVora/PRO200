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
    class NoteController
    {
        public Note GetNote(string guid, string username)
        {
            ServerIOController serverIo = new ServerIOController();
            return serverIo.GetNote(guid, username);
        }

        public List<NoteMini> GetNotePreviews(string username)
        {
            ServerIOController serverIo = new ServerIOController();
            return serverIo.GetNotePreviews(username);
        }

        public void CreateNote(Note note, string username)
        {
            ServerIOController serverIo = new ServerIOController();
            serverIo.CreateNote(note, username);
        }

        public void UpdateNote(Note note)
        {
            ServerIOController serverIo = new ServerIOController();
            serverIo.UpdateNote(note);
        }
    }
}