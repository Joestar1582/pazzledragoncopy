/****************************************************************************
*
* CRI Middleware SDK
*
* Copyright (c) 2011-2012 CRI Middleware Co.,Ltd.
*
* Library  : CRI Atom
* Module   : CRI Atom for Unity
* File     : CriAtomProjInfo_Unity.cs
* Tool Ver.          : CRI Atom Craft LE Ver.1.30.00
* Date Time          : 2014/01/11 15:09
* Project Name       : HMF_Origami
* Project Comment    : 
*
****************************************************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class CriAtomAcfInfo
{
    static partial void GetCueInfoInternal()
    {
        acfInfo = new AcfInfo("ACF", 0, "", "HMF_Origami.acf","b5599db6-2038-4da3-89ea-935831ba9916","DspBusSetting_0");
        acfInfo.aisacControlNameList.Add("Any");
        acfInfo.aisacControlNameList.Add("Distance");
        acfInfo.aisacControlNameList.Add("AisacControl02");
        acfInfo.aisacControlNameList.Add("AisacControl03");
        acfInfo.aisacControlNameList.Add("AisacControl04");
        acfInfo.aisacControlNameList.Add("AisacControl05");
        acfInfo.aisacControlNameList.Add("AisacControl06");
        acfInfo.aisacControlNameList.Add("AisacControl07");
        acfInfo.aisacControlNameList.Add("AisacControl08");
        acfInfo.aisacControlNameList.Add("AisacControl09");
        acfInfo.aisacControlNameList.Add("AisacControl10");
        acfInfo.aisacControlNameList.Add("AisacControl11");
        acfInfo.aisacControlNameList.Add("AisacControl12");
        acfInfo.aisacControlNameList.Add("AisacControl13");
        acfInfo.aisacControlNameList.Add("AisacControl14");
        acfInfo.aisacControlNameList.Add("AisacControl15");
        acfInfo.acbInfoList.Clear();
        AcbInfo newAcbInfo = null;
        newAcbInfo = new AcbInfo("BGM", 0, "", "BGM.acb", "BGM_streamfiles.awb","d3c7b73c-14d6-4ac3-8375-08d2b9a8e52f");
        acfInfo.acbInfoList.Add(newAcbInfo);
        newAcbInfo.cueInfoList.Add(2, new CueInfo("Result", 2, ""));
        newAcbInfo.cueInfoList.Add(0, new CueInfo("01 Prologue", 0, ""));
        newAcbInfo.cueInfoList.Add(1, new CueInfo("InGame", 1, ""));
        newAcbInfo.cueInfoList.Add(3, new CueInfo("Tutorial", 3, ""));
        newAcbInfo.cueInfoList.Add(4, new CueInfo("InGame_take1", 4, ""));
        newAcbInfo = new AcbInfo("SE", 1, "", "SE.acb", "SE_streamfiles.awb","3f8813b9-1d67-45c6-80cd-d3b2bd40bbed");
        acfInfo.acbInfoList.Add(newAcbInfo);
        newAcbInfo.cueInfoList.Add(3, new CueInfo("ContactEffect", 3, ""));
        newAcbInfo.cueInfoList.Add(4, new CueInfo("FlashEffect", 4, ""));
        newAcbInfo.cueInfoList.Add(6, new CueInfo("GaugeMax", 6, ""));
        newAcbInfo.cueInfoList.Add(7, new CueInfo("WaveEffect", 7, ""));
        newAcbInfo.cueInfoList.Add(8, new CueInfo("ButtonTouch", 8, ""));
        newAcbInfo.cueInfoList.Add(2, new CueInfo("Bad", 2, ""));
        newAcbInfo.cueInfoList.Add(1, new CueInfo("GoodSafe", 1, ""));
        newAcbInfo.cueInfoList.Add(5, new CueInfo("GaugeAppear", 5, ""));
        newAcbInfo.cueInfoList.Add(0, new CueInfo("Excellent", 0, ""));
        newAcbInfo.cueInfoList.Add(9, new CueInfo("S13", 9, ""));
    }
}
