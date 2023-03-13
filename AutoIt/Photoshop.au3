
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
;模糊
Func MoHu()
	MouseMove(383,309)
	Sleep(2000)
EndFunc
;模糊平均
Func MoHuPingJun()
	;620,476
	MouseClick("left",620,476,1)
	Sleep(2000)
EndFunc
;杂色
Func ZaSe()
	MouseMove(383,454)
	Sleep(2000)
EndFunc
;中间值
Func ZhongJianZhi()
	;625,539
	MouseClick("left",625,539,1)
	Sleep(2000)
EndFunc
;滤镜再做一次
Func LvJingZaiZuoYiCi()
	;403,31
	MouseClick("left",383,31,1)
	Sleep(2000)
EndFunc
;切换图层可见状态
Func QieHuanTuCengKeJianZhuangTai()
	;736,350
	MouseClick("left",736,350,1)
	Sleep(2000)
EndFunc