using AlgoControlLibrary.AlgoBaseFactory;
using OpenCvSharp;
using SmartMore.ViMo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlgoControlLibrary.VimoAlgo
{

   

    public abstract class VimoManager
    {

        /// <summary>
        /// 算法初始化
        /// </summary>
        /// <param name="algoinput"></param>
        /// <returns></returns>
        public abstract EnumReturnVal Init(AlgoInitInput algoinput);

        /// <summary>
        /// 算法执行
        /// </summary>
        /// <param name="RunInput"></param>
        /// <param name="rsps"></param>
        /// <returns></returns>
        public abstract EnumReturnVal Run(AlgoRunInput RunInput, out AlgoRunOutput rsps);

        /// <summary>
        /// 析构算法内存资源
        /// </summary>
        /// <returns></returns>
        public abstract EnumReturnVal Free();
        

    }
}
