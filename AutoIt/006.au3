;photoshop
#include "Photoshop.au3"
HotKeySet("{F2}","start")
 
While 1
WEnd

Func action1()
	SuoFangGongJu()
	MouseClick("left",543,281,6)
	Sleep(2000)
	TongGuoKaoBeiDeTuCeng()
	TongGuoKaoBeiDeTuCeng()
	MouseClick("left",1198,610,1)
	Sleep(2000)

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


EndFunc

Func action3()


EndFunc

Func action4()


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
	Sleep(1000);

	QieHuanTuCengKeJianZhuangTai()

	;action1()
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