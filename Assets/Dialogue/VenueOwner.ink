VAR chosen_date = "'never'"
VAR should_accept_booking = false

<i>The owner looks at you with a cool grin.</i>\\n"What can I do you for?" # Test

* "I'd like to book a gig [] for my band <i>'The Fat Cats'</i>."
    "Ok, when?"
    ... # BookGig       // prompts player to choose date from calendar
    ... "Well? When??"  // shows after player finished choosing date
    ** [...Is <i>{chosen_date}</i> ok?]
        {
            - should_accept_booking : "Works for me." # AcceptGigDate
            - else: <i>The owner sighs.</i>\\n"Obviously not. Thanks for wasting my time by the way."
        }
        
* [Uh... Nevermind! <i>*runs away nervously*]
    "What the heck..."