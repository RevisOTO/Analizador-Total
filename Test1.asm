.MODEL small
.STACK 100h
.DATA
    newline DB 0DH, 0AH, '$'
    msg1 DB '"Alumno-aprobado"$'
    msg2 DB '"Reprobaste-XD"$'
    cal DW 0
    T1 DW 0
.CODE
    MOV AX, @DATA
    MOV DS, AX
    ; Declaración de variables
    MOV cal,5
if:
    CMP cal, 7
   JB if_end
    MOV AH, 9
    MOV DX, OFFSET msg1
    INT 21H

    MOV AH, 9
    MOV DX, OFFSET newline
    INT 21H
    JMP else_end
if_end:
else:
    MOV AH, 9
    MOV DX, OFFSET msg2
    INT 21H

    MOV AH, 9
    MOV DX, OFFSET newline
    INT 21H
else_end:
    MOV AH, 4CH
    INT 21H
.EXIT
END
