using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;
using PagedList;
using PagedList.Mvc;
using System.Web;

namespace Model.Dao
{

    public class ContentDao
    {
        OnlineShopDBContext db = null;
        public ContentDao()
        {
            db = new OnlineShopDBContext();
        }
        public long Insert(Content entity)
        {
            db.Contents.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }
        public IEnumerable<Content> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Content> model = db.Contents;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }
        //--Update
        public bool Update(Content entity)
        {
            try
            {
                var content = db.Contents.Find(entity.ID);
                content.Name = entity.Name;
                content.MetaTitle = entity.MetaTitle;
                content.Description = entity.Description;
                content.Image = entity.Image;
                content.CategoryID = entity.CategoryID;
                content.Detail = entity.Detail;
                content.Warranty = entity.Warranty;
                content.MetaKeywords = entity.MetaKeywords;
                content.MetaDescriptions = entity.MetaDescriptions;
                content.Status = entity.Status;
                db.SaveChanges();
                return true;
            }catch(Exception e)
            {
                return false;
            }
        }
        //--
        public Content GetById(string name)
        {
            return db.Contents.SingleOrDefault(x => x.Name == name);
        }
        public Content ViewDetail(int id)
        {
            return db.Contents.Find(id);
        }

    }
}
