/*
* ==============================================================================
*
* Filename: ProtocolType
* ClrVersion: 4.0.30319.42000
* Description:协议号
*
* Version: 1.0
* Created: 2019/6/5 11:32:09
* Compiler: Visual Studio 2017
*
* Author: lifu
* Copyright: lyq
*
* ==============================================================================
*/

namespace GIS.Authority.Contract
{
    /// <summary>
    /// 说明  所有协议号为8位，遇到需要进一位的时候，前一位进位即可，例如：10000009，下一条协议为10000010,最后的0不可省略
    /// 1000000 1字头开始的协议用于基础协议（登录、退出、心跳等）与北京铁路相关协议
    /// 1字头协议已经使用至10000076，请从10000077开始使用
    ///
    /// 20000026 2字头开始的协议用于海洋王版本相关协议
    /// 2字头协议已经使用至20000007，请从20000118开始使用
    ///
    /// 90000001 9字头协议用于协议增补：用于已经排序的协议，后续发现需要补充协议，但是顺序又无法改变的情况，用9字头做补充
    /// 9字头协议已经使用至90000025，请从90000026开始使用
    /// </summary>
    public enum ProtocolType
    {
        #region 通用相关

        /// <summary>
        /// 心跳包
        /// </summary>
        Heartbeat = 1000000,

        /// <summary>
        /// 通用回复协议-一般传送简单的文本消息
        /// </summary>
        NormalResponse = 1000001,

        /// <summary>
        /// 协议包，指包含多个协议的网络请求
        /// </summary>
        PackageProtocol = 90000022,

        /// <summary>
        /// 集群协议发送请求
        /// </summary>
        SwapMessageRequest = 90000023,

        /// <summary>
        /// 集群协议发送请求的回复
        /// </summary>
        SwapMessageResponse = 90000024,

        #endregion 通用相关

        #region 登录相关

        /// <summary>
        /// 登录协议
        /// </summary>
        RegisterRequest = 1000002,

        /// <summary>
        /// 注销登录
        /// </summary>
        UnRegisterRequest = 1000003,

        /// <summary>
        /// 服务器主动关闭连接的协议
        /// </summary>
        ServerClose = 1000004,

        /// <summary>
        /// 更新登陆信息--用于web端页面跳转
        /// </summary>
        LoginReLoad = 1000005,

        /// <summary>
        /// 登录返回协议
        /// </summary>
        RegisterResponse = 1000006,

        /// <summary>
        /// 修改个人信息协议
        /// </summary>
        ModifyPersonalRequest = 1000007,

        /// <summary>
        /// 修改个人信息返回协议
        /// </summary>
        ModifyPersonalResponse = 1000008,

        #endregion 登录相关

        #region 信息

        /// <summary>
        /// 转发信息请求
        /// </summary>
        TransferInfoRequest = 20000001,

        /// <summary>
        /// 转发信息回复
        /// </summary>
        TransferInfoResponse = 20000002,

        #endregion 信息
    }
}