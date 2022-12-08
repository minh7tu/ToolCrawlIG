using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VCCorp.IG.Core.DAO;
using VCCorp.IG.Core.DTO;

namespace VCCorp.IG.Core.BUS
{
    public class SiCrawlDataExcelBUS
    {
        SiCrawlDataExcelDAO _dao;

        public SiCrawlDataExcelBUS()
        {
            _dao = new SiCrawlDataExcelDAO();
        }

        public List<SiCrawlDataExcelDTO> GetListComment()
        {
            return _dao.GetListComment();
        }

        public void Update(int id, string postId, int status)
        {
            _dao.Update(id, postId, status);
        }

        public List<SiCrawlDataExcelDTO> GetListPostId()
        {
            return _dao.GetListPostId();
        }
    }
}
