HotKeySet("{F2}","start")

While 1
WEnd

Func action1()
	MouseClick("left",18,675,1)
	Sleep(2000)
	MouseClick("left",499,225,7)
	Sleep(2000)
	MouseClick("left",21,389,1)
	Sleep(2000)
	MouseClick("left",144,44,1)
	Sleep(2000)
	
	MouseClick("left",192,79,1)
	Sleep(2000)
	
	Send("^a")
	Sleep(1000)
	
	Send("1")
	Sleep(1000)
	
	Send("{Enter}")
	Sleep(2000)
	MouseClick("left",838,44,1)
	Sleep(2000)
	MouseClick("left",831,77,1)
	Sleep(2000)

EndFunc

Func action2()
	send("{ALTDOWN}")
	Sleep(1000)
	MouseClick("left",341,217,1)
	Sleep(1000)
	send("{ALTUP}")
	Sleep(1000)
	MouseClickDrag("left",178,300,248,260)
	Sleep(1000)
	MouseClickDrag("left",186,311,241,282)
	Sleep(1000)

EndFunc

Func action3()
	Send("^0")
	Sleep(2000)
	MouseClick("left",18,675,1)
	Sleep(2000)
	MouseClick("left",511,218,6)
	Sleep(2000)
	MouseClick("left",21,389,1)
	Sleep(2000)
	MouseClick("left",429,41,1)
	Sleep(2000)
	Send("^a")
	Sleep(1000)
	Send("30")
	Sleep(1000)
	
	Send("{Enter}")
	Sleep(2000)
	MouseClick("left",144,44,1)
	Sleep(2000)

	MouseClick("left",192,79,1)
	Sleep(2000)

	Send("^a")
	Sleep(1000)

	Send("80")
	Sleep(1000)

	Send("{Enter}")
	Sleep(2000)
	send("{ALTDOWN}")
	Sleep(2000)
	MouseClick("left",447,126,1)
	Sleep(1000)
	send("{ALTUP}")
	Sleep(2000)

EndFunc

Func action4()
	MouseMove(274,472)
	Sleep(1000)
	MouseDown("left")
	Sleep(1000)
	MouseMove(383,547)
	Sleep(1000)
	MouseMove(538,497)
	Sleep(2000)
	MouseUp("left")
	MouseClick("left",1048,573,1)
	Sleep(2000)
	MouseClick("left",1048,573,1)
	Sleep(2000)

EndFunc

;C:\Users\Administrator\AppData\Local\Adobe
Func start()
	Sleep(1000)
	MouseMove(1008,311)
	Sleep(2000)
	Send("^0")
	Sleep(2000) 
	action1()
	action2();
	action3()
	action4()
EndFunc

start()