using OpenCvSharp;
using SmartMore.ViMo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoControlLibrary.AlgoBaseFactory
{

   
    public interface IAlgo
    {
        #region property

        string Name { get; set; }
        string MoudleID { get; set; }
        #endregion


        #region interface
        EnumReturnVal Init(AlgoInitInput algoinput);

        EnumReturnVal Run<T>(AlgoRunInput RunInput, out ResponseList<T> rsps) where T : SmartMore.ViMo.Response;

        EnumReturnVal Free();

        #endregion
    }
}
