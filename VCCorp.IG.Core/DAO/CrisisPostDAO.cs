using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VCCorp.IG.Core.DTO;
using VCCorp.IG.Core.Helper;
using MySql.Data.MySqlClient;

namespace VCCorp.IG.Core.DAO
{
    public class CrisisPostDAO
    {
        private readonly MySqlDbContext _context;

        public CrisisPostDAO()
        {

        }

        public CrisisPostDAO(MySqlDbContext context)
        {
            _context = context;
        }

        //Lấy ra tất cả list comment
        public List<CrisisPostDTO> GetListComment()
        {
            List<CrisisPostDTO> listComment = new List<CrisisPostDTO>();

            _context.OpenMySql();

            string sql = "SELECT Id, link, is_status, type FROM crisis_post where link like '%instagram%'";

            MySqlCommand cmd = new MySqlCommand(sql, _context._connect);

            MySqlDataReader read = cmd.ExecuteReader();

            CrisisPostDTO dto = new CrisisPostDTO();

            while(read.Read())
            {
                dto.Id = Convert.ToInt32(read["Id"]);
                //item.urlCrawler = "https://www.instagram.com/graphql/query/?query_hash=33ba35852cb50da46f5b5e889df7d159&variables={%22shortcode%22:%22" + shortcode + "%22,%22first%22:50}";
                dto.Link = "https://www.instagram.com/graphql/query/?query_hash=33ba35852cb50da46f5b5e889df7d159&variables={%22shortcode%22:%22" + read["link"].ToString() + "%22,%22first%22:50}";
                dto.IsStatus = Convert.ToInt32(read["is_status"]);

                listComment.Add(dto);
            }
            _context.Dispose();

            return listComment;
        }

        //Lấy tất cả list comment chưa bóc
        public List<CrisisPostDTO> GetListNoCrawlComment()
        {
            List<CrisisPostDTO> listComment = new List<CrisisPostDTO>();

            _context.OpenMySql();

            string sql = "SELECT Id, link, is_status, type FROM crisis_post where link like '%instagram%' and is_status = 0";

            MySqlCommand cmd = new MySqlCommand(sql, _context._connect);

            MySqlDataReader read = cmd.ExecuteReader();

            CrisisPostDTO dto = new CrisisPostDTO();

            while (read.Read())
            {
                dto.Id = Convert.ToInt32(read["Id"]);
                dto.Link = read["link"].ToString() + "/?__a=1&__d=dis";
                dto.IsStatus = Convert.ToInt32(read["is_status"]);

                listComment.Add(dto);
            }
            _context.Dispose();

            return listComment;
        }

        //Cập nhập trạng thái trên bảng crisis_post
        public void Update(int id, int status)
        {
            _context.OpenMySql();

            string sql = "update crisis_post set";
            sql += " is_status='" + status + "'";
            sql += " Where Id='" + id + "'";

            MySqlCommand cmd = new MySqlCommand(sql, _context._connect);

            cmd.ExecuteNonQuery();
        }
    }
}
