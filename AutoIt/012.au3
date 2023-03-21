;photoshop

;缩放工具
Func SuoFangGongJu()
	MouseClick("left",18,675,1)
	Sleep(1000)
EndFunc

Func AltClick($x,$y)
	send("{ALTDOWN}")
	Sleep(1000)
	MouseClick("left",$x,$y,1)
	Sleep(1000)
	send("{ALTUP}")
	Sleep(1000)
EndFunc

Func CtrlSend($key)
	Send("^"&$key)
	Sleep(1000)
EndFunc

;通过拷贝的图层
Func TongGuoKaoBeiDeTuCeng()
	Send("^j")
	Sleep(1000)
EndFunc
;滤镜
Func LvJing()
	;358,9
	MouseClick("left",358,9,1)
	Sleep(1000)
EndFunc
;图层
Func TuCeng()
	;194,9
	MouseClick("left",194,9,1)
	Sleep(1000)
EndFunc
;创建剪贴蒙版
Func ChuangJianJianTieMengBan()
	;345,400
	MouseClick("left",345,400,1)
	Sleep(1000)
EndFunc
;图像
Func TuXiang()
	;147,11
	MouseClick("left",147,9,1)
	Sleep(1000)
EndFunc
;模糊
Func MoHu()
	MouseMove(383,309)
	Sleep(1000)
EndFunc
;模糊平均
Func MoHuPingJun()
	;620,476
	MouseClick("left",620,476,1)
	Sleep(1000)
EndFunc
;杂色
Func ZaSe()
	MouseMove(383,454)
	Sleep(1000)
EndFunc
;中间值
Func ZhongJianZhi()
	;625,539
	MouseClick("left",625,539,1)
	Sleep(1000)
EndFunc
;滤镜再做一次
Func LvJingZaiZuoYiCi()
	;403,31
	MouseClick("left",383,31,1)
	Sleep(1000)
EndFunc
;切换图层可见状态
Func QieHuanTuCengKeJianZhuangTai()
	;736,350
	MouseClick("left",1046,573,1)
	Sleep(1000)
EndFunc
;应用图像
Func YingYongTuXiang()
	;220,328
	MouseClick("left",220,328,1)
	Sleep(1000)
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
	Sleep(1000)
EndFunc
;线性光
Func XianXingGuang()
	;1140,494
	MouseClick("left",1140,494,1)
	Sleep(1000)
EndFunc
;正常
Func ZhengChang()
	;1133,172
	MouseClick("left",1140,172,1)
	Sleep(1000)
EndFunc
;创建新图层
Func ChuangJianXinTuCeng()
	;1220,708
	MouseClick("left",1220,708,1)
	Sleep(1000)
EndFunc

;放大
Func FangDa($x,$y,$times=5)
	;688,441
	MouseClick("left",$x,$y,$times)
	Sleep(1000)
EndFunc
HotKeySet("{F9}","start")

While 1
WEnd

;C:\Users\Administrator\AppData\Local\Adobe
Func start()
	initialize()
	
	FuZhiTuCeng()
	XiuGaiTuCengMingCheng("颜色")
	FuZhiTuCeng()
	XiuGaiTuCengMingCheng("纹理")
	LJing()
	YingYong()
	SheZhi()
	XinJian()
	XiuFu()
	SuoFangGongJu()
 	FangDa(395,300,2)

	;--------------------------
	 MouseClick("left",21,698,1)
	 Sleep(1000)
	 ;--------------------------
	 HuaBi()
	 HuaBi()
	 HuaBi()
	 
	;--------------------------
	MouseClick("left",1048,321,1)
	Sleep(1000)
	;--------------------------

	;--------------------------
	MouseClick("left",1213,572,1)
	Sleep(1000)
	;--------------------------

	;--------------------------
	MouseClick("left",210,9,1)
	Sleep(1000)
	;--------------------------

	;--------------------------
	MouseClick("left",414,634,1)
	Sleep(1000)
	;--------------------------

	;--------------------------
	MouseClick("left",603,552,1)
	Sleep(1000)
	;--------------------------

	;--------------------------
	MouseClick("left",1049,237,1)
	Sleep(1000)
	;--------------------------

	;--------------------------
	MouseClick("left",1049,237,1)
	Sleep(1000)
	;--------------------------

	;--------------------------
	MouseClick("left",1049,237,1)
	Sleep(1000)
	;--------------------------
	;--------------------------
	MouseClick("left",1049,237,1)
	Sleep(1000)
	;--------------------------

	;--------------------------
	MouseClick("left",1049,237,1)
	Sleep(1000)
	;--------------------------

	;--------------------------
	MouseClick("left",1049,237,1)
	Sleep(1000)
	;--------------------------
EndFunc
;画笔
Func HuaBi()
	
	;--------------------------
	MouseMove(388,266)
	Sleep(100)
	MouseDown("left")
	Sleep(100)

	;--------------------------
	MouseMove(420,364)
	Sleep(100)
	;--------------------------

	;--------------------------
	MouseMove(459,441)
	Sleep(100)
	;--------------------------

	;--------------------------
	MouseMove(483,497)
	Sleep(100)
	;--------------------------

	;--------------------------
	MouseMove(515,574)
	Sleep(100)
	;--------------------------

	;--------------------------
	MouseMove(539,647)
	Sleep(100)
	;--------------------------

	;--------------------------
	MouseMove(553,669)
	Sleep(100)
	MouseUp("left")
	Sleep(1000)
	;--------------------------

	;--------------------------
	MouseMove(302,294)
	Sleep(100)
	MouseDown("left")
	Sleep(100)

	;--------------------------
	MouseMove(311,362)
	Sleep(100)
	;--------------------------

	;--------------------------
	MouseMove(325,421)
	Sleep(100)
	;--------------------------

	;--------------------------
	MouseMove(341,473)
	Sleep(100)
	;--------------------------

	;--------------------------
	MouseMove(364,562)
	Sleep(100)
	MouseUp("left")
	Sleep(1000)
	;--------------------------

	;--------------------------
	MouseMove(262,318)
	Sleep(100)
	MouseDown("left")
	Sleep(100)

	;--------------------------
	MouseMove(265,366)
	Sleep(100)
	;--------------------------

	;--------------------------
	MouseMove(275,403)
	Sleep(100)
	;--------------------------

	;--------------------------
	MouseMove(279,468)
	Sleep(100)
	;--------------------------

	;--------------------------
	MouseMove(292,529)
	Sleep(100)
	;--------------------------

	;--------------------------
	MouseMove(290,560)
	Sleep(100)
	MouseUp("left")
	Sleep(1000)
	;--------------------------

EndFunc
;修复
Func XiuFu()

	;--------------------------
	MouseClick("left",1049,319,1)
	Sleep(1000)
	;--------------------------

	;--------------------------
	MouseMove(28,701)
	Sleep(1000)
	MouseDown("left")
	Sleep(1000)
	MouseUp("left")
	Sleep(1000)
	;--------------------------

	;--------------------------
	MouseClick("left",202,481,1)
	Sleep(1000)
	;--------------------------

	
	;--------------------------
	MouseClick("left",246,40,1)
	Sleep(1000)
	;--------------------------

	;--------------------------
	MouseClick("left",482,41,1)
	Sleep(1000)
	Send("^a")
	Sleep(1000)
	Send("30")
	Sleep(1000)
	Send("{Enter}")
	;--------------------------

	;--------------------------
	MouseClick("left",769,41,1)
	Sleep(1000)
	Send("^a")
	Sleep(1000)
	Send("30")
	Sleep(1000)
	Send("{Enter}")
	;--------------------------

	;--------------------------
	MouseClick("left",1013,43,1)
	Sleep(1000)
	;--------------------------

	;--------------------------
	MouseClick("left",143,41,1)
	Sleep(1000)
	;--------------------------

	;--------------------------
	MouseClick("left",219,79,1)
	Sleep(1000)
	Send("^a")
	Sleep(1000)
	Send("150")
	Sleep(1000)
	Send("{Enter}")
	;--------------------------


EndFunc
;新建
Func XinJian()
	
	;--------------------------
	MouseClick("left",1187,400,1)
	Sleep(1000)
	;--------------------------

	;--------------------------
	MouseClick("left",1220,705,1)
	Sleep(1000)
	;--------------------------

	;--------------------------
	MouseClick("left",1191,400,2)
	Sleep(1000)
	Send("^a")
	Sleep(1000)
	Send("修复")
	Sleep(1000)
	Send("{Enter}")
	;--------------------------

EndFunc
;设置
Func SheZhi()
	;--------------------------
	MouseClick("left",1146,148,1)
	Sleep(1000)
	;--------------------------

	;--------------------------
	MouseClick("left",1155,489,1)
	Sleep(1000)
	;--------------------------
	FuZhiTuCeng()
	;--------------------------
	MouseClick("left",1146,148,1)
	Sleep(1000)
	;--------------------------
	
	;--------------------------
	MouseClick("left",1155,164,1)
	Sleep(1000)
	;--------------------------

	;--------------------------
	MouseClick("left",192,11,1)
	Sleep(1000)
	;--------------------------

	;--------------------------
	MouseClick("left",422,400,1)
	Sleep(1000)
	;--------------------------

EndFunc
;应用
Func YingYong()
 	;--------------------------
	MouseClick("left",1048,237,1)
	Sleep(1000)
	;--------------------------

	;--------------------------
	MouseClick("left",1186,236,1)
	Sleep(1000)
	;--------------------------

	;--------------------------
	MouseClick("left",166,10,1)
	Sleep(1000)
	;--------------------------

	;--------------------------
	MouseClick("left",228,328,1)
	Sleep(1000)
	;--------------------------

	;--------------------------
	MouseClick("left",754,194,1)
	Sleep(1000)
	;--------------------------

	;--------------------------
	MouseClick("left",745,261,1)
	Sleep(1000)
	;--------------------------

	;--------------------------
	MouseClick("left",610,278,1)
	Sleep(1000)
	;--------------------------

	;--------------------------
	MouseClick("left",599,629,1)
	Sleep(1000)
	;--------------------------

	;--------------------------
	MouseClick("left",697,308,1)
	Sleep(1000)
	Send("^a")
	Sleep(1000)
	Send("2")
	Sleep(1000)
	;--------------------------

	;--------------------------
	MouseClick("left",698,340,1)
	Sleep(1000)
	Send("^a")
	Sleep(1000)
	Send("128")
	Sleep(1000)
	Send("{Enter}")
	;--------------------------

EndFunc
;滤镜
Func LJing()

	;--------------------------
	MouseClick("left",1048,237,1)
	Sleep(1000)
	;--------------------------

	;--------------------------
	MouseClick("left",1189,318,1)
	Sleep(1000)
	;--------------------------

	;--------------------------
	MouseClick("left",358,9,1)
	Sleep(1000)
	;--------------------------

	;--------------------------
	MouseClick("left",548,451,1)
	Sleep(1000)
	;--------------------------

	;--------------------------
	MouseClick("left",659,539,1)
	Sleep(1000)
	;--------------------------
	;--------------------------
	MouseClick("left",557,391,1)
	Sleep(1000)
	Send("^a")
	Sleep(1000)
	Send("13")
	Sleep(1000)
	Send("{Enter}")
	;--------------------------
EndFunc
;复制图层
Func FuZhiTuCeng()

	;--------------------------
	MouseClick("left",191,9,1)
	Sleep(1000)
	;--------------------------

	;--------------------------
	MouseMove(389,41)
	Sleep(1000)
	;--------------------------

	;--------------------------
	MouseClick("left",623,240,1)
	Sleep(1000)
	;--------------------------
EndFunc
;修改图层名称
Func XiuGaiTuCengMingCheng($name)

	;--------------------------
	MouseClick("left",1199,237,2)
	Sleep(1000)
	Send("^a")
	Sleep(1000)
	Send($name)
	Sleep(1000)
	Send("{Enter}")
	;--------------------------
EndFunc
Func initialize()
	Sleep(1000)
	MouseMove(1008,311)
	Sleep(1000)
	Send("^0")
	Sleep(1000)
EndFunc

start()