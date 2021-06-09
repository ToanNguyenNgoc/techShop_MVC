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
    public class FeebBackDao
    {
        OnlineShopDBContext db = null;
        public FeebBackDao()
        {
            db = new OnlineShopDBContext();
        }
        public int Insert(Feedback entity)
        {
            db.Feedbacks.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }
        public Feedback ViewDetail(int id)
        {
            return db.Feedbacks.Find(id);
        }
        public IEnumerable<Feedback> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Feedback> model = db.Feedbacks;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString) || x.Phone.Contains(searchString) || x.Email.Contains(searchString) || x.Address.Contains(searchString));

            }
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }
    }
    
}
