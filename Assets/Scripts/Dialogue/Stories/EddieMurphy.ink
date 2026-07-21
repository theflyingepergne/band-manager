//---External Functions---//
EXTERNAL trigger_dialogue_event(eventName, eventParameter)

//---Variables---//
VAR recruitName = "Eddie"
VAR playerName = "Andy"

Ay yo yo wassup it's ya boy {recruitName}. Who dat?
+ [Hi {recruitName}, it's {playerName}]
    {playerName}... Shiiii man, wha's yo yellow ass doin' hittin' up a brother such as ma self?
    ++ [I was just calling to see if you were interested in joining a band?]
        ... Wit chu?
        +++ [Yes]
            Yea man, fo sho. Gotta be at least one bad mother trucker in the band - if you know what I'm sayin'
            ++++ [Great! Yes, I think I know what you're saying!]
                ~ trigger_dialogue_event("recruit", "after_dialogue")
                Aight!
                ->DONE

+ [Oops sorry, wrong number]
    Call dis number again and I'ma pop a cap in yo ass, ya hear? {recruitName} out.
    ->DONE