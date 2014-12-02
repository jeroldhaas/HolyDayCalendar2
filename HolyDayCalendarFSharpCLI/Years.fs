module Years

open Months



type Year =

    member val Next = 0 with get, set
    member val Prev = 0 with get, set
    member val IsLeap = false with get, set
    member val NMonths = 0 with get, set
    member val Months :Month[]= Array.empty with get, set
    member val Num = 0 with get, set

    //member abstract connect(year :Year)

    new (year :int) as x =
        x.Num <- year
        x.IsLeap <- false
        x

    new (year: int, leap :bool) as x =
        x.Num <- year
        x.IsLeap <- leap
        x

    
    
type SolarYear(num) =
    inherit Year(num)

    member x.SolarYear(num) =
        //new base(num)
        x.Num <- num
        x.NMonths <- 12
        let mod4 = x.Num % 4
        let mod100 = x.Num % 100
        let mod400 = x.Num % 400
        if (mod4 = 0 && mod100 = 0) || mod400 = 0 then
            x.IsLeap <- true
        else
            x.IsLeap <- false
        if x.Num > 0 && x.Num < 1583 then
            if x.Num % 4 = 0 then
                x.IsLeap <- true
        if x.Num < 0 && x.Num % 4 = -1 then
            x.IsLeap <- true
        x.Months <- Array.zeroCreate 12
        x.Months.[0] <- new Month(0, SolarMonths.January, 31)
        if x.IsLeap then
            x.Months.[1] <- new Month(1, SolarMonths.February, 29)
        else
            x.Months.[1] <- new Month(1, SolarMonths.February, 28)
        x.Months.[2] <- new Month(2, SolarMonths.March, 31)
        x.Months.[3] <- new Month(3, SolarMonths.April, 30)
        x.Months.[4] <- new Month(4, SolarMonths.May, 31)
        x.Months.[5] <- new Month(5, SolarMonths.June, 30)
        x.Months.[6] <- new Month(6, SolarMonths.July, 31)
        x.Months.[7] <- new Month(7, SolarMonths.August, 31)
        x.Months.[8] <- new Month(8, SolarMonths.September, 30)
        x.Months.[9] <- new Month(9, SolarMonths.October, 31)
        x.Months.[10]<- new Month(10, SolarMonths.November, 30)
        x.Months.[11]<- new Month(11, SolarMonths.December, 31)
        for i in [0..11] do
            x.Months.[i].Next <- x.Months.[i+1]
            // set the month's lastday.next to nextmonth's.firstday ([0])
            //x.Months.[i].Num

type LunarYear (num) =
    inherit Year (num)

    member val IsMalei = false with get, set
    member val IsHaser = false with get, set
    member val IsHebLeapYear = false with get, set
    member val YearType = 0 with get, set

    member x.PesachDay(year, lunarFlag) =
        let sYear = 0
        if year >= 3761 then
           let sYear = year - 3760
        else
           let sYear = year - 3761

        let a = (12 * year + 17) % 19
        let b = year % 4
        let mm = 32.044093199999999 + 1.5542418 * a + 0.25 * b - 0.003177794 * year
        let m = mm % 1
        let mutable bigm = int mm
        let mutable c = (bigm + 3 * year + 5 * b + 5) % 7
        if c = 2 || c = 4 || (c = 0 && a >= 12 && m >= 0.8977000000000001) then
            c <- c + 1
            bigm <- bigm + 1
        else if c = 6 then
            c = 0
            bigm <- bigm + 1
        else if c = 1 && a >= 7 && m >= 0.6329 then
            c <- c + 2
            bigm <- bigm + 2
        c



    member x.LunarYear(num, lunarFlag) =
        let lastPesach = x.GetPesachDay(x.Year - 1, lunarFlag)
        let thisPesach = x.Pesach
        // TODO: unused? x.IsHebLeapYear <- x.IsLeap

        let mutable diff = thisPesach - lastPesach
        if diff < 0 then
            diff <- diff + 7
        match diff with
        | 3 -> x.IsHaser <- true
        | 5 -> if x.IsLeap then x.IsHaser <- true else x.IsMalei <- true
        | 0 -> x.IsMalei <- true

        if x.IsLeap then
            if x.IsMalei then
                match x.Prev with
                | 0 -> x.YearType <- 4
                | 3 -> x.YearType <- 10
                | 5 -> x.YearType <- 14
            else if x.IsHaser then
                match x.Prev with
                | 1 -> x.YearType <- 8
                | 3 -> x.YearType <- 12
                | 5 -> x.YearType <- 2
            else
                x.YearType <- 6
        else if x.IsMalei then
            match x.Prev with
            | 1 -> x.YearType <- 9
            | 3 -> x.YearType <- 13
            | 5 -> x.YearType <- 3
        else if x.IsHaser then
            match x.Prev with
            | 1 -> x.YearType <- 11
            | 3 -> x.YearType <- 1
        else
            match x.Prev with
            | 0 -> x.YearType <- 9
            | 5 -> x.YearType <- 3

        if x.IsLeap then
            x.NMonths <- 13
            x.Months <- Array.zeroCreate x.NMonths
            x.Months.[0] <- new Month(0, LunarMonths.Tishri, 30)
            if x.IsMalei then
                x.Months.[1] <- new Month(1, LunarMonths.Heshvan, 30)
            else
                x.Months.[1] <- new Month(1, LunarMonths.Heshvan, 29)
            if x.IsHaser then
                x.Months.[2] <- new Month(2, LunarMonths.Kislev, 29)
            else
                x.Months.[2] <- new Month(2, LunarMonths.Kislev, 30)
            x.Months.[3] <- new Month(3, LunarMonths.Tebeth, 29)
            x.Months.[4] <- new Month(4, LunarMonths.Shebat, 30)
            x.Months.[5] <- new Month(5, LunarMonths.``Adar I``, 30)
            x.Months.[6] <- new Month(6, LunarMonths.``Adar II``, 29)
            x.Months.[7] <- new Month(7, LunarMonths.Nisan, 30)
            x.Months.[8] <- new Month(8, LunarMonths.Iyar, 29)
            x.Months.[9] <- new Month(9, LunarMonths.Sivan, 30)
            x.Months.[10]<- new Month(10, LunarMonths.Tammuz, 29)
            x.Months.[11]<- new Month(11, LunarMonths.Ab, 30)
            x.Months.[12]<- new Month(12, LunarMonths.Elul, 29)
        else
            x.NMonths <- 12
            x.Months <- Array.zeroCreate x.NMonths
            x.Months.[0] <- new Month(0, LunarMonths.Tishri, 30)
            if x.IsMalei then
                x.Months.[1] <- new Month(1, LunarMonths.Heshvan, 30)
            else
                x.Months.[1] <- new Month(1, LunarMonths.Heshvan, 29)
            if x.IsHaser then
                x.Months.[2] <- new Month(2, LunarMonths.Kislev, 29)
            else
                x.Months.[2] <- new Month(2, LunarMonths.Kislev, 30)
            x.Months.[3] <- new Month(3, LunarMonths.Tebeth, 29)
            x.Months.[4] <- new Month(4, LunarMonths.Shebat, 30)
            x.Months.[5] <- new Month(5, LunarMonths.Adar, 29)
            x.Months.[7] <- new Month(7, LunarMonths.Nisan, 30)
            x.Months.[8] <- new Month(8, LunarMonths.Iyar, 29)
            x.Months.[9] <- new Month(9, LunarMonths.Sivan, 30)
            x.Months.[10]<- new Month(10, LunarMonths.Tammuz, 29)
            x.Months.[11]<- new Month(11, LunarMonths.Ab, 30)
            x.Months.[12]<- new Month(12, LunarMonths.Elul, 29)

