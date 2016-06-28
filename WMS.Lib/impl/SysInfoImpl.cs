using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Lib.intf;
using WMS.Lib.vo.sys;
using System.Configuration;
using Extension.Util.Archer;
using WMS.Lib.util;
using Security.Util.Archer;
using System.Diagnostics;

namespace WMS.Lib.impl
{
    public class SysInfoImpl:ISysInfo
    {
        DataWMSDataContext wmsContext = new DataWMSDataContext();

        /// <summary>
        /// 向下获取子级ID
        /// </summary>
        /// <param name="pID"></param>
        /// <returns></returns>
        private IEnumerable<T_Sys_Structure> getSubStructureID(int? pID)
        {
            var result = from m in wmsContext.T_Sys_Structure
                         where m.ParentID == pID
                         select m;
            return result.ToList().Concat(result.ToList().SelectMany(t => getSubStructureID(t.ID)));
        }

        /// <summary>
        /// 向上获取父级ID
        /// </summary>
        /// <param name="pID"></param>
        /// <returns></returns>
        private IEnumerable<T_Sys_Structure> getParentStructureID(int? pID)
        {
            var result = from m in wmsContext.T_Sys_Structure
                         where m.ID == pID
                         select m;
            return result.ToList().Concat(result.ToList().SelectMany(t => getParentStructureID(t.ParentID)));
        }

        
        public List<SysCompanyEasyVO> getCompanyEasyList()
        {
            List<SysCompanyEasyVO> lst = new List<SysCompanyEasyVO>();

            var result = from m in wmsContext.T_Sys_Company
                         where m.Status == 1
                         orderby m.Code 
                         select new
                         {
                             m.ID,
                             m.CompanyName
                         };
            foreach (var item in result)
            {
                SysCompanyEasyVO scev = new SysCompanyEasyVO();
                scev.ID = item.ID;
                scev.CompanyName = item.CompanyName;

                lst.Add(scev);
            }
            return lst;
        }

        public SysAccountEasyVO checkLogin(string pAccount, string pPwd)
        {
            if(pAccount.IsNullOrEmpty() || pPwd.IsNullOrEmpty())
            {
                throw new WMSException("ER001");
            }

            var result = (from m in wmsContext.T_Sys_Account
                          where m.Code.ToUpper().Equals(pAccount.ToUpper())
                         select new
                         {
                             Code = m.Code,
                             AccountName = m.AccountName,
                             Pwd = m.Password,
                             Status = m.Status
                         }).SingleOrDefault();
            if(result.IsNullOrEmpty())
            {
                throw new WMSException("ER002",pAccount);
            }
            if(result.Status == 0)
            {
                throw new WMSException("ER003",pAccount);
            }
            //加密原始口令,使用默认DES加密
            pPwd = SymmetricUtil.Encrypt(pPwd);
            if(!result.Pwd.Equals(pPwd))
            {
                throw new WMSException("ER004");
            }

            SysAccountEasyVO saev = new SysAccountEasyVO();
            saev.Code = result.Code;
            saev.AccountName = result.AccountName;
            
            return saev;
        }

        public List<SysRoleEasyVO> getRoleByAccount(string pAccount)
        {
            if(pAccount.IsNullOrEmpty())
            {
                throw new WMSException("ER001");
            }
            //先查询出用户ID及对应的部门ID
            var tmpacct = (from m in wmsContext.T_Sys_Account
                          where m.Code.ToUpper().Equals(pAccount.ToUpper())
                          select new
                          {
                              m.ID,
                              m.StructureID
                          }).SingleOrDefault();
            
            //获取用户及部门关联的角色列表，去重
            List<SysRoleEasyVO> lst = new List<SysRoleEasyVO>();

            var tmpa = (from n in wmsContext.T_Sys_RoleAccount_R
                       from k in wmsContext.T_Sys_Role
                       where n.RoleID == k.ID
                       && n.AccountID == tmpacct.ID
                       select new
                       {
                           k.ID,
                           k.RoleName
                       }).ToList();

            var r1 = getParentStructureID(tmpacct.StructureID)
                .Union(getSubStructureID(tmpacct.StructureID))
                .ToList();

            var tmpd = (from m in wmsContext.T_Sys_RoleStructure_R.AsEnumerable() //转为本地序列
                        from k in wmsContext.T_Sys_Role.AsEnumerable()
                        from n in r1
                        where m.RoleID == k.ID
                        && m.StructureID == n.ID 
                        select new
                        {
                            k.ID,
                            k.RoleName
                        }).ToList();

            var result = tmpa.Union(tmpd).Distinct();
            if (result.IsNullOrEmpty() || result.Count() <= 0)
            {
                throw new WMSException("ER005", pAccount);
            }

            foreach (var item in result)
            {
                SysRoleEasyVO srev = new SysRoleEasyVO();
                srev.ID = item.ID;
                srev.RoleName = item.RoleName;

                lst.Add(srev);
            }
                         
            return lst;
        }

        public List<SysMenuVO> getMenuByRoleId(int pRoleId)
        {
            var result = from n in wmsContext.T_Sys_Menu
                         from k in wmsContext.T_Sys_RoleMenu_R
                         where k.RoleID == pRoleId
                         && k.MenuID == n.ID
                         select new
                         {
                             n.ID,
                             n.ParentID,
                             n.MenuName,
                             n.Description,
                             n.CmdForm,
                             n.CmdExe,
                             n.Icon
                         };
            if(result.IsNullOrEmpty() || result.Count() <= 0)
            {
                throw new WMSException("ER006",pRoleId);
            }

            List<SysMenuVO> lst = new List<SysMenuVO>();
            foreach (var item in result)
            {
                SysMenuVO smv = new SysMenuVO();
                smv.ID = item.ID;
                smv.ParentID = item.ParentID;
                smv.MenuName = item.MenuName;
                smv.Description = item.Description;
                smv.CmdForm = item.CmdForm;
                smv.CmdExe = item.CmdExe;
                smv.Icon = item.Icon;

                lst.Add(smv);
            }
            return lst;
        }

        public List<SysMenuVO> getMenuList()
        {
            var result = from m in wmsContext.T_Sys_Menu
                         select m;
            List<SysMenuVO> lst = new List<SysMenuVO>();
            foreach (var item in result)
            {
                SysMenuVO smv = new SysMenuVO();
                smv.ID = item.ID;
                smv.ParentID = item.ParentID;
                smv.MenuName = item.MenuName;
                smv.Description = item.Description;
                smv.CmdForm = item.CmdForm;
                smv.CmdExe = item.CmdExe;
                smv.Icon = item.Icon;
                smv.Status = item.Status;
                smv.CreatedBy = item.CreatedBy;
                smv.CreateTime = item.CreateTime;
                smv.UpdatedBy = item.UpdatedBy;
                smv.UpdateTime = item.UpdateTime;
                smv.StatusDesc = item.Status == 1 ? "启用" : "失效";

                    lst.Add(smv);
            }
            return lst;
        }

        public List<SysRoleVO> getRoleList()
        {
            var result = from m in wmsContext.T_Sys_Role
                         select m;
            List<SysRoleVO> lst = new List<SysRoleVO>();
            foreach (var item in result)
            {
                SysRoleVO srv = new SysRoleVO();
                srv.ID = item.ID;
                srv.Code = item.Code;
                srv.RoleName = item.RoleName;
                srv.Description = item.Description;
                srv.Status = item.Status;
                srv.CreatedBy = item.CreatedBy;
                srv.CreateTime = item.CreateTime;
                srv.UpdatedBy = item.UpdatedBy;
                srv.UpdateTime = item.UpdateTime;
                srv.StatusDesc = item.Status == 1 ? "启用" : "失效";

                    lst.Add(srv);
            }
            return lst;
        }

        public List<SysCompanyVO> getCompanyList()
        {
            var result = from m in wmsContext.T_Sys_Company
                         select m;
            List<SysCompanyVO> lst = new List<SysCompanyVO>();
            foreach (var item in result)
            {
                SysCompanyVO scv = new SysCompanyVO();
                scv.ID = item.ID;
                scv.Code = item.Code;
                scv.CompanyName = item.CompanyName;
                scv.Description = item.Description;
                scv.Status = item.Status;
                scv.Address = item.Address;
                scv.Contacts = item.Contacts;
                scv.Email = item.Email;
                scv.Fax = item.Fax;
                scv.Tel = item.Tel;
                scv.Corporation = item.Corporation;
                scv.RegistrationNo = item.RegistrationNo;
                scv.CreatedBy = item.CreatedBy;
                scv.CreateTime = item.CreateTime;
                scv.UpdatedBy = item.UpdatedBy;
                scv.UpdateTime = item.UpdateTime;
                scv.StatusDesc = item.Status == 1 ? "启用" : "失效";

                    lst.Add(scv);
            }
            return lst;
        }

        public List<SysStructureVO> getStructureList()
        {
            var result = from m in wmsContext.T_Sys_Structure
                         select m;
            List<SysStructureVO> lst = new List<SysStructureVO>();
            foreach (var item in result)
            {
                SysStructureVO sdv = new SysStructureVO();
                sdv.ID = item.ID;
                sdv.ParentID = item.ParentID;
                sdv.Code = item.Code;
                sdv.StructureName = item.StructureName;
                sdv.Description = item.Description;
                sdv.Status = item.Status;
                sdv.CompanyID = item.CompanyID;
                sdv.CreatedBy = item.CreatedBy;
                sdv.CreateTime = item.CreateTime;
                sdv.UpdatedBy = item.UpdatedBy;
                sdv.UpdateTime = item.UpdateTime;
                sdv.StatusDesc = item.Status == 1 ? "启用" : "失效";

                    lst.Add(sdv);
            }
            return lst;
        }

        public List<SysAccountVO> getAccountList()
        {
            var result = from m in wmsContext.T_Sys_Account
                         join n in wmsContext.T_Sys_Structure on m.StructureID equals n.ID into temp
                         from tt in temp.DefaultIfEmpty()
                         select new
                         {
                             m.ID,
                             m.Code,
                             m.AccountName,
                             m.Password,
                             m.Status,
                             m.StructureID,
                             m.Description,
                             m.EnglishName,
                             m.Email,
                             m.Tel,
                             m.Address,
                             m.Age,
                             m.Sex,
                             m.Education,
                             m.Marriage,
                             m.HireDate,
                             m.EmergencyContacts,
                             m.EmergencyPhone,m.CreatedBy,
                             m.CreateTime,
                             m.UpdatedBy,
                             m.UpdateTime,
                             StructureDesc = tt==null?"": tt.StructureName
                         };
            List<SysAccountVO> lst = new List<SysAccountVO>();
            foreach (var item in result)
            {
                SysAccountVO sav = new SysAccountVO();
                sav.ID = item.ID;
                sav.Code = item.Code;
                sav.AccountName = item.AccountName;
                sav.Password = item.Password;
                sav.Status = item.Status;
                sav.StructureID = item.StructureID;
                sav.Description = item.Description;
                sav.EnglishName = item.EnglishName;
                sav.Email = item.Email;
                sav.Tel=item.Tel;
                sav.Address = item.Address;
                sav.Age = item.Age;
                sav.Sex = item.Sex;
                sav.Education = item.Education;
                sav.Marriage = item.Marriage;
                sav.HireDate = item.HireDate;
                sav.EmergencyContacts = item.EmergencyContacts;
                sav.EmergencyPhone = item.EmergencyPhone;
                sav.CreatedBy = item.CreatedBy;
                sav.CreateTime = item.CreateTime;
                sav.UpdatedBy = item.UpdatedBy;
                sav.UpdateTime = item.UpdateTime;
                sav.StatusDesc = item.Status == 1 ? "启用" : "失效";
                sav.StructureDesc = item.StructureDesc;

                    lst.Add(sav);
            }
            return lst;
        }

        public List<SysUserDefinedVO> getUserDeinedList()
        {
            var result = from m in wmsContext.T_Sys_UserDefined
                         select m;
            List<SysUserDefinedVO> lst = new List<SysUserDefinedVO>();
            foreach (var item in result)
            {
                SysUserDefinedVO sudv = new SysUserDefinedVO();
                sudv.ID = item.ID;
                sudv.SysCode = item.SysCode;
                sudv.Category = item.Category;
                sudv.UKey = item.UKey;
                sudv.UValue = item.UValue;
                sudv.Description = item.Description;
                sudv.Status = item.Status;
                sudv.CreatedBy = item.CreatedBy;
                sudv.CreateTime = item.CreateTime;
                sudv.UpdatedBy = item.UpdatedBy;
                sudv.UpdateTime = item.UpdateTime;
                sudv.StatusDesc = item.Status == 1 ? "启用" : "失效";

                    lst.Add(sudv);
            }
            return lst;
        }

        public bool saveMenu(SysMenuVO pMenuVO)
        {
            if(pMenuVO.IsNullOrEmpty() || 
                pMenuVO.MenuName.IsNullOrEmpty())
            {
                throw new WMSException("ER001");
            }

            if(pMenuVO.ID == -1)
            {
                //新增
                var result = (from m in wmsContext.T_Sys_Menu
                              where m.MenuName.Equals(pMenuVO.MenuName)
                              select m).SingleOrDefault();
                if(null != result)
                {
                    throw new WMSException("ER007",pMenuVO.MenuName);
                }
                T_Sys_Menu tsm = new T_Sys_Menu();
                tsm.ParentID = pMenuVO.ParentID;
                tsm.MenuName = pMenuVO.MenuName;
                tsm.Description = pMenuVO.Description;
                tsm.CmdForm = pMenuVO.CmdForm;
                tsm.CmdExe = pMenuVO.CmdExe;
                tsm.Icon = pMenuVO.Icon;
                tsm.Status = 1;//默认启用
                tsm.CreatedBy = pMenuVO.CreatedBy;
                tsm.CreateTime = DateTime.Now;

                wmsContext.T_Sys_Menu.InsertOnSubmit(tsm);
                wmsContext.SubmitChanges();
                return true;
            }
            else
            {
                //编辑
                var result =(from m in wmsContext.T_Sys_Menu
                             where m.ID != pMenuVO.ID
                             && m.MenuName.Equals(pMenuVO.MenuName)
                             select m).SingleOrDefault();
                if(null !=result)
                {
                    throw new WMSException("ER007", pMenuVO.MenuName);
                }

                result = (from m in wmsContext.T_Sys_Menu
                          where m.ID == pMenuVO.ID
                          select m).SingleOrDefault();
                if(null == result)
                {
                    throw new WMSException("ER008", pMenuVO.MenuName);
                }

                result.MenuName = pMenuVO.MenuName;
                result.Description = pMenuVO.Description;
                result.CmdForm = pMenuVO.CmdForm;
                result.CmdExe = pMenuVO.CmdExe;
                result.Icon = pMenuVO.Icon;
                result.UpdatedBy = pMenuVO.UpdatedBy;
                result.UpdateTime = DateTime.Now;

                wmsContext.SubmitChanges();
                return true;
            }

        }

        public bool saveRole(SysRoleVO pRoleVO)
        {
            if(pRoleVO.IsNullOrEmpty() || 
                pRoleVO.Code.IsNullOrEmpty() || 
                pRoleVO.RoleName.IsNullOrEmpty())
            {
                throw new WMSException("ER001");
            }

            if(pRoleVO.ID == -1)
            {
                //新增
                var result = (from m in wmsContext.T_Sys_Role
                              where m.Code.ToUpper().Equals(pRoleVO.Code.ToUpper())
                              select m).SingleOrDefault();
                if(null != result)
                {
                    throw new WMSException("ER009", pRoleVO.Code);
                }
                T_Sys_Role tsr = new T_Sys_Role();
                tsr.Code = pRoleVO.Code;
                tsr.RoleName = pRoleVO.RoleName;
                tsr.Description = pRoleVO.Description;
                tsr.Status = 1;
                tsr.CreatedBy = pRoleVO.CreatedBy;
                tsr.CreateTime = DateTime.Now;

                wmsContext.T_Sys_Role.InsertOnSubmit(tsr);
                wmsContext.SubmitChanges();
                return true;
            }
            else
            {
                //编辑
                var result = (from m in wmsContext.T_Sys_Role
                             where m.ID != pRoleVO.ID
                             && m.Code.ToUpper().Equals(pRoleVO.Code.ToUpper())
                             select m).SingleOrDefault();
                if(null != result)
                {
                    throw new WMSException("ER009", pRoleVO.Code);
                }
                result = (from m in wmsContext.T_Sys_Role
                          where m.ID == pRoleVO.ID
                          select m).SingleOrDefault();
                if(null == result)
                {
                    throw new WMSException("ER010", pRoleVO.Code);
                }

                result.Code = pRoleVO.Code;
                result.RoleName = pRoleVO.RoleName;
                result.Description = pRoleVO.Description;
                result.UpdatedBy = pRoleVO.UpdatedBy;
                result.UpdateTime = DateTime.Now;

                wmsContext.SubmitChanges();
                return true;
            }

        }

        public bool saveCompany(SysCompanyVO pCompanyVO)
        {
            if(pCompanyVO.IsNullOrEmpty() || 
                pCompanyVO.Code.IsNullOrEmpty() || 
                pCompanyVO.CompanyName.IsNullOrEmpty())
            {
                throw new WMSException("ER001");
            }

            if(pCompanyVO.ID == -1)
            {
                //新增
                var result = (from m in wmsContext.T_Sys_Company
                             where m.Code.ToUpper().Equals(pCompanyVO.Code.ToUpper())
                             select m).SingleOrDefault();
                if(null != result)
                {
                    throw new WMSException("ER011", pCompanyVO.Code);
                }

                T_Sys_Company tsc = new T_Sys_Company();
                tsc.Code = pCompanyVO.Code;
                tsc.CompanyName = pCompanyVO.CompanyName;
                tsc.Description = pCompanyVO.Description;
                tsc.Address = pCompanyVO.Address;
                tsc.Email = pCompanyVO.Email;
                tsc.Tel = pCompanyVO.Tel;
                tsc.Fax = pCompanyVO.Fax;
                tsc.Contacts = pCompanyVO.Contacts;
                tsc.Corporation = pCompanyVO.Corporation;
                tsc.RegistrationNo = pCompanyVO.RegistrationNo;
                tsc.Status = 1;
                tsc.CreatedBy = pCompanyVO.CreatedBy;
                tsc.CreateTime = DateTime.Now;

                wmsContext.T_Sys_Company.InsertOnSubmit(tsc);
                wmsContext.SubmitChanges();
                return true;
            }
            else
            {
                //编辑
                var result = (from m in wmsContext.T_Sys_Company
                             where m.ID != pCompanyVO.ID
                             && m.Code.ToUpper().Equals(pCompanyVO.Code.ToUpper())
                             select m).SingleOrDefault();
                if(null != result)
                {
                    throw new WMSException("ER011",pCompanyVO.Code);
                }

                result = (from m in wmsContext.T_Sys_Company
                          where m.ID == pCompanyVO.ID
                          select m).SingleOrDefault();
                if(null == result)
                {
                    throw new WMSException("ER012", pCompanyVO.Code);
                }

                result.Code = pCompanyVO.Code;
                result.CompanyName = pCompanyVO.CompanyName;
                result.Description = pCompanyVO.Description;
                result.Address = pCompanyVO.Address;
                result.Email = pCompanyVO.Email;
                result.Tel = pCompanyVO.Tel;
                result.Fax = pCompanyVO.Fax;
                result.Contacts = pCompanyVO.Contacts;
                result.Corporation = pCompanyVO.Corporation;
                result.RegistrationNo = pCompanyVO.RegistrationNo;
                result.UpdatedBy = pCompanyVO.UpdatedBy;
                result.UpdateTime = DateTime.Now;

                wmsContext.SubmitChanges();
                return true;
            }

        }

        public bool saveStructure(SysStructureVO pStructureVO)
        {
           if(pStructureVO.IsNullOrEmpty() || 
               pStructureVO.Code.IsNullOrEmpty() ||
               pStructureVO.StructureName.IsNullOrEmpty())
           {
               throw new WMSException("ER001");
           }

            if(pStructureVO.ID == -1)
            {
                //新增
                var result = (from m in wmsContext.T_Sys_Structure
                              where m.Code.ToUpper().Equals(pStructureVO.Code.ToUpper())
                              select m).SingleOrDefault();
                if(null != result)
                {
                    throw new WMSException("ER013", pStructureVO.Code);
                }
                T_Sys_Structure tsd = new T_Sys_Structure();
                tsd.ParentID = pStructureVO.ParentID;
                tsd.Code = pStructureVO.Code;
                tsd.StructureName = pStructureVO.StructureName;
                tsd.Description = pStructureVO.Description;
                tsd.CompanyID = pStructureVO.CompanyID;
                tsd.Status = 1;
                tsd.CreatedBy = pStructureVO.CreatedBy;
                tsd.CreateTime = DateTime.Now;

                wmsContext.T_Sys_Structure.InsertOnSubmit(tsd);
                wmsContext.SubmitChanges();
                return true;
            }
            else
            {
                //编辑
                var result = (from m in wmsContext.T_Sys_Structure
                              where m.ID != pStructureVO.ID
                              && m.Code.ToUpper().Equals(pStructureVO.Code.ToUpper())
                              select m).SingleOrDefault();
                if(null != result)
                {
                    throw new WMSException("ER013", pStructureVO.Code);
                }

                result = (from m in wmsContext.T_Sys_Structure
                          where m.ID == pStructureVO.ID
                          select m).SingleOrDefault();
                if(null == result)
                {
                    throw new WMSException("ER014", pStructureVO.Code);
                }

                result.Code = pStructureVO.Code;
                result.StructureName = pStructureVO.StructureName;
                result.Description = pStructureVO.Description;
                result.CompanyID = pStructureVO.CompanyID;
                result.UpdatedBy = pStructureVO.UpdatedBy;
                result.UpdateTime = pStructureVO.UpdateTime;

                wmsContext.SubmitChanges();
                return true;
            }

        }

        public bool saveAccount(SysAccountVO pAccountVO)
        {
            if(pAccountVO.IsNullOrEmpty() || 
                pAccountVO.Code.IsNullOrEmpty())
            {
                throw new WMSException("ER001");
            }

            if(pAccountVO.ID ==-1)
            {
                //新增
                var result = (from m in wmsContext.T_Sys_Account
                              where m.Code.ToUpper().Equals(pAccountVO.Code.ToUpper())
                              select m).SingleOrDefault();
                if(null != result)
                {
                    throw new WMSException("ER015", pAccountVO.Code);
                }
                T_Sys_Account tsa = new T_Sys_Account();
                tsa.Code = pAccountVO.Code;
                tsa.AccountName = pAccountVO.AccountName;
                tsa.Password = SymmetricUtil.Encrypt("123456");//默认密码为123456，并加密
                tsa.StructureID = pAccountVO.StructureID;
                tsa.Description = pAccountVO.Description;
                tsa.Email = pAccountVO.Email;
                tsa.Tel = pAccountVO.Tel;
                tsa.EnglishName = pAccountVO.EnglishName;
                tsa.Address = pAccountVO.Address;
                tsa.Age = pAccountVO.Age;
                tsa.Sex = pAccountVO.Sex;
                tsa.Education = pAccountVO.Education;
                tsa.Marriage = pAccountVO.Marriage;
                tsa.HireDate = pAccountVO.HireDate;
                tsa.EmergencyContacts = pAccountVO.EmergencyContacts;
                tsa.EmergencyPhone = pAccountVO.EmergencyPhone;
                tsa.Status = 1;
                tsa.CreatedBy = pAccountVO.CreatedBy;
                tsa.CreateTime = DateTime.Now;

                wmsContext.T_Sys_Account.InsertOnSubmit(tsa);
                wmsContext.SubmitChanges();
                return true;
            }
            else
            {
                //编辑
                var result = (from m in wmsContext.T_Sys_Account
                              where m.ID != pAccountVO.ID
                              && m.Code.ToUpper().Equals(pAccountVO.Code.ToUpper())
                              select m).SingleOrDefault();
                if(null != result)
                {
                    throw new WMSException("ER015", pAccountVO.Code);
                }
                result = (from m in wmsContext.T_Sys_Account
                          where m.ID == pAccountVO.ID
                          select m).SingleOrDefault();
                if(null == result)
                {
                    throw new WMSException("ER016", pAccountVO.Code);
                }

                result.Code = pAccountVO.Code;
                result.AccountName = pAccountVO.AccountName;
                result.StructureID = pAccountVO.StructureID;
                result.Description = pAccountVO.Description;
                result.Email = pAccountVO.Email;
                result.Tel = pAccountVO.Tel;
                result.EnglishName = pAccountVO.EnglishName;
                result.Address = pAccountVO.Address;
                result.Age = pAccountVO.Age;
                result.Sex = pAccountVO.Sex;
                result.Education = pAccountVO.Education;
                result.Marriage = pAccountVO.Marriage;
                result.HireDate = pAccountVO.HireDate;
                result.EmergencyContacts = pAccountVO.EmergencyContacts;
                result.EmergencyPhone = pAccountVO.EmergencyPhone;
                result.UpdatedBy = pAccountVO.UpdatedBy;
                result.UpdateTime = DateTime.Now;

                wmsContext.SubmitChanges();
                return true;
            }
        }

        public bool saveUserDefined(SysUserDefinedVO pUserDefinedVO)
        {
            if(pUserDefinedVO.IsNullOrEmpty() || 
                pUserDefinedVO.SysCode.IsNullOrEmpty() || 
                pUserDefinedVO.Category.IsNullOrEmpty() || 
                pUserDefinedVO.UKey.IsNullOrEmpty() ||
                pUserDefinedVO.UValue.IsNullOrEmpty())
            {
                throw new WMSException("ER001");
            }

            if(pUserDefinedVO.ID == -1)
            {
                //新增
                var result = (from m in wmsContext.T_Sys_UserDefined
                              where m.SysCode.ToUpper().Equals(pUserDefinedVO.SysCode.ToUpper())
                              && m.Category.ToUpper().Equals(pUserDefinedVO.Category.ToUpper())
                              && m.UKey.ToUpper().Equals(pUserDefinedVO.UKey.ToUpper())
                              select m).SingleOrDefault();
                if(null != result)
                {
                    throw new WMSException("ER017", pUserDefinedVO.UKey);
                }
                T_Sys_UserDefined tsud = new T_Sys_UserDefined();
                tsud.SysCode = pUserDefinedVO.SysCode;
                tsud.Category = pUserDefinedVO.Category;
                tsud.UKey = pUserDefinedVO.UKey;
                tsud.UValue = pUserDefinedVO.UValue;
                tsud.Description = pUserDefinedVO.Description;
                tsud.Status = 1;
                tsud.CreatedBy = pUserDefinedVO.CreatedBy;
                tsud.CreateTime = DateTime.Now;

                wmsContext.T_Sys_UserDefined.InsertOnSubmit(tsud);
                wmsContext.SubmitChanges();
                return true;
            }
            else
            {
                //编辑
                var result = (from m in wmsContext.T_Sys_UserDefined
                              where m.ID != pUserDefinedVO.ID
                              && m.SysCode.ToUpper().Equals(pUserDefinedVO.SysCode.ToUpper())
                              && m.Category.ToUpper().Equals(pUserDefinedVO.Category.ToUpper())
                              && m.UKey.ToUpper().Equals(pUserDefinedVO.UKey.ToUpper())
                              select m).SingleOrDefault();
                if(null != result)
                {
                    throw new WMSException("ER017", pUserDefinedVO.UKey);
                }
                result = (from m in wmsContext.T_Sys_UserDefined
                          where m.ID == pUserDefinedVO.ID
                          select m).SingleOrDefault();
                if(null == result)
                {
                    throw new WMSException("ER018", pUserDefinedVO.UKey);
                }

                result.SysCode = pUserDefinedVO.SysCode;
                result.Category = pUserDefinedVO.Category;
                result.UKey = pUserDefinedVO.UKey;
                result.UValue = pUserDefinedVO.UValue;
                result.Description = pUserDefinedVO.Description;
                result.UpdatedBy = pUserDefinedVO.UpdatedBy;
                result.UpdateTime = DateTime.Now;

                wmsContext.SubmitChanges();
                return true;
            }

        }

        public bool changePassword(string pAccount, string pOldPwd, string pNewPwd)
        {
            if(pAccount.IsNullOrEmpty() || 
                pOldPwd.IsNullOrEmpty() || 
                pNewPwd.IsNullOrEmpty())
            {
                throw new WMSException("ER001");
            }

            var result = (from m in wmsContext.T_Sys_Account
                          where m.Code.ToUpper().Equals(pAccount.ToUpper())
                          select m).SingleOrDefault();
            if(result.IsNullOrEmpty())
            {
                throw new WMSException("ER002", pAccount);
            }
            //加密原始密码
            pOldPwd = SymmetricUtil.Encrypt(pOldPwd);
            if(!result.Password.Equals(pOldPwd))
            {
                throw new WMSException("ER004");
            }
            //加密新密码
            pNewPwd = SymmetricUtil.Encrypt(pNewPwd);
            result.Password = pNewPwd;
            result.UpdateTime = DateTime.Now;

            wmsContext.SubmitChanges();
            return true;
        }

        public bool resetPassword(string pAccount, string pPwd = "123456")
        {
            if(pAccount.IsNullOrEmpty())
            {
                throw new WMSException("ER001");
            }

            var result = (from m in wmsContext.T_Sys_Account
                         where m.Code.ToUpper().Equals(pAccount.ToUpper())
                         select m).SingleOrDefault();
            if(result.IsNullOrEmpty())
            {
                throw new WMSException("ER002");
            }
            if(pPwd.IsNullOrEmpty())
            {
                pPwd = "123456";
            }
            //加密密码
            pPwd = SymmetricUtil.Encrypt(pPwd);
            result.Password = pPwd;
            result.UpdateTime = DateTime.Now;

            wmsContext.SubmitChanges();
            return true;
        }

        public bool enableAndDisableByMenuId(int pMenuId, int pStatus, string pAccount)
        {
            var result = (from m in wmsContext.T_Sys_Menu
                          where m.ID == pMenuId
                          select m).SingleOrDefault();
            if(null == result)
            {
                throw new WMSException("ER008", pMenuId);
            }
            result.Status = pStatus;
            result.UpdatedBy = pAccount;
            result.UpdateTime = DateTime.Now;

            wmsContext.SubmitChanges();
            return true;
        }

        public bool enableAndDisableByCompanyId(int pCompanyId, int pStatus, string pAccount)
        {
            var result = (from m in wmsContext.T_Sys_Company
                          where m.ID == pCompanyId
                          select m).SingleOrDefault();
            if(null == result)
            {
                throw new WMSException("ER012", pCompanyId);
            }
            result.Status = pStatus;
            result.UpdatedBy = pAccount;
            result.UpdateTime = DateTime.Now;

            wmsContext.SubmitChanges();
            return true;
        }

        public bool enableAndDisableByStructureId(int pStructureId, int pStatus, string pAccount)
        {
            var result = (from m in wmsContext.T_Sys_Structure
                          where m.ID == pStructureId
                          select m).SingleOrDefault();
            if(null == result)
            {
                throw new WMSException("ER014", pStructureId);
            }
            result.Status = pStatus;
            result.UpdatedBy = pAccount;
            result.UpdateTime = DateTime.Now;

            wmsContext.SubmitChanges();
            return true;
        }

        public bool enableAndDisableByAccountId(int pAccountId, int pStatus, string pAccount)
        {
            var result = (from m in wmsContext.T_Sys_Account
                          where m.ID == pAccountId
                          select m).SingleOrDefault();
            if(null == result)
            {
                throw new WMSException("ER016", pAccountId);
            }
            result.Status = pStatus;
            result.UpdatedBy = pAccount;
            result.UpdateTime = DateTime.Now;

            wmsContext.SubmitChanges();
            return true;
        }

        public bool enableAndDisableByRoleId(int pRoleId, int pStatus, string pAccount)
        {
            var result = (from m in wmsContext.T_Sys_Role
                          where m.ID == pRoleId
                          select m).SingleOrDefault();
            if(null ==result)
            {
                throw new WMSException("ER010", pRoleId);
            }
            result.Status = pStatus;
            result.UpdatedBy = pAccount;
            result.UpdateTime = DateTime.Now;

            wmsContext.SubmitChanges();
            return true;
        }

        public bool enableAndDisableByUdcId(int pUdcId, int pStatus, string pAccount)
        {
            var result = (from m in wmsContext.T_Sys_UserDefined
                          where m.ID == pUdcId
                          select m).SingleOrDefault();
            if(null == result)
            {
                throw new WMSException("ER018", pUdcId);
            }
            result.Status = pStatus;
            result.UpdatedBy = pAccount;
            result.UpdateTime = DateTime.Now;

            wmsContext.SubmitChanges();
            return true;
        }

        public List<SysRoleAccountVO> getRoleAccountByAccountId(int pAccountId)
        {
            var result = from m in wmsContext.T_Sys_RoleAccount_R
                         where m.AccountID == pAccountId
                         select m;
            List<SysRoleAccountVO> lst = new List<SysRoleAccountVO>();
            foreach (var item in result)
            {
                SysRoleAccountVO srav = new SysRoleAccountVO();
                srav.ID = item.ID;
                srav.RoleID = item.RoleID;
                srav.AccountID = item.AccountID;

                lst.Add(srav);
            }
            return lst;
        }

        public void removeAccountId2Role(int pRoleId, int pAccountId)
        {
            var result = from m in wmsContext.T_Sys_RoleAccount_R
                          where m.RoleID == pRoleId
                          && m.AccountID == pAccountId
                          select m;
            if(null == result || result.Count() <= 0)
            {
                return;
            }
            wmsContext.T_Sys_RoleAccount_R.DeleteAllOnSubmit(result);
            wmsContext.SubmitChanges();
        }

        public void saveAccountId2Role(int pRoleId, int pAccountId)
        {
            var result = from m in wmsContext.T_Sys_RoleAccount_R
                         where m.RoleID == pRoleId
                         && m.AccountID == pAccountId
                         select m;
            if(null == result || result.Count() <= 0)
            {
                T_Sys_RoleAccount_R tsrr = new T_Sys_RoleAccount_R();
                tsrr.RoleID = pRoleId;
                tsrr.AccountID = pAccountId;

                wmsContext.T_Sys_RoleAccount_R.InsertOnSubmit(tsrr);
                wmsContext.SubmitChanges();
            }
        }

        public List<SysRoleStructureVO> getRoleStructureByStructureId(int pStructureId)
        {
            var result = from m in wmsContext.T_Sys_RoleStructure_R
                         where m.StructureID == pStructureId
                         select m;
            List<SysRoleStructureVO> lst = new List<SysRoleStructureVO>();
            foreach (var item in result)
            {
                SysRoleStructureVO srdv = new SysRoleStructureVO();
                srdv.ID = item.ID;
                srdv.RoleID = item.RoleID;
                srdv.StructureID = item.StructureID;

                lst.Add(srdv);
            }
            return lst;
        }

        public void removeStructureId2Role(int pRoleId, int pStructureId)
        {
            var result = from m in wmsContext.T_Sys_RoleStructure_R
                         where m.RoleID == pRoleId
                         && m.StructureID == pStructureId
                         select m;
            if (null == result || result.Count() <= 0)
            {
                return;
            }
            wmsContext.T_Sys_RoleStructure_R.DeleteAllOnSubmit(result);
            wmsContext.SubmitChanges();
        }

        public void saveStructureId2Role(int pRoleId, int pStructureId)
        {
            var result = from m in wmsContext.T_Sys_RoleStructure_R
                         where m.RoleID == pRoleId
                         && m.StructureID == pStructureId
                         select m;
            if (null == result || result.Count() <= 0)
            {
                T_Sys_RoleStructure_R tsrr = new T_Sys_RoleStructure_R();
                tsrr.RoleID = pRoleId;
                tsrr.StructureID = pStructureId;

                wmsContext.T_Sys_RoleStructure_R.InsertOnSubmit(tsrr);
                wmsContext.SubmitChanges();
            }
        }

        public List<SysRoleMenuVO> getRoleMenuByRoleId(int pRoleId)
        {
            var result = from m in wmsContext.T_Sys_RoleMenu_R
                         where m.RoleID == pRoleId
                         select m;
            List<SysRoleMenuVO> lst = new List<SysRoleMenuVO>();
            foreach (var item in result)
            {
                SysRoleMenuVO srmv = new SysRoleMenuVO();
                srmv.ID = item.ID;
                srmv.RoleID = item.RoleID;
                srmv.MenuID = item.MenuID;

                lst.Add(srmv);
            }
            return lst;
        }

        public void removeMenuId2Role(int pRoleId, int pMenuId)
        {
            var result = from m in wmsContext.T_Sys_RoleMenu_R
                         where m.RoleID == pRoleId
                         && m.MenuID == pMenuId
                         select m;
            if (null == result || result.Count() <= 0)
            {
                return;
            }
            wmsContext.T_Sys_RoleMenu_R.DeleteAllOnSubmit(result);
            wmsContext.SubmitChanges();
        }

        public void saveMenuId2Role(int pRoleId, int pMenuId)
        {
            var result = from m in wmsContext.T_Sys_RoleMenu_R
                         where m.RoleID == pRoleId
                         && m.MenuID == pMenuId
                         select m;
            if (null == result || result.Count() <= 0)
            {
                T_Sys_RoleMenu_R tsrm = new T_Sys_RoleMenu_R();
                tsrm.RoleID = pRoleId;
                tsrm.MenuID = pMenuId;

                wmsContext.T_Sys_RoleMenu_R.InsertOnSubmit(tsrm);
                wmsContext.SubmitChanges();
            }
        }
    }
}
