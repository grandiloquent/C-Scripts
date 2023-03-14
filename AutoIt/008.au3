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
Func FangDa($x,$y)
	;688,441
	MouseClick("left",$x,$y,5)
	Sleep(2000)
EndFunc
HotKeySet("{F2}","start")

While 1
WEnd

Func action1()
	MouseMove(28,702)
	Sleep(1000)
	MouseDown("left")
	Sleep(1000)
	MouseClick("left",196,482,1)
	Sleep(2000)
	MouseUp("left")
	Sleep(2000)
EndFunc

Func action2()
	MouseMove(225,45)
	Sleep(500)
	MouseDown("left")
	Sleep(500)
	MouseUp("left")
	Sleep(500)

	MouseMove(266,84)
	Sleep(500)
	MouseDown("left")
	Sleep(500)
	MouseUp("left")
	Sleep(500)

EndFunc

Func action3()

	
	;--------------------------
	MouseMove(481,42)
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

	
	;--------------------------
	MouseMove(578,41)
	Sleep(500)
	MouseDown("left")
	Sleep(10)
	MouseUp("left")
	Sleep(500)
	Send("^a")
	Sleep(500)
	Send("50")
	Sleep(500)
	Send("{Enter}")
	;--------------------------

	
	;--------------------------
	MouseMove(669,41)
	Sleep(500)
	MouseDown("left")
	Sleep(10)
	MouseUp("left")
	Sleep(500)
	Send("^a")
	Sleep(500)
	Send("50")
	Sleep(500)
	Send("{Enter}")
	;--------------------------

	
	;--------------------------
	MouseMove(768,42)
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

	MouseMove(1015,43)
	Sleep(500)
	MouseDown("left")
	Sleep(500)
	MouseUp("left")
	Sleep(500)


	;--------------------------
	MouseMove(145,44)
	Sleep(500)
	MouseDown("left")
	Sleep(500)
	MouseUp("left")
	Sleep(500)
	;--------------------------

	;--------------------------
	MouseMove(219,79)
	Sleep(500)
	MouseDown("left")
	Sleep(10)
	MouseUp("left")
	Sleep(500)
	Send("^a")
	Sleep(500)
	Send("90")
	Sleep(500)
	Send("{Enter}")
	;--------------------------

	

EndFunc

Func action4()
	;--------------------------
	MouseMove(1070,206)
	Sleep(500)
	MouseDown("left")
	Sleep(500)
	MouseUp("left")
	Sleep(500)
	;--------------------------

	;--------------------------
	MouseMove(1211,441)
	Sleep(500)
	MouseDown("left")
	Sleep(500)
	MouseUp("left")
	Sleep(500)
	;--------------------------
	;~ 切换图层可见状态
	;--------------------------
	MouseMove(1048,353)
	Sleep(500)
	MouseDown("left")
	Sleep(500)
	MouseUp("left")
	Sleep(500)
	;--------------------------
EndFunc
Func action5()

	;--------------------------
	MouseMove(492,214)
	Sleep(100)
	MouseDown("left")
	Sleep(100)

	MouseMove(577,231)
	Sleep(100)

	MouseMove(671,206)
	Sleep(100)
	MouseUp("left")
	Sleep(100)
;--------------------------

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
	SuoFangGongJu()
	FangDa(589,264)


	action1()
	action2();
	action3()
	action4()
	YanShiShiYongHuaBiDeDiYiZhongFangShi();

	YanShiDiErZhongFangShi()
	DuiBiXiuFuQianHouDeZhuangTai();	

	action6()
	action7()
	action8()
	action9()
	action10()
EndFunc


;演示第二种方式
Func YanShiDiErZhongFangShi()
	;~ 切换图层可见状态
	;--------------------------
	MouseMove(1048,353)
	Sleep(500)
	MouseDown("left")
	Sleep(500)
	MouseUp("left")
	Sleep(500)
	;--------------------------
	;--------------------------
	MouseMove(246,41)
	Sleep(500)
	MouseDown("left")
	Sleep(500)
	MouseUp("left")
	Sleep(500)
	;--------------------------


	;--------------------------
	MouseMove(552,423)
	Sleep(500)
	MouseDown("left")
	Sleep(500)
	
	MouseMove(636,471)
	Sleep(500)
	
	MouseMove(703,436)
	Sleep(500)
	
	MouseMove(691,318)
	Sleep(500)

	MouseUp("left")
	Sleep(500)
	;--------------------------

EndFunc

;演示使用画笔的第一种方式
Func YanShiShiYongHuaBiDeDiYiZhongFangShi()
	AltClick(570,316)
	action5()
	action5()
	action5()
	action5()
	action5()
	DuiBiXiuFuQianHouDeZhuangTai()
EndFunc
Func initialize()
	Sleep(1000)
	MouseMove(1008,311)
	Sleep(2000)
	Send("^0")
	Sleep(2000)
EndFunc

start()

;对比修复前后的状态
Func DuiBiXiuFuQianHouDeZhuangTai()
	;~ 切换图层可见状态
	;--------------------------
	MouseMove(1048,353)
	Sleep(500)
	MouseDown("left")
	Sleep(500)
	MouseUp("left")
	Sleep(500)
	;--------------------------
	
	;--------------------------
	MouseMove(1047,210)
	Sleep(500)
	MouseDown("left")
	Sleep(500)
	MouseUp("left")
	Sleep(1000)
	;--------------------------

	;--------------------------
	MouseMove(1047,210)
	Sleep(500)
	MouseDown("left")
	Sleep(500)
	MouseUp("left")
	Sleep(1000)
	;--------------------------
	;--------------------------
	MouseMove(1047,210)
	Sleep(500)
	MouseDown("left")
	Sleep(500)
	MouseUp("left")
	Sleep(1000)
	;--------------------------

	;--------------------------
	MouseMove(1047,210)
	Sleep(500)
	MouseDown("left")
	Sleep(500)
	MouseUp("left")
	Sleep(1000)
	;--------------------------
EndFunc