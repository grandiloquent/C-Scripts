;exe

HotKeySet("{F9}","start")

While 1
WEnd

;C:\Users\Administrator\AppData\Local\Adobe
Func start()
	;initialize()
	Sleep(1000)
	;DiYiBu()
	;SheZhiAnQuan()
	;BaoCunHong()
	DiErBu()
	
	


EndFunc
;第二步
Func DiErBu()
	;--------------------------
	MouseClick("left",232,178,1)
	Sleep(1000)
	;--------------------------

	;--------------------------
	MouseClick("left",29,47,1)
	Sleep(1000)
	;--------------------------

	;--------------------------
	MouseClick("left",279,195,1)
	Sleep(1000)
	;--------------------------

	;--------------------------
	MouseClick("left",424,50,1)
	Sleep(1000)
	;--------------------------

	;--------------------------
	MouseClick("left",969,127,1)
	Sleep(1000)
	;--------------------------

	;--------------------------
	MouseClick("left",1009,157,1)
	Sleep(1000)
	;--------------------------

	;--------------------------
	MouseClick("left",778,192,1)
	Sleep(1000)
	;--------------------------
EndFunc
;第一步
Func DiYiBu()

	;--------------------------
	MouseClick("left",490,50,1)
	Sleep(1000)
	;--------------------------

	;--------------------------
	MouseClick("left",160,79,1)
	Sleep(1000)
	;--------------------------

	;--------------------------
	MouseClick("left",514,234,1)
	Sleep(1000)
	Send("^a")
	Sleep(1000)
	Send("设置单元格的值")
	Sleep(1000)
	Send("{Enter}")
	;--------------------------

	;--------------------------
	MouseMove(63,238)
	Sleep(1000)
	MouseDown("left")
	Sleep(10)
	MouseUp("left")
	Sleep(1000)
	Send("你好")
	Sleep(1000)
	Send("{Enter}")
	;--------------------------

	;--------------------------
	MouseClick("left",160,79,1)
	Sleep(1000)
	;--------------------------

	

EndFunc

;设置安全
Func SheZhiAnQuan()

	;--------------------------
	MouseClick("left",170,128,1)
	Sleep(1000)
	;--------------------------

	;--------------------------
	MouseClick("left",435,161,1)
	Sleep(1000)
	;--------------------------

	;--------------------------
	MouseClick("left",944,687,1)
	Sleep(1000)
	;--------------------------

EndFunc

;保存宏
Func BaoCunHong()
	;--------------------------
	MouseClick("left",1262,17,1)
	Sleep(1000)
	;--------------------------

	;--------------------------
	MouseClick("left",557,394,1)
	Sleep(1000)
	;--------------------------

	;--------------------------
	MouseMove(205,341)
	Sleep(500)
	MouseDown("left")
	Sleep(10)
	MouseUp("left")
	Sleep(500)
	Send("^a")
	Sleep(500)
	Send("带有宏的Excel文件")
	Sleep(500)
	Send("{Enter}")
	;--------------------------

		;--------------------------
	MouseClick("left",467,476,1)
	Sleep(1000)
	;--------------------------

	;--------------------------
	MouseClick("left",646,473,1)
	Sleep(1000)
	;--------------------------

	;--------------------------
	MouseClick("left",619,371,1)
	Sleep(1000)
	;--------------------------

	;--------------------------
	MouseClick("left",303,24,1)
	Sleep(1000)
	;--------------------------

	;--------------------------
	MouseClick("left",466,476,1)
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