using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLyKhachSan.Models
{
    public class GenerateIDPhongThue
    {
        public string generateID()
        {
            string id = Guid.NewGuid().ToString("N").Substring(0, 13);
            string genID = "PhThue" + id;
            return genID;
        }

        public string generateIDKH()
        {
            string id = Guid.NewGuid().ToString("N").Substring(0, 17);
            string genID = "KH" + id;
            return genID;
        }


        public string generateIDPhong()
        {
            string id = Guid.NewGuid().ToString("N").Substring(0, 17);
            string genID = "Ph" + id;
            return genID;
        }

        public string generateIDDV()
        {
            string id = Guid.NewGuid().ToString("N").Substring(0, 15);
            string genID = "Dv" + id;
            return genID;
        }

    }
}