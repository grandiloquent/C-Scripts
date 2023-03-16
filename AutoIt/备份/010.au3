;exe

HotKeySet("{F9}","start")

While 1
WEnd

;C:\Users\Administrator\AppData\Local\Adobe
Func start()
	;initialize()
	;Sleep(1000)
	DaKaiKaiFaZhe()
	LuZhiHong()
	JiSuan()
	YunXingHong()
EndFunc
;打开开发者
Func DaKaiKaiFaZhe()
	
	;--------------------------
	MouseMove(476,49)
	Sleep(500)
	MouseDown("right")
	Sleep(10)
	MouseUp("right")
	Sleep(500)
	 
	;--------------------------
	MouseMove(507,69)
	Sleep(500)
	MouseDown("left")
	Sleep(10)
	MouseUp("left")
	Sleep(500)
	;--------------------------

	;--------------------------
	MouseMove(749,394)
	Sleep(500)
	MouseDown("left")
	Sleep(500)
	MouseUp("left")
	Sleep(500)
	;--------------------------

	;--------------------------
	MouseMove(931,675)
	Sleep(500)
	MouseDown("left")
	Sleep(500)
	MouseUp("left")
	Sleep(500)
	;--------------------------

	
	
	;--------------------------
	MouseMove(493,50)
	Sleep(500)
	MouseDown("left")
	Sleep(10)
	MouseUp("left")
	Sleep(500)
	;--------------------------


	;录制宏
	MouseMove(158,79)
	Sleep(500)
	MouseDown("left")
	Sleep(10)
	MouseUp("left")
	Sleep(500)
	;--------------------------


EndFunc

;计算
Func JiSuan()
	;--------------------------
	MouseMove(138,492)
	Sleep(500)
	MouseDown("left")
	Sleep(10)
	MouseUp("left")
	Sleep(500)
	Send("^a")
	Sleep(500)
	Send("=SUM(")
	Sleep(500)
	MouseClickDrag("left",139,263,143,471)
	Sleep(500)
	Send("{Enter}")
	Sleep(500)
	;--------------------------

	;--------------------------
	MouseMove(142,492)
	Sleep(500)
	MouseDown("left")
	Sleep(10)
	MouseUp("left")
	Sleep(500)
	;--------------------------
	MouseClickDrag("left",173,501,317,500)
	Sleep(500)
EndFunc

;录制宏
Func LuZhiHong()
	
	;--------------------------
	MouseMove(513,234)
	Sleep(500)
	MouseDown("left")
	Sleep(10)
	MouseUp("left")
	Sleep(500)
	Send("^a")
	Sleep(500)
	Send("计算总销售额")
	Sleep(2000)
	Send("{Enter}")
	;--------------------------

EndFunc

;运行宏
Func YunXingHong()
	;录制宏
	MouseMove(158,79)
	Sleep(500)
	MouseDown("left")
	Sleep(10)
	MouseUp("left")
	Sleep(500)
	;--------------------------
	MouseClickDrag("left",143,491,283,491)
	Sleep(500)

	;--------------------------
	MouseMove(289,490)
	Sleep(500)
	MouseDown("right")
	Sleep(10)
	MouseUp("right")
	Sleep(1000)
	;--------------------------

	;--------------------------
	MouseMove(396,386)
	Sleep(500)
	MouseDown("left")
	Sleep(10)
	MouseUp("left")
	Sleep(500)
	;--------------------------
	;--------------------------
	MouseMove(104,91)
	Sleep(500)
	MouseDown("left")
	Sleep(10)
	MouseUp("left")
	Sleep(500)
	;--------------------------
	
	;--------------------------
	MouseMove(773,188)
	Sleep(500)
	MouseDown("left")
	Sleep(10)
	MouseUp("left")
	Sleep(500)
	;--------------------------

EndFunc

;查看宏
Func ChaKanHong()
	;录制宏
	MouseMove(158,79)
	Sleep(500)
	MouseDown("left")
	Sleep(10)
	MouseUp("left")
	Sleep(500)
	;--------------------------
	;--------------------------
	MouseMove(104,91)
	Sleep(500)
	MouseDown("left")
	Sleep(10)
	MouseUp("left")
	Sleep(500)
	;--------------------------

	;--------------------------
	MouseMove(778,269)
	Sleep(500)
	MouseDown("left")
	Sleep(10)
	MouseUp("left")
	Sleep(5000)
	;--------------------------

	;--------------------------
	MouseMove(1250,17)
	Sleep(500)
	MouseDown("left")
	Sleep(10)
	MouseUp("left")
	Sleep(500)
	;--------------------------
	
	;--------------------------
	MouseMove(777,188)
	Sleep(500)
	MouseDown("left")
	Sleep(10)
	MouseUp("left")
	Sleep(500)
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