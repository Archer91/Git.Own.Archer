using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Lib.vo.sys;
using System.ServiceModel;

namespace WMS.Lib.intf
{
    [ServiceContract]
    interface ISysInfo
    {
        /// <summary>
        /// 获取公司列表（编号和名称）
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<SysCompanyEasyVO> getCompanyEasyList();

        /// <summary>
        /// 登录校验
        /// </summary>
        /// <param name="pAccount">用户编号</param>
        /// <param name="pPwd">密码</param>
        /// <returns></returns>
        [OperationContract]
        SysAccountEasyVO checkLogin(string pAccount, string pPwd);

        /// <summary>
        /// 根据用户编号获取关联角色列表
        /// </summary>
        ///<param name="pAccount">用户编号</param>
        /// <returns></returns>
        [OperationContract]
        List<SysRoleEasyVO> getRoleByAccount(string pAccount);

        /// <summary>
        /// 根据角色获取菜单列表
        /// </summary>
        /// <param name="pRoleId">角色ID</param>
        /// <returns></returns>
        [OperationContract]
        List<SysMenuVO> getMenuByRoleId(int pRoleId);

        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <param name="pPredicate">过滤条件</param>
        /// <returns></returns>
        [OperationContract]
        List<SysMenuVO> getMenuList();

        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="pPredicate">过滤条件</param>
        /// <returns></returns>
        [OperationContract]
        List<SysRoleVO> getRoleList();

        /// <summary>
        /// 获取公司列表
        /// </summary>
        /// <param name="pPredicate">过滤条件</param>
        /// <returns></returns>
        [OperationContract]
        List<SysCompanyVO> getCompanyList();

        /// <summary>
        /// 获取组织列表
        /// </summary>
        /// <param name="pPredicate">过滤条件</param>
        /// <returns></returns>
        [OperationContract]
        List<SysStructureVO> getStructureList();

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="pPredicate">过滤条件</param>
        /// <returns></returns>
        [OperationContract]
        List<SysAccountVO> getAccountList();

        /// <summary>
        /// 获取用户自定义列表
        /// </summary>
        /// <param name="pPredicate">过滤条件</param>
        /// <returns></returns>
        [OperationContract]
        List<SysUserDefinedVO> getUserDeinedList();

        /// <summary>
        /// 保存菜单
        /// </summary>
        /// <param name="pMenuVO">菜单信息</param>
        /// <returns>true操作成功，false操作失败</returns>
        [OperationContract]
        bool saveMenu(SysMenuVO pMenuVO);

        /// <summary>
        /// 保存角色
        /// </summary>
        /// <param name="pRoleVO">角色信息</param>
        /// <returns>true操作成功，false操作失败</returns>
        [OperationContract]
        bool saveRole(SysRoleVO pRoleVO);

        /// <summary>
        /// 保存公司
        /// </summary>
        /// <param name="pCompanyVO">公司信息</param>
        /// <returns>true操作成功，false操作失败</returns>
        [OperationContract]
        bool saveCompany(SysCompanyVO pCompanyVO);

        /// <summary>
        /// 保存组织
        /// </summary>
        /// <param name="pStructureVO">组织信息</param>
        /// <returns>true操作成功，false操作失败</returns>
        [OperationContract]
        bool saveStructure(SysStructureVO pStructureVO);

        /// <summary>
        /// 保存用户
        /// </summary>
        /// <param name="pAccountVO">用户信息</param>
        /// <returns>true操作成功，false操作失败</returns>
        [OperationContract]
        bool saveAccount(SysAccountVO pAccountVO);

        /// <summary>
        /// 保存用户自定义
        /// </summary>
        /// <param name="pUserDefinedVO">自定义信息</param>
        /// <returns>true操作成功，false操作失败</returns>
        [OperationContract]
        bool saveUserDefined(SysUserDefinedVO pUserDefinedVO);

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="pAccount">用户编号</param>
        /// <param name="pOldPwd">旧密码</param>
        /// <param name="pNewPwd">新密码</param>
        /// <returns>true操作成功，false操作失败</returns>
        [OperationContract]
        bool changePassword(string pAccount, string pOldPwd, string pNewPwd);

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="pAccount">用户编号</param>
        /// <param name="pPwd">密码（默认123456）</param>
        /// <returns>true操作成功，false操作失败</returns>
        [OperationContract]
        bool resetPassword(string pAccount, string pPwd = "123456");

        /// <summary>
        /// 启用或失效菜单
        /// </summary>
        /// <param name="pMenuId">菜单ID</param>
        /// <param name="pStatus">状态标识（1启用，0失效）</param>
        /// <param name="pAccount">操作人</param>
        /// <returns>true操作成功，false操作失败</returns>
        [OperationContract]
        bool enableAndDisableByMenuId(int pMenuId, int pStatus, string pAccount);

        /// <summary>
        /// 启用或失效公司
        /// </summary>
        /// <param name="pCompanyId">公司ID</param>
        /// <param name="pStatus">状态标识（1启用，0失效）</param>
        /// <param name="pAccount">操作人</param>
        /// <returns>true操作成功，false操作失败</returns>
        [OperationContract]
        bool enableAndDisableByCompanyId(int pCompanyId, int pStatus, string pAccount);

        /// <summary>
        /// 启用或失效组织
        /// </summary>
        /// <param name="pStructureId">组织ID</param>
        /// <param name="pStatus">状态标识（1启用，0失效）</param>
        /// <param name="pAccount">操作人</param>
        /// <returns>true操作成功，false操作失败</returns>
        [OperationContract]
        bool enableAndDisableByStructureId(int pStructureId, int pStatus, string pAccount);

        /// <summary>
        /// 启用或失效用户
        /// </summary>
        /// <param name="pAccountId">用户ID</param>
        /// <param name="pStatus">状态标识（1启用，0失效）</param>
        /// <param name="pAccount">操作人</param>
        /// <returns>true操作成功，false操作失败</returns>
        [OperationContract]
        bool enableAndDisableByAccountId(int pAccountId, int pStatus, string pAccount);

        /// <summary>
        /// 启用或失效角色
        /// </summary>
        /// <param name="pRoleId">角色ID</param>
        /// <param name="pStatus">状态标识（1启用，0失效）</param>
        /// <param name="pAccount">操作人</param>
        /// <returns>true操作成功，false操作失败</returns>
        [OperationContract]
        bool enableAndDisableByRoleId(int pRoleId, int pStatus, string pAccount);

        /// <summary>
        /// 启用或失效自定义信息
        /// </summary>
        /// <param name="pUdcId">自定义ID</param>
        /// <param name="pStatus">状态标识（1启用，0失效）</param>
        /// <param name="pAccount">操作人</param>
        /// <returns>true操作成功，false操作失败</returns>
        [OperationContract]
        bool enableAndDisableByUdcId(int pUdcId, int pStatus, string pAccount);


        [OperationContract]
        List<SysRoleAccountVO> getRoleAccountByAccountId(int pAccountId);

        [OperationContract]
        void removeAccountId2Role(int pRoleId, int pAccountId);

        [OperationContract]
        void saveAccountId2Role(int pRoleId, int pAccountId);

        [OperationContract]
        List<SysRoleStructureVO> getRoleStructureByStructureId(int pStructureId);

        [OperationContract]
        void removeStructureId2Role(int pRoleId, int pStructureId);

        [OperationContract]
        void saveStructureId2Role(int pRoleId, int pStructureId);

        [OperationContract]
        List<SysRoleMenuVO> getRoleMenuByRoleId(int pRoleId);

        [OperationContract]
        void removeMenuId2Role(int pRoleId, int pMenuId);

        [OperationContract]
        void saveMenuId2Role(int pRoleId, int pMenuId);
    }
}
