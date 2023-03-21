;单击第一个填充柄
Func DanJiDiYiGeTianChongBing()
	DanJiDiYiGeDanYuanGe()
	MouseMove(100,248)
	Sleep(10)
	MouseDown('left')
	Sleep(2000)
EndFunc
;单击第一个单元格
Func DanJiDiYiGeDanYuanGe($x=0,$y=0,$z=False)
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
	Send("^a")
	Sleep(1000)
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
;~ ------------------------------------------------------
Global $fillHandleX=100
Global $fillHandleY=248
HotKeySet("{F9}","start")
While 1
WEnd
;向右移动8格
Func XiangYouYiDong8Ge()
	Local $x=$fillHandleX
	For $i = 1 To 12 Step 1
		If $i==1 Then
			$x=$x+36
		Else
			$x=$x+72
		EndIf
		MouseMove($x,248)
		Sleep(50)
	Next
EndFunc
;查看宏
Func ChaKanHong($run=False)
	;~ 491,45
	MouseClick("left",491,45,1)
	Sleep(1000)
	;~ 108,116
	MouseClick("left",108,116,1)
	Sleep(2000)
	If $run Then
	;~ 801,198
	MouseClick("left",801,198,1)
	Sleep(1000)	
	EndIf
EndFunc
Func start()

	DaKaiWenDang()
	ShuRuDanGeDanYuanGe()
	XuLieTianChong()
	CongXiaLaCaiDanXuanZe()
	ZiDongBuQuan()
	TianChongDuoGeDanYuanGe()
	KuaiSuTianChong()
	ZiDongTiaoZhengLieKuan()
	;~ 370,248
	MouseClick("left",370,248,1)
	Sleep(1000)
EndFunc

;快速填充
Func KuaiSuTianChong()
	ChaKanHong(True)
	DanJiDiYiGeDanYuanGe(3,1)
	Send("女 凌沛岚 21")
	Sleep(500)
	Send("{Enter}")
	Sleep(1000)
	Send("男")
	Sleep(2000)
	Send("{Enter}")
EndFunc
;打开文档
Func DaKaiWenDang()
	;~ 329,243
	MouseMove(329,243)
	Sleep(2000)
	;~ 323,461
	MouseClick("left",323,461,1)
	Sleep(1000)
EndFunc
;填充多个单元格
Func TianChongDuoGeDanYuanGe()
	TuoZhuaiDanYuanGe(0,4,11,5)
	Send("kpkpkp.cn")
	Sleep(1000)
	Send("^{Enter}")
	Sleep(2000)
	DanJiDiYiGeDanYuanGe(0,6)
EndFunc
;自动补全
Func ZiDongBuQuan()
	DanJiDiYiGeDanYuanGe(0,3,False)
	Send("二")
	Sleep(2000)
	Send("{Enter}")
EndFunc
;从下拉菜单选择
Func CongXiaLaCaiDanXuanZe()
	DanJiDiYiGeDanYuanGe(0,2,True)
	;~ 240,612
	MouseClick("left",240,612,1)
	Sleep(1000)
	;~ 99,303
	MouseClick("left",99,303,1)
	Sleep(1000)
EndFunc
;输入单个单元格
Func ShuRuDanGeDanYuanGe()
	DanJiDiYiGeDanYuanGe()
	Send("一月")
	Sleep(1000)
	Send("{Enter}")
	Sleep(2000)
	Send("二月")
	Sleep(1000)
	Send("{Tab}")
	Sleep(1000)
EndFunc
;序列填充
Func XuLieTianChong()
	DanJiDiYiGeTianChongBing()
	XiangYouYiDong8Ge()
	MouseUp("left")
	Sleep(1000)
	MouseDown('left')
	Sleep(100)
	MouseUp("left")
	Sleep(1000)
EndFunc