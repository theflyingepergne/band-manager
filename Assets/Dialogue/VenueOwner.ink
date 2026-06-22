//---External Functions---//
EXTERNAL check_booking_status()

//---Variables---//
VAR chosen_date = "'never'"
VAR should_accept_booking = false
VAR decline_reason = "<style=desc>The owner sighs.</style>\\n<style=speech>Yeah, so you have to actually choose a date if you want to book a gig."

//---story start---//
<style=desc>You open the door and find yourself in a dingy, grotty live music bar.
<style=desc>You want to leave as soon as possible...
<style=desc>But the band will be happy to play here!

<style=desc>The owner looks at you with a cool grin.</style>\\n
<><style=speech>What can I do you for?" ->book_or_leave

=== book_or_leave ===
+ [I'd like to book a gig]
    <style=speech>Ok, when?" # BookGig // prompts player to choose date from calendar
    <style=speech>Have you settled on a date?"    // shows after player finished choosing date
    ++ [...Is <i>{chosen_date}</i> ok?]
        ~ check_booking_status()
        {
            - should_accept_booking : <style=speech>Works for me." # AcceptGigDate ->DONE
            - else : {decline_reason}
        }
<style=speech>How about we try this again and you give me a real answer this time?" ->book_or_leave
->END

+ [Uh... Nevermind! <i>*runs away nervously*]
    <style=speech>What the heck..."
->DONE