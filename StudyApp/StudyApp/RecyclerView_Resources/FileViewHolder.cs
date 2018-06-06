using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using StudyApp.Interface;

namespace StudyApp.RecyclerView_Resources
{
    public class FileViewHolder : RecyclerView.ViewHolder, View.IOnClickListener, View.IOnLongClickListener
    {

        public TextView FileName { get; private set; }
        public TextView FileSize { get; private set; }
        public TextView FileId { get; private set; }

        private IItemClickListener itemClickListener;


        public FileViewHolder(View itemView) : base(itemView)
        {
            Color textColor = Color.White;
            FileName = itemView.FindViewById<TextView>(Resource.Id.textView);
            FileName.SetTextColor(textColor);

            FileSize = itemView.FindViewById<TextView>(Resource.Id.fileSize);
            FileSize.SetTextColor(textColor);
            
            FileId = itemView.FindViewById<TextView>(Resource.Id.hiddenFileId);

            itemView.SetOnClickListener(this);
            itemView.SetOnLongClickListener(this);

        }

        public void SetItemClickListener(IItemClickListener itemClickListener)
        {
            this.itemClickListener = itemClickListener;
        }

        public void OnClick(View v)
        {
            itemClickListener.OnClick(v, AdapterPosition, false);
        }

        public bool OnLongClick(View v)
        {
            itemClickListener.OnClick(v, AdapterPosition, true);
            return true;
        }
    }
}