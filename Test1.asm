.MODEL small
.STACK 100h
.DATA
    newline DB 0DH, 0AH, '$'
    msg1 DB '"es-par"$'
    msg2 DB '"es-impar"$'
    con DW 0
    aux DW 0
    T1 DW 0
.CODE
    MOV AX, @DATA
    MOV DS, AX
    ; Declaración de variables
    MOV con,0
while:
    CMP con, 5
   JE while_end
    MOV AX,con
    MOV BX,2
    DIV BX
    MOV aux,DX
if:
    CMP aux, 0
   JNE if_end
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
    ADD con, 1
    JMP while
while_end:
    MOV AH, 4CH
    INT 21H
.EXIT
END
