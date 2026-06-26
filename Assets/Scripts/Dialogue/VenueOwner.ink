//---External Functions---//
EXTERNAL trigger_dialogue_event(eventName, eventParameter)

//---Variables---//
VAR chosen_date = "'never'"
VAR should_accept_booking = false
VAR decline_reason = ""

//---story start---//
<style=desc>You open the door and find yourself in a dingy, grotty live music bar.
<style=desc>You want to leave as soon as possible...
<style=desc>But the band will be happy to play here!

<style=desc>The owner looks at you with a cool grin.</style>\\n
<><style=speech>What can I do you for?" ->book_or_leave

=== book_or_leave ===
+ [I'd like to book a gig]
    ~trigger_dialogue_event("start_booking_gig", "after_typing")    // prompts player to choose date from calendar
    <style=speech>Ok, when?"

    <style=speech>Have you settled on a date?"          // shows after player finished choosing date
    ++ [...Is <i>'{chosen_date}'</i> ok?]
        ~ trigger_dialogue_event("check_booking", "") // consult DialogueManager for which response to choose
        {
            - should_accept_booking : <style=speech>Works for me."
                ~trigger_dialogue_event("accept_booking", "")
                ->DONE

            - else : {decline_reason} #Confused:true
        }
<style=speech>How about we try this again and you give me a real answer this time?" #Confused:false ->book_or_leave
->END

+ [Uh... Nevermind! <i>*runs away nervously*]
    <style=speech>What the heck..." #Confused:true
->DONE