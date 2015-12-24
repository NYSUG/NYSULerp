/******************
The MIT License (MIT)
Copyright (c) 2015 No, You Shut Up Inc.

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation 
files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, 
modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the 
Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE 
WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR 
COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, 
ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
********************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NYSULerp : MonoBehaviour {

	// Struct for our lerp values
	private class LerpTuple 
	{
		public float currentLerpTime;
		public float lerpTime;
	}

	// Storage for our lerps
	private static Dictionary<string, LerpTuple> _lerpValues = new Dictionary<string, LerpTuple> ();

#region Adding/Removing Lerp requestors

	public static string AddLerpRequestor (float lerpTime)
	{
		string guid = System.Guid.NewGuid ().ToString ();
		LerpTuple tuple = new LerpTuple () {
			currentLerpTime = 0f,
			lerpTime = lerpTime,
		};

		_lerpValues.Add (guid, tuple);
		return guid;
	}

	public static void RemoveLerpRequestor (string guid)
	{
		if (!_lerpValues.ContainsKey (guid)) {
			Debug.LogWarning (string.Format ("No key found for {0}", guid));
			return;
		}

		_lerpValues.Remove (guid);
	}

#endregion

#region Lerp Methods

	public static float LerpNoEasing (string guid)
	{
		if (!_lerpValues.ContainsKey (guid)) {
			Debug.LogWarning (string.Format ("No key found for {0}", guid));
			return 0f;
		}

		//increment timer once per call
		_lerpValues [guid].currentLerpTime += Time.deltaTime;
		if (_lerpValues [guid].currentLerpTime > _lerpValues [guid].lerpTime) {
			_lerpValues [guid].currentLerpTime = _lerpValues [guid].lerpTime;
		}

		return _lerpValues [guid].currentLerpTime / _lerpValues [guid].lerpTime;
	}

	// Use Sinerp to Ease Out
	public static float LerpEaseOut (string guid)
	{
		if (!_lerpValues.ContainsKey (guid)) {
			Debug.LogWarning (string.Format ("No key found for {0}", guid));
			return 0f;
		}

		//increment timer once per call
		_lerpValues [guid].currentLerpTime += Time.deltaTime;
		if (_lerpValues [guid].currentLerpTime > _lerpValues [guid].lerpTime) {
			_lerpValues [guid].currentLerpTime = _lerpValues [guid].lerpTime;
		}
		
		float t = _lerpValues [guid].currentLerpTime / _lerpValues [guid].lerpTime;
		return Mathf.Sin (t * Mathf.PI * 0.5f);
	}

	// Use Coserp to Ease Out
	public static float LerpEaseIn (string guid)
	{
		if (!_lerpValues.ContainsKey (guid)) {
			Debug.LogWarning (string.Format ("No key found for {0}", guid));
			return 0f;
		}
		
		//increment timer once per call
		_lerpValues [guid].currentLerpTime += Time.deltaTime;
		if (_lerpValues [guid].currentLerpTime > _lerpValues [guid].lerpTime) {
			_lerpValues [guid].currentLerpTime = _lerpValues [guid].lerpTime;
		}
		
		float t = _lerpValues [guid].currentLerpTime / _lerpValues [guid].lerpTime;
		return 1f - Mathf.Cos (t * Mathf.PI * 0.5f);
	}

	public static float LerpExponential (string guid)
	{
		if (!_lerpValues.ContainsKey (guid)) {
			Debug.LogWarning (string.Format ("No key found for {0}", guid));
			return 0f;
		}
		
		//increment timer once per call
		_lerpValues [guid].currentLerpTime += Time.deltaTime;
		if (_lerpValues [guid].currentLerpTime > _lerpValues [guid].lerpTime) {
			_lerpValues [guid].currentLerpTime = _lerpValues [guid].lerpTime;
		}
		
		float t = _lerpValues [guid].currentLerpTime / _lerpValues [guid].lerpTime;
		return t * t;
	}

	public static float LerpSmoothStep (string guid)
	{
		if (!_lerpValues.ContainsKey (guid)) {
			Debug.LogWarning (string.Format ("No key found for {0}", guid));
			return 0f;
		}
		
		//increment timer once per call
		_lerpValues [guid].currentLerpTime += Time.deltaTime;
		if (_lerpValues [guid].currentLerpTime > _lerpValues [guid].lerpTime) {
			_lerpValues [guid].currentLerpTime = _lerpValues [guid].lerpTime;
		}
		
		float t = _lerpValues [guid].currentLerpTime / _lerpValues [guid].lerpTime;
		return t * t * (3f - 2f * t);
	}

	public static float LerpSmootherStep (string guid)
	{
		if (!_lerpValues.ContainsKey (guid)) {
			Debug.LogWarning (string.Format ("No key found for {0}", guid));
			return 0f;
		}
		
		//increment timer once per call
		_lerpValues [guid].currentLerpTime += Time.deltaTime;
		if (_lerpValues [guid].currentLerpTime > _lerpValues [guid].lerpTime) {
			_lerpValues [guid].currentLerpTime = _lerpValues [guid].lerpTime;
		}
		
		float t = _lerpValues [guid].currentLerpTime / _lerpValues [guid].lerpTime;
		return t * t * t * (t * (6f * t - 15f) + 10f);
	}

#endregion

}
