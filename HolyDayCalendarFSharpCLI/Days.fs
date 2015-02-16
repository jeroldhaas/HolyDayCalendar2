module Days


open System
open Months


type DayOfWeek =
    | INVALID   = -1
    | Sunday    = 1
    | Monday    = 2
    | Tuesday   = 3
    | Wednesday = 4
    | Thursday  = 5
    | Friday    = 6
    | Saturday  = 7


type Day () =
    // TODO: cleanup; from java decomp
    let val1  = 0
    let val2  = 0
    let val3  = 0
    let val4  = 0
    let val5  = 0
    let val6  = 0
    let val7  = 0
    let mval1 = 0
    let mval2 = 0
    let mval3 = 0
    let mval4 = 0
    let mval5 = 0
    let mval6 = 0
    let mval7 = 0

    member x.setDoubleParasha (i :int) =
            i //raise System.Exception.SystemException.NotImplementedException // NotImplementedException
    member val Num = 0 with get, set
    member val Name = "" with get, set
    member val Dow = DayOfWeek.Sunday with get, set // TODO: fix
    member val Date = DateTime.Now with get, set
    member val Month = "" with get, set
    member val IsHag = false with get, set
    member val Hag :string[] = Array.empty with get, set

    member x.AddHag (newHag) =
        let newHagArray = [|newHag;|]
        if Array.isEmpty x.Hag then
            x.Hag <- newHagArray
        else
            x.Hag <- Array.append x.Hag newHagArray

    new (dow :DayOfWeek, date :DateTime, month :Month) as x =
        //x.BgColour = System.Drawing.Colors.White
        x.Dow <- dow
        x.Date <- date
        x.Name <- "" //(dow.ToString())

    member x.PentecostSab() =
        let mutable PentWaveMonth :int
        let PentWaveDate =
            use y = x.Month.Year :> LunarYear in
            y.WaveHebDate ()

        let HebrewLeapYear =
            use y = x.Month.Year :> LunarYear in
            y.IsHebrewLeapYear ()
        if HebrewLeapYear then
            PentWaveMonth <- 7
        else
            PentWaveMonth <- 6
        
        // TODO: WHAT??????
        // ok... so this is the first month,
        // and so on
        mval1 <- PentWaveMonth
        val1 <- PentWaveDate + 6

        //
        mval2 <- mval1
        val2 <- val1 + 7
        if val2 > 30 then
            val2 <- val2 - 30
            mval2 <- mval2 + 1

        //
        mval3 <- mval2
        val3 <- val2 + 7
        if val3 > 30 then
            val3 <- val3 - 30
            mval3 <- mval3 + 1

        //
        mval4 <- mval3
        val4 <- val3 + 7
        if val4 > 29 then
            val4 <- val4 - 29
            mval4 <- mval4 + 1

        //
        mval5 <- mval4
        val5 <- val4 + 7
        if val5 > 29 then
            val5 <- val5 - 29
            mval5 <- mval5 + 1

        //
        mval6 <- mval5
        val6 <- val5 + 7
        if val6 > 29 then
            val6 <- val6 - 29
            mval5 <- mval5 + 1

        //
        mval7 <- mval6
        val7 <- val6 + 7
        if val7 > 29 then
            val7 <- val7 - 29
            mval7 <- mval7 + 1
    
    /// <summary>Checks for Pentecost<summary>
    /// <todo>This needs to be changed - no 'if'</todo>
    member x.PentecostSabbathCheck () =
        let nextDay = x.Date.AddDays(1.0)
        match (x.Month, nextDay.Day) with
        | (mval1, val1) ->
            x.IsHag <- true
            x.AddHag("Day 7 Toward Pentecost")
        | (mval2, val2) ->
            x.IsHag <- true
            x.AddHag("Day 14 Toward Pentecost")
        | (mval3, val3) ->
            x.IsHag <- true
            x.AddHag("Day 21 Toward Pentecost")
        | (mval4, val4) ->
            x.IsHag <- true
            x.AddHag("Day 28 Toward Pentecost")
        | (mval5, val5) ->
            x.IsHag <- true
            x.AddHag("Day 35 Toward Pentecost")
        | (mval6, val6) ->
            x.IsHag <- true
            x.AddHag("Day 42 Toward Pentecost")
        | (mval7, val7) ->
            x.IsHag <- true
            x.AddHag("Day 42 Toward Pentecost")

    member x.SetHagim (eretzFlag) =
        // if x.Year is LunarYear
        let mutable month = x.Month.Num
        let nextDay = x.Date + 1
        if x.Month.Year.IsLeap && month >= 6 then
            month <- month - 1
        let PDate =
            use y = x.Month.Year :> LunarYear in
            y.PentHebDate
        let WDate =
            use y = x.Month.Year :> LunarYear in
            y.PassoverDay
        x.PentecostSab()
        // TODO: revise this mess
        if ((x.Month.Num = 8 or x.Month.Num = 9) &&
            (x.Month.Name = "Sivan" && x.Date.Day + 1 = PDate)) then
            x.IsHag <- true
            x.AddHag("Pentecost")
        let CkSpecialYear = x.Month.Year.Num

        match month with
        | 0 ->
            match nextDay with
            | 1  ->
                x.IsHag <- true
                if CkSpecialYear = 3757 then
                    x.AddHag("Feast of Trumpets - birth of Jesus")
                else
                    x.AddHag("Feast of Trumpets")
            | 10 ->
                x.IsHag <- true
                x.AddHag("Day of Atonement")
            | 21 ->
                x.IsHag <- true
                x.AddHag("Feast of Tabernacles")
            | 22 ->
                x.IsHag <- true
                x.AddHag("Last Great Day")
            | 6  ->
                if x.Month.Name != "Adar II" then
                    match nextDay with
                    | 13 ->
                        if (CkSpecialYear = 3790) then
                            x.IsHag <- true
                            x.AddHag("Passover observed after sunset by Jesus and apostles")
                    | 14 ->
                        if CkSpecialYear = 3790 then
                            x.IsHag <- true
                            x.AddHag("Passover - Day of Jesus's crucifixion.")
                        else
                            x.IsHag <- true
                            x.AddHag("Passover")
                    | 17 ->
                        if CkSpecialYear = 3790 then
                            x.IsHag <- true
                            x.AddHag("Feast of Unleavened Bread - Jesus arose at sunset ending Sabbath.")
                        else
                            x.IsHag <- true
                            x.AddHag("Feast of Unleavened Bread")
                    | 21 ->
                        x.IsHag <- true
                        x.AddHag("Feast of Unleavened Bread")
            | 7 ->
                // TODO: total crap code
                if x.Month.Name == "Iyar" then
                    match nextDay with
                    | 13 ->
                        x.IsHag <- true
                        if CkSpecialYear = 3790 then
                            x.AddHag("Passover observed after sunset by Jesus and apostles")
                    | 14 ->
                        x.IsHag <- true
                        if CkSpecialYear = 3790 then
                            x.AddHag("Passover - Day of Jesus's crucifixion.")
                        else
                            x.AddHag("Passover")
                    | 17 ->
                        x.IsHag <- true
                        if CkSpecialYear = 3790 then
                            x.AddHag("Feast of Unleavened Bread - Jesus arose at sunset ending Sabbath.")
                        else
                            x.AddHag("Feast of Unleavened Bread")
                    | 21 ->
                        x.IsHag <- true
                        x.AddHag("Feast of Unleavened Bread")
        x.PentecostSabbathCheck() // TODO: correct name
    
    member x.Name (daynum) =
        match daynum with
        | 0 -> DayOfWeek.Sunday
        | 1 -> DayOfWeek.Monday
        | 2 -> DayOfWeek.Tuesday
        | 3 -> DayOfWeek.Wednesday
        | 4 -> DayOfWeek.Thursday
        | 5 -> DayOfWeek.Friday
        | 6 -> DayOfWeek.Saturday
        | _ -> DayOfWeek.INVALID

    member x.MonthName (month) =
        month.ToString()