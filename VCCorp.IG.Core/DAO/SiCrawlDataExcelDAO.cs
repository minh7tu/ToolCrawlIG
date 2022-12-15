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

        //Lấy danh sách có status = 0
        public List<SiCrawlDataExcelDTO> GetListPost()
        {
            List<SiCrawlDataExcelDTO> listPostId = new List<SiCrawlDataExcelDTO>();

            _context.OpenMySql();

            string sql = "select id,post_id,status,link from si_crawl_data_excel where link like '%instagram%' and status = 0";

            MySqlCommand cmd = new MySqlCommand(sql, _context._connect);

            MySqlDataReader read = cmd.ExecuteReader();

            while(read.Read())
            {
                SiCrawlDataExcelDTO dto = new SiCrawlDataExcelDTO();

                dto.Id = Convert.ToInt32(read["id"]);
                dto.PostId = read["post_id"].ToString();
                dto.Status = (int)read["status"];
                dto.Link = read["link"].ToString();
                dto.LinkCrawl = read["link"] + "?__a=1&__d=dis";
                dto.ShortCode = read["link"].ToString().Replace("https://www.instagram.com/p/", "").Replace("/", "").Trim();
                dto.Status = (int)read["status"];

                listPostId.Add(dto);
            }    

            return listPostId;
        }

        //Lấy danh sách có status = 3
        public List<SiCrawlDataExcelDTO> GetListComment()
        {
            List<SiCrawlDataExcelDTO> listPostId = new List<SiCrawlDataExcelDTO>();

            _context.OpenMySql();

            string sql = "select id,post_id,status,link from si_crawl_data_excel where link like '%instagram%' and status = 3";

            MySqlCommand cmd = new MySqlCommand(sql, _context._connect);

            MySqlDataReader read = cmd.ExecuteReader();

            while (read.Read())
            {
                SiCrawlDataExcelDTO dto = new SiCrawlDataExcelDTO();

                dto.Id = Convert.ToInt32(read["id"]);
                dto.PostId = read["post_id"].ToString();
                dto.Status = (int)read["status"];
                dto.Link = read["link"].ToString();
                dto.LinkCrawl = read["link"] + "?__a=1&__d=dis";
                dto.ShortCode = read["link"].ToString().Replace("https://www.instagram.com/p/", "").Replace("/", "").Trim();
                dto.Status = (int)read["status"];

                listPostId.Add(dto);
            }

            return listPostId;
        }

        //Insert vào bảng si data excel history
        public void InsertHistory()
        {

        }
    }
}
