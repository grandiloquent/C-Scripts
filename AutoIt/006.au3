;photoshop
HotKeySet("{F2}","start")
 
While 1
WEnd

Func action1()

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
	LvJing()
	
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

;缩放工具
Func SuoFangGongJu()
	MouseClick("left",18,675,1)
	Sleep(2000)
EndFunc

Func AltClick($x,$y)
	send("{ALTDOWN}")
	Sleep(1000)
	MouseClick("left",$x,$y,1)
	Sleep(1000)
	send("{ALTUP}")
	Sleep(2000)
EndFunc

Func CtrlSend($key)
	Send("^"&$key)
	Sleep(2000)
EndFunc

;通过拷贝的图层
Func TongGuoKaoBeiDeTuCeng()
	Send("^j")
	Sleep(2000)
EndFunc

;滤镜
Func LvJing()
	;358,9	
	MouseClick("left",358,9,1)
	Sleep(2000)
EndFunc

start()