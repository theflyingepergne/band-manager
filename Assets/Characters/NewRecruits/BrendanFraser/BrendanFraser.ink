//---External Functions---//
EXTERNAL trigger_dialogue_event(eventName, eventParameter)

//---Variables---//
VAR recruitName = "Brendan"
VAR playerName = "Andy"

Ahoy-hoy, {recruitName} speaking. May I ask who is calling?
+ [Hi {recruitName}, it's {playerName}]
    {playerName}... Why does that name ring a bell...
    Oh that's it, you're that up-and-coming band manager I've been hearing so much about!
    What can I do for you {playerName}?
    ++ [Well {recruitName}, I was just calling to see if you were interested in joining a band?]
        Hm... With you?
        +++ [Yes]
            Hm...
            Sounds great! Count me in.
            ++++ [Sweet! This is gunna be great]
                ~ trigger_dialogue_event("recruit", "after_dialogue")
                Totally!
                ->DONE

+ [Oops sorry, wrong number]
    No problemo sweet potato
    ->DONE