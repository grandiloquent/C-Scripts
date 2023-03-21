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

Func start()
	XuanZeDanYuanGe()
	BuLianXuXuanZeDanYuanGe()
	TuoZhuaiYiDong()
	XuanZeDanXing()
	XuanZeBuLianXuDeXing()
	TuoZhuaiXuanZeXing()
	XuanZeLie()
	FuZhiDanYuanGe()
	FuZhiDuoGeDanYuanGe()
	Send("{ESC}")
	Sleep(1000)
	;~ For $j = 0 To 1 Step +1
	;~ 	For $i = 0 To 3 Step +1
	;~ 		If Not($i=3 And $j=1) Then
	;~ 			ConsoleWrite(17+$i*30 &","&197+$j*30 & ","&$i  & ","&$j & @CRLF)
	;~ 		EndIf
			
	;~ 	Next
	;~ Next
	KuaiSuGeShi()
	DanJiDanYuanGe()
EndFunc
;快速格式
Func KuaiSuGeShi()
	DanJiDanYuanGe(0,1)
	TuoZhuaiDanYuanGe(0,1,2,8)
	  ;~ 286,422
    MouseClick("left",286,422,1)
    Sleep(1000)
	
	For $i = 0 To 4 Step +1
		MouseClick("left",131+67*$i,461,1)
    	Sleep(1000) 
		For $j = 0 To 5 Step +1
			If $i<3 Then
				MouseMove(114+66*$j,518)
				Sleep(1000)
			ElseIf $i<4 Then
				If $j<2 Then
					MouseMove(114+66*$j,518)
					Sleep(1000)
				Else
					
				EndIf
			Else
				If $j<3 Then
					MouseMove(114+66*$j,518)
					Sleep(1000)
				Else
					
				EndIf
				
			EndIf
			
		Next
	Next
EndFunc
;复制多个单元格
Func FuZhiDuoGeDanYuanGe()
	TuoZhuaiDanYuanGe(0,1,2,8)
	Send("^c")
	Sleep(1000)
		  ;~ 301,260
    MouseClick("left",301,260,1)
    Sleep(1000)
	  ;~ 30,128
    MouseClick("left",30,128,1)
    Sleep(1000)

	For $j = 0 To 1 Step +1
		For $i = 0 To 3 Step +1
			If Not($i=3 And $j=1) Then
				MouseMove(17+$i*30,197+$j*30)
				Sleep(1000)
			EndIf
		Next
	Next

    MouseClick("left",131,378,1)
	Sleep(1000)
    MouseClick("left",131,378,1)
    Sleep(2000)
    MouseClick("left",792,152,1)
    Sleep(1000)
EndFunc
;复制单元格
Func FuZhiDanYuanGe()
	DanJiDanYuanGe(0,1)
	  ;~ 81,45
    MouseClick("left",81,45,1)
    Sleep(1000)
  ;~ 64,102
    MouseClick("left",64,102,1)
    Sleep(1000)
	  ;~ 61,79
    MouseClick("left",61,79,1)
    Sleep(1000)
	DanJiDanYuanGe(3,1)
	  ;~ 30,90
    MouseClick("left",30,90,1)
    Sleep(1000)
	Send("^z")
    Sleep(1000)
	Send("{ESC}")
	Sleep(1000)
EndFunc
;选择列
Func XuanZeLie()
  ;~ 65,220
    MouseClick("left",65,220,1)
    Sleep(1000)
    Send("{CTRLDOWN}")
    Sleep(1000)
    MouseClick("left",139,220,1)
    Sleep(1000)
    MouseClick("left",516,224,1)
    Sleep(1000)
    MouseClick("left",300,223,1)
    Sleep(1000)
    Send("{CTRLUP}")
    Sleep(1000)
    MouseClick("left",65,220,1)
    Sleep(1000)
    MouseClickDrag("left",64,218,217,224)
    Sleep(1000)
EndFunc
;拖拽选择行
Func TuoZhuaiXuanZeXing()
	;~ 24,260
	MouseClickDrag("left",24,260,23,449)
	Sleep(1000)
EndFunc
;选择单行
Func XuanZeDanXing()
	XuanZeZhengXing()
EndFunc
;选择不连续的行
Func XuanZeBuLianXuDeXing()
	Send("{CTRLDOWN}")
	Sleep(1000)
	XuanZeZhengXing(2)
	XuanZeZhengXing(4)
	XuanZeZhengXing(7)
	XuanZeZhengXing(12)
	Send("{CTRLUP}")
	Sleep(1000)
EndFunc
;选择单元格
Func XuanZeDanYuanGe()
	DanJiDanYuanGe(0,1)
EndFunc
;不连续选择单元格
Func BuLianXuXuanZeDanYuanGe()
	
	DanJiDanYuanGe(1,1)
	Send("{CTRLDOWN}")
	Sleep(1000)
	DanJiDanYuanGe(1,3)
	DanJiDanYuanGe(1,5)
	DanJiDanYuanGe(1,7)
	DanJiDanYuanGe(1,9)
	DanJiDanYuanGe(1,12)
	Send("{CTRLUP}")
	Sleep(1000)
EndFunc
;拖拽移动
Func TuoZhuaiYiDong()
	TuoZhuaiDanYuanGe(0,1,2,6)
	;~ 263,324
	MouseMove(263,344)
	Sleep(1000)
	MouseClickDrag("left",263,344,623,344)
	Sleep(1000)
	ZiDongTiaoZhengLieKuan()
	Send("^z")
	Sleep(1000)
	Send("^z")
	Sleep(1000)
EndFunc