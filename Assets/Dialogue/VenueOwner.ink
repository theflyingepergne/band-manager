VAR chosen_date = "'never'"

<i>The owner looks at you with a cool grin.</i>\\n"What can I do you for?" # Test

* "I'd like to book a gig [] for my band 'The Fat Cats'."
    "Ok, when?"
    ... # BookGig       // prompts player to choose date from calendar
    ... "Well? When??"  // shows after player finished choosing date
    ** [...Is {chosen_date} ok?]
        {
            - chosen_date == "'never'": <i>The owner sighs.<i/>\\n"Obviously not. Thanks for wasting my time by the way."
            - else: "Works for me."
        }
        
* [Uh... Nevermind! *runs away nervously*]
    "What the heck..."