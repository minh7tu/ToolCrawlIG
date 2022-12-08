using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VCCorp.IG.Core.DAO;
using VCCorp.IG.Core.DTO;

namespace VCCorp.IG.Core.BUS
{
    public class SiDemandSourcePostBUS
    {
        SiDemandSourcePostDAO _dao;

        public SiDemandSourcePostBUS()
        {
            _dao = new SiDemandSourcePostDAO();
        }

        public void Insert(SiDemandSourcePostDTO info)
        {
            _dao.Insert(info);
        }

        public void Update(string id, string status, string stscurrentime)
        {
            _dao.Update(id, status, stscurrentime);
        }

        public List<SiDemandSourcePostDTO> GetListSourcePost()
        {
            return _dao.GetListSourcePost();
        }
    }
}
