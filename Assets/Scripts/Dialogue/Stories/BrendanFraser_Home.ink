//---Globals---//
INCLUDE _Globals.ink

//---External Functions---//
EXTERNAL trigger_dialogue_event(eventName, eventParameter)

Hey {playerName}, what's up?
+ [Write me a song.]
    ~ trigger_dialogue_event("write_song", "")
    Ok boss! It'll take me a couple of days, if my name isn't <i>"{recruitName}"</i>!
    ->DONE

+ [Nothing, get the hell out of here.]
    You got it boss!
    Hey, did you know you have: \\n£{money} \\n{chemistry}% band chemistry \\n{fans} fans
    ->DONE