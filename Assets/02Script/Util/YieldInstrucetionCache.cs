using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// DP ����� �����ؼ� 
// 1�� ���� WaitForSeconds �� ��Ȱ���ϴ� ��� (������ �÷��� ���� ����)

internal static class YieldInstrucetionCache
{
    // readonly ��� �����Ҷ� �ʱ�ȭ �ϰ�, �ش� ����� �ٲ������ϰ�(�������)
    public static readonly WaitForEndOfFrame WaitForEndOfFrame = new WaitForEndOfFrame();
    public static readonly WaitForFixedUpdate WaitForFixedUpdate = new WaitForFixedUpdate();

    // 1��¥�� wait
    // 0.8��¥�� wait 
    // Dictionary => 2�������� ����� Map �ڷᱸ�� 
    private static readonly Dictionary<float, WaitForSeconds> waitForSeconds = new Dictionary<float, WaitForSeconds>();


    public static WaitForSeconds WaitForSeconds(float second)
    {
        WaitForSeconds wfs; // ����������... C++ ������ 

        if(!waitForSeconds.TryGetValue(second, out wfs))
        {
            waitForSeconds.Add(second, wfs = new WaitForSeconds(second));
        }
        return wfs;
    }


}
