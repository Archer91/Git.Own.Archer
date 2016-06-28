using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Lib.intf;
using WMS.Lib.vo.basic;
using Extension.Util.Archer;
using WMS.Lib.util;

namespace WMS.Lib.impl
{
    public class BasicImpl:IBasic
    {
        DataWMSDataContext wmsContext = new DataWMSDataContext();
        public List<WarehouseVO> getWarehouseList()
        {
            var result = from m in wmsContext.T_Basic_Warehouse
                         select m;
            List<WarehouseVO> lst = new List<WarehouseVO>();
            foreach (var item in result)
            {
                WarehouseVO whv = new WarehouseVO();
                whv.ID = item.ID;
                whv.ParentID = item.ParentID;
                whv.Code = item.Code;
                whv.WareName = item.WareName;
                whv.Description = item.Description;
                whv.CompanyID = item.CompanyID;
                whv.Status = item.Status;
                whv.CreatedBy = item.CreatedBy;
                whv.CreateTime = item.CreateTime;
                whv.UpdatedBy = item.UpdatedBy;
                whv.UpdateTime = item.UpdateTime;
                whv.StatusDesc = item.Status == 1 ? "启用" : "失效";

                    lst.Add(whv);
            }
            return lst;
        }

        public List<LocationVO> getLocationList()
        {
            var result = from m in wmsContext.T_Basic_Location
                         select m;
            List<LocationVO> lst = new List<LocationVO>();
            foreach (var item in result)
            {
                LocationVO lv = new LocationVO();
                lv.ID = item.ID;
                lv.Code = item.Code;
                lv.LocationName = item.LocationName;
                lv.Description = item.Description;
                lv.WarehouseID = item.WarehouseID;
                lv.Status = item.Status;
                lv.CreatedBy = item.CreatedBy;
                lv.CreateTime = item.CreateTime;
                lv.UpdatedBy = item.UpdatedBy;
                lv.UpdateTime = item.UpdateTime;
                lv.StatusDesc = item.Status == 1 ? "启用" : "失效";
                
                    lst.Add(lv);
            }
            return lst;
        }

        public List<CustomerVO> getCustomerList()
        {
            var result = from m in wmsContext.T_Basic_Customer
                         select m;
            List<CustomerVO> lst = new List<CustomerVO>();
            foreach (var item in result)
            {
                CustomerVO cv = new CustomerVO();
                cv.ID = item.ID;
                cv.Code = item.Code;
                cv.CustName = item.CustName;
                cv.Description = item.Description;
                cv.Contacts = item.Contacts;
                cv.Address = item.Address;
                cv.Tel = item.Tel;
                cv.Fax = item.Fax;
                cv.Email = item.Email;
                cv.Status = item.Status;
                cv.CreatedBy = item.CreatedBy;
                cv.CreateTime = item.CreateTime;
                cv.UpdatedBy = item.UpdatedBy;
                cv.UpdateTime = item.UpdateTime;
                cv.StatusDesc = item.Status == 1 ? "启用" : "失效";

                    lst.Add(cv);
            }
            return lst;
        }

        public List<SupplierVO> getSupplierList()
        {
            var result = from m in wmsContext.T_Basic_Supplier
                         select m;
            List<SupplierVO> lst = new List<SupplierVO>();
            foreach (var item in result)
            {
                SupplierVO sv = new SupplierVO();
                sv.ID = item.ID;
                sv.Code = item.Code;
                sv.SupplierName = item.SupplierName;
                sv.Description = item.Description;
                sv.Contacts = item.Contacts;
                sv.Address = item.Address;
                sv.Tel = item.Tel;
                sv.Fax = item.Fax;
                sv.Email = item.Email;
                sv.Status = item.Status;
                sv.CreatedBy = item.CreatedBy;
                sv.CreateTime = item.CreateTime;
                sv.UpdatedBy = item.UpdatedBy;
                sv.UpdateTime = item.UpdateTime;
                sv.StatusDesc = item.Status == 1 ? "启用" : "失效";

                    lst.Add(sv);
            }
            return lst;
        }

        public List<ItemCategoryVO> getItemCategoryList()
        {
            var result = from m in wmsContext.T_Basic_ItemCategory
                         select m;
            List<ItemCategoryVO> lst = new List<ItemCategoryVO>();
            foreach (var item in result)
            {
                ItemCategoryVO icv = new ItemCategoryVO();
                icv.ID = item.ID;
                icv.ParentID = item.ParentID;
                icv.ItemCategoryName = item.ItemCategoryName;
                icv.Description = item.Description;
                icv.Status = item.Status;
                icv.CreatedBy = item.CreatedBy;
                icv.CreateTime = item.CreateTime;
                icv.UpdatedBy = item.UpdatedBy;
                icv.UpdateTime = item.UpdateTime;
                icv.StatusDesc = item.Status == 1 ? "启用" : "失效";

                    lst.Add(icv);
            }
            return lst;
        }

        public List<MaterialVO> getMaterialList()
        {
            var result = from m in wmsContext.T_Basic_MaterialList
                         join n in wmsContext.T_Basic_ItemCategory on m.CategoryID equals n.ID into temp1
                         from t1 in temp1.DefaultIfEmpty()
                         join k in wmsContext.T_Basic_Supplier on m.SupplierID equals k.ID into temp2
                         from t2 in temp2.DefaultIfEmpty()
                         select new
                         {
                             m.ID,
                             m.CategoryID,
                             m.Code,
                             m.ItemName,
                             m.Description,
                             m.SupplierID,
                             m.Type,
                             m.Status,
                             m.CreatedBy,
                             m.CreateTime,
                             CategoryDesc = t1==null?"": t1.ItemCategoryName,
                             SupplierDesc = t2==null?"": t2.SupplierName
                         };
            List<MaterialVO> lst = new List<MaterialVO>();
            foreach (var item in result)
            {
                MaterialVO mv = new MaterialVO();
                mv.ID = item.ID;
                mv.CategoryID = item.CategoryID;
                mv.Code = item.Code;
                mv.ItemName = item.ItemName;
                mv.Description = item.Description;
                mv.SupplierID = item.SupplierID;
                mv.Type = item.Type;
                mv.Status = item.Status;
                mv.CreatedBy = item.CreatedBy;
                mv.CreateTime = item.CreateTime;
                mv.StatusDesc = item.Status == 1 ? "启用" : "失效";
                mv.CategoryDesc = item.CategoryDesc;
                mv.SupplierDesc = item.SupplierDesc;

                lst.Add(mv);
            }
            return lst;
        }

        public bool saveWarehouse(WarehouseVO pWarehouseVO)
        {
            if (pWarehouseVO.IsNullOrEmpty() ||
                pWarehouseVO.Code.IsNullOrEmpty())
            {
                throw new WMSException("ER001");
            }

            if (pWarehouseVO.ID == -1)
            {
                //新增
                var result = (from m in wmsContext.T_Basic_Warehouse
                              where m.Code.ToUpper().Equals(pWarehouseVO.Code.ToUpper())
                              select m).SingleOrDefault();
                if (null != result)
                {
                    throw new WMSException("ER019", pWarehouseVO.Code);
                }
                T_Basic_Warehouse tbw = new T_Basic_Warehouse();
                tbw.ParentID = pWarehouseVO.ParentID;
                tbw.Code = pWarehouseVO.Code;
                tbw.WareName = pWarehouseVO.WareName;
                tbw.Description = pWarehouseVO.Description;
                tbw.CompanyID = pWarehouseVO.CompanyID;
                tbw.Status = 1;
                tbw.CreatedBy = pWarehouseVO.CreatedBy;
                tbw.CreateTime = DateTime.Now;

                wmsContext.T_Basic_Warehouse.InsertOnSubmit(tbw);
                wmsContext.SubmitChanges();
                return true;
            }
            else
            {
                //编辑
                var result = (from m in wmsContext.T_Basic_Warehouse
                              where m.ID != pWarehouseVO.ID
                              && m.Code.ToUpper().Equals(pWarehouseVO.Code.ToUpper())
                              select m).SingleOrDefault();
                if (null != result)
                {
                    throw new WMSException("ER019", pWarehouseVO.Code);
                }
                result = (from m in wmsContext.T_Basic_Warehouse
                          where m.ID == pWarehouseVO.ID
                          select m).SingleOrDefault();
                if (null == result)
                {
                    throw new WMSException("ER020", pWarehouseVO.Code);
                }

                result.ParentID = pWarehouseVO.ParentID;
                result.Code = pWarehouseVO.Code;
                result.WareName = pWarehouseVO.WareName;
                result.Description = pWarehouseVO.Description;
                result.CompanyID = pWarehouseVO.CompanyID;
                result.UpdatedBy = pWarehouseVO.UpdatedBy;
                result.UpdateTime = DateTime.Now;

                wmsContext.SubmitChanges();
                return true;
            }
        }

        public bool saveLocation(LocationVO pLocationVO)
        {
            if (pLocationVO.IsNullOrEmpty() ||
                pLocationVO.Code.IsNullOrEmpty())
            {
                throw new WMSException("ER001");
            }

            if (pLocationVO.ID == -1)
            {
                //新增
                var result = (from m in wmsContext.T_Basic_Location
                              where m.Code.ToUpper().Equals(pLocationVO.Code.ToUpper())
                              select m).SingleOrDefault();
                if (null != result)
                {
                    throw new WMSException("ER021", pLocationVO.Code);
                }
                T_Basic_Location tbl = new T_Basic_Location();
                tbl.Code = pLocationVO.Code;
                tbl.LocationName = pLocationVO.LocationName;
                tbl.Description = pLocationVO.Description;
                tbl.WarehouseID = pLocationVO.WarehouseID;
                tbl.Status = 1;
                tbl.CreatedBy = pLocationVO.CreatedBy;
                tbl.CreateTime = DateTime.Now;

                wmsContext.T_Basic_Location.InsertOnSubmit(tbl);
                wmsContext.SubmitChanges();
                return true;
            }
            else
            {
                //编辑
                var result = (from m in wmsContext.T_Basic_Location
                              where m.ID != pLocationVO.ID
                              && m.Code.ToUpper().Equals(pLocationVO.Code.ToUpper())
                              select m).SingleOrDefault();
                if (null != result)
                {
                    throw new WMSException("ER021", pLocationVO.Code);
                }
                result = (from m in wmsContext.T_Basic_Location
                          where m.ID == pLocationVO.ID
                          select m).SingleOrDefault();
                if (null == result)
                {
                    throw new WMSException("ER022", pLocationVO.Code);
                }

                result.Code = pLocationVO.Code;
                result.LocationName = pLocationVO.LocationName;
                result.Description = pLocationVO.Description;
                result.WarehouseID = pLocationVO.WarehouseID;
                result.UpdatedBy = pLocationVO.UpdatedBy;
                result.UpdateTime = DateTime.Now;

                wmsContext.SubmitChanges();
                return true;
            }
        }

        public bool saveCustomer(CustomerVO pCustomerVO)
        {
            if (pCustomerVO.IsNullOrEmpty() ||
                pCustomerVO.Code.IsNullOrEmpty())
            {
                throw new WMSException("ER001");
            }

            if (pCustomerVO.ID == -1)
            {
                //新增
                var result = (from m in wmsContext.T_Basic_Customer
                              where m.Code.ToUpper().Equals(pCustomerVO.Code.ToUpper())
                              select m).SingleOrDefault();
                if (null != result)
                {
                    throw new WMSException("ER023", pCustomerVO.Code);
                }
                T_Basic_Customer tbc = new T_Basic_Customer();
                tbc.Code = pCustomerVO.Code;
                tbc.CustName = pCustomerVO.CustName;
                tbc.Description = pCustomerVO.Description;
                tbc.Contacts = pCustomerVO.Contacts;
                tbc.Address = pCustomerVO.Address;
                tbc.Tel = pCustomerVO.Tel;
                tbc.Fax = pCustomerVO.Fax;
                tbc.Email = pCustomerVO.Email;
                tbc.Status = 1;
                tbc.CreatedBy = pCustomerVO.CreatedBy;
                tbc.CreateTime = DateTime.Now;

                wmsContext.T_Basic_Customer.InsertOnSubmit(tbc);
                wmsContext.SubmitChanges();
                return true;
            }
            else
            {
                //编辑
                var result = (from m in wmsContext.T_Basic_Customer
                              where m.ID != pCustomerVO.ID
                              && m.Code.ToUpper().Equals(pCustomerVO.Code.ToUpper())
                              select m).SingleOrDefault();
                if (null != result)
                {
                    throw new WMSException("ER023", pCustomerVO.Code);
                }
                result = (from m in wmsContext.T_Basic_Customer
                          where m.ID == pCustomerVO.ID
                          select m).SingleOrDefault();
                if (null == result)
                {
                    throw new WMSException("ER024", pCustomerVO.Code);
                }

                result.Code = pCustomerVO.Code;
                result.CustName = pCustomerVO.CustName;
                result.Description = pCustomerVO.Description;
                result.Contacts = pCustomerVO.Contacts;
                result.Address = pCustomerVO.Address;
                result.Tel = pCustomerVO.Tel;
                result.Fax = pCustomerVO.Fax;
                result.Email = pCustomerVO.Email;
                result.UpdatedBy = pCustomerVO.UpdatedBy;
                result.UpdateTime = DateTime.Now;

                wmsContext.SubmitChanges();
                return true;
            }
        }

        public bool saveSupplier(SupplierVO pSupplierVO)
        {
            if (pSupplierVO.IsNullOrEmpty() ||
                pSupplierVO.Code.IsNullOrEmpty())
            {
                throw new WMSException("ER001");
            }

            if (pSupplierVO.ID == -1)
            {
                //新增
                var result = (from m in wmsContext.T_Basic_Supplier
                              where m.Code.ToUpper().Equals(pSupplierVO.Code.ToUpper())
                              select m).SingleOrDefault();
                if (null != result)
                {
                    throw new WMSException("ER025", pSupplierVO.Code);
                }
                T_Basic_Supplier tbs = new T_Basic_Supplier();
                tbs.Code = pSupplierVO.Code;
                tbs.SupplierName = pSupplierVO.SupplierName;
                tbs.Description = pSupplierVO.Description;
                tbs.Contacts = pSupplierVO.Contacts;
                tbs.Address = pSupplierVO.Address;
                tbs.Tel = pSupplierVO.Tel;
                tbs.Fax = pSupplierVO.Fax;
                tbs.Email = pSupplierVO.Email;
                tbs.Status = 1;
                tbs.CreatedBy = pSupplierVO.CreatedBy;
                tbs.CreateTime = DateTime.Now;

                wmsContext.T_Basic_Supplier.InsertOnSubmit(tbs);
                wmsContext.SubmitChanges();
                return true;
            }
            else
            {
                //编辑
                var result = (from m in wmsContext.T_Basic_Supplier
                              where m.ID != pSupplierVO.ID
                              && m.Code.ToUpper().Equals(pSupplierVO.Code.ToUpper())
                              select m).SingleOrDefault();
                if (null != result)
                {
                    throw new WMSException("ER025", pSupplierVO.Code);
                }
                result = (from m in wmsContext.T_Basic_Supplier
                          where m.ID == pSupplierVO.ID
                          select m).SingleOrDefault();
                if (null == result)
                {
                    throw new WMSException("ER026", pSupplierVO.Code);
                }

                result.Code = pSupplierVO.Code;
                result.SupplierName = pSupplierVO.SupplierName;
                result.Description = pSupplierVO.Description;
                result.Contacts = pSupplierVO.Contacts;
                result.Address = pSupplierVO.Address;
                result.Tel = pSupplierVO.Tel;
                result.Fax = pSupplierVO.Fax;
                result.Email = pSupplierVO.Email;
                result.UpdatedBy = pSupplierVO.UpdatedBy;
                result.UpdateTime = pSupplierVO.UpdateTime;

                wmsContext.SubmitChanges();
                return true;
            }
        }

        public bool saveItemCategory(ItemCategoryVO pItemCategoryVO)
        {
            if (pItemCategoryVO.IsNullOrEmpty() ||
                pItemCategoryVO.ItemCategoryName.IsNullOrEmpty())
            {
                throw new WMSException("ER001");
            }

            if (pItemCategoryVO.ID == -1)
            {
                //新增
                var result = (from m in wmsContext.T_Basic_ItemCategory
                              where m.ItemCategoryName.Equals(pItemCategoryVO.ItemCategoryName)
                              select m).SingleOrDefault();
                if (null != result)
                {
                    throw new WMSException("ER027", pItemCategoryVO.ItemCategoryName);
                }
                T_Basic_ItemCategory tbic = new T_Basic_ItemCategory();
                tbic.ParentID = pItemCategoryVO.ParentID;
                tbic.ItemCategoryName = pItemCategoryVO.ItemCategoryName;
                tbic.Description = pItemCategoryVO.Description;
                tbic.Status = 1;
                tbic.CreatedBy = pItemCategoryVO.CreatedBy;
                tbic.CreateTime = DateTime.Now;

                wmsContext.T_Basic_ItemCategory.InsertOnSubmit(tbic);
                wmsContext.SubmitChanges();
                return true;
            }
            else
            {
                //编辑
                var result = (from m in wmsContext.T_Basic_ItemCategory
                              where m.ID != pItemCategoryVO.ID
                              && m.ItemCategoryName.Equals(pItemCategoryVO.ItemCategoryName)
                              select m).SingleOrDefault();
                if (null != result)
                {
                    throw new WMSException("ER027", pItemCategoryVO.ItemCategoryName);
                }
                result = (from m in wmsContext.T_Basic_ItemCategory
                          where m.ID == pItemCategoryVO.ID
                          select m).SingleOrDefault();
                if (null == result)
                {
                    throw new WMSException("ER028", pItemCategoryVO.ItemCategoryName);
                }

                result.ParentID = pItemCategoryVO.ParentID;
                result.ItemCategoryName = pItemCategoryVO.ItemCategoryName;
                result.Description = pItemCategoryVO.Description;
                result.UpdatedBy = pItemCategoryVO.UpdatedBy;
                result.UpdateTime = DateTime.Now;

                wmsContext.SubmitChanges();
                return true;
            }
        }

        public bool saveMaterial(MaterialVO pMaterialVO)
        {
            if (pMaterialVO.IsNullOrEmpty() ||
                pMaterialVO.Code.IsNullOrEmpty())
            {
                throw new WMSException("ER001");
            }

            if (pMaterialVO.ID == -1)
            {
                //新增
                var result = (from m in wmsContext.T_Basic_MaterialList
                              where m.Code.ToUpper().Equals(pMaterialVO.Code.ToUpper())
                              select m).SingleOrDefault();
                if (null != result)
                {
                    throw new WMSException("ER029", pMaterialVO.Code);
                }
                T_Basic_MaterialList tbm = new T_Basic_MaterialList();
                tbm.CategoryID = pMaterialVO.CategoryID;
                tbm.Code = pMaterialVO.Code;
                tbm.ItemName = pMaterialVO.ItemName;
                tbm.Description = pMaterialVO.Description;
                tbm.SupplierID = pMaterialVO.SupplierID;
                tbm.Type = pMaterialVO.Type;
                tbm.Status = 1;
                tbm.CreatedBy = pMaterialVO.CreatedBy;
                tbm.CreateTime = DateTime.Now;

                wmsContext.T_Basic_MaterialList.InsertOnSubmit(tbm);
                wmsContext.SubmitChanges();
                return true;
            }
            else
            {
                //编辑
                var result = (from m in wmsContext.T_Basic_MaterialList
                              where m.ID != pMaterialVO.ID
                              && m.Code.ToUpper().Equals(pMaterialVO.Code.ToUpper())
                              select m).SingleOrDefault();
                if (null != result)
                {
                    throw new WMSException("ER029", pMaterialVO.Code);
                }
                result = (from m in wmsContext.T_Basic_MaterialList
                          where m.ID == pMaterialVO.ID
                          select m).SingleOrDefault();
                if (null == result)
                {
                    throw new WMSException("ER030", pMaterialVO.Code);
                }

                result.CategoryID = pMaterialVO.CategoryID;
                result.Code = pMaterialVO.Code;
                result.ItemName = pMaterialVO.ItemName;
                result.Description = pMaterialVO.Description;
                result.SupplierID = pMaterialVO.SupplierID;
                result.Type = pMaterialVO.Type;
                result.UpdatedBy = pMaterialVO.UpdatedBy;
                result.UpdateTime = DateTime.Now;

                wmsContext.SubmitChanges();
                return true;
            }
        }

        public bool enableAndDisableWarehouseByWareID(int pWarehouseID, int pStatus, string pAccount)
        {
            var result = (from m in wmsContext.T_Basic_Warehouse
                          where m.ID == pWarehouseID
                          select m).SingleOrDefault();
            if (null == result)
            {
                throw new WMSException("ER020", pWarehouseID);
            }
            result.Status = pStatus;
            result.UpdatedBy = pAccount;
            result.UpdateTime = DateTime.Now;

            wmsContext.SubmitChanges();
            return true;
        }

        public bool enableAndDisableLocationByLocationID(int pLocationID, int pStatus, string pAccount)
        {
            var result = (from m in wmsContext.T_Basic_Location
                          where m.ID == pLocationID
                          select m).SingleOrDefault();
            if (null == result)
            {
                throw new WMSException("ER022", pLocationID);
            }
            result.Status = pStatus;
            result.UpdatedBy = pAccount;
            result.UpdateTime = DateTime.Now;

            wmsContext.SubmitChanges();
            return true;
        }

        public bool enableAndDisableCustomerByCustomerID(int pCustomerID, int pStatus, string pAccount)
        {
            var result = (from m in wmsContext.T_Basic_Customer
                          where m.ID == pCustomerID
                          select m).SingleOrDefault();
            if (null == result)
            {
                throw new WMSException("ER024", pCustomerID);
            }
            result.Status = pStatus;
            result.UpdatedBy = pAccount;
            result.UpdateTime = DateTime.Now;

            wmsContext.SubmitChanges();
            return true;
        }

        public bool enableAndDisableSupplierBySupplierID(int pSupplierID, int pStatus, string pAccount)
        {
            var result = (from m in wmsContext.T_Basic_Supplier
                          where m.ID == pSupplierID
                          select m).SingleOrDefault();
            if (null == result)
            {
                throw new WMSException("ER026", pSupplierID);
            }
            result.Status = pStatus;
            result.UpdatedBy = pAccount;
            result.UpdateTime = DateTime.Now;

            wmsContext.SubmitChanges();
            return true;
        }

        public bool enableAndDisableItemCategoryByItemCategoryID(int pItemCategoryID, int pStatus, string pAccount)
        {
            var result = (from m in wmsContext.T_Basic_ItemCategory
                          where m.ID == pItemCategoryID
                          select m).SingleOrDefault();
            if (null == result)
            {
                throw new WMSException("ER028", pItemCategoryID);
            }
            result.Status = pStatus;
            result.UpdatedBy = pAccount;
            result.UpdateTime = DateTime.Now;

            wmsContext.SubmitChanges();
            return true;
        }

        public bool enableAndDisableMaterialByMaterialID(int pMaterialID, int pStatus, string pAccount)
        {
            var result = (from m in wmsContext.T_Basic_MaterialList
                          where m.ID == pMaterialID
                          select m).SingleOrDefault();
            if (null == result)
            {
                throw new WMSException("ER030", pMaterialID);
            }
            result.Status = pStatus;
            result.UpdatedBy = pAccount;
            result.UpdateTime = DateTime.Now;

            wmsContext.SubmitChanges();
            return true;
        }

        public List<LocationVO> getLocationByWarehouseID(int pWarehouseID)
        {
            var result = from m in wmsContext.T_Basic_Location
                         join n in wmsContext.T_Basic_Warehouse on m.WarehouseID equals n.ID
                         where m.WarehouseID == pWarehouseID
                         select new
                         {
                             m.ID,
                             m.Code,
                             m.LocationName,
                             m.Description,
                             m.WarehouseID,
                             m.Status,
                             m.CreatedBy,
                             m.CreateTime,
                             WarehouseDesc=n.WareName
                         };
            List<LocationVO> lst = new List<LocationVO>();
            foreach (var item in result)
            {
                LocationVO lv = new LocationVO();
                lv.ID = item.ID;
                lv.Code = item.Code;
                lv.LocationName = item.LocationName;
                lv.Description = item.Description;
                lv.WarehouseID = item.WarehouseID;
                lv.Status = item.Status;
                lv.CreatedBy = item.CreatedBy;
                lv.CreateTime = item.CreateTime;
                lv.StatusDesc = item.Status == 1 ? "启用" : "失效";
                lv.WarehouseDesc = item.WarehouseDesc;

                lst.Add(lv);
            }
            return lst;
        }

        public List<MaterialVO> getMaterialByCategoryID(int pCategoryID)
        {
            var result = from m in wmsContext.T_Basic_MaterialList
                         join n in wmsContext.T_Basic_ItemCategory on m.CategoryID equals n.ID into temp1
                         from t1 in temp1.DefaultIfEmpty()
                         join k in wmsContext.T_Basic_Supplier on m.SupplierID equals k.ID into temp2
                         from t2 in temp2.DefaultIfEmpty()
                         where m.CategoryID == pCategoryID
                         select new
                         {
                             m.ID,
                             m.CategoryID,
                             m.Code,
                             m.ItemName,
                             m.Description,
                             m.SupplierID,
                             m.Type,
                             m.Status,
                             m.CreatedBy,
                             m.CreateTime,
                             CategoryDesc = t1==null ? "" : t1.ItemCategoryName,
                             SupplierDesc = t2==null ? "" : t2.SupplierName
                         };
            List<MaterialVO> lst = new List<MaterialVO>();
            foreach (var item in result)
            {
                MaterialVO mv = new MaterialVO();
                mv.ID = item.ID;
                mv.CategoryID = item.CategoryID;
                mv.Code = item.Code;
                mv.ItemName = item.ItemName;
                mv.Description = item.Description;
                mv.SupplierID = item.SupplierID;
                mv.Type = item.Type;
                mv.Status = item.Status;
                mv.CreatedBy = item.CreatedBy;
                mv.CreateTime = item.CreateTime;
                mv.StatusDesc = item.Status == 1 ? "启用" : "失效";
                mv.CategoryDesc = item.CategoryDesc;
                mv.SupplierDesc = item.SupplierDesc;

                lst.Add(mv);
            }
            return lst;
        }

        public List<SerialNumberVO> getSerialNumberList()
        {
            var result = from m in wmsContext.T_Basic_SerialNumber
                         join n in wmsContext.T_Sys_Company on m.CompanyID equals n.ID into temp
                         from t in temp.DefaultIfEmpty()
                         select new
                         {
                             m.ID,
                             m.CompanyID,
                             m.Type,
                             m.Prefix,
                             m.YYMMDD,
                             m.SeqLength,
                             m.SeqStep,
                             m.Suffix,
                             m.Description,
                             m.Status,
                             m.CreatedBy,
                             m.CreateTime,
                             m.UpdatedBy,
                             m.UpdateTime,
                             t.CompanyName
                         };
            List<SerialNumberVO> lst = new List<SerialNumberVO>();
            foreach (var item in result)
            {
                SerialNumberVO snv = new SerialNumberVO();
                snv.ID = item.ID;
                snv.CompanyID = item.CompanyID;
                snv.Type = item.Type;
                snv.Prefix = item.Prefix;
                snv.YYMMDD = item.YYMMDD;
                snv.SeqLength = item.SeqLength;
                snv.SeqStep = item.SeqStep;
                snv.Suffix = item.Suffix;
                snv.Description = item.Description;
                snv.Status = item.Status;
                snv.CreatedBy = item.CreatedBy;
                snv.CreateTime = item.CreateTime;
                snv.UpdatedBy = item.UpdatedBy;
                snv.UpdateTime = item.UpdateTime;
                snv.StatusDesc = item.Status == 1 ? "启用" : "失效";
                snv.CompanyDesc = item.CompanyName;

                lst.Add(snv);
            }
            return lst;
        }

        public bool saveSerialNumber(SerialNumberVO pSNVO)
        {
            if (pSNVO.IsNullOrEmpty() ||
                pSNVO.Type.IsNullOrEmpty())
            {
                throw new WMSException("ER001");
            }

            if (pSNVO.ID == -1)
            {
                //新增
                var result = (from m in wmsContext.T_Basic_SerialNumber
                              where m.CompanyID == pSNVO.CompanyID
                              && m.Type.ToUpper().Equals(pSNVO.Type.ToUpper())
                              select m).SingleOrDefault();
                if (null != result)
                {
                    throw new WMSException("ER033", pSNVO.Type);
                }
                T_Basic_SerialNumber tbsn = new T_Basic_SerialNumber();
                tbsn.CompanyID = pSNVO.CompanyID;
                tbsn.Type = pSNVO.Type;
                tbsn.Prefix = pSNVO.Prefix;
                tbsn.YYMMDD = pSNVO.YYMMDD;
                tbsn.SeqLength = pSNVO.SeqLength;
                tbsn.SeqStep = pSNVO.SeqStep;
                tbsn.Suffix = pSNVO.Suffix;
                tbsn.Description = pSNVO.Description;
                tbsn.Status = 1;
                tbsn.CreatedBy = pSNVO.CreatedBy;
                tbsn.CreateTime = DateTime.Now;

                wmsContext.T_Basic_SerialNumber.InsertOnSubmit(tbsn);
                wmsContext.SubmitChanges();
                return true;
            }
            else
            {
                //编辑
                var result = (from m in wmsContext.T_Basic_SerialNumber
                              where m.ID != pSNVO.ID
                              && m.CompanyID == pSNVO.CompanyID
                              && m.Type.ToUpper().Equals(pSNVO.Type.ToUpper())
                              select m).SingleOrDefault();
                if (null != result)
                {
                    throw new WMSException("ER033", pSNVO.Type);
                }
                result = (from m in wmsContext.T_Basic_SerialNumber
                          where m.ID == pSNVO.ID
                          select m).SingleOrDefault();
                if (null == result)
                {
                    throw new WMSException("ER034", pSNVO.Type);
                }

                result.CompanyID = pSNVO.CompanyID;
                result.Type = pSNVO.Type;
                result.Prefix = pSNVO.Prefix;
                result.YYMMDD = pSNVO.YYMMDD;
                result.SeqLength = pSNVO.SeqLength;
                result.SeqStep = pSNVO.SeqStep;
                result.Suffix = pSNVO.Suffix;
                result.Description = pSNVO.Description;
                result.UpdatedBy = pSNVO.UpdatedBy;
                result.UpdateTime = DateTime.Now;

                wmsContext.SubmitChanges();
                return true;
            }
        }

        public bool enableAndDisableSerialNumberBySerialID(int pSerialID, int pStatus, string pAccount)
        {
            var result = (from m in wmsContext.T_Basic_SerialNumber
                          where m.ID == pSerialID
                          select m).SingleOrDefault();
            if (null == result)
            {
                throw new WMSException("ER034", pSerialID);
            }
            result.Status = pStatus;
            result.UpdatedBy = pAccount;
            result.UpdateTime = DateTime.Now;

            wmsContext.SubmitChanges();
            return true;
        }

        public string getSN(string pType, int pCompanyID)
        {
            if (string.IsNullOrEmpty(pType))
            {
                throw new WMSException("ER001");
            }
            //先获取序号规则
            SerialNumberVO snv = getSerialNumberByType(pType, pCompanyID);
            //获取服务器日期
            DateTime tmpdt = DateTime.Now;
            //获取序号规则中的年月日
            string tmpYMD = string.Empty;
            switch (snv.YYMMDD)
            {
                case "YY":
                    tmpYMD = tmpdt.ToString("yy");
                    break;
                case "YYMM":
                    tmpYMD = tmpdt.ToString("yyMM");
                    break;
                case "YYMMDD":
                    tmpYMD = tmpdt.ToString("yyMMdd");
                    break;
                default:
                    //与年月无关
                    tmpYMD = "000000";
                    break;
            }

            var result = (from m in wmsContext.T_Basic_SerialNumberDetail
                          where m.CompanyID == pCompanyID
                          && m.Type.ToUpper().Equals(pType.ToUpper())
                          && m.YYMMDD.Equals(tmpYMD)
                          select m).SingleOrDefault();
            if(null == result)
            {
                //新增
                string tmpsn = snv.Prefix + (tmpYMD.Equals("000000") ? "" : tmpYMD) + buildSeqNo(0, snv.SeqLength, snv.SeqStep) + snv.Suffix;

                T_Basic_SerialNumberDetail tbsnd = new T_Basic_SerialNumberDetail();
                tbsnd.CompanyID = snv.CompanyID;
                tbsnd.Type = snv.Type;
                tbsnd.Prefix = snv.Prefix;
                tbsnd.YYMMDD = tmpYMD;
                tbsnd.SeqLength = snv.SeqLength;
                tbsnd.SeqStep = snv.SeqStep;
                tbsnd.CurVal = snv.SeqStep;
                tbsnd.Description = snv.Description;
                tbsnd.Status = 1;
                tbsnd.CreateTime = DateTime.Now;

                wmsContext.T_Basic_SerialNumberDetail.InsertOnSubmit(tbsnd);
                wmsContext.SubmitChanges();

                return tmpsn;
            }
            else
            {
                //更新
                string tmpsn = result.Prefix +(result.YYMMDD.Equals("000000")?"":result.YYMMDD)+buildSeqNo(result.CurVal,result.SeqLength,result.SeqStep) + result.Suffix;

                result.CurVal += result.SeqStep;
                result.UpdateTime = DateTime.Now;

                wmsContext.SubmitChanges();

                return tmpsn;
            }
        }
       
        private SerialNumberVO getSerialNumberByType(string pType,int pCompanyID)
        {
            if (string.IsNullOrEmpty(pType))
            {
                throw new WMSException("ER001");
            }
            var result = (from m in wmsContext.T_Basic_SerialNumber
                          where m.CompanyID == pCompanyID 
                          && m.Type.ToUpper().Equals(pType.Trim().ToUpper())
                          select m).SingleOrDefault();
            if(null == result)
            {
                throw new WMSException("ER034", pType);
            }
            SerialNumberVO snv = new SerialNumberVO();
            snv.ID = result.ID;
            snv.CompanyID = result.CompanyID;
            snv.Type = result.Type;
            snv.Prefix = result.Prefix;
            snv.YYMMDD = result.YYMMDD;
            snv.SeqLength = result.SeqLength;
            snv.SeqStep = result.SeqStep;
            snv.Suffix = result.Suffix;
            snv.Description = result.Description;
            snv.Status = result.Status;

            return snv;
        }

        private string buildSeqNo(int? pCurrVal, int? pLength, int? pStep)
        {
            int tmpVal = pCurrVal == null ? 0 : int.Parse(pCurrVal.ToString()) + (pStep == null ? 0 : int.Parse(pStep.ToString()));
            int tmpLength = tmpVal.ToString().Length;
            if (tmpLength > pLength)
            {
                throw new WMSException("ER035");
            }
            if (tmpLength == pLength)
            {
                return tmpVal.ToString();
            }
            StringBuilder sb = new StringBuilder();
            for (int i = 1; i <= (pLength - tmpLength); i++)
            {
                sb.Append("0");
            }
            sb.Append(tmpVal);
            return sb.ToString();
        }

    }
}
