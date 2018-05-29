using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace StudyApp.Assets.Views.CompoundViews {
    
    public class CompoundNavBarView : LinearLayout {

        protected CompoundNavBarView(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer) { }
        public CompoundNavBarView(Context context) : base(context) { }
        public CompoundNavBarView(Context context, IAttributeSet attrs) : base(context, attrs) { }
        public CompoundNavBarView(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr) { }
        public CompoundNavBarView(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes) { }

        void Initialize() {
            Inflate(Context, Resource.Layout.navbar, this);
        }
    }
}