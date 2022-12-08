﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VCCorp.IG.Core.DTO;
using VCCorp.IG.Core.Helper;

namespace VCCorp.IG.Core.DAO
{
    public class SiDemandSourceDAO
    {
        private readonly MySqlDbContext _context = new MySqlDbContext() ;

        public SiDemandSourceDAO()
        { }

        public SiDemandSourceDAO(MySqlDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Lấy danh sách theo thứ tự ưu tiên
        /// </summary>
        /// <returns></returns>
        public List<SiDemandSourceDTO> GetList(int status)
        {
            List<SiDemandSourceDTO> listPost = new List<SiDemandSourceDTO>();
            _context.OpenMySql();

            string sql = "SELECT id, link, source_id,platform, status, frequency_crawl_current_date FROM si_demand_source where lock_tmp != 1 AND link like '%instagram%' and status='"+status+"'"+" order by priority";

            MySqlCommand cmd = new MySqlCommand(sql, _context._connect);
            cmd.CommandTimeout = int.MaxValue;

            MySqlDataReader dataReader = cmd.ExecuteReader();

            

            while (dataReader.Read())
            {
                SiDemandSourceDTO dto = new SiDemandSourceDTO();

                dto.Id = Convert.ToInt32(dataReader["id"]);
                dto.SourceId = dataReader["source_id"].ToString();             
                string profileid = dto.SourceId;
                //dto.Link = dataReader["link"].ToString();
                dto.Link = "https://www.instagram.com/graphql/query/?query_hash=472f257a40c653c64c666ce877d59d2b&variables={%22id%22:%22" + profileid + "%22,%22first%22:5000}";
                dto.Platform = dataReader["platform"].ToString();
                dto.Status = Convert.ToInt32( dataReader["status"]);

                listPost.Add(dto);
            }

            _context.Dispose();

            return listPost;
        }
    
        /// <summary>
        /// Cập nhập trạng thái source
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <param name="stscurren"></param>
        /// <param name="crawlcurrent"></param>
        /// <param name="crawledate"></param>
        public void Update(string id, string status, string stscurren, string crawlcurrent, string crawledate)
        {
            _context.OpenMySql();

            try
            {
                string sql = "Update si_demand_source set status=" + status;
                if (!string.IsNullOrEmpty(stscurren))
                {
                    sql += ", frequency_crawl_status_current_date = '" + stscurren + "'";
                }
                if (!string.IsNullOrEmpty(crawlcurrent))
                {
                    sql += ", frequency_crawl_current_date = '" + crawlcurrent + "'";
                }
                sql += ", user_crawler = 'Cucnt'";
                if (!string.IsNullOrEmpty(crawledate))
                {
                    sql += ", update_time_crawl = " + "STR_TO_DATE('" + crawledate.ToString() + "', '%m/%d/%Y %H:%i:%s')";
                }

                sql += " where Id='" + id + "'";

                MySqlCommand cmd = new MySqlCommand(sql, _context._connect);
                cmd.ExecuteNonQuery();

               
            }
            catch (Exception)
            {
               
            }

            _context.Dispose();
        }

        /// <summary>
        /// Lấy danh sách có source id null
        /// </summary>
        /// <returns></returns>
        public List<SiDemandSourceDTO> GetListNullIdSource()
        {
            List<SiDemandSourceDTO> listPost = new List<SiDemandSourceDTO>();
            _context.OpenMySql();

            string sql = "SELECT id, link, source_id FROM si_demand_source where link like '%instagram%' AND  source_id is null";         

            MySqlCommand cmd = new MySqlCommand(sql, _context._connect);
            cmd.CommandTimeout = int.MaxValue;

            MySqlDataReader dataReader = cmd.ExecuteReader();

            

            while (dataReader.Read())
            {
                SiDemandSourceDTO dto = new SiDemandSourceDTO();

                dto.Id = (int)dataReader["id"];
                dto.Link = dataReader["link"].ToString() + "?__a=1&__d=dis";
                dto.SourceId = dataReader["source_id"].ToString();

                listPost.Add(dto);
            }

            _context.Dispose();

            return listPost;
        }

        /// <summary>
        /// Cập nhập source id theo id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="sourceId"></param> 
        public void Update(string id, string sourceId)
        {
            _context.OpenMySql();

            try
            {
                string sql = "Update si_demand_source set source_id=" + sourceId;
                sql += " where Id='" + id + "'";

                MySqlCommand cmd = new MySqlCommand(sql, _context._connect);
                cmd.ExecuteNonQuery();

                
            }
            catch (Exception )
            {
                
            }
        }
    }
}
