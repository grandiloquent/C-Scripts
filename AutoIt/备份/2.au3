; Create a hot key. I'm using the "Pause/Break" key typically located near the number pad on the keyboard. The keycode for this key is simply "{PAUSE}"
HotKeySet("{F1}", "togglePause")

; Create a Boolean (True of False variable) to tell your program whether or not your program is paused or unpaused. Set it equal to 'False' so the script is running by default.
Global $isPaused = False
While 1
WEnd
; Create the togglePause function to stop/start the script
Func togglePause()
    ; When this function is initiated, the code on the next line 'toggles' the variable to True/False. If the script is unpaused (the $isPaused variable is set to 'False') then the next line will change it to 'True' and vice versa.
    $isPaused = Not $isPaused
    ; Create a while loop to stall the program
    ; The line below is the same thing as "While $isPaused == True"
    While $isPaused
        ; This code will run constantly until the $isPaused variable is set to 'True'. To make the script do nothing, simply add a sleep command.
        Send("2")
	Sleep(100)
	Send("3")
	Sleep(100)
    WEnd
EndFunc