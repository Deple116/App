using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmoreControlLibrary.EquipmentDriver
{
    public interface CameraInterface
    {
        /// <summary>
        /// 初试化设备
        /// </summary>
        /// <returns></returns>
        int Initial();
        /// <summary>
        /// 关闭设备
        /// </summary>
        /// <returns></returns>
        int DeInitial();
        /// <summary>
        /// 设置为软触发模式
        /// </summary>
        /// <returns></returns>
        int SetSoftwareTriggerMode();
        /// <summary>
        /// 设置为外部触发模式
        /// </summary>
        /// <returns></returns>
        int SetExternalTriggerMode();
        /// <summary>
        /// 设置为自由采集模式
        /// </summary>
        /// <returns></returns>
        int SetFreeRunMode();
        /// <summary>
        /// 软触发一次
        /// </summary>
        /// <returns></returns>
        int SoftWareTriggerOnce();
        /// <summary>
        /// 停止采集
        /// </summary>
        /// <returns></returns>
        int StopGrab();
        /// <summary>
        /// 开始采集
        /// </summary>
        /// <returns></returns>
        int StartGrab();
        /// <summary>
        /// 设置曝光时间(us)
        /// </summary>
        /// <param name="_value"></param>
        /// <returns></returns>
        int SetExposure(float _value);
        /// <summary>
        /// 获取曝光时间(us)
        /// </summary>
        /// <param name="_value"></param>
        /// <returns></returns>
        int GetExposure(ref float _value);
        /// <summary>
        /// 设置白平衡
        /// </summary>
        /// <param name="_rvalue"></param>
        /// <param name="_gvalue"></param>
        /// <param name="_bvalue"></param>
        /// <returns></returns>
        int SetBlanceWhite(uint _rvalue, uint _gvalue, uint _bvalue);
        /// <summary>
        /// 设置触发延时(us)
        /// </summary>
        /// <param name="_value"></param>
        /// <returns></returns>
        int SetTriggerDlay(float _value);
        /// <summary>
        /// 设置增益
        /// </summary>
        /// <param name="_value"></param>
        /// <returns></returns>
        int setGain(float _value);
        /// <summary>
        /// 获取增益
        /// </summary>
        /// <param name="_value"></param>
        /// <returns></returns>
        int getGain(ref float _value);
    }
}
