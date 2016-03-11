using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SSO.Lib.VO.Basic;

namespace SSO.Lib.Intf.Basic
{
    interface IBasic
    {
        List<RoleRVO> getRoleByAccount(Guid pAccountID,Guid? pDeptID);

        List<MenuVO> getMenuByRole(Guid pRoleID);

        List<FactoryVO> getFactory();

        bool enableAndDisableByFactoryCode(string pFactoryCode,string pStatus,string pAccount);

        bool saveFactory(FactoryVO pFactoryVO);

        List<DepartmentVO> getDepartment();

        bool enableAndDisableByDepartmentCode(string pDeptCode, string pStatus, string pAccount);
       
        bool saveDepartment(DepartmentVO pDeptVO);

         List<AccountVO> getAccount();

         bool enableAndDisableByAccountCode(string pAccountCode, string pStatus, string pAccount);

         bool saveAccount(AccountVO pAccountVO);

         List<RoleVO> getRole();

         bool enableAndDisableByRoleCode(string pRoleCode, string pStatus, string pAccount);

         bool saveRole(RoleVO pRoleVO);

         List<UdcVO> getUdc();

         bool enableAndDisableByUdcCode(string pUdcCode, string pStatus, string pAccount);

         bool saveUdc(UdcVO pUdcVO);

         List<MenuVO> getMenu();

         bool enableAndDisableByMenuId(Guid pMenuId, string pStatus, string pAccount);

         bool saveMenu(MenuVO pMenuVO);

         List<RoleRVO> getRoleRByRID(Guid pRID);

         void removeRID2Role(Guid pRoleID, Guid pRID);

        void  saveRID2Role(Guid pRoleID, Guid pRID,string pRName);
    }
}
