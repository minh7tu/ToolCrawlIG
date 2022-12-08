using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VCCorp.IG.Core.DTO.JsonToObjectIG
{
    public class Profile
    {
        public class Data
        {
            public User user { get; set; }
        }

        public class Dimensions
        {
            public int height { get; set; }
            public int width { get; set; }
        }

        public class Edge
        {
            public Node node { get; set; }
        }

        public class EdgeMediaPreviewLike
        {
            public int count { get; set; }
        }

        public class EdgeMediaToCaption
        {
            public List<Edge> edges { get; set; }
        }

        public class EdgeMediaToComment
        {
            public int count { get; set; }
        }

        public class EdgeOwnerToTimelineMedia
        {
            public int count { get; set; }
            public PageInfo page_info { get; set; }
            public List<Edge> edges { get; set; }
        }

        public class Node
        {
            public string id { get; set; }
            public string __typename { get; set; }
            public EdgeMediaToCaption edge_media_to_caption { get; set; }
            public string shortcode { get; set; }
            public EdgeMediaToComment edge_media_to_comment { get; set; }
            public bool comments_disabled { get; set; }
            public int taken_at_timestamp { get; set; }
            public Dimensions dimensions { get; set; }
            public string display_url { get; set; }
            public EdgeMediaPreviewLike edge_media_preview_like { get; set; }
            public Owner owner { get; set; }
            public string thumbnail_src { get; set; }
            public List<ThumbnailResource> thumbnail_resources { get; set; }
            public bool is_video { get; set; }
            public int video_view_count { get; set; }
            public string text { get; set; }
        }

        public class Owner
        {
            public string id { get; set; }
        }

        public class PageInfo
        {
            public bool has_next_page { get; set; }
            public string end_cursor { get; set; }
        }

        public class Root
        {
            public Data data { get; set; }
            public string status { get; set; }
        }

        public class ThumbnailResource
        {
            public string src { get; set; }
            public int config_width { get; set; }
            public int config_height { get; set; }
        }

        public class User
        {
            public EdgeOwnerToTimelineMedia edge_owner_to_timeline_media { get; set; }
        }
    }
}
