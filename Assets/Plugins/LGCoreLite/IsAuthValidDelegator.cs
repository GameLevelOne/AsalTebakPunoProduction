using UnityEngine;
using System.Collections;
using System;
using LGCoreLite;
using System.Collections.Generic;

namespace LGCoreLite
{

public class IsAuthValidDelegator : IsAuthValidListener {

	private Action<bool, Error> m_Action;
	private static List<IsAuthValidDelegator> s_AuthValidDelegateObjList = new List<IsAuthValidDelegator>();

	public IsAuthValidDelegator(Action<bool, Error> action)
	{
		AddRef();
		m_Action = action;
	}

	public override void onIsAuthValidAsyncComplete(bool isValid, Error error)
	{	
		//construct new error to avoid use internal dangling pointer.
		Error newError = (error!=null)?new Error(error):null;
		LGCoreLiteExt.Post( () => 
		{
			if(m_Action != null)
				m_Action(isValid, newError);
		}
		);
		
		RemoveRef();
	}

	private void AddRef()
	{
		lock(s_AuthValidDelegateObjList)
		{
			s_AuthValidDelegateObjList.Add(this);
		}
	}

	private void RemoveRef()
	{
		lock(s_AuthValidDelegateObjList)
		{
			s_AuthValidDelegateObjList.Remove(this);
		}
	}
}
}
