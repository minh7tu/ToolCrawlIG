using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VCCorp.IG.Core.DAO;
using VCCorp.IG.Core.DTO;

namespace VCCorp.IG.Core.BUS
{
    public class CrisisPostBUS
    {
        CrisisPostDAO _dao;

        public CrisisPostBUS()
        {
            _dao = new CrisisPostDAO();
        }

        public List<CrisisPostDTO> GetListComment()
        {
            return _dao.GetListComment();
        }

        public List<CrisisPostDTO> GetListNoCrawlComment()
        {
            return _dao.GetListNoCrawlComment();
        }

        public void Update(int id, int status)
        {
            _dao.Update(id, status);
        }
    }
}
