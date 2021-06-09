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
    public class ProductCategoryDao
    {
        OnlineShopDBContext db = null;
        public ProductCategoryDao()
        {
            db = new OnlineShopDBContext();
        }
        //Quan ly
        public long Insert(ProductCategory entity)
        {
            db.ProductCategories.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }
        public IEnumerable<ProductCategory> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<ProductCategory> model = db.ProductCategories;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString));
            }
            return model.OrderByDescending(x => x.ID).ToPagedList(page, pageSize);
        }
        //Update
        public bool Update(ProductCategory entity)
        {
            try
            {
                var productCategory = db.ProductCategories.Find(entity.ID);
                productCategory.Name = entity.Name;
                productCategory.MetaTitle = entity.MetaTitle;
                productCategory.DisplayOrder = entity.DisplayOrder;
                productCategory.CreatedDate = entity.CreatedDate;
                db.SaveChanges();
                return true;
            }catch(Exception ex)
            {
                return false;
            }
        }
        //End Update
      
        //-------
        public List<ProductCategory> ListAll()
        {
            return db.ProductCategories.Where(x => x.Status == true).OrderBy(x => x.DisplayOrder).ToList();
        }
        public ProductCategory ViewDetail(long id)
        {
            return db.ProductCategories.Find(id);
        }
    }
}
