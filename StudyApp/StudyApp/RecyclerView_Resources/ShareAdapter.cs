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

namespace StudyApp.RecyclerView_Resources
{
    public class ShareAdapter : RecyclerView.Adapter
    {
        private Context context;

        public ShareAlbum mShareAlbum;
        public ShareAdapter(ShareAlbum shareUser, Context contxt)
        {
            mShareAlbum = shareUser;
            context = contxt;
        }
        public override RecyclerView.ViewHolder
            OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.ShareCardView, parent, false);
            
            RecyclerView_Resources.ShareViewHolder vh = new RecyclerView_Resources.ShareViewHolder(itemView);
            return vh;
        }
        public override void
           OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            RecyclerView_Resources.ShareViewHolder vh = holder as RecyclerView_Resources.ShareViewHolder;
            vh.SharedUserName.Text= mShareAlbum[position].UserName;
            vh.FilePermissionType.Text = "Edit";
        }

        public override int ItemCount
        {
            get { return mShareAlbum.NumUsers; }
        }
    }
} 