HotKeySet("{F2}","start")

While 1
WEnd

Func action1()
	MouseClick("left",18,675,1)
	Sleep(2000)

	MouseClick("left",547,368,6)
	Sleep(2000)
	
	MouseClick("left",19,308,1)
	Sleep(2000)

	MouseClick("left",337,43,1)
	Sleep(2000)
	
	MouseClick("left",337,79,1)
	Sleep(2000)

	MouseClick("left",402,41,1)
	Sleep(2000)
	Send("^a")
	Sleep(2000)
	Send("1")
	Sleep(2000)
	Send("{Enter}")
	Sleep(2000)

	MouseMove(528,43)
	Sleep(2000)
	MouseMove(491,343)
	Sleep(500)

	MouseDown("left")	

	MouseMove(580,319)
	Sleep(500)

	MouseMove(597,390)
	Sleep(500)

	MouseMove(503,410)
	Sleep(500)

	MouseMove(469,365)
	Sleep(500)

	MouseMove(491,343)
	Sleep(500)

	MouseUp("left")
	Sleep(2000)

	MouseClickDrag("left",491,343,575,399)
	Sleep(2000)

	Send("^d")
EndFunc

;C:\Users\Administrator\AppData\Local\Adobe
Func start()
	Sleep(3000)
	MouseMove(1008,311)
	Sleep(2000)
	Send("^0")
	Sleep(2000)
	action1()
EndFunc