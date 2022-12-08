using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VCCorp.IG.Core.DAO;
using VCCorp.IG.Core.DTO;

namespace VCCorp.IG.Core.BUS
{
    public class SiDemandSourceBUS
    {
        SiDemandSourceDAO _dao;

        public SiDemandSourceBUS()
        {
            _dao = new SiDemandSourceDAO();
        }

        public List<SiDemandSourceDTO> GetList(int status)
        {
            return _dao.GetList(status);
        }

        public void Update(string id, string status, string stscurren, string crawlcurrent, string crawledate)
        {
            _dao.Update(id, status, stscurren, crawlcurrent, crawledate);
        }

        public List<SiDemandSourceDTO> GetListNullIdSource()
        {
            return _dao.GetListNullIdSource();
        }

        public void Update(string id, string sourceId)
        {
            _dao.Update(id, sourceId);
        }
    }
}
