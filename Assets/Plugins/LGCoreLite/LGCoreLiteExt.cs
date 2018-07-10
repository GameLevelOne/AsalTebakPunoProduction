using UnityEngine;
using System.Collections;
using System;

public class LGCoreLiteExt 
{
	private static bool s_Inited = false;
	private static LGCoreLite.ConcurrentQueue s_ActionQueue = new LGCoreLite.ConcurrentQueue();
	private static Action<Exception> s_UnhandledExceptionCallback = ex => Debug.LogException(ex); // default

	public static void Init()
	{
		if( !s_Inited ) 
		{
			GameObject msgProcessorGo = new GameObject();
			GameObject.DontDestroyOnLoad(msgProcessorGo);
			msgProcessorGo.hideFlags = HideFlags.HideInHierarchy | HideFlags.HideInInspector;
			msgProcessorGo.AddComponent<LGCoreLite.MsgProcessor>();

			s_Inited = true;		
		}
	}

	public static void Post(Action action)
	{
		s_ActionQueue.Enqueue( action );
	}

	public static void Update()
	{
		s_ActionQueue.ExecuteAll( s_UnhandledExceptionCallback );
	}
}
