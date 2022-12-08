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
    public class SiCrawlDataExcelDAO
    {
        private readonly MySqlDbContext _context = new MySqlDbContext();

        public SiCrawlDataExcelDAO()
        {

        }

        public SiCrawlDataExcelDAO(MySqlDbContext context)
        {
            _context = context;
        }

        //Lấy ra list comment
        public List<SiCrawlDataExcelDTO> GetListComment()
        {
            List<SiCrawlDataExcelDTO> listComment = new List<SiCrawlDataExcelDTO>();

            _context.OpenMySql();

            string sql = "SELECT Id, post_id, link, status, type FROM si_crawl_data_excel WHERE post_id not  REGEXP '[0-9]_[0-9]' AND link like '%instagram.com%'";

            MySqlCommand cmd = new MySqlCommand(sql, _context._connect);

            MySqlDataReader read = cmd.ExecuteReader();

            SiCrawlDataExcelDTO dto = new SiCrawlDataExcelDTO();

            while(read.Read())
            {
                dto.Id = Convert.ToInt32(read["Id"]);
                dto.Link = read["link"].ToString();
                dto.Status = Convert.ToInt32(read["status"]);

                listComment.Add(dto);
            }

            _context.Dispose();

            return listComment;
        }

        //Cập nhập lại trạng thái, postid
        public void Update(int id, string postId, int status)
        {
            _context.OpenMySql();

            string sql = "update si_crawl_data_excel set post_id='" + postId + "'";
            sql += ", status='" + status + "'";
            sql += " Where Id='" + id + "'";

            MySqlCommand cmd = new MySqlCommand(sql, _context._connect);

            cmd.ExecuteNonQuery();

            _context.Dispose();
        }

        //Lấy danh sách PostId null
        public List<SiCrawlDataExcelDTO> GetListPostId()
        {
            List<SiCrawlDataExcelDTO> listPostId = new List<SiCrawlDataExcelDTO>();

            _context.OpenMySql();

            string sql = "select id,post_id,status from si_crawl_data_excel where link like '%instagram%'";

            MySqlCommand cmd = new MySqlCommand(sql, _context._connect);

            MySqlDataReader read = cmd.ExecuteReader();

            while(read.Read())
            {
                SiCrawlDataExcelDTO dto = new SiCrawlDataExcelDTO();

                dto.Id = Convert.ToInt32(read["id"]);
                dto.PostId = read["post_id"].ToString();
                dto.Status = (int)read["status"];

                listPostId.Add(dto);
            }    

            return listPostId;
        }
    }
}
