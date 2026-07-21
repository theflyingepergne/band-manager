//---External Functions---//
EXTERNAL trigger_dialogue_event(eventName, eventParameter)

//---Variables---//
VAR recruitName = "Jack"
VAR playerName = "Andy"

Hello, {recruitName} speaking. Who's this?
+ [Hi {recruitName}, it's {playerName}. Wanna join my band?]
    ~ trigger_dialogue_event("recruit", "after_dialogue")
    Hell yeah!
    ->DONE

+ [Oops sorry, wrong number]
    Don't sweat it
    ->DONE