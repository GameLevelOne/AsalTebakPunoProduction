using UnityEngine;
using System.Collections;
using System;

namespace LGCoreLite
{

public class ConcurrentQueue 
{
	const int m_InitialSize = 10;
	
	object m_Gate = new object();
	bool m_Dequing = false;
	
	int m_ActionListCount = 0;
	Action[] m_ActionList = new Action[m_InitialSize];
	
	int m_WaitingListCount = 0;
	Action[] m_WaitingList = new Action[m_InitialSize];
	
	public void Enqueue(Action action)
	{
		lock (m_Gate)
		{
			if (m_Dequing)
			{
				if (m_WaitingList.Length == m_WaitingListCount)
				{
					var newArray = new Action[checked(m_WaitingListCount * 2)];
					Array.Copy(m_WaitingList, newArray, m_WaitingListCount);
					m_WaitingList = newArray;
				}
				m_WaitingList[m_WaitingListCount++] = action;
			}
			else
			{
				if (m_ActionList.Length == m_ActionListCount)
				{
					var newArray = new Action[checked(m_ActionListCount * 2)];
					Array.Copy(m_ActionList, newArray, m_ActionListCount);
					m_ActionList = newArray;
				}
				m_ActionList[m_ActionListCount++] = action;
			}
		}
	}
	
	public void ExecuteAll(Action<Exception> unhandledExceptionCallback)
	{
		lock (m_Gate)
		{
			if (m_ActionListCount == 0) return;
			
			m_Dequing = true;
		}
		
		for (int i = 0; i < m_ActionListCount; i++)
		{
			var action = m_ActionList[i];
			
			try
			{
				action();
			}
			catch (Exception ex)
			{
				unhandledExceptionCallback(ex);
			}
		}
		
		lock (m_Gate)
		{
			m_Dequing = false;
			Array.Clear(m_ActionList, 0, m_ActionListCount);
			
			var swapTempActionList = m_ActionList;
			
			m_ActionListCount = m_WaitingListCount;
			m_ActionList = m_WaitingList;
			
			m_WaitingListCount = 0;
			m_WaitingList = swapTempActionList;
		}
	}
}

}
