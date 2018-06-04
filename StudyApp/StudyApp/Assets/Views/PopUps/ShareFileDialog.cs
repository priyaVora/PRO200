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
using StudyApp.RecyclerView_Resources;

namespace StudyApp.Assets.Views.PopUps
{
   public class ShareFileDialog: DialogFragment
    {
        RecyclerView mRecyclerView;
        //RecyclerView.LayoutManager mLayoutManager;
        ShareAdapter mAdapter;
        ShareAlbum mShareAlbum;
        Context currentContext = null;
        public ShareFileDialog(Context context)
        {
            currentContext = context;
        }
 
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            mShareAlbum = new ShareAlbum();
            
            var view = inflater.Inflate(Resource.Layout.ShareFilePage, container,false);
 
            Button cancelBtn = view.FindViewById<Button>(Resource.Id.cancelBtn);
            cancelBtn.Click += CancelClick;
            Button confirmBtn = view.FindViewById<Button>(Resource.Id.confirmBtn);
            confirmBtn.Click += ConfirmClick;


            mRecyclerView = (RecyclerView)view.FindViewById(Resource.Id.sharedUserRecyclerView);
            mRecyclerView.SetLayoutManager(new LinearLayoutManager(currentContext));

            mAdapter = new ShareAdapter(mShareAlbum, currentContext);

            ShareAdapter adapter = new ShareAdapter(mShareAlbum, currentContext);
            mRecyclerView.SetAdapter(adapter);
           
            return view;
        }

        public void CancelClick(object sender, EventArgs args)
        {
            Toast.MakeText(currentContext, "Cancel", ToastLength.Short).Show();

            this.Dismiss();
        }

        public void ConfirmClick(object sender, EventArgs args)
        {
            Toast.MakeText(currentContext, "Save", ToastLength.Short).Show();

            this.Dismiss();
        }


    }
}