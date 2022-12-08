using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VCCorp.IG.Core.DTO.JsonToObjectIG
{
    public class CommentOfPost
    {
        public class Data
        {
            public ShortcodeMedia shortcode_media { get; set; }
        }

        public class Edge
        {
            public Node node { get; set; }
        }

        public class EdgeMediaToComment
        {
            public int count { get; set; }
            public PageInfo page_info { get; set; }
            public List<Edge> edges { get; set; }
        }

        public class Node
        {
            public string id { get; set; }
            public string text { get; set; }
            public int created_at { get; set; }
            public Owner owner { get; set; }
        }

        public class Owner
        {
            public string id { get; set; }
            public string profile_pic_url { get; set; }
            public string username { get; set; }
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

        public class ShortcodeMedia
        {
            public EdgeMediaToComment edge_media_to_comment { get; set; }
        }
    }
}
