using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Database;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

using StudyApp.Assets.Controllers;
using StudyApp.RecyclerView_Resources;
using StudyApp.Assets.Models;
using File = StudyApp.Assets.Models.File;
using Android.Graphics;

namespace StudyApp.Assets.Views
{

    [Activity(Label = "FileViewActivity")]
    public class FileViewActivity : CommonActivity
    {
        RecyclerView mRecyclerView;
        RecyclerView.LayoutManager mLayoutManager;
        FileAdapter mAdapter;
         FileAlbum mFileAlbum;



        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            ActionBar.Hide();
            mFileAlbum = new FileAlbum();
            
            SetFileData();
            //Dictionary<string, Permission> users = new Dictionary<string, Permission>();
            //users.Add(this.userController.CurrentUser.UserName, Permission.Owner);

            //byte[] content = new byte[10];
            //File testFile = new File();
            //testFile.Users = users;
            //testFile.GUID = "675_2;";
            //testFile.Content = content;
            //testFile.Extension = ".txt";
            //testFile.Name = "fileTwo";

            //FileController controller = new FileController();
            //controller.UploadFile(testFile);

            // mCommonAlbum = new CommonAlbum();
            /*
             * This code is how to replace the placeholder layout that's part of the CommonLayout.
             */
            FrameLayout frame = FindViewById<FrameLayout>(Resource.Id.Common_FrameLayout);
            View file = LayoutInflater.Inflate(Resource.Layout.FilePage, null); // Replace the inside of this method call with your desired layout
           
            frame.AddView(file.FindViewById<LinearLayout>(Resource.Id.File_Layout));
            SetUpNavBar();


            Button AddFileBtn = FindViewById<Button>(Resource.Id.FilePage_AddFileButton);
            AddFileBtn.Click += UploadClick;

            StudyApp.Assets.Models.File fileOne = new StudyApp.Assets.Models.File();

           
            /*
             * This method needs to be called on the OnCreate method for any activities inheriting from CommonActivity,
             * since this is what initializes the navbar
             */

            mRecyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView);

            // Plug in the linear layout manager:
            mLayoutManager = new LinearLayoutManager(this);
            mRecyclerView.SetLayoutManager(mLayoutManager);

            // Plug in my adapter:
            mAdapter = new FileAdapter(mFileAlbum, this, this.userController.CurrentUser);
            mRecyclerView.SetAdapter(mAdapter);
        }

        private void SetFileData()
        {
            List<File> files = this.userController.GetUser(userController.CurrentUser.UserName).ListOfFiles;

            mFileAlbum = new FileAlbum(files.Select(f => (FileMini) f).ToList());
           
        }

        Intent intent;

        private void PickFile(string folder, string extension)
        {
            intent = new Intent(Intent.ActionGetContent);
            folder = "C:\\Users\\Priya\\Desktop\\FolderName";
            extension = ".txt";
            // intent.SetType(folder + "/" + extension);
            intent.SetType("image/*");
           // intent.AddCategory(Intent.CategoryOpenable);

            //intent.SetType("file/*");

            intent.AddCategory(Intent.CategoryOpenable);
            try
            {
                StartActivityForResult(Intent.CreateChooser(intent, "Select a file"),
                          0);

            }
            catch (System.Exception exAct)
            {
                System.Diagnostics.Debug.Write(exAct);
            }
          //  StartActivityForResult(intent, 0);
        }

   
        public void UploadClick(object sender, EventArgs args)
        {
            Toast.MakeText(this, "Add File", ToastLength.Short).Show();

            PickFile(null, null);
        }

        public void LongPress(object sender, EventArgs args)
        {
            Console.WriteLine("File was long pressed");
        }
        private string GetRealPathFromURI(Android.Net.Uri uri)
        {
            string doc_id = "";
            using (var c1 = ContentResolver.Query(uri, null, null, null, null))
            {
                c1.MoveToFirst();
                string document_id = c1.GetString(0);
                doc_id = document_id.Substring(document_id.LastIndexOf(":") + 1);
            }

            string path = null;

            // The projection contains the columns we want to return in our query.
            string selection = Android.Provider.MediaStore.Images.Media.InterfaceConsts.Id + " =? ";
            using (var cursor = ContentResolver.Query(Android.Provider.MediaStore.Images.Media.ExternalContentUri, null, selection, new string[] { doc_id }, null))
            {
                if (cursor == null) return path;
                var columnIndex = cursor.GetColumnIndexOrThrow(Android.Provider.MediaStore.Images.Media.InterfaceConsts.Data);
                cursor.MoveToFirst();
                path = cursor.GetString(columnIndex);
            }
            return path;
        }
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent intent)
        {
            try
            {
                base.OnActivityResult(requestCode, resultCode, intent);
                if (requestCode == 0)
                {
                    if (resultCode == Result.Ok)
                    {
                        Android.Net.Uri uri = intent.Data;
                        //intent variable has a Field named Data which is the complete URI for the file. 
                        //Android.Net.Uri uris = Android.Net.Uri.FromParts(intent.Data.Scheme, intent.Data.SchemeSpecificPart, intent.Data.Fragment);
                        //System.IO.Stream input = ContentResolver.OpenInputStream(intent.Data);

                        //FileStream fileStream = input as FileStream;

                        //if (fileStream != null)
                        //{
                        //    //It was really a file stream, get your information here
                        //    Toast.MakeText(this, "Stream is still null", ToastLength.Short).Show();
                        //}
                        //else
                        //{
                        //    //The stream was not a file stream, do whatever is required in that case
                        //    Toast.MakeText(this, "Stream is not a filestream", ToastLength.Short).Show();
                        //}


                        if (GetRealPathFromURI(uri) == null)
                        {

                            Toast.MakeText(this, "Path is still null", ToastLength.Short).Show();
                        }
                        Dictionary<string, Permission> users = new Dictionary<string, Permission>();
                        users.Add(this.userController.CurrentUser.UserName, Permission.Owner);
                        Toast.MakeText(this, GetRealPathFromURI(uri), ToastLength.Short).Show();
                        byte[] file = System.IO.File.ReadAllBytes(GetRealPathFromURI(uri));
                        File uploadFile = new File();
                        uploadFile.GUID = Guid.NewGuid().ToString();
                        string filename = System.IO.Path.GetFileName(GetRealPathFromURI(uri));
                        uploadFile.Name = filename;
                        uploadFile.Users = users;
                        uploadFile.Extension = "jpg";
                        uploadFile.Content = file;
                        
                        fileController.UploadFile(uploadFile);
                    }
                }
            } catch(Exception e)
            {
                Toast.MakeText(this, e.ToString(), ToastLength.Long).Show();
                string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);
                Note testNote = new Note();
                //owner title content guid
                testNote.Owner = this.userController.CurrentUser.UserName;
                testNote.GUID = "5";
                testNote.Title = "Error Note";
                testNote.Content = e.ToString();
                System.IO.File.WriteAllText(path, e.ToString());
                NoteController controller = new NoteController();
                controller.CreateNote(testNote, this.userController.CurrentUser.UserName);
            }
        }


    }
} 