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

namespace StudyApp.RecyclerView_Resources
{
    // Photo: contains image resource ID and caption:
    public class CommonItem
    {
        // Photo ID for this photo:
        public int mCommonId;

        // Caption text for this photo:
        public string mCaption;

        // Return the ID of the photo:
        public int PhotoID
        {
            get { return mCommonId; }
        }

        // Return the Caption of the photo:
        public string Caption
        {
            get { return mCaption; }
        }
    }

    // Photo album: holds image resource IDs and caption:
    public class CommonAlbum
    {
        // Built-in photo collection - this could be replaced with
        // a photo database:

        /*
        * Get Number of Files in User's List of Files
        */
        int fileCount = 9;

        /*
        * This code is how to replace the placeholder layout that's part of the CommonLayout.
        */


        static CommonItem[] mBuiltInPhotos = {
            new CommonItem { mCommonId = Resource.Drawable.buckingham_guards,
                        mCaption = "Buckingham Palace" },
            new CommonItem { mCommonId = Resource.Drawable.la_tour_eiffel,
                        mCaption = "The Eiffel Tower" },
            new CommonItem  { mCommonId = Resource.Drawable.louvre_1,
                        mCaption = "The Louvre" },
            new CommonItem  { mCommonId = Resource.Drawable.before_mobile_phones,
                        mCaption = "Before mobile phones" },
            new CommonItem  { mCommonId = Resource.Drawable.big_ben_1,
                        mCaption = "Big Ben skyline" },
            new CommonItem  { mCommonId = Resource.Drawable.big_ben_2,
                        mCaption = "Big Ben from below" },
            new CommonItem  { mCommonId = Resource.Drawable.london_eye,
                        mCaption = "The London Eye" },
            new CommonItem  { mCommonId = Resource.Drawable.eurostar,
                        mCaption = "Eurostar Train" },
            new CommonItem  { mCommonId = Resource.Drawable.arc_de_triomphe,
                        mCaption = "Arc de Triomphe" },
            new CommonItem  { mCommonId = Resource.Drawable.louvre_2,
                        mCaption = "Inside the Louvre" },
            new CommonItem  { mCommonId = Resource.Drawable.versailles_fountains,
                        mCaption = "Versailles fountains" },
            new CommonItem  { mCommonId = Resource.Drawable.modest_accomodations,
                        mCaption = "Modest accomodations" },
            new CommonItem  { mCommonId = Resource.Drawable.notre_dame,
                        mCaption = "Notre Dame" },
            new CommonItem  { mCommonId = Resource.Drawable.inside_notre_dame,
                        mCaption = "Inside Notre Dame" },
            new CommonItem  { mCommonId = Resource.Drawable.seine_river,
                        mCaption = "The Seine" },
            new CommonItem  { mCommonId = Resource.Drawable.rue_cler,
                        mCaption = "Rue Cler" },
            new CommonItem  { mCommonId = Resource.Drawable.champ_elysees,
                        mCaption = "The Avenue des Champs-Elysees" },
            new CommonItem  { mCommonId = Resource.Drawable.seine_barge,
                        mCaption = "Seine barge" },
            new CommonItem  { mCommonId = Resource.Drawable.versailles_gates,
                        mCaption = "Gates of Versailles" },
            new CommonItem { mCommonId = Resource.Drawable.edinburgh_castle_2,
                        mCaption = "Edinburgh Castle" },
            new CommonItem  { mCommonId = Resource.Drawable.edinburgh_castle_1,
                        mCaption = "Edinburgh Castle up close" },
            new CommonItem  { mCommonId = Resource.Drawable.old_meets_new,
                        mCaption = "Old meets new" },
            new CommonItem  { mCommonId = Resource.Drawable.edinburgh_from_on_high,
                        mCaption = "Edinburgh from on high" },
            new CommonItem  { mCommonId = Resource.Drawable.edinburgh_station,
                        mCaption = "Edinburgh station" },
            new CommonItem  { mCommonId = Resource.Drawable.scott_monument,
                        mCaption = "Scott Monument" },
            new CommonItem  { mCommonId = Resource.Drawable.view_from_holyrood_park,
                        mCaption = "View from Holyrood Park" },
            new CommonItem { mCommonId = Resource.Drawable.tower_of_london,
                        mCaption = "Outside the Tower of London" },
            new CommonItem  { mCommonId = Resource.Drawable.tower_visitors,
                        mCaption = "Tower of London visitors" },
            new CommonItem  { mCommonId = Resource.Drawable.one_o_clock_gun,
                        mCaption = "One O'Clock Gun" },
            new CommonItem  { mCommonId = Resource.Drawable.victoria_albert,
                        mCaption = "Victoria and Albert Museum" },
            new CommonItem  { mCommonId = Resource.Drawable.royal_mile,
                        mCaption = "The Royal Mile" },
            new CommonItem  { mCommonId = Resource.Drawable.museum_and_castle,
                        mCaption = "Edinburgh Museum and Castle" },
            new CommonItem  { mCommonId = Resource.Drawable.portcullis_gate,
                        mCaption = "Portcullis Gate" },
            new CommonItem  { mCommonId = Resource.Drawable.to_notre_dame,
                        mCaption = "Left or right?" },
            new CommonItem  { mCommonId = Resource.Drawable.pompidou_centre,
                        mCaption = "Pompidou Centre" },
            new CommonItem  { mCommonId = Resource.Drawable.heres_lookin_at_ya,
                        mCaption = "Here's Lookin' at Ya!" },
            };

        // Array of photos that make up the album:
        private CommonItem[] mCItems;

        // Random number generator for shuffling the photos:
        Random mRandom;

        // Create an instance copy of the built-in photo list and
        // create the random number generator:
        public CommonAlbum()
        {
            mCItems = mBuiltInPhotos;
            mRandom = new Random();
        }

        // Return the number of photos in the photo album:
        public int NumPhotos
        {
            get { return mCItems.Length; }
        }

        // Indexer (read only) for accessing a photo:
        public CommonItem this[int i]
        {
            get { return mCItems[i]; }
        }

        // Pick a random photo and swap it with the top:
        public int RandomSwap()
        {
            // Save the photo at the top:
            CommonItem tmpCItems = mCItems[0];

            // Generate a next random index between 1 and 
            // Length (noninclusive):
            int rnd = mRandom.Next(1, mCItems.Length);

            // Exchange top photo with randomly-chosen photo:
            mCItems[0] = mCItems[rnd];
            mCItems[rnd] = tmpCItems;

            // Return the index of which photo was swapped with the top:
            return rnd;
        }

        // Shuffle the order of the photos:
        public void Shuffle()
        {
            // Use the Fisher-Yates shuffle algorithm:
            for (int idx = 0; idx < mCItems.Length; ++idx)
            {
                // Save the photo at idx:
                CommonItem tmpItem = mCItems[idx];

                // Generate a next random index between idx (inclusive) and 
                // Length (noninclusive):
                int rnd = mRandom.Next(idx, mCItems.Length);

                // Exchange photo at idx with randomly-chosen (later) photo:
                mCItems[idx] = mCItems[rnd];
                mCItems[rnd] = tmpItem;
            }
        }
    }
}