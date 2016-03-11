using SSO.Lib.Intf.Basic;
using SSO.Lib.VO.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSO.Lib.Impl.Basic
{
    public class BasicImpl:IBasic
    {
       
        CS_SystemDataContext DBContext = new CS_SystemDataContext();

        public List<RoleRVO> getRoleByAccount(Guid pAccountID, Guid? pDeptID)
        {
            var result = (from k in getParentDepartmentID(pDeptID)
                          from m in DBContext.TB_Role_R
                          where m.RoleID == pAccountID
                         || m.RoleID == k.ID
                          select new
                          {
                              m.RID,
                              m.RName
                          }).Distinct();

            if(null == result || result.Count() <= 0)
            {
                
                throw new Exception("用户未关联任何角色");
            }

            List<RoleRVO> lst = new List<RoleRVO>();
            foreach (var item in result)
            {
                RoleRVO roleRVO = new RoleRVO();
                roleRVO.RID = item.RID;
                roleRVO.RName = item.RName;

                lst.Add(roleRVO);
            }

            return lst;
        }

        /// <summary>
        /// 向下获取子级ID
        /// </summary>
        /// <param name="pID"></param>
        /// <returns></returns>
        private IEnumerable<TB_Department> getSubDepartmentID(Guid? pID)
        {
            var result = from m in DBContext.TB_Department
                         where m.ParentID == pID
                         select m;
            return result.ToList().Concat(result.ToList().SelectMany(t => getSubDepartmentID(t.ID)));
        }

        /// <summary>
        /// 向上获取父级ID
        /// </summary>
        /// <param name="pID"></param>
        /// <returns></returns>
        private IEnumerable<TB_Department> getParentDepartmentID(Guid? pID)
        {
            var result = from m in DBContext.TB_Department
                         where m.ID == pID
                         select m;
            return result.ToList().Concat(result.ToList().SelectMany(t => getParentDepartmentID(t.ParentID)));
        }

        public List<MenuVO> getMenuByRole(Guid pRoleID)
        {
            var result = from m in DBContext.TB_Role_R
                         from n in DBContext.TB_Menu
                         where m.RID == n.ID
                         && m.RoleID == pRoleID
                         select new
                         {
                             n.ID,
                             n.ParentID,
                             n.Name,
                             n.Description,
                             n.CmdForm,
                             n.CmdExe
                         };
            if(null== result || result.Count() <= 0)
            {
                throw new Exception("角色未关联任何菜单");
            }

            List<MenuVO> lst = new List<MenuVO>();
            foreach (var item in result)
            {
                MenuVO menuVO = new MenuVO();
                menuVO.ID = item.ID;
                menuVO.ParentID = item.ParentID;
                menuVO.Name = item.Name;
                menuVO.Description = item.Description;
                menuVO.CmdForm = item.CmdForm;
                menuVO.CmdExe = item.CmdExe;

                lst.Add(menuVO);
            }

            return lst;
        }

        public List<FactoryVO> getFactory()
        {
            var result = from m in DBContext.TB_Factory
                         select new
                         {
                             m.ID,
                             m.Code,
                             m.Name,
                             m.Description,
                             m.Status,
                             m.CreateBy,
                             m.CreateOn,
                             m.UpdateBy,
                             m.UpdateOn
                         };
            List<FactoryVO> lst = new List<FactoryVO>();
            foreach (var item in result)
            {
                FactoryVO factoryVO = new FactoryVO();
                factoryVO.ID = item.ID;
                factoryVO.Code=item.Code;
                factoryVO.Name = item.Name;
                factoryVO.Description = item.Description;
                factoryVO.Status = item.Status;
                factoryVO.CreateBy = item.CreateBy;
                factoryVO.CreateOn = item.CreateOn;
                factoryVO.UpdateBy = item.UpdateBy;
                factoryVO.UpdateOn = item.UpdateOn;

                lst.Add(factoryVO);
            }
            return lst;
        }

        public bool enableAndDisableByFactoryCode(string pFactoryCode,string pStatus,string pAccount)
        {
            if(string.IsNullOrEmpty(pFactoryCode) ||
                string.IsNullOrEmpty(pStatus))
            {
                throw new ArgumentNullException("E-0001", "参数为空");
            }

            var result = (from m in DBContext.TB_Factory
                         where m.Code.ToUpper().Equals(pFactoryCode.ToUpper())
                         select m).FirstOrDefault();
            if(null == result)
            {
                throw new Exception("工厂编号不存在");
            }

            result.Status = pStatus;
            result.UpdateBy = pAccount;
            result.UpdateOn = DateTime.Now;

            DBContext.SubmitChanges();
            return true;
        }

        public bool saveFactory(FactoryVO pFactoryVO)
        {
            if(null == pFactoryVO ||
                string.IsNullOrEmpty(pFactoryVO.Code) ||
                string.IsNullOrEmpty(pFactoryVO.Name))
            {
                throw new ArgumentNullException("E-0001", "参数为空");
            }

            if(null == pFactoryVO.ID || pFactoryVO.ID.Equals(Guid.Empty))
            {
                //新增
                var result = (from m in DBContext.TB_Factory
                              where m.Code.ToUpper().Equals(pFactoryVO.Code.ToUpper())
                              select m).FirstOrDefault();
                if (null != result)
                {
                    throw new Exception(string.Format(@"已存在编号为【{0}】的工厂信息", pFactoryVO.Code));
                }

                TB_Factory tbFactory = new TB_Factory();
                tbFactory.ID = Guid.NewGuid();
                tbFactory.Code = pFactoryVO.Code;
                tbFactory.Name = pFactoryVO.Name;
                tbFactory.Description = pFactoryVO.Description;
                tbFactory.Status = "1";
                tbFactory.CreateBy = pFactoryVO.CreateBy;
                tbFactory.CreateOn = DateTime.Now;

                DBContext.TB_Factory.InsertOnSubmit(tbFactory);
                DBContext.SubmitChanges();
                return true;
            }
            else
            {
                //更新
                var result = (from m in DBContext.TB_Factory
                              where  !m.ID.Equals(pFactoryVO.ID)
                              && m.Code.ToUpper().Equals(pFactoryVO.Code.ToUpper())
                              select m).FirstOrDefault();
                if (null != result)
                {
                    throw new Exception(string.Format(@"已存在编号为【{0}】的工厂信息", pFactoryVO.Code));
                }

                result = (from m in DBContext.TB_Factory
                          where m.ID.Equals(pFactoryVO.ID)
                          select m).FirstOrDefault();
                
                //result.Code = pFactoryVO.Code;
                result.Name = pFactoryVO.Name;
                result.Description = pFactoryVO.Description;
                result.UpdateBy = pFactoryVO.CreateBy;
                result.UpdateOn = DateTime.Now;

                DBContext.SubmitChanges();
                return true;
            }
        }

        public List<DepartmentVO> getDepartment()
        {
            var result = from m in DBContext.TB_Department
                         select new
                         {
                             m.ID,
                             m.ParentID,
                             m.Code,
                             m.Name,
                             m.Description,
                             m.Status,
                             m.CreateBy,
                             m.CreateOn,
                             m.UpdateBy,
                             m.UpdateOn
                         };
            List<DepartmentVO> lst = new List<DepartmentVO>();
            foreach (var item in result)
            {
                DepartmentVO departmentVO = new DepartmentVO();
                departmentVO.ID = item.ID;
                departmentVO.ParentID = item.ParentID;
                departmentVO.Code = item.Code;
                departmentVO.Name = item.Name;
                departmentVO.Description = item.Description;
                departmentVO.Status = item.Status;
                departmentVO.CreateBy = item.CreateBy;
                departmentVO.CreateOn = item.CreateOn;
                departmentVO.UpdateBy = item.UpdateBy;
                departmentVO.UpdateOn = item.UpdateOn;

                lst.Add(departmentVO);
            }
            return lst;
        }

        public bool enableAndDisableByDepartmentCode(string pDeptCode, string pStatus, string pAccount)
        {
            if (string.IsNullOrEmpty(pDeptCode) ||
               string.IsNullOrEmpty(pStatus))
            {
                throw new ArgumentNullException("E-0001", "参数为空");
            }

            var result = (from m in DBContext.TB_Department
                          where m.Code.ToUpper().Equals(pDeptCode.ToUpper())
                          select m).FirstOrDefault();
            if(null == result)
            {
                throw new Exception("部门编号不存在");
            }

            result.Status = pStatus;
            result.UpdateBy = pAccount;
            result.UpdateOn = DateTime.Now;

            DBContext.SubmitChanges();
            return true;
        }

        public bool saveDepartment(DepartmentVO pDeptVO)
        {
            if(null == pDeptVO ||
                string.IsNullOrEmpty(pDeptVO.Code) ||
                string.IsNullOrEmpty(pDeptVO.Name))
            {
                throw new ArgumentNullException("E-0001", "参数为空");
            }

            if(null == pDeptVO.ID || pDeptVO.ID.Equals(Guid.Empty))
            {
                //新增
                var result = (from m in DBContext.TB_Department
                             where m.Code.ToUpper().Equals(pDeptVO.Code.ToUpper())
                             select m).FirstOrDefault();
                if(null != result)
                {
                    throw new Exception(string.Format(@"已存在编号为【{0}】的部门信息", pDeptVO.Code));
                }
                TB_Department tbDepartment = new TB_Department();
                tbDepartment.ID = Guid.NewGuid();
                tbDepartment.ParentID = pDeptVO.ParentID;
                tbDepartment.Code = pDeptVO.Code;
                tbDepartment.Name = pDeptVO.Name;
                tbDepartment.Description = pDeptVO.Description;
                tbDepartment.Status = "1";
                tbDepartment.CreateBy = pDeptVO.CreateBy;
                tbDepartment.CreateOn = DateTime.Now;

                DBContext.TB_Department.InsertOnSubmit(tbDepartment);
                DBContext.SubmitChanges();
                return true;
            }
            else
            {
                //编辑
                var result = (from m in DBContext.TB_Department
                             where !m.ID.Equals(pDeptVO.ID)
                             && m.Code.ToUpper().Equals(pDeptVO.Code.ToUpper())
                             select m).FirstOrDefault();
                if(null !=result)
                {
                    throw new Exception(string.Format(@"已存在编号为【{0}】的部门信息", pDeptVO.Code));
                }

                result = (from m in DBContext.TB_Department
                          where m.ID.Equals(pDeptVO.ID)
                          select m).FirstOrDefault();
                //result.Code = pDeptVO.Code;
                result.Name = pDeptVO.Name;
                result.Description = pDeptVO.Description;
                result.UpdateBy = pDeptVO.UpdateBy;
                result.UpdateOn = DateTime.Now;

                DBContext.SubmitChanges();
                return true;
            }
        }

        public List<AccountVO> getAccount()
        {
            var result = from m in DBContext.TB_Account
                         select new
                         {
                             m.ID,
                             m.Code,
                             m.Name,
                             m.EngName,
                             m.DeptID,
                             m.DeptCode,
                             m.Email,
                             m.Tel,
                             m.Status,
                             m.CreateBy,
                             m.CreateOn,
                             m.UpdateBy,
                             m.UpdateOn
                         };
            List<AccountVO> lst = new List<AccountVO>();
            foreach (var item in result)
            {
                AccountVO accountVO = new AccountVO();
                accountVO.ID = item.ID;
                accountVO.Code = item.Code;
                accountVO.Name = item.Name;
                accountVO.EngName = item.EngName;
                accountVO.DeptID = item.DeptID;
                accountVO.DeptCode = item.DeptCode;
                accountVO.Email = item.Email;
                accountVO.Tel = item.Tel;
                accountVO.Status = item.Status;
                accountVO.CreateBy = item.CreateBy;
                accountVO.CreateOn = item.CreateOn;
                accountVO.UpdateBy = item.UpdateBy;
                accountVO.UpdateOn = item.UpdateOn;

                lst.Add(accountVO);
            }

            return lst;
        }

        public bool enableAndDisableByAccountCode(string pAccountCode, string pStatus, string pAccount)
        {
            if(string.IsNullOrEmpty(pAccountCode) ||
                string.IsNullOrEmpty(pStatus))
            {
                throw new ArgumentNullException("E-0001", "参数为空");
            }

            var result = (from m in DBContext.TB_Account
                          where m.Code.ToUpper().Equals(pAccountCode.ToUpper())
                          select m).FirstOrDefault();
            if(null == result)
            {
                throw new Exception("用户编号不存在");
            }
            result.Status = pStatus;
            result.UpdateBy = pAccount;
            result.UpdateOn = DateTime.Now;

            DBContext.SubmitChanges();
            return true;
        }

        public bool saveAccount(AccountVO pAccountVO)
        {
            if(null == pAccountVO ||
                string.IsNullOrEmpty(pAccountVO.Code))
            {
                throw new ArgumentNullException("E-0001", "参数为空");
            }

            if(null == pAccountVO.ID || pAccountVO.ID.Equals(Guid.Empty))
            {
                //新增
                var result = (from m in DBContext.TB_Account
                              where m.Code.ToUpper().Equals(pAccountVO.Code.ToUpper())
                              select m).FirstOrDefault();
                if(null != result)
                {
                    throw new Exception(string.Format(@"已存在编号为【{0}】的用户信息", pAccountVO.Code));
                }
                TB_Account tbAccount = new TB_Account();
                tbAccount.ID = Guid.NewGuid();
                tbAccount.Code = pAccountVO.Code;
                tbAccount.Name = pAccountVO.Name;
                tbAccount.EngName = pAccountVO.EngName;
                tbAccount.Password = "123456";
                tbAccount.DeptID = pAccountVO.DeptID;
                tbAccount.DeptCode = pAccountVO.DeptCode;
                tbAccount.Email = pAccountVO.Email;
                tbAccount.Tel = pAccountVO.Tel;
                tbAccount.Status = "1";
                tbAccount.CreateBy = pAccountVO.CreateBy;
                tbAccount.CreateOn = DateTime.Now;

                DBContext.TB_Account.InsertOnSubmit(tbAccount);
                DBContext.SubmitChanges();
                return true;
            }
            else
            {
                //编辑
                var result = (from m in DBContext.TB_Account
                              where !m.ID.Equals(pAccountVO.ID)
                              && m.Code.ToUpper().Equals(pAccountVO.Code.ToUpper())
                              select m).FirstOrDefault();
                if (null != result)
                {
                    throw new Exception(string.Format(@"已存在编号为【{0}】的用户信息", pAccountVO.Code));
                }
                result = (from m in DBContext.TB_Account
                          where m.ID.Equals(pAccountVO.ID)
                          select m).FirstOrDefault();
                //result.Code = pAccountVO.Code;
                result.Name = pAccountVO.Name;
                result.EngName = pAccountVO.EngName;
                result.DeptID = pAccountVO.DeptID;
                result.DeptCode = pAccountVO.DeptCode;
                result.Email = pAccountVO.Email;
                result.Tel = pAccountVO.Tel;
                result.UpdateBy = pAccountVO.UpdateBy;
                result.UpdateOn = DateTime.Now;

                DBContext.SubmitChanges();
                return true;
            }

        }

        public List<RoleVO> getRole()
        {
            var result = from m in DBContext.TB_Role
                         select new
                         {
                             m.ID,
                             m.ParentID,
                             m.Code,
                             m.Name,
                             m.Description,
                             m.Status,
                             m.CreateBy,
                             m.CreateOn,
                             m.UpdateBy,
                             m.UpdateOn
                         };
            List<RoleVO> lst = new List<RoleVO>();
            foreach (var item in result)
            {
                RoleVO roleVO = new RoleVO();
                roleVO.ID = item.ID;
                roleVO.ParentID = item.ParentID;
                roleVO.Code = item.Code;
                roleVO.Name = item.Name;
                roleVO.Description = item.Description;
                roleVO.Status = item.Status;
                roleVO.CreateBy = item.CreateBy;
                roleVO.CreateOn = item.CreateOn;
                roleVO.UpdateBy = item.UpdateBy;
                roleVO.UpdateOn = item.UpdateOn;

                lst.Add(roleVO);
            }

            return lst;
        }

        public bool enableAndDisableByRoleCode(string pRoleCode, string pStatus, string pAccount)
        {
            if (string.IsNullOrEmpty(pRoleCode) ||
                string.IsNullOrEmpty(pStatus))
            {
                throw new ArgumentNullException("E-0001", "参数为空");
            }

            var result = (from m in DBContext.TB_Role
                          where m.Code.ToUpper().Equals(pRoleCode.ToUpper())
                          select m).FirstOrDefault();
            if (null == result)
            {
                throw new Exception("角色编号不存在");
            }
            result.Status = pStatus;
            result.UpdateBy = pAccount;
            result.UpdateOn = DateTime.Now;

            DBContext.SubmitChanges();
            return true;
        }

        public bool saveRole(RoleVO pRoleVO)
        {
            if (null == pRoleVO ||
                string.IsNullOrEmpty(pRoleVO.Code) ||
                string.IsNullOrEmpty(pRoleVO.Name))
            {
                throw new ArgumentNullException("E-0001", "参数为空");
            }

            if (null == pRoleVO.ID || pRoleVO.ID.Equals(Guid.Empty))
            {
                //新增
                var result = (from m in DBContext.TB_Role
                              where m.Code.ToUpper().Equals(pRoleVO.Code.ToUpper())
                              select m).FirstOrDefault();
                if (null != result)
                {
                    throw new Exception(string.Format(@"已存在编号为【{0}】的角色信息", pRoleVO.Code));
                }
                TB_Role tbRole = new TB_Role();
                tbRole.ID = Guid.NewGuid();
                tbRole.ParentID = pRoleVO.ParentID;
                tbRole.Code = pRoleVO.Code;
                tbRole.Name = pRoleVO.Name;
                tbRole.Description = pRoleVO.Description;
                tbRole.Status = "1";
                tbRole.CreateBy = pRoleVO.CreateBy;
                tbRole.CreateOn = DateTime.Now;

                DBContext.TB_Role.InsertOnSubmit(tbRole);
                DBContext.SubmitChanges();
                return true;
            }
            else
            {
                //编辑
                var result = (from m in DBContext.TB_Role
                              where !m.ID.Equals(pRoleVO.ID)
                              && m.Code.ToUpper().Equals(pRoleVO.Code.ToUpper())
                              select m).FirstOrDefault();
                if (null != result)
                {
                    throw new Exception(string.Format(@"已存在编号为【{0}】的角色信息", pRoleVO.Code));
                }

                result = (from m in DBContext.TB_Role
                          where m.ID.Equals(pRoleVO.ID)
                          select m).FirstOrDefault();
                //result.Code = pRoleVO.Code;
                result.Name = pRoleVO.Name;
                result.Description = pRoleVO.Description;
                result.UpdateBy = pRoleVO.UpdateBy;
                result.UpdateOn = DateTime.Now;

                DBContext.SubmitChanges();
                return true;
            }
        }

        public List<UdcVO> getUdc()
        {
            var result = from m in DBContext.TB_Udc
                         select new
                         {
                             m.ID,
                             m.Code,
                             m.SysCode,
                             m.Category,
                             m.Value,
                             m.Description,
                             m.Status,
                             m.CreateBy,
                             m.CreateOn,
                             m.UpdateBy,
                             m.UpdateOn
                         };
            List<UdcVO> lst = new List<UdcVO>();
            foreach (var item in result)
            {
                UdcVO udcVO = new UdcVO();
                udcVO.ID = item.ID;
                udcVO.Code = item.Code;
                udcVO.SysCode = item.SysCode;
                udcVO.Category = item.Category;
                udcVO.Value = item.Value;
                udcVO.Description = item.Description;
                udcVO.Status = item.Status;
                udcVO.CreateBy = item.CreateBy;
                udcVO.CreateOn = item.CreateOn;
                udcVO.UpdateBy = item.UpdateBy;
                udcVO.UpdateOn = item.UpdateOn;

                lst.Add(udcVO);
            }
            return lst;
        }

        public bool enableAndDisableByUdcCode(string pUdcCode, string pStatus, string pAccount)
        {
            if (string.IsNullOrEmpty(pUdcCode) ||
                 string.IsNullOrEmpty(pStatus))
            {
                throw new ArgumentNullException("E-0001", "参数为空");
            }

            var result = (from m in DBContext.TB_Udc
                          where m.Code.ToUpper().Equals(pUdcCode.ToUpper())
                          select m).FirstOrDefault();
            if (null == result)
            {
                throw new Exception("自定义Code不存在");
            }
            result.Status = pStatus;
            result.UpdateBy = pAccount;
            result.UpdateOn = DateTime.Now;

            DBContext.SubmitChanges();
            return true;
        }

        public bool saveUdc(UdcVO pUdcVO)
        {
            if (null == pUdcVO ||
                string.IsNullOrEmpty(pUdcVO.Code) ||
                string.IsNullOrEmpty(pUdcVO.SysCode) ||
                string.IsNullOrEmpty(pUdcVO.Category))
            {
                throw new ArgumentNullException("E-0001", "参数为空");
            }

            if (null == pUdcVO.ID || pUdcVO.ID.Equals(Guid.Empty))
            {
                //新增
                var result = (from m in DBContext.TB_Udc
                              where m.Code.ToUpper().Equals(pUdcVO.Code.ToUpper())
                              && m.SysCode.ToUpper().Equals(pUdcVO.SysCode.ToUpper())
                              && m.Category.ToUpper().Equals(pUdcVO.Category.ToUpper())
                              select m).FirstOrDefault();
                if (null != result)
                {
                    throw new Exception(string.Format(@"已存在Code为【{0}】的自定义信息", pUdcVO.Code));
                }
                TB_Udc tbUdc = new TB_Udc();
                tbUdc.ID = Guid.NewGuid();
                tbUdc.Code = pUdcVO.Code;
                tbUdc.Value = pUdcVO.Value;
                tbUdc.SysCode = pUdcVO.SysCode;
                tbUdc.Category = pUdcVO.Category;
                tbUdc.Description = pUdcVO.Description;
                tbUdc.Status = "1";
                tbUdc.CreateBy = pUdcVO.CreateBy;
                tbUdc.CreateOn = DateTime.Now;

                DBContext.TB_Udc.InsertOnSubmit(tbUdc);
                DBContext.SubmitChanges();
                return true;
            }
            else
            {
                //编辑
                var result = (from m in DBContext.TB_Udc
                              where !m.ID.Equals(pUdcVO.ID)
                              && m.Code.ToUpper().Equals(pUdcVO.Code.ToUpper())
                              && m.SysCode.ToUpper().Equals(pUdcVO.SysCode.ToUpper())
                              && m.Category.ToUpper().Equals(pUdcVO.Category.ToUpper())
                              select m).FirstOrDefault();
                if (null != result)
                {
                    throw new Exception(string.Format(@"已存在Code为【{0}】的自定义信息", pUdcVO.Code));
                }
                result = (from m in DBContext.TB_Udc
                          where m.ID.Equals(pUdcVO.ID)
                          select m).FirstOrDefault();
                //result.Code = pUdcVO.Code;
                result.Value = pUdcVO.Value;
                result.SysCode = pUdcVO.SysCode;
                result.Category = pUdcVO.Category;
                result.Description = pUdcVO.Description;
                result.UpdateBy = pUdcVO.UpdateBy;
                result.UpdateOn = DateTime.Now;

                DBContext.SubmitChanges();
                return true;
            }
        }

        public List<MenuVO> getMenu()
        {
            var result = from m in DBContext.TB_Menu
                         select new
                         {
                             m.ID,
                             m.ParentID,
                             m.Name,
                             m.Description,
                             m.CmdForm,
                             m.CmdExe,
                             m.Status,
                             m.CreateBy,
                             m.CreateOn,
                             m.UpdateBy,
                             m.UpdateOn
                         };
            List<MenuVO> lst = new List<MenuVO>();
            foreach (var item in result)
            {
                MenuVO menuVO = new MenuVO();
                menuVO.ID = item.ID;
                menuVO.ParentID = item.ParentID;
                menuVO.Name = item.Name;
                menuVO.Description = item.Description;
                menuVO.CmdForm = item.CmdForm;
                menuVO.CmdExe = item.CmdExe;
                menuVO.Status = item.Status;
                menuVO.CreateBy = item.CreateBy;
                menuVO.CreateOn = item.CreateOn;
                menuVO.UpdateBy = item.UpdateBy;
                menuVO.UpdateOn = item.UpdateOn;

                lst.Add(menuVO);
            }

            return lst;
        }

        public bool enableAndDisableByMenuId(Guid pMenuId, string pStatus, string pAccount)
        {
            if (pMenuId.Equals(Guid.Empty) ||
               string.IsNullOrEmpty(pStatus))
            {
                throw new ArgumentNullException("E-0001", "参数为空");
            }

            var result = (from m in DBContext.TB_Menu
                          where m.ID == pMenuId
                          select m).FirstOrDefault();
            if (null == result)
            {
                throw new Exception("菜单项不存在");
            }

            result.Status = pStatus;
            result.UpdateBy = pAccount;
            result.UpdateOn = DateTime.Now;

            DBContext.SubmitChanges();
            return true;
        }

        public bool saveMenu(MenuVO pMenuVO)
        {
            if (null == pMenuVO ||
                 string.IsNullOrEmpty(pMenuVO.Name))
            {
                throw new ArgumentNullException("E-0001", "参数为空");
            }

            if (null == pMenuVO.ID || pMenuVO.ID.Equals(Guid.Empty))
            {
                //新增
                var result = (from m in DBContext.TB_Department
                              where m.Name.Equals(pMenuVO.Name)
                              select m).FirstOrDefault();
                if (null != result)
                {
                    throw new Exception(string.Format(@"已存在名为【{0}】的菜单信息", pMenuVO.Name));
                }
                TB_Menu tbMenu = new TB_Menu();
                tbMenu.ID = Guid.NewGuid();
                tbMenu.ParentID = pMenuVO.ParentID;
                tbMenu.Name = pMenuVO.Name;
                tbMenu.Description = pMenuVO.Description;
                tbMenu.CmdForm = pMenuVO.CmdForm;
                tbMenu.CmdExe = pMenuVO.CmdExe;
                tbMenu.Status = "1";
                tbMenu.CreateBy = pMenuVO.CreateBy;
                tbMenu.CreateOn = DateTime.Now;

                DBContext.TB_Menu.InsertOnSubmit(tbMenu);
                DBContext.SubmitChanges();
                return true;
            }
            else
            {
                //编辑
                var result = (from m in DBContext.TB_Menu
                              where !m.ID.Equals(pMenuVO.ID)
                              && m.Name.Equals(pMenuVO.Name)
                              select m).FirstOrDefault();
                if (null != result)
                {
                    throw new Exception(string.Format(@"已存在名为【{0}】的菜单信息", pMenuVO.Name));
                }

                result = (from m in DBContext.TB_Menu
                          where m.ID.Equals(pMenuVO.ID)
                          select m).FirstOrDefault();

                result.Name = pMenuVO.Name;
                result.Description = pMenuVO.Description;
                result.CmdForm = pMenuVO.CmdForm;
                result.CmdExe = pMenuVO.CmdExe;
                result.UpdateBy = pMenuVO.UpdateBy;
                result.UpdateOn = DateTime.Now;

                DBContext.SubmitChanges();
                return true;
            }
        }


        public List<RoleRVO> getRoleRByRID(Guid pRID)
        {
            if(pRID.Equals(Guid.Empty))
            {
                throw new ArgumentNullException("E-0001", "参数为空");
            }
            var result = from m in DBContext.TB_Role_R
                         where m.RoleID.Equals(pRID)
                         //|| m.RID.Equals(pRID)
                         select new
                         {
                             m.ID,
                             m.RoleID,
                             m.RID,
                             m.RName,
                             m.Status
                         };

            List<RoleRVO> lst = new List<RoleRVO>();
            foreach (var item in result)
            {
                RoleRVO roleRVO = new RoleRVO();
                roleRVO.ID = item.ID;
                roleRVO.RoleID = item.RoleID;
                roleRVO.RID = item.RID;
                roleRVO.RName = item.RName;
                roleRVO.Status = item.Status;

                lst.Add(roleRVO);
            }

            return lst;
        }


        public void removeRID2Role(Guid pRoleID, Guid pRID)
        {
            var result = (from m in DBContext.TB_Role_R
                         where m.RoleID.Equals(pRoleID)
                         && m.RID.Equals(pRID)
                         select m).FirstOrDefault();
            if(null == result)
            {
                return;
            }
            DBContext.TB_Role_R.DeleteOnSubmit(result);
            DBContext.SubmitChanges();
        }

        public void saveRID2Role(Guid pRoleID, Guid pRID, string pRName)
        {
            var result = from m in DBContext.TB_Role_R
                         where m.RoleID.Equals(pRoleID)
                         && m.RID.Equals(pRID)
                         select m;
            if(null == result || result.Count() <= 0)
            {
                TB_Role_R tbRoleR = new TB_Role_R();
                tbRoleR.ID = Guid.NewGuid();
                tbRoleR.RoleID = pRoleID;
                tbRoleR.RID = pRID;
                tbRoleR.RName = pRName;
                tbRoleR.Status = "1";
                tbRoleR.CreateOn = DateTime.Now;

                DBContext.TB_Role_R.InsertOnSubmit(tbRoleR);
                DBContext.SubmitChanges();
            }
        }
    }
}
