[BITS 16]
[ORG 0x7C00]

WSCREEN equ 320
HSCREEN equ 200

main:
    call setup
    call drawStars
    jmp $ 


setup:
    mov ah, 0x00 
    mov al, 0x13 
    int 0x10  

    push 0xA000    
    pop es

    xor al, al     
    xor bx, bx 
    ret


drawStars:
    mov cx, WSCREEN / 2  
    mov dx, HSCREEN / 2   

drawLoop:
    mov ax, cx      
    add ax, dx  
    xor ah, ah   

    xor bl, bl    
    int 0x10   

    inc cx 
    cmp cx, WSCREEN
    jl drawLoop   

    inc dx         
    cmp dx, HSCREEN 
    jl drawStars 

    ret

; MBR Ass
times 510 - ($ - $$) db 0
dw 0xAA55