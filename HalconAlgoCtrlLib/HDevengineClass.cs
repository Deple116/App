using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HalconDotNet;

namespace HalconAlgoCtrlLib
{
    class HDevengineClass
    {
        private HDevEngine MyEngine = new HDevEngine();
        private HDevProcedureCall[] ProcCall;

        string ProgramPathString;
        public HDevengineClass()
        {

        }
        /////////////////// 初始化路径////////////////////
        public bool Init(string ProgramPath, string ProcedurePath, HTuple hProcedureName)
        {
            try
            {
                ProgramPathString = ProgramPath;

                MyEngine.SetProcedurePath(ProcedurePath);

                LoadProduce(hProcedureName);
                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }


        private bool LoadProduce(HTuple hProcedureName)
        {
            try
            {
                //加载halcon主程序
                HDevProgram Program = new HDevProgram(ProgramPathString);

                int inum = hProcedureName.Length;
                ProcCall = new HDevProcedureCall[inum];
                for (int i = 0; i < inum;i++ )
                {
                    HDevProcedure Proc = new HDevProcedure(Program, hProcedureName[i].S);
                    ProcCall[i] = new HDevProcedureCall(Proc);
                }
                
                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        public bool SetTup(HTuple IndexProceduce, string strName, HTuple InputTup)
        {
            try
            {
                ProcCall[IndexProceduce[0].I].SetInputCtrlParamTuple(strName, InputTup);
                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        public bool SetObj(HTuple IndexProceduce, string strName, HObject InputObj)
        {
            try
            {
                ProcCall[IndexProceduce[0].I].SetInputIconicParamObject(strName, InputObj);
                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        public bool Excute(HTuple IndexProceduce)
        {
            try
            {
                ProcCall[IndexProceduce[0].I].Execute();


                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        public HTuple  GetTup(HTuple IndexProceduce, string strName)
        {
            HTuple ResultTup="";
            try
            {
                ResultTup=ProcCall[IndexProceduce[0].I].GetOutputCtrlParamTuple(strName);
                return ResultTup;
            }
            catch (System.Exception ex)
            {
                return ResultTup;
            }
        }

        public HObject GetObj(HTuple IndexProceduce, string strName)
        {
            HObject ResultObj = null;
            try
            {
                ResultObj = ProcCall[IndexProceduce[0].I].GetOutputIconicParamObject(strName);
                return ResultObj;
            }
            catch (System.Exception ex)
            {
                return ResultObj;
            }
        }

        public HImage GetImg(HTuple IndexProceduce, string strName)
        {
            HImage ResultImg = null;
            try
            {
                ResultImg = ProcCall[IndexProceduce[0].I].GetOutputIconicParamImage(strName);
                return ResultImg;
            }
            catch (System.Exception ex)
            {
                return ResultImg;
            }
        }

        public bool Excute(HTuple IndexProceduce,HTuple InputTup,HObject InputObj,HTuple OutputTup,HObject OutputObj)
        {
            try
            {
                int index = IndexProceduce[0].I;
                int iInputTupNum=InputTup.Length;
                for (int i=0;i<iInputTupNum;i++)
                {
                    ProcCall[index].SetInputCtrlParamTuple("ImagePath", InputTup[0]);
                }
                

                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        
    }
}
