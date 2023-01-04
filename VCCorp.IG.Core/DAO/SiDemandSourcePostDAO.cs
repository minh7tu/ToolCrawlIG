using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VCCorp.IG.Core.DTO;
using VCCorp.IG.Core.Helper;

namespace VCCorp.IG.Core.DAO
{
    public class SiDemandSourcePostDAO
    {
        private readonly MySqlDbContext _context = new MySqlDbContext();

        public SiDemandSourcePostDAO()
        { }

        public SiDemandSourcePostDAO(MySqlDbContext context)
        {
            _context = context;
        }

        //Thêm bản ghi vào database si_demand_source_post
        public void Insert(SiDemandSourcePostDTO info)
        {
            _context.OpenMySql();

            //SiDemandSourcePostDTO info = new SiDemandSourcePostDTO();

            string sql = "insert ignore into si_demand_source_post (si_demand_source_id, post_id, platform, link, create_time, update_time, status";
            sql += ", title, content, total_comment, total_like,total_share,user_crawler, server_name_crawl) values ('";
            sql += info.SiDemandSourceId + "'";
            sql += ", N'" + info.PostId + "'";
            sql += ", '" + info.Platform + "'";
            sql += ", '" + info.Link + "'";
            sql += ",STR_TO_DATE('" + info.CreateTime.ToString("MM/dd/yyyy HH:mm:ss") + "', '%m/%d/%Y %H:%i:%s')";
            sql += ",STR_TO_DATE('" + info.UpdateTime.ToString("MM/dd/yyyy HH:mm:ss") + "', '%m/%d/%Y %H:%i:%s')";
            sql += ", '" + info.Status + "'";
            sql += ", N'" + info.Title + "'";
            sql += ", N'" + info.Content + "'";
            sql += ", '" + info.TotalComment + "'";
            sql += ", '" + info.TotalLike + "'";
            sql += ", '" + info.TotalShare + "'";
            sql += ", '" + info.UserCrawler + "'";
            sql += ", N'" + info.ServerNameCrawl + "'";
            sql += ")";

            MySqlCommand cmd = new MySqlCommand(sql, _context._connect);
            cmd.ExecuteNonQuery();

            _context.Dispose();
        }

        //Cập nhập trạng thái trong bảng si_demand_source_post
        public void Update(string id, string status, string stscurrentime)
        {
            _context.OpenMySql();

            string sql = "Update si_demand_source_post set status=" + status;
            if (!string.IsNullOrEmpty(stscurrentime))
            {
                sql += ", crawled_time = '" + stscurrentime + "'";
            }
            sql += " where Id='" + id + "'";

            MySqlCommand cmd = new MySqlCommand(sql, _context._connect);
            cmd.ExecuteNonQuery();

            _context.Dispose();

        }
        
        //Lấy danh sách link có lượng comment khác 0 và trạng thái khác -1 , 2
        public List<SiDemandSourcePostDTO> GetListSourcePost()
        {
            
            List<SiDemandSourcePostDTO> listPost = new List<SiDemandSourcePostDTO>();            

            _context.OpenMySql();

            string sql = "SELECT *  from si_demand_source_post  where total_comment != 0 AND status != -1 and status != 2 AND link like '%instagram%'";

            MySqlCommand cmd = new MySqlCommand(sql, _context._connect);          

            MySqlDataReader dataReader = cmd.ExecuteReader();

            

            while (dataReader.Read())
            {
                SiDemandSourcePostDTO dto = new SiDemandSourcePostDTO();

                dto.Id = Convert.ToInt32(dataReader["id"]);
                dto.PostId = dataReader["post_id"].ToString();
                dto.Link = dataReader["link"].ToString();
                dto.ShortCode = dataReader["link"].ToString().Replace("https://www.instagram.com/p/", "");
                dto.LinkCrawler = "https://www.instagram.com/graphql/query/?query_hash=33ba35852cb50da46f5b5e889df7d159&variables=%7B%22shortcode%22:%22" + dto.ShortCode.Trim() + "%22,%22first%22:100,%22after%22:%22%22%7D";

                listPost.Add(dto);
            }

            _context.Dispose();

            return listPost;
        }
    }
}
