using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using WMS.Lib.vo.basic;
using WMS.Lib.util;

namespace WMS.Lib.intf
{
    [ServiceContract]
    interface IBasic
    {
        /// <summary>
        /// 获取仓库列表
        /// </summary>
        /// <param name="pPredicate">过滤条件</param>
        /// <returns></returns>
        [OperationContract]
        List<WarehouseVO> getWarehouseList();

        /// <summary>
        /// 获取库位列表
        /// </summary>
        /// <param name="pPredicate">过滤条件</param>
        /// <returns></returns>
        [OperationContract]
        List<LocationVO> getLocationList();

        /// <summary>
        /// 获取客户列表
        /// </summary>
        /// <param name="pPredicate">过滤条件</param>
        /// <returns></returns>
        [OperationContract]
        List<CustomerVO> getCustomerList();

        /// <summary>
        /// 获取供应商列表
        /// </summary>
        /// <param name="pPredicate">过滤条件</param>
        /// <returns></returns>
        [OperationContract]
        List<SupplierVO> getSupplierList();

        /// <summary>
        /// 获取物料分类列表
        /// </summary>
        /// <param name="pPredicate">过滤条件</param>
        /// <returns></returns>
        [OperationContract]
        List<ItemCategoryVO> getItemCategoryList();

        /// <summary>
        /// 获取物料列表
        /// </summary>
        /// <param name="pPredicate">过滤条件</param>
        /// <returns></returns>
        [OperationContract]
        List<MaterialVO> getMaterialList();

        /// <summary>
        /// 保存仓储信息
        /// </summary>
        /// <param name="pWarehouseVO">仓储信息</param>
        /// <returns>true操作成功，false操作失败</returns>
        [OperationContract]
        bool saveWarehouse(WarehouseVO pWarehouseVO);

        /// <summary>
        /// 保存库位信息
        /// </summary>
        /// <param name="pLocationVO">库位信息</param>
        /// <returns>true操作成功，false操作失败</returns>
        [OperationContract]
        bool saveLocation(LocationVO pLocationVO);

        /// <summary>
        /// 保存客户信息
        /// </summary>
        /// <param name="pCustomerVO">客户信息</param>
        /// <returns>true操作成功，false操作失败</returns>
        [OperationContract]
        bool saveCustomer(CustomerVO pCustomerVO);

        /// <summary>
        /// 保存供应商信息
        /// </summary>
        /// <param name="pSupplierVO">供应商信息</param>
        /// <returns>true操作成功，false操作失败</returns>
        [OperationContract]
        bool saveSupplier(SupplierVO pSupplierVO);

        /// <summary>
        /// 保存物料分类信息
        /// </summary>
        /// <param name="pItemCategoryVO">物料分类信息</param>
        /// <returns>true操作成功，false操作失败</returns>
        [OperationContract]
        bool saveItemCategory(ItemCategoryVO pItemCategoryVO);

        /// <summary>
        /// 保存物料信息
        /// </summary>
        /// <param name="pMaterialVO">物料信息</param>
        /// <returns>true操作成功，false操作失败</returns>
        [OperationContract]
        bool saveMaterial(MaterialVO pMaterialVO);

        /// <summary>
        /// 启用或失效仓储
        /// </summary>
        /// <param name="pWarehouseID">仓储ID</param>
        /// <param name="pStatus">状态标识（1启用，0失效）</param>
        /// <param name="pAccount">操作者</param>
        /// <returns>true操作成功，false操作失败</returns>
        [OperationContract]
        bool enableAndDisableWarehouseByWareID(int pWarehouseID, int pStatus, string pAccount);

        /// <summary>
        /// 启用或失效库位
        /// </summary>
        /// <param name="pLocationID">库位ID</param>
        /// <param name="pStatus">状态标识（1启用，0失效）</param>
        /// <param name="pAccount">操作者</param>
        /// <returns>true操作成功，false操作失败</returns>
        [OperationContract]
        bool enableAndDisableLocationByLocationID(int pLocationID, int pStatus, string pAccount);

        /// <summary>
        /// 启用或失效客户
        /// </summary>
        /// <param name="pCustomerID">客户ID</param>
        /// <param name="pStatus">状态标识（1启用，0失效）</param>
        /// <param name="pAccount"></param>
        /// <returns>true操作成功，false操作失败</returns>
        [OperationContract]
        bool enableAndDisableCustomerByCustomerID(int pCustomerID, int pStatus, string pAccount);

        /// <summary>
        /// 启用或失效供应商
        /// </summary>
        /// <param name="pSupplierID">供应商ID</param>
        /// <param name="pStatus">状态标识（1启用，0失效）</param>
        /// <param name="pAccount">操作者</param>
        /// <returns>true操作成功，false操作失败</returns>
        [OperationContract]
        bool enableAndDisableSupplierBySupplierID(int pSupplierID, int pStatus, string pAccount);

        /// <summary>
        /// 启用或失效物料分类
        /// </summary>
        /// <param name="pItemCategoryID">物料分类ID</param>
        /// <param name="pStatus">状态标识（1启用，0失效）</param>
        /// <param name="pAccount">操作者</param>
        /// <returns>true操作成功，false操作失败</returns>
        [OperationContract]
        bool enableAndDisableItemCategoryByItemCategoryID(int pItemCategoryID, int pStatus, string pAccount);

        /// <summary>
        /// 启用或失效物料
        /// </summary>
        /// <param name="pMaterialID">物料ID</param>
        /// <param name="pStatus">状态标识（1启用，0失效）</param>
        /// <param name="pAccount">操作者</param>
        /// <returns>true操作成功，false操作失败</returns>
        [OperationContract]
        bool enableAndDisableMaterialByMaterialID(int pMaterialID, int pStatus, string pAccount);

        /// <summary>
        /// 获取仓库关联库位
        /// </summary>
        /// <param name="pWarehouseID">仓库ID</param>
        /// <returns></returns>
        [OperationContract]
        List<LocationVO> getLocationByWarehouseID(int pWarehouseID);

        /// <summary>
        /// 获取物料分类关联物料
        /// </summary>
        /// <param name="pCategoryID">分类ID</param>
        /// <returns></returns>
        [OperationContract]
        List<MaterialVO> getMaterialByCategoryID(int pCategoryID);

        /// <summary>
        /// 获取序列号配置信息
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<SerialNumberVO> getSerialNumberList();

        /// <summary>
        /// 保存序列号配置信息
        /// </summary>
        /// <param name="pSNVO">序列号配置信息</param>
        /// <returns>true操作成功，false操作失败</returns>
        [OperationContract]
        bool saveSerialNumber(SerialNumberVO pSNVO);

        /// <summary>
        /// 启用或失效序号规则
        /// </summary>
        /// <param name="pSerialID">序号规则ID</param>
        /// <param name="pStatus">状态标识（1启用，0失效）</param>
        /// <param name="pAccount">操作者</param>
        /// <returns>true操作成功，false操作失败</returns>
        [OperationContract]
        bool enableAndDisableSerialNumberBySerialID(int pSerialID, int pStatus, string pAccount);

        /// <summary>
        /// 获取序列号
        /// </summary>
        /// <param name="pType">序号规则类型名</param>
        /// <param name="pCompanyID">所属公司ID</param>
        /// <returns></returns>
        [OperationContract]
        string getSN(string pType ,int pCompanyID);
    }
}
