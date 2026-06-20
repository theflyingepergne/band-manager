VAR chosen_date = "'never'"
VAR should_accept_booking = false
VAR decline_reason = ""

<i>The owner looks at you with a cool grin.</i>\\n"What can I do you for?" # Test
->book_or_leave
=== book_or_leave ===
+ [I'd like to book a gig]
    "Ok, when?"
    ... # BookGig       // prompts player to choose date from calendar
    ... "Well? When??"  // shows after player finished choosing date
    ++ [...Is <i>{chosen_date}</i> ok?]
        {
            - should_accept_booking : "Works for me." # AcceptGigDate ->DONE
            - else : {decline_reason}
        }
"How about we try this again and you give me a real answer this time?" ->book_or_leave
->END

+ [Uh... Nevermind! <i>*runs away nervously*]
    "What the heck..."
->DONE