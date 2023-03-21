;单击填充柄
Func DanJiDiYiGeTianChongBing($x=0,$y=0)
	DanJiDanYuanGe()
	MouseMove(100+$x*72,248+$y*20)
	Sleep(10)
	MouseDown('left')
	Sleep(2000)
EndFunc
;单击单元格
Func DanJiDanYuanGe($x=0,$y=0,$z=False)
	If $z Then
		MouseClick("right",90+$x*72,238+$y*20,1)
	Else
		MouseClick("left",90+$x*72,238+$y*20,1)
	EndIf
	Sleep(1000)
EndFunc
;拖拽单元格
Func TuoZhuaiDanYuanGe($x,$y,$x1,$y1)
	MouseClickDrag("left",90+$x*72,238+$y*20,90+$x1*72,238+$y1*20)
	Sleep(1000)
EndFunc
;自动调整列宽
Func ZiDongTiaoZhengLieKuan()
	;~ 80,47
	MouseClick("left",80,47,1)
	Sleep(1000)
	;~ 1045,125
	MouseClick("left",1045,125,1)
	Sleep(1000)
	;~ 1115,277
	MouseClick("left",1115,277,1)
	Sleep(1000)
EndFunc
;选择整行
Func XuanZeZhengXing($y=0)
	MouseClick("left",14,238+$y*19,1)
	Sleep(1000)
EndFunc
;~ ------------------------------------------------------
Global $fillHandleX=100
Global $fillHandleY=248
HotKeySet("{F9}","start")
While 1
WEnd
;~ 删除 计算机\HKEY_CURRENT_USER\SOFTWARE\Microsoft\Office\16.0\Excel
Func start()
    ;~ DiYiBu()
	
    MouseClick("left",331,53,1)
    Sleep(1000)

    MouseClick("left",482,116,1)
    Sleep(1000)

    MouseClick("left",543,338,1)
    Sleep(1000)

    MouseClick("left",482,366,1)
    Sleep(1000)

    MouseClick("left",818,475,1)
    Sleep(1000)
	
    MouseClick("left",278,53,1)
    Sleep(1000)

    MouseClick("left",703,84,1)
    Sleep(1000)

    MouseClick("left",776,405,1)
    Sleep(1000)
	TuoZhuaiDanYuanGe(0,1,2,2)
    MouseClick("left",773,282,1)
    Sleep(1000)
	  ;~ 674,440
    MouseClick("left",674,440,1)
    Sleep(1000)
EndFunc
;第一步
Func DiYiBu()
	DanJiDanYuanGe(0,1)
	  ;~ 27,187
    MouseClick("left",27,187,1)
    Sleep(1000)
    Send("^a")
    Sleep(1000)
    Send("孙悟空")
    Sleep(1000)
    Send("{Enter}")
    Sleep(1000)

	MouseClick("left",279,53,1)
    Sleep(1000)
	
    MouseClick("left",632,131,1)
    Sleep(1000)

    MouseClick("left",597,192,1)
    Sleep(1000)

    MouseClick("left",613,433,1)
    Sleep(1000)
	  ;~ 900,151
    MouseClick("left",900,151,1)
    Sleep(1000)
	;~ 公式
    MouseClick("left",279,53,1)
    Sleep(1000)

    MouseClick("left",719,81,1)
    Sleep(1000)

    MouseClick("left",749,111,1)
    Sleep(1000)
	

    MouseClick("left",580,284,1)
    Sleep(1000)
    Send("^a")
    Sleep(1000)
    Send("孙悟空")
    Sleep(1000)
	  ;~ 677,443
    MouseClick("left",677,443,1)
    Sleep(1000)
EndFunc