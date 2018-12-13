using CourseWork.Models.DataBaseModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Course.Models.DataBaseModel
{
    public class AccessTokens
    {
        public string Id { get; set; }

        public string vkId { get; set; }
        public string vkAT { get; set; }
        public string vkGroups { get; set; }

        public string youtubeId { get; set; }
        public string youtubeAT { get; set; }
        public string youtubeChannels { get; set; }

        public string instagramId { get; set; }
        public string instagramAT { get; set; }
        public string instagramFollowing { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public User User { get; set; }
    }
}