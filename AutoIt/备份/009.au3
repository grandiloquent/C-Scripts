;photoshop

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
;放大
Func FangDa($x,$y,$times=5)
	;688,441
	MouseClick("left",$x,$y,$times)
	Sleep(2000)
EndFunc
HotKeySet("{F9}","start")

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
	initialize()
	
	FangDaTouFaSi()
	XuanZeWenLi()
	SheZhiXiuFuHuaBiGongJu()
	ZhiXingXiuFuCaoZuo()
	DuiBi()
	
	action1()
	action2();
	action3()
	action4()
	action5()
	action6()
	action7()
	action8()
	action9()
	action10()
EndFunc
;放大头发丝
Func FangDaTouFaSi()
	SuoFangGongJu()
	FangDa(573,153,7)
EndFunc
;选择纹理
Func XuanZeWenLi()
;--------------------------
	MouseMove(1070,207)
	Sleep(500)
	MouseDown("left")
	Sleep(500)
	MouseUp("left")
	Sleep(500)
	;--------------------------

	;--------------------------
	MouseMove(1224,352)
	Sleep(500)
	MouseDown("left")
	Sleep(500)
	MouseUp("left")
	Sleep(500)
	;--------------------------


	;--------------------------
	MouseMove(1146,150)
	Sleep(500)
	MouseDown("left")
	Sleep(500)
	MouseUp("left")
	Sleep(500)
	;--------------------------

	;--------------------------
	MouseMove(1146,167)
	Sleep(500)
	MouseDown("left")
	Sleep(500)
	MouseUp("left")
	Sleep(500)
	;--------------------------

	;--------------------------
	MouseMove(1242,265)
	Sleep(500)
	MouseDown("left")
	Sleep(500)
	MouseUp("left")
	Sleep(500)
	;--------------------------

EndFunc

;设置修复画笔工具
Func SheZhiXiuFuHuaBiGongJu()
	;--------------------------
	MouseMove(20,283)
	Sleep(500)
	MouseDown("left")
	Sleep(500)
	MouseUp("left")
	Sleep(500)
	;--------------------------

	;--------------------------
	MouseMove(145,42)
	Sleep(500)
	MouseDown("left")
	Sleep(500)
	MouseUp("left")
	Sleep(500)
	;--------------------------

	;--------------------------
	MouseMove(207,78)
	Sleep(500)
	MouseDown("left")
	Sleep(10)
	MouseUp("left")
	Sleep(500)
	Send("^a")
	Sleep(500)
	Send("10")
	Sleep(500)
	;--------------------------
	
	;--------------------------
	MouseMove(201,127)
	Sleep(500)
	MouseDown("left")
	Sleep(10)
	MouseUp("left")
	Sleep(500)
	Send("^a")
	Sleep(500)
	Send("30")
	Sleep(500)
	Send("{Enter}")
	;--------------------------

	

EndFunc
;执行修复操作
Func ZhiXingXiuFuCaoZuo()

	;--------------------------
	AltClick(530,377)
	Sleep(500)
	;--------------------------


	;--------------------------
	MouseMove(527,186)
	Sleep(500)
	MouseDown("left")
	Sleep(500)

	;--------------------------
	MouseMove(570,250)
	Sleep(500)
	MouseUp("left")
	Sleep(500)
	;--------------------------

	;--------------------------
	MouseMove(579,220)
	Sleep(500)
	MouseDown("left")
	Sleep(500)

	;--------------------------
	MouseMove(579,281)
	Sleep(500)
	;--------------------------

	;--------------------------
	MouseMove(584,338)
	Sleep(500)
	;--------------------------

	;--------------------------
	MouseMove(585,393)
	Sleep(500)
	;--------------------------

	;--------------------------
	MouseMove(581,441)
	Sleep(500)
	MouseUp("left")
	Sleep(500)
	;--------------------------
EndFunc
;对比
Func DuiBi()
	;--------------------------
	MouseMove(1198,351)
	Sleep(500)
	MouseDown("left")
	Sleep(500)
	MouseUp("left")
	Sleep(500)
	;--------------------------






	;--------------------------
	MouseMove(1145,151)
	Sleep(500)
	MouseDown("left")
	Sleep(500)
	MouseUp("left")
	Sleep(500)
	;--------------------------

	;--------------------------
	MouseMove(1145,488)
	Sleep(500)
	MouseDown("left")
	Sleep(500)
	MouseUp("left")
	Sleep(500)
	;--------------------------


	;--------------------------
	MouseMove(1048,208)
	Sleep(500)
	MouseDown("left")
	Sleep(500)
	MouseUp("left")
	Sleep(2000)
	;--------------------------

	;--------------------------
	MouseMove(1048,208)
	Sleep(500)
	MouseDown("left")
	Sleep(500)
	MouseUp("left")
	Sleep(2000)
	;--------------------------

	;--------------------------
	MouseMove(1048,208)
	Sleep(500)
	MouseDown("left")
	Sleep(500)
	MouseUp("left")
	Sleep(2000)
	;--------------------------

	;--------------------------
	MouseMove(1048,208)
	Sleep(500)
	MouseDown("left")
	Sleep(500)
	MouseUp("left")
	Sleep(2000)
	;--------------------------

EndFunc

Func initialize()
	Sleep(1000)
	MouseMove(1008,311)
	Sleep(2000)
	Send("^0")
	Sleep(2000)
EndFunc

start()