using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// DP 기법을 적용해서 
// 1번 사용된 WaitForSeconds 를 재활용하는 기법 (가비지 컬렉터 생성 억제)

internal static class YieldInstrucetionCache
{
    // readonly 멤버 선언할때 초기화 하고, 해당 멤버를 바꾸지못하게(쓰기금지)
    public static readonly WaitForEndOfFrame WaitForEndOfFrame = new WaitForEndOfFrame();
    public static readonly WaitForFixedUpdate WaitForFixedUpdate = new WaitForFixedUpdate();

    // 1초짜리 wait
    // 0.8초짜리 wait 
    // Dictionary => 2개월차에 배웠던 Map 자료구조 
    private static readonly Dictionary<float, WaitForSeconds> waitForSeconds = new Dictionary<float, WaitForSeconds>();


    public static WaitForSeconds WaitForSeconds(float second)
    {
        WaitForSeconds wfs; // 참조형변수... C++ 포인터 

        if(!waitForSeconds.TryGetValue(second, out wfs))
        {
            waitForSeconds.Add(second, wfs = new WaitForSeconds(second));
        }
        return wfs;
    }


}
