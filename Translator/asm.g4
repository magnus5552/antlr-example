grammar asm;

prog: line* EOF ;

line: command ;

command : aInstruction | cInstruction | LABEL ;

cInstruction : dest '=' comp  ';'  jump
    | dest '=' comp
    | comp ';' jump;

aInstruction : A_INT
    | A_SYMBOL
    ;

dest: (A | D | M)
    | (A | D | M) (A | D | M) 
    | (A | D | M) (A | D | M) (A | D | M)
    ;

jump: 'JGT'
    | 'JEQ'
    | 'JGE'
    | 'JLT'
    | 'JNE'
    | 'JLE'
    | 'JMP'
    ;

comp: '0'
    | '1'
    | '-1'
    | D
    | A | M
    | '!'  D
    | '!'  A | '!'  M
    | '-'  D
    | '-'  A | '-'  M
    | D  '+1'
    | A  '+1' | M  '+1'
    | D  '-1'
    | A  '-1' | M  '-1'
    | D  '+'  A | D  '+'  M  
    | D  '-'  A | D  '-'  M
    | A  '-'  D | M  '-'  D
    | D  '&'  A | D  '&'  M
    | D  '|'  A | D  '|'  M
    ;

A: 'A' ;
D: 'D' ;
M: 'M' ;

fragment SYMBOL : [a-zA-Z][.$_0-9a-zA-Z]* ;
fragment INT : [0-9]+ ;

A_SYMBOL : '@' SYMBOL ;
A_INT : '@' INT ; 

SPACE : [﻿ \t]+ -> skip ;

LABEL : '(' SYMBOL ')' ;

NEWLINE : ('\r'? '\n' | '\r')+ -> skip ;

COMMENT : '//' ~[\r\n]* -> skip ;
