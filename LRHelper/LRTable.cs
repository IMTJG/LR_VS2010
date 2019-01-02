using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace LRHelper
{
    public class LRTable
    {
        public List<LRAction> lstAction = new List<LRAction>();

        public LRTable()
        {
        }

        /// <summary>
        /// 加载LR分析表的动作序列
        /// </summary>
        /// <param name="strFilePath">LR分析表文件的路径</param>
        public void Read(string strFilePath)
        {
            // 打开文件，返回数据流数据类型
            Stream stream = File.Open(strFilePath, FileMode.Open, FileAccess.Read);

            // 读取数量
            int nCount = Helper.BinaryIOHelper.ReadAInt(stream);

            // 逐个读取对象
            LRAction action;
            for (int i = 0; i < nCount; i++)
            {
                action = new LRAction();
                action.Read(stream);
                lstAction.Add(action);
            }

            // 关闭数据流
            stream.Close();
        }

        public void Save(string strFilePath)
        {
            Stream stream = File.Open(strFilePath, FileMode.Create, FileAccess.Write);

            Helper.BinaryIOHelper.WriteAInt(stream, lstAction.Count);
            foreach (LRAction action in lstAction)
                action.Save(stream);

            stream.Close();
        }

        /// <summary>
        /// 根据当前状态和当前符号，查询LR分析表，确定动作
        /// </summary>
        /// <param name="nCurrentStatus">当前状态</param>
        /// <param name="cCurrentCharactor">当前符号</param>
        /// <returns>动作</returns>
        public LRAction GetAction(int nCurrentStatus, char cCurrentCharactor)
        {
            foreach (LRAction a in lstAction)
            {
                if (a.CurrentStatus == nCurrentStatus && a.CurrentCharactor == cCurrentCharactor)
                    return a;
            }
            // 如果表中没有查到，则生成一个表示错误的动作
            return new LRAction(nCurrentStatus, cCurrentCharactor, ActionType.Error, -1);
        }

        public void AddAction(int nCurrentStatus, char cCurrentCharactor, ActionType actionType, int nStatusOrProductionIndex)
        {
            LRAction action = GetAction(nCurrentStatus, cCurrentCharactor);

            if (action.actionType == ActionType.Error)
            {
                action = new LRAction(nCurrentStatus, cCurrentCharactor, actionType, nStatusOrProductionIndex);
                lstAction.Add(action);
            }
            else
            {
                // 移进优先于归约
                if (actionType == ActionType.Push)
                {
                    action.actionType = actionType;
                    action.StatusOrProductionIndex = nStatusOrProductionIndex;
                }
            }
        }
    }
}
