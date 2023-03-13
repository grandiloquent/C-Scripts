;photoshop
#include "Photoshop.au3"
HotKeySet("{F2}","start")
 
While 1
WEnd

Func action1()
	SuoFangGongJu()
	MouseClick("left",543,281,5)
	Sleep(2000)
	TongGuoKaoBeiDeTuCeng()
	MouseClick("left",1117,571,2)
	Sleep(2000)
	Send("颜色")
	Sleep(2000)
	Send("{Enter}")
	Sleep(2000)
	TongGuoKaoBeiDeTuCeng()
	MouseClick("left",1117,571,2)
	Sleep(2000)
	Send("纹理")
	Sleep(2000)
	Send("{Enter}")
	Sleep(2000)
	MouseClick("left",1198,610,1)
	Sleep(2000)
	QieHuanTuCengKeJianZhuangTai()
	LvJing()
	ZaSe()
	ZhongJianZhi()
	Send("9")
	Sleep(2000)
	Send("{Enter}")
	Sleep(2000)
	LvJing()
	LvJingZaiZuoYiCi()
EndFunc

Func action2()
	QieHuanTuCengKeJianZhuangTai()
	MouseClick("left",1171,574,1)
	Sleep(2000)
	TuXiang()
	YingYongTuXiang()
EndFunc

Func action3()

	MouseClick("left",754,193,1)
	Sleep(2000)
	MouseClick("left",732,261,1)
	Sleep(2000)
	MouseClick("left",610,279,1)
	Sleep(2000)
	MouseClick("left",571,632,1)
	Sleep(2000)
	MouseClick("left",707,309,1)
	Sleep(2000)
	QuanXuan()
	Send("2")
	Sleep(2000)
	;713,339
	MouseClick("left",713,339,1)
	Sleep(2000)
	QuanXuan()
	Send("128")
	Sleep(2000)
	Send("{Enter}")
	Sleep(2000)
EndFunc

Func action4()
	TuCengMoShi()
	XianXingGuang()
	
	TongGuoKaoBeiDeTuCeng()
	TuCengMoShi()
	ZhengChang()
	TuCeng()
	ChuangJianJianTieMengBan()
	MouseClick("left",1178,652,1)
	Sleep(2000)
	ChuangJianXinTuCeng()
	XiuGaiTuCengMingCheng(1111,656,"修复")

EndFunc
Func action5()


EndFunc
Func action6()


EndFunc
Func action7()


EndFunc
Func action8()


EndFunc
Func action9()


EndFunc
Func action10()


EndFunc
;C:\Users\Administrator\AppData\Local\Adobe
Func start()
	initialize()
	action1()
	action2()
	action3()
	action4()
	action5()
	action6()
	action7()
	action8()
	action9()
	action10()
EndFunc

Func initialize()
	Sleep(1000)
	MouseMove(1008,311)
	Sleep(2000)
	Send("^0")
	Sleep(2000)
EndFunc

start()