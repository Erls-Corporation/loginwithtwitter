using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GetConnectedWithTwitter.Models
{
    public class TwitterProfile
    {
        public string ProfileImageUrl { get; set; }
        public string ScreenName { get; set; }
        public int FollowerCount { get; set; }
        public int StatusCount { get; set; }
        public int Id { get; set; }
    }

    public class TwitterFollower
    {
       // public string ProfileImageUrl { get; set; }
        public string ScreenName { get; set; }
        //public int FollowerCount { get; set; }
       // public int StatusCount { get; set; }
        public int Id { get; set; }
    }
}