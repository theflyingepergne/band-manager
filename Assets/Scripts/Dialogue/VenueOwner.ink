//---External Functions---//
EXTERNAL trigger_dialogue_event(eventName, eventParameter)

//---Variables---//
VAR chosen_date = "'never'"
VAR should_accept_booking = false
VAR decline_reason = ""

//---Story Start---//
<style=desc>You open the door and find yourself in a dingy, grotty live music bar.
<style=desc>Your managerial instincts tell you to leave as soon as possible...
<> But you're sure the band will be happy to play here!

<style=desc>The owner emerges from behind the bar.\\n</style>
<>Hey, we're not open for a little while yet. What's up? ->book_or_leave

=== book_or_leave ===
+ [I'd like to book a gig]
    ~trigger_dialogue_event("start_booking_gig", "after_typing")
    
    Ok, when? // prompts player to choose date from calendar after typing
    So, you settled on a date? // shows after player finished choosing date

    ++ [...Is <i>'{chosen_date}'</i> ok?]
        // ask DialogueManager which response to choose
        ~ trigger_dialogue_event("check_booking", "")
        {
            - should_accept_booking : Works for me. #Happy:true
            // tell CalendarManager to accept booking and schedule gig
            ~trigger_dialogue_event("accept_booking", "")
            See you on '{chosen_date}' - oh, and if you don't show...
            I won't forget it. #Happy:false
            ->DONE

            - else : {decline_reason} #Confused:true
        }

// Loop dialogue
How about we try this again and you give me a real answer this time? #Confused:false ->book_or_leave
->END

+ [Uh... Nevermind! <i>*runs away nervously*]
    What the heck... #Confused:true
    ->DONE