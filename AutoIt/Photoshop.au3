
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
;图层
Func TuCeng()
	;194,9
	MouseClick("left",194,9,1)
	Sleep(2000)
EndFunc
;创建剪贴蒙版
Func ChuangJianJianTieMengBan()
	;345,400
	MouseClick("left",345,400,1)
	Sleep(2000)
EndFunc
;图像
Func TuXiang()
	;147,11
	MouseClick("left",147,9,1)
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
	MouseClick("left",1046,573,1)
	Sleep(2000)
EndFunc
;应用图像
Func YingYongTuXiang()
	;220,328
	MouseClick("left",220,328,1)
	Sleep(2000)
EndFunc
;全选
Func QuanXuan()
	Send("^a")
	Sleep(1000)
EndFunc
;图层模式
Func TuCengMoShi()
	;1145,508
	MouseClick("left",1145,508,1)
	Sleep(2000)
EndFunc
;线性光
Func XianXingGuang()
	;1140,494
	MouseClick("left",1140,494,1)
	Sleep(2000)
EndFunc
;正常
Func ZhengChang()
	;1133,172
	MouseClick("left",1140,172,1)
	Sleep(2000)
EndFunc
;创建新图层
Func ChuangJianXinTuCeng()
	;1220,708
	MouseClick("left",1220,708,1)
	Sleep(2000)
EndFunc
;修改图层名称
Func XiuGaiTuCengMingCheng($x,$y,$name)
	MouseClick("left",$x,$y,2)
	Sleep(2000)
	Send($name)
	Sleep(2000)
	Send("{Enter}")
	Sleep(2000)
EndFunc