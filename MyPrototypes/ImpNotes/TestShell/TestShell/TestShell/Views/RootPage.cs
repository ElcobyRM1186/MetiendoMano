﻿ 


namespace TestShell.Views
{
    using System;
    using System.Threading.Tasks;
    using Xamarin.Forms;
    using System.Collections.Generic; 
    using MvvmHelpers;

    using TestShell.Controls;
    using TestShell.Models;

    public class RootPage : MasterDetailPage
    {
        public static bool IsUWPDesktop { get; set; }
        Dictionary<int, NavigationPage> Pages { get; set; }
        public RootPage()
        {
            if (IsUWPDesktop)
                MasterBehavior = MasterBehavior.Popover;

            Pages = new Dictionary<int, NavigationPage>();
            Master = new MenuPage(this);
            BindingContext = new BaseViewModel
            {
                Title = "Hanselman",
                Icon = "slideout.png"
            };
            //setup home page
            Pages.Add((int)MenuType.About, new MyNavigationPage(new AboutPage()));
            Detail = Pages[(int)MenuType.About];

            InvalidateMeasure();
        }



        public async Task NavigateAsync(int id)
        {

            if (Detail != null)
            {
                if (IsUWPDesktop || Device.Idiom != TargetIdiom.Tablet)
                    IsPresented = false;

                if (Device.RuntimePlatform == Device.Android)
                    await Task.Delay(300);
            }

            Page newPage;
            if (!Pages.ContainsKey(id))
            {

                switch (id)
                {
                    case (int)MenuType.About:
                        Pages.Add(id, new MyNavigationPage(new AboutPage()));
                        break;
                    case (int)MenuType.Blog:
                        Pages.Add(id, new MyNavigationPage(new BlogPage()));
                        break;
                    //case (int)MenuType.DeveloperLife:
                    //    Pages.Add(id, new MyNavigationPage(new PodcastPage((MenuType)id)));
                    //    break;
                    //case (int)MenuType.Hanselminutes:
                    //    Pages.Add(id, new MyNavigationPage(new PodcastPage((MenuType)id)));
                    //    break;
                    //case (int)MenuType.Ratchet:
                    //    Pages.Add(id, new MyNavigationPage(new PodcastPage((MenuType)id)));
                    //    break;
                    case (int)MenuType.Twitter:
                        Pages.Add(id, new MyNavigationPage(new TwitterPage()));
                        break;
                    //case (int)MenuType.Videos:
                    //    Pages.Add(id, new MyNavigationPage(new Channel9VideosPage()));
                    //    break;
                }
            }

            newPage = Pages[id];
            if (newPage == null)
                return;

            Detail = newPage;
        }
    }
}
