using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace LRHelper
{
    /// <summary>
    /// 动作类别枚举
    /// </summary>
    public enum ActionType
    {
        Push,   // 移进
        Sumup,  // 归约
        Accept, // 接受
        Goto,   // 转移
        Error   // 出错
    }

    // LR动作
    public class LRAction
    {
        public int CurrentStatus { get; set; }           // 当前状态
        public char CurrentCharactor { get; set; }       // 当前符号
        public ActionType actionType { get; set; }       // 动作类别
        public int StatusOrProductionIndex { get; set; }        // 下一状态（移进、转移），或产生式序号（归约）

        public string StringInTable
        {
            get
            {
                switch (actionType)
                {
                    case ActionType.Accept:
                        return "acc";
                    case ActionType.Push:
                        return string.Format("S{0}", StatusOrProductionIndex);
                    case ActionType.Sumup:
                        return string.Format("r{0}", StatusOrProductionIndex);
                    case ActionType.Goto:
                        return StatusOrProductionIndex.ToString();
                }

                return "";
            }
        }

        public LRAction()
        {
        }

        public LRAction(int nCurrentStatus, char cCurrentCharactor, ActionType actionType, int nStatusOrProductionIndex)
        {
            this.CurrentStatus = nCurrentStatus;
            this.CurrentCharactor = cCurrentCharactor;
            this.actionType = actionType;
            this.StatusOrProductionIndex = nStatusOrProductionIndex;
        }

        public void Save(Stream stream)
        {
            Helper.BinaryIOHelper.WriteAInt(stream, CurrentStatus);
            Helper.BinaryIOHelper.WriteAASCIICharactor(stream, CurrentCharactor);
            Helper.BinaryIOHelper.WriteAInt(stream, (int)actionType);
            Helper.BinaryIOHelper.WriteAInt(stream, StatusOrProductionIndex);
        }

        public void Read(Stream stream)
        {
            CurrentStatus = Helper.BinaryIOHelper.ReadAInt(stream);
            CurrentCharactor = Helper.BinaryIOHelper.ReadAASCIICharactor(stream);
            actionType = (ActionType)Helper.BinaryIOHelper.ReadAInt(stream);
            StatusOrProductionIndex = Helper.BinaryIOHelper.ReadAInt(stream);
        }
    }
}
