using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Lib.vo.accept;
using System.ServiceModel;

namespace WMS.Lib.intf
{
    /// <summary>
    /// 验收管理业务接口
    /// </summary>
    [ServiceContract]
    interface IAcceptance
    {
        /// <summary>
        /// 获取接收信息列表
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<AcceptReceivingVO> getAcceptReceivingList();

        /// <summary>
        /// 保存接收信息
        /// </summary>
        /// <param name="pReceivingVO">接收信息</param>
        /// <returns>true操作成功，false操作失败</returns>
        [OperationContract]
        bool saveAcceptReceiving(AcceptReceivingVO pReceivingVO);

        /// <summary>
        /// 启用或失效接收信息
        /// </summary>
        /// <param name="pReceiveID">接收信息ID</param>
        /// <param name="pStatus">状态标识（1启用，0失效）</param>
        /// <param name="pAccount">操作者</param>
        /// <returns>true操作成功，false操作失败</returns>
        [OperationContract]
        bool enableAndDisableReceivingByReceiveID(int pReceiveID, int pStatus, string pAccount);

        /// <summary>
        /// 保存验收
        /// </summary>
        /// <param name="pCheckVO">验收信息</param>
        /// <returns>true操作成功，false操作失败</returns>
        [OperationContract]
        bool saveAcceptCheck(AcceptCheckVO pCheckVO);

        /// <summary>
        /// 保存拆分
        /// </summary>
        /// <param name="pListSplitVO">拆分信息</param>
        /// <returns>true操作成功，false操作失败</returns>
        [OperationContract]
        bool saveAcceptCheckSplit(List<AcceptCheckSplitVO> pListSplitVO);

        /// <summary>
        /// 根据待检编号获取已验收信息
        /// </summary>
        /// <param name="pReceiveNo">待检编号</param>
        /// <returns></returns>
        [OperationContract]
        List<AcceptCheckVO> getAcceptCheckByReceiveNo(string pReceiveNo);
    }
}
