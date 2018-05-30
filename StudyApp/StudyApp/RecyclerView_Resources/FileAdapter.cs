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
using StudyApp.RecyclerView_Resources;

namespace StudyApp
{
    public class FileAdapter : RecyclerView.Adapter
    {
        public FileAlbum mFileAlbum;
        public FileAdapter(FileAlbum file)
        {
            mFileAlbum = file;
        }

        public override RecyclerView.ViewHolder
            OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.CardView, parent, false);
            RecyclerView_Resources.FileViewHolder vh = new RecyclerView_Resources.FileViewHolder(itemView);
            return vh;
        }

        public override void
            OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            RecyclerView_Resources.FileViewHolder vh = holder as RecyclerView_Resources.FileViewHolder;
            vh.FileName.Text = mFileAlbum[position].Name;
        }


        public override int ItemCount
        {
            get { return mFileAlbum.NumFiles; }
        }

    }
}