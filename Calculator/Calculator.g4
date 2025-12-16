grammar Calculator ;

start : expression ;

expression : LEFT_BRACKET expression RIGHT_BRACKET #Parenthesis
    | INT                                   #Number
    | MINUS expression                      #UnaryMinus 
    | left=expression OP_0 right=expression #Pow 
    | left=expression OP_1 right=expression #MultOrDiv
    | left=expression OP_2 right=expression #AddOrSub
    ;

INT: [0-9]+ ;

MINUS: '-' ;
OP_0: '^' ;
OP_1: '*' | '/' ;
OP_2: '+' | MINUS ; 

LEFT_BRACKET: '(' ;
RIGHT_BRACKET: ')' ;