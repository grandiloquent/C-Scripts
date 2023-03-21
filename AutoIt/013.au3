;exe

HotKeySet("{F9}","start")

While 1
WEnd

;C:\Users\Administrator\AppData\Local\Adobe
Func start()
	;initialize()
	;Sleep(1000)
	Sleep(1000)
	DaKaiVBE()
	ChaRuMoKuai()
	NianTieDaiMa()
	
	;--------------------------
	MouseClick("left",714,454,1)
	Sleep(1000)
	;--------------------------

	;--------------------------
	MouseClick("left",102,126,1)
	Sleep(1000)
	;--------------------------

	;--------------------------
	MouseClick("left",807,134,1)
	Sleep(1000)
	;--------------------------

EndFunc
;切换窗口
Func QieHuanChuangKou()
	Send("!{F11}")
	Sleep(2000)
EndFunc
;粘贴代码
Func NianTieDaiMa()
	Send("^v")
	Sleep(2000)
	
	;--------------------------
	MouseClick("left",386,159,1)
	Sleep(1000)
	;--------------------------

	QieHuanChuangKou()
	MouseClickDrag("left",65,239,642,240)
	Sleep(1000)
	QieHuanChuangKou()
	
	;--------------------------
	MouseClick("left",444,410,1)
	Sleep(1000)
	;--------------------------

	QieHuanChuangKou()
	MouseClickDrag("left",64,261,641,411)
	Sleep(1000)
	QieHuanChuangKou()
	;--------------------------
	MouseClick("left",255,62,1)
	Sleep(1000)
	;--------------------------

	;--------------------------
	MouseClick("left",98,150,1)
	Sleep(1000)
	;--------------------------

	;--------------------------
	MouseClick("left",39,112,1)
	Sleep(1000)
	;--------------------------

EndFunc
;插入模块
Func ChaRuMoKuai()

	;--------------------------
	MouseClick("left",240,36,1)
	Sleep(1000)
	;--------------------------

	;--------------------------
	MouseClick("left",287,108,1)
	Sleep(1000)
	;--------------------------

EndFunc
;打开VBE
Func DaKaiVBE()
	;--------------------------
	MouseClick("left",492,50,1)
	Sleep(1000)
	;--------------------------

	;--------------------------
	MouseClick("left",556,106,1)
	Sleep(1000)
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