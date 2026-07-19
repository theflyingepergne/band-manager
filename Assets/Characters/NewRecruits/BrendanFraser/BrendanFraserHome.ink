//---External Functions---//
EXTERNAL trigger_dialogue_event(eventName, eventParameter)

//---Variables---//
VAR recruitName = "Brendan"
VAR playerName = "Andy"

Hey Chief, what's up?
+ [Write me a song.]
    ~ trigger_dialogue_event("write_song", "")
    Ok boss! It'll take me a couple of days
    ->DONE

+ [Nothing, get the hell out of here.]
    You got it boss!
    ->DONE