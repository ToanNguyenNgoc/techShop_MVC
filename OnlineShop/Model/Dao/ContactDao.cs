using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;
using PagedList.Mvc;
using PagedList;

namespace Model.Dao
{
    
    public class ContactDao
    {
        OnlineShopDBContext db = null;
        public ContactDao()
        {
            db = new OnlineShopDBContext();
        }
        public Contact GetActiveContact()
        {
            return db.Contacts.Single(x => x.Status == true);
        }
        public int InsertFeedBack(Feedback fb)
        {
            db.Feedbacks.Add(fb);
            db.SaveChanges();
            return fb.ID;
        }
    }
}
