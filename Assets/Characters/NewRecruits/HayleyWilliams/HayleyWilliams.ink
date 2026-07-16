//---External Functions---//
EXTERNAL trigger_dialogue_event(eventName, eventParameter)

//---Variables---//
VAR recruitName = "Hayley"
VAR playerName = "Andy"

Hello, this is {recruitName}, who's this?
+ [Hi {recruitName}, it's {playerName}]
    ... Who?
    ++ [{playerName}, the band manager. Wanna join my band?]
        Yes, I actually wanna join like a hundred bands before i die
        +++ [Awesome, you're hired!]
            ~ trigger_dialogue_event("recruit", "after_dialogue")
            Cool!
            ->DONE

+ [Oops sorry, wrong number]
    What the hell...
    ->DONE