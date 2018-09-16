using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamProject2017.Models.InfluenterViewModels
{
    public class MediaKitViewModel
    {
        public int InstagramPosts { get; set; }
        public int InstagramFollowers { get; set; }
        public int InstagramDayReach { get; set; }
        public int InstagramDayImpression { get; set; }



        public InfluencerProfile InflucencerProfile { get; set; }
        public string ApplicationUserId { get; set; }
        public bool HasInstagramData { get; set; }
        public List<InfluencerCategory> InfluenterKategori { get; set; }
        public List<InfluencerPlatform> InfluenterPlatform { get; set; }
    }
}
