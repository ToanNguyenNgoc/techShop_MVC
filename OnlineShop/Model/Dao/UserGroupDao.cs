using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;

namespace Model.Dao
{
    public class UserGroupDao
    {
        OnlineShopDBContext db = null;
        public UserGroupDao()
        {
            db = new OnlineShopDBContext();
        }
        public List<UserGroup> ListAll()
        {
            return db.UserGroups.ToList();
        }
    }
}
