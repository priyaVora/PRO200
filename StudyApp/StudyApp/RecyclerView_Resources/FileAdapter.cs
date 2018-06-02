using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using StudyApp.Assets.Models;
using StudyApp.Assets.Views.PopUps;
using StudyApp.Interface;
using StudyApp.RecyclerView_Resources;

namespace StudyApp
{
    public class FileAdapter : RecyclerView.Adapter, IItemClickListener
    {
        private Context context;

        public FileAlbum mFileAlbum;
        public FileAdapter(FileAlbum file, Context contxt)
        {
            mFileAlbum = file;
            context = contxt;
        }

        public override RecyclerView.ViewHolder
            OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.File_CardView, parent, false);
            RecyclerView_Resources.FileViewHolder vh = new RecyclerView_Resources.FileViewHolder(itemView);
            return vh;
        }

        public override void
            OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            RecyclerView_Resources.FileViewHolder vh = holder as RecyclerView_Resources.FileViewHolder;
            vh.FileName.Text = mFileAlbum[position].Name;
            vh.FileSize.Text = "81mb";
            vh.SetItemClickListener(this);
        }

        public void OnClick(View itemView, int position, bool isLongClick)
        {
            try
            {
                if (isLongClick)
                {
                    Toast.MakeText(context, "Share Options", ToastLength.Short).Show();
                    FragmentTransaction transaction = ((Activity)context).FragmentManager.BeginTransaction();
                    ShareFileDialog currentDialog = new ShareFileDialog();
                    currentDialog.Show(transaction,"dialog fragment");
                }
                else
                {
                    Toast.MakeText(context, "Downloaded File ", ToastLength.Short).Show();
                }
            }
            catch (Exception e)
            {
                Toast.MakeText(context, e.ToString() , ToastLength.Short).Show();

            }

        }

        public override int ItemCount
        {
            get { return mFileAlbum.NumFiles; }
        }

    }
}